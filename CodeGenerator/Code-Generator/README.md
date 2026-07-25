# Communication Route Source Generator Example

This sample shows a C# source generator that reads a route definition file and
generates strongly typed communication route and network I/O code for each
project.

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
CSV file. During compilation, the generator reads `MSBuildProjectName` and emits
outgoing routes whose `From` column matches the current project.

Each generated `Route` includes:

- `Connect`
- `Send`
- `Recv`
- `Disconnect`

The app projects expose their names through `CommunicationRouteProjectName` using
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

`global.json` pins this sample to the installed .NET 9 SDK so the generator and
the SDK-provided Roslyn assemblies target the same runtime.

## Try It

```powershell
dotnet build .\CodeGenerator.sln -m:1

dotnet build .\src\Project1.App\Project1.App.csproj
dotnet run --project .\src\Project1.App\Project1.App.csproj

dotnet build .\src\Project2.App\Project2.App.csproj
dotnet run --project .\src\Project2.App\Project2.App.csproj
```

The solution marks the app projects as dependent on the generator project.
`-m:1` keeps this analyzer sample on a single MSBuild node so the generator
assembly is not touched by multiple projects at the same time.

After build, generated files are written under each app's `obj/generated`
directory because `EmitCompilerGeneratedFiles` is enabled.

## Generated TCP Example

The generator emits route objects like this:

```csharp
namespace Generated.Communication
{
    public static class CommunicationRoutes
    {
        public static readonly Route Project2 = new Route(
            "Project2.App",
            "127.0.0.1",
            Protocol.Tcp,
            5000,
            "Project1 sends commands to Project2");
    }
}
```

Use the generated object directly:

```csharp
CommunicationRoutes.Project2.Connect();
CommunicationRoutes.Project2.Send("hello");
var reply = CommunicationRoutes.Project2.Recv();
CommunicationRoutes.Project2.Disconnect();
```

## Generated UDP Example

```csharp
CommunicationRoutes.DeviceA.Connect();
CommunicationRoutes.DeviceA.Send("heartbeat");
var datagram = CommunicationRoutes.DeviceA.Recv();
CommunicationRoutes.DeviceA.Disconnect();
```
