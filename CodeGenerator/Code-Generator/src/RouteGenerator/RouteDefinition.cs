namespace RouteGenerator;

internal sealed class RouteDefinition
{
    public RouteDefinition(
        string from,
        string to,
        string protocol,
        string host,
        int? port,
        string description)
    {
        From = from;
        To = to;
        Protocol = protocol;
        Host = host;
        Port = port;
        Description = description;
    }

    public string From { get; }
    public string To { get; }
    public string Protocol { get; }
    public string Host { get; }
    public int? Port { get; }
    public string Description { get; }
}
