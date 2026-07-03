using Generated.Communication;

Console.WriteLine($"Generated routes for {CommunicationRoutes.ProjectName}");

foreach (var route in CommunicationRoutes.All)
{
    var endpoint = route.Port is null
        ? $"{route.Protocol} {route.Host}"
        : $"{route.Protocol} {route.Host}:{route.Port}";

    Console.WriteLine($"{route.Target} -> {endpoint} | {route.Description}");
}

