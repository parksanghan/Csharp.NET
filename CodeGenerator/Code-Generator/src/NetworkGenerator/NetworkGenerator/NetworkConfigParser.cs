using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NetworkGenerator;

internal static class NetworkConfigParser
{
    public static NetworkParseResult Parse(string filePath, string csvText)
    {
        var endpoints = new List<NetworkEndpointDefinition>();
        var errors = new List<NetworkParseError>();
        var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var lines = csvText.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');

        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var columns = SplitCsvLine(line);
            if (columns.Count < 6)
            {
                errors.Add(new NetworkParseError(filePath, i + 1, "Expected columns: Name, Type, Host, Port, MaxConnections, Description."));
                continue;
            }

            var name = columns[0].Trim();
            var typeText = columns[1].Trim();
            var host = columns[2].Trim();
            var portText = columns[3].Trim();
            var maxConnectionsText = columns[4].Trim();
            var description = columns[5].Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new NetworkParseError(filePath, i + 1, "Name is required."));
                continue;
            }

            if (!names.Add(name))
            {
                errors.Add(new NetworkParseError(filePath, i + 1, $"Duplicate endpoint name '{name}'."));
                continue;
            }

            if (!TryParseKind(typeText, out var kind))
            {
                errors.Add(new NetworkParseError(filePath, i + 1, "Type must be Server or Client."));
                continue;
            }

            if (string.IsNullOrWhiteSpace(host))
            {
                host = kind == NetworkEndpointKind.Server ? "0.0.0.0" : string.Empty;
            }

            if (kind == NetworkEndpointKind.Client && string.IsNullOrWhiteSpace(host))
            {
                errors.Add(new NetworkParseError(filePath, i + 1, "Host is required for Client endpoints."));
                continue;
            }

            if (!TryParsePort(portText, out var port))
            {
                errors.Add(new NetworkParseError(filePath, i + 1, "Port must be between 1 and 65535."));
                continue;
            }

            var maxConnections = 1;
            if (kind == NetworkEndpointKind.Server)
            {
                maxConnections = 100;
                if (!string.IsNullOrWhiteSpace(maxConnectionsText)
                    && (!int.TryParse(maxConnectionsText, NumberStyles.None, CultureInfo.InvariantCulture, out maxConnections)
                        || maxConnections < 1))
                {
                    errors.Add(new NetworkParseError(filePath, i + 1, "MaxConnections must be greater than 0."));
                    continue;
                }
            }

            endpoints.Add(new NetworkEndpointDefinition(
                name,
                kind,
                host,
                port,
                maxConnections,
                description));
        }

        return new NetworkParseResult(endpoints.ToArray(), errors.ToArray());
    }

    private static bool TryParseKind(string value, out NetworkEndpointKind kind)
    {
        if (string.Equals(value, "Server", StringComparison.OrdinalIgnoreCase))
        {
            kind = NetworkEndpointKind.Server;
            return true;
        }

        if (string.Equals(value, "Client", StringComparison.OrdinalIgnoreCase))
        {
            kind = NetworkEndpointKind.Client;
            return true;
        }

        kind = default;
        return false;
    }

    private static bool TryParsePort(string value, out int port)
    {
        return int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out port)
            && port >= 1
            && port <= 65535;
    }

    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        var inQuotes = false;

        for (var i = 0; i < line.Length; i++)
        {
            var ch = line[i];
            if (ch == '"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                {
                    current.Append('"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (ch == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(ch);
            }
        }

        result.Add(current.ToString());
        return result;
    }
}

