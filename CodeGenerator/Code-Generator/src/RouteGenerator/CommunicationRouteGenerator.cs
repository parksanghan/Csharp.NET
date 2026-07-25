using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace RouteGenerator;

[Generator]
public sealed class CommunicationRouteGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor InvalidRouteFile = new(
        id: "ROUTE001",
        title: "Invalid route definition",
        messageFormat: "{0}",
        category: "CommunicationRoutes",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var routeFiles = context.AdditionalTextsProvider
            .Where(static file => Path.GetFileName(file.Path).Equals("communication-routes.csv", StringComparison.OrdinalIgnoreCase))
            .Select(static (file, cancellationToken) => file.GetText(cancellationToken)?.ToString() ?? string.Empty)
            .Collect();

        var projectName = context.AnalyzerConfigOptionsProvider
            .Select(static (provider, _) =>
                provider.GlobalOptions.TryGetValue("build_property.CommunicationRouteProjectName", out var value)
                    ? value
                    : provider.GlobalOptions.TryGetValue("build_property.MSBuildProjectName", out value)
                    ? value
                    : "UnknownProject");

        var input = routeFiles.Combine(projectName);

        context.RegisterSourceOutput(input, static (sourceProductionContext, value) =>
        {
            var routeTexts = value.Left;
            var projectName = value.Right;

            if (routeTexts.Length == 0)
            {
                sourceProductionContext.ReportDiagnostic(Diagnostic.Create(
                    InvalidRouteFile,
                    Location.None,
                    "communication-routes.csv was not registered as an AdditionalFiles item."));
                return;
            }

            var parseResult = RouteParser.Parse(routeTexts[0]);
            foreach (var error in parseResult.Errors)
            {
                sourceProductionContext.ReportDiagnostic(Diagnostic.Create(InvalidRouteFile, Location.None, error));
            }

            if (parseResult.Errors.Count > 0)
            {
                return;
            }

            var generatedSource = RouteSourceBuilder.Build(projectName, parseResult.Routes);
            sourceProductionContext.AddSource(
                "CommunicationRoutes.g.cs",
                SourceText.From(generatedSource, Encoding.UTF8));
        });
    }
}
