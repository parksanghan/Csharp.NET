using System;

namespace NetworkGenerator;

internal enum NetworkEndpointKind
{
    Server,
    Client
}

internal sealed class NetworkEndpointDefinition
{
    public NetworkEndpointDefinition(
        string name,
        NetworkEndpointKind kind,
        string host,
        int port,
        int maxConnections,
        string description)
    {
        Name = name;
        Kind = kind;
        Host = host;
        Port = port;
        MaxConnections = maxConnections;
        Description = description;
    }

    public string Name { get; }
    public NetworkEndpointKind Kind { get; }
    public string Host { get; }
    public int Port { get; }
    public int MaxConnections { get; }
    public string Description { get; }
}

internal sealed class NetworkParseError
{
    public NetworkParseError(string filePath, int lineNumber, string message)
    {
        FilePath = filePath;
        LineNumber = lineNumber;
        Message = message;
    }

    public string FilePath { get; }
    public int LineNumber { get; }
    public string Message { get; }

    public override string ToString()
        => $"{FilePath} line {LineNumber}: {Message}";
}

internal sealed class NetworkParseResult
{
    public NetworkParseResult(
        NetworkEndpointDefinition[] endpoints,
        NetworkParseError[] errors)
    {
        Endpoints = endpoints;
        Errors = errors;
    }

    public NetworkEndpointDefinition[] Endpoints { get; }
    public NetworkParseError[] Errors { get; }
}

