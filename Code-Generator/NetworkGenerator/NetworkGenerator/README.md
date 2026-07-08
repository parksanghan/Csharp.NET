# NetworkGenerator

`NetworkGenerator` is a C# source generator targeting `netstandard2.0`.

It reads an AdditionalFiles CSV named `network-endpoints.csv` and generates TCP
socket server/client endpoint code.

## CSV Format

```csv
Name,Type,Host,Port,MaxConnections,Description
GatewayServer,Server,0.0.0.0,5000,100,Accepts equipment connections
Project2Client,Client,127.0.0.1,5000,,Connects to Project2
```

`Type` must be `Server` or `Client`.

## Generated Usage

```csharp
using Generated.Network;

NetworkEndpoints.GatewayServer.Connect();
var packet = (NetworkReceivedData)NetworkEndpoints.GatewayServer.Recv();
NetworkEndpoints.GatewayServer.SendData(packet.ConnectionId, packet.Data);

NetworkEndpoints.Project2Client.Connect();
object data = NetworkEndpoints.Project2Client.Recv();
NetworkEndpoints.Project2Client.SendData((object)"hello");
NetworkEndpoints.Project2Client.Disconnect();
```

For server endpoints, `Recv` starts one ThreadPool receive loop per connected
client. Server `Recv()` returns `object`, but the object is a
`NetworkReceivedData` instance so the caller can read `ConnectionId` and `Data`.

`SendData(object)` is the primary generated send function. The default conversion
is simple: `byte[]` is sent as-is, `string` is encoded as UTF-8, and any other
object is sent as `data.ToString()` encoded as UTF-8. Users can interpret the
returned object and add domain-specific parsing outside the generated code.
