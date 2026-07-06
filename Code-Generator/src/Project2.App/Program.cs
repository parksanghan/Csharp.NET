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

Route tcpRoute = CommunicationRoutes.DeviceB;
Route udpRoute = CommunicationRoutes.Project1;

Console.WriteLine("CommunicationRoutes.DeviceB.Connect();");
Console.WriteLine("CommunicationRoutes.DeviceB.Send(\"hello\");");
Console.WriteLine("var reply = CommunicationRoutes.DeviceB.Recv();");
Console.WriteLine("CommunicationRoutes.DeviceB.Disconnect();");
Console.WriteLine();
Console.WriteLine("CommunicationRoutes.Project1.Connect();");
Console.WriteLine("CommunicationRoutes.Project1.Send(\"heartbeat\");");
Console.WriteLine("var datagram = CommunicationRoutes.Project1.Recv();");
Console.WriteLine("CommunicationRoutes.Project1.Disconnect();");

// Real usage:
// tcpRoute.Connect();
// tcpRoute.Send("hello");
// var reply = tcpRoute.Recv();
// tcpRoute.Disconnect();

