# Communication Route Source Generator Example

This sample shows a C# source generator that reads a route definition file and
generates strongly typed communication route code for each project.

## Flow

```text
config/communication-routes.csv
        |
        v
src/RouteGenerator
        |
        v
Generated.Communication.CommunicationRoutes
```

`Project1.App` and `Project2.App` both reference the same generator and the same
CSV file. During compilation, the generator reads `MSBuildProjectName` and only
emits routes whose `From` column matches the current project.

The app projects expose that name through `CommunicationRouteProjectName` using
`CompilerVisibleProperty`, because source generators only receive MSBuild
properties that the project explicitly makes visible to the compiler.

The Excel workbook in `outputs/route-generator-example/communication-routes.xlsx`
contains the same route table in a human-friendly format. For build-time source
generation, the CSV is used because it is simple, deterministic, and does not
force the generator to carry an Excel parser.

This local sample references the Roslyn assemblies that ship with the installed
.NET SDK so it can build without downloading NuGet packages. In a real reusable
generator package, prefer targeting `netstandard2.0` and referencing
`Microsoft.CodeAnalysis.CSharp` through NuGet.

## Try It

```powershell
dotnet build .\CodeGenerator.sln

dotnet build .\src\Project1.App\Project1.App.csproj
dotnet run --project .\src\Project1.App\Project1.App.csproj

dotnet build .\src\Project2.App\Project2.App.csproj
dotnet run --project .\src\Project2.App\Project2.App.csproj
```

`Directory.Solution.props` disables solution-level parallel restore for this
sample. The `.sln` also marks the app projects as dependent on the generator
project, so the analyzer assembly is built before the apps compile.

After build, generated files are written under each app's `obj/generated`
directory because `EmitCompilerGeneratedFiles` is enabled.
