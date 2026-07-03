namespace RouteGenerator;

internal sealed record RouteDefinition(
    string From,
    string To,
    string Protocol,
    string Host,
    int? Port,
    string Description);

