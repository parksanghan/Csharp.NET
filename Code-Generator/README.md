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
Generated.Communication.CommunicationNetwork
```

`Project1.App` and `Project2.App` both reference the same generator and the same
CSV file. During compilation, the generator reads `MSBuildProjectName` and emits
both outgoing routes whose `From` column matches the current project and incoming
routes whose `To` column matches the current project.

The generated network helper includes:

- `ConnectAsync`
- `Disconnect`
- `SendAsync`
- `ReceiveAsync`
- `StartReceiveServerAsync`
- `ReceiveFromAsync`
- `ReplyAsync`
- `StopReceiveServer`
- `SendUdpAsync`
- `ReceiveUdpFromAsync`

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

On the receiving PC/project:

```csharp
await CommunicationNetwork.StartReceiveServerAsync("Project1.App");
var message = await CommunicationNetwork.ReceiveFromAsync("Project1.App");
await CommunicationNetwork.ReplyAsync("Project1.App", "received");
CommunicationNetwork.StopReceiveServer("Project1.App");
```

On the sending PC/project:

```csharp
await CommunicationNetwork.ConnectAsync("Project2.App");
await CommunicationNetwork.SendAsync("Project2.App", "hello");
var reply = await CommunicationNetwork.ReceiveAsync("Project2.App");
CommunicationNetwork.Disconnect("Project2.App");
```

## Generated UDP Example

```csharp
await CommunicationNetwork.SendUdpAsync("Project1.App", "heartbeat");
var datagram = await CommunicationNetwork.ReceiveUdpFromAsync("Project2.App");
```
