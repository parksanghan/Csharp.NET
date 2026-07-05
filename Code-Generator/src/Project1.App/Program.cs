using Generated.Communication;

Console.WriteLine($"Generated routes for {CommunicationRoutes.ProjectName}");
Console.WriteLine();
Console.WriteLine("Outgoing routes");

foreach (var route in CommunicationRoutes.Outgoing)
{
    Console.WriteLine($"{route.Target} -> {route.Endpoint} | {route.Description}");
}

Console.WriteLine();
Console.WriteLine("Incoming routes");

foreach (var route in CommunicationRoutes.Incoming)
{
    Console.WriteLine($"{route.Source} -> {route.Endpoint} | {route.Description}");
}

Console.WriteLine();
Console.WriteLine("Generated network API examples");

foreach (var route in CommunicationRoutes.Outgoing)
{
    if (route.Protocol == Protocol.Tcp)
    {
        Console.WriteLine($"await CommunicationNetwork.ConnectAsync(\"{route.Target}\");");
        Console.WriteLine($"await CommunicationNetwork.SendAsync(\"{route.Target}\", \"hello\");");
        Console.WriteLine($"var reply = await CommunicationNetwork.ReceiveAsync(\"{route.Target}\");");
        Console.WriteLine($"CommunicationNetwork.Disconnect(\"{route.Target}\");");
    }
    else
    {
        Console.WriteLine($"await CommunicationNetwork.SendUdpAsync(\"{route.Target}\", \"hello\");");
    }
}

foreach (var route in CommunicationRoutes.Incoming)
{
    if (route.Protocol == Protocol.Tcp)
    {
        Console.WriteLine($"await CommunicationNetwork.StartReceiveServerAsync(\"{route.Source}\");");
        Console.WriteLine($"var message = await CommunicationNetwork.ReceiveFromAsync(\"{route.Source}\");");
        Console.WriteLine($"CommunicationNetwork.StopReceiveServer(\"{route.Source}\");");
    }
    else
    {
        Console.WriteLine($"var message = await CommunicationNetwork.ReceiveUdpFromAsync(\"{route.Source}\");");
    }
}

