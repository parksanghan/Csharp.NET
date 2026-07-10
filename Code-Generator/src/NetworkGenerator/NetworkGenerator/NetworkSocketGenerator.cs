using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace NetworkGenerator;

[Generator]
public sealed class NetworkSocketGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor InvalidConfig = new(
        id: "NETGEN001",
        title: "Invalid network generator configuration",
        messageFormat: "{0}",
        category: "NetworkGenerator",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var configFiles = context.AdditionalTextsProvider
            .Where(static file => IsNetworkConfig(file.Path))
            .Select(static (file, cancellationToken) => new NetworkConfigFile(
                file.Path,
                file.GetText(cancellationToken)?.ToString() ?? string.Empty))
            .Collect();

        context.RegisterSourceOutput(configFiles, static (sourceProductionContext, files) =>
        {
            if (files.Length == 0)
            {
                return;
            }

            var endpoints = new List<NetworkEndpointDefinition>();
            var hasErrors = false;

            foreach (var file in files)
            {
                var result = NetworkConfigParser.Parse(file.Path, file.Text);
                endpoints.AddRange(result.Endpoints);

                foreach (var error in result.Errors)
                {
                    hasErrors = true;
                    sourceProductionContext.ReportDiagnostic(Diagnostic.Create(
                        InvalidConfig,
                        Location.None,
                        error.ToString()));
                }
            }

            if (hasErrors)
            {
                return;
            }

            var generatedSource = NetworkSourceBuilder.Build(endpoints);
            sourceProductionContext.AddSource(
                "GeneratedNetworkSockets.g.cs",
                SourceText.From(generatedSource, Encoding.UTF8));
        });
    }

    private static bool IsNetworkConfig(string path)
    {
        var fileName = Path.GetFileName(path);
        return string.Equals(fileName, "network-endpoints.csv", StringComparison.OrdinalIgnoreCase)
            || string.Equals(fileName, "network-generator.csv", StringComparison.OrdinalIgnoreCase)
            || fileName.EndsWith(".network.csv", StringComparison.OrdinalIgnoreCase);
    }

    private sealed class NetworkConfigFile
    {
        public NetworkConfigFile(string path, string text)
        {
            Path = path;
            Text = text;
        }

        public string Path { get; }
        public string Text { get; }
    }
}

