using Generated.Communication;

Console.WriteLine($"Generated routes for {CommunicationRoutes.ProjectName}");
Console.WriteLine();
Console.WriteLine("Generated routes");

foreach (var route in CommunicationRoutes.All)
{
    Console.WriteLine($"{route.Target} -> {route.Endpoint} | {route.Description}");
}

Console.WriteLine();
Console.WriteLine("Direct generated API examples");

Route tcpRoute = CommunicationRoutes.Project2;
Route udpRoute = CommunicationRoutes.DeviceA;

Console.WriteLine("CommunicationRoutes.Project2.Connect();");
Console.WriteLine("CommunicationRoutes.Project2.Send(\"hello\");");
Console.WriteLine("var reply = CommunicationRoutes.Project2.Recv();");
Console.WriteLine("CommunicationRoutes.Project2.Disconnect();");
Console.WriteLine();
Console.WriteLine("CommunicationRoutes.DeviceA.Connect();");
Console.WriteLine("CommunicationRoutes.DeviceA.Send(\"heartbeat\");");
Console.WriteLine("var datagram = CommunicationRoutes.DeviceA.Recv();");
Console.WriteLine("CommunicationRoutes.DeviceA.Disconnect();");

// Real usage:
// tcpRoute.Connect();
// tcpRoute.Send("hello");
// var reply = tcpRoute.Recv();
// tcpRoute.Disconnect();

