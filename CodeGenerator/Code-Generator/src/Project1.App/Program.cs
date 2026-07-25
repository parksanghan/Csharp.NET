using System.Text;
using Generated.Network;

Console.WriteLine("Generated network endpoints");

foreach (var endpoint in NetworkEndpoints.All)
{
    Console.WriteLine($"{endpoint.Name} | {endpoint.Kind} | {endpoint.Host}:{endpoint.Port} | {endpoint.Description}");
}

Console.WriteLine();
Console.WriteLine("Direct generated API examples:");
Console.WriteLine("NetworkEndpoints.GatewayServer.Connect();");
Console.WriteLine("NetworkEndpoints.GatewayServer.Recv();");
Console.WriteLine("NetworkEndpoints.GatewayServer.SendData(connectionId, data);");
Console.WriteLine("NetworkEndpoints.GatewayServer.Disconnect();");
Console.WriteLine();
Console.WriteLine("NetworkEndpoints.Project2Client.Connect();");
Console.WriteLine("NetworkEndpoints.Project2Client.Recv();");
Console.WriteLine("NetworkEndpoints.Project2Client.SendData(data);");
Console.WriteLine("NetworkEndpoints.Project2Client.Disconnect();");

if (args.Length > 0 && string.Equals(args[0], "server", StringComparison.OrdinalIgnoreCase))
{
    ServerExample();
}
else if (args.Length > 0 && string.Equals(args[0], "client", StringComparison.OrdinalIgnoreCase))
{
    ClientExample();
}

static void ServerExample()
{
    NetworkEndpoints.GatewayServer.Connect();

    object recv = NetworkEndpoints.GatewayServer.Recv();
    var packet = (NetworkReceivedData)recv;
    var bytes = (byte[])packet.Data;
    var message = Encoding.UTF8.GetString(bytes);

    Console.WriteLine($"Client ID: {packet.ConnectionId}");
    Console.WriteLine(message);

    NetworkEndpoints.GatewayServer.SendData(packet.ConnectionId, "ACK");
    NetworkEndpoints.GatewayServer.Disconnect();
}

static void ClientExample()
{
    NetworkEndpoints.Project2Client.Connect();
    NetworkEndpoints.Project2Client.SendData("hello");

    object data = NetworkEndpoints.Project2Client.Recv();
    var bytes = (byte[])data;

    Console.WriteLine(Encoding.UTF8.GetString(bytes));
    NetworkEndpoints.Project2Client.Disconnect();
}

