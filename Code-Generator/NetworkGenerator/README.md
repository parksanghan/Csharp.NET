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
NetworkEndpoints.GatewayServer.Recv((connection, data) =>
{
    NetworkEndpoints.GatewayServer.SendData(connection.Id, data);
});

NetworkEndpoints.Project2Client.Connect();
NetworkEndpoints.Project2Client.Recv(data =>
{
    Console.WriteLine(Encoding.UTF8.GetString(data));
});
NetworkEndpoints.Project2Client.SendData("hello");
NetworkEndpoints.Project2Client.Disconnect();
```

For server endpoints, `Recv` starts one ThreadPool receive loop per connected
client. `SendData(byte[])` broadcasts to every connection, and
`SendData(connectionId, byte[])` sends to one connection.

