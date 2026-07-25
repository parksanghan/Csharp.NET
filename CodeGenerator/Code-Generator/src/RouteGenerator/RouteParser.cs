using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RouteGenerator;

internal static class RouteParser
{
    public static ParseResult Parse(string csvText)
    {
        var routes = new List<RouteDefinition>();
        var errors = new List<string>();
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
                errors.Add($"Line {i + 1}: expected 6 columns: From, To, Protocol, Host, Port, Description.");
                continue;
            }

            var from = columns[0].Trim();
            var to = columns[1].Trim();
            var protocol = columns[2].Trim().ToUpperInvariant();
            var host = columns[3].Trim();
            var portText = columns[4].Trim();
            var description = columns[5].Trim();

            if (string.IsNullOrWhiteSpace(from))
            {
                errors.Add($"Line {i + 1}: From is required.");
            }

            if (string.IsNullOrWhiteSpace(to))
            {
                errors.Add($"Line {i + 1}: To is required.");
            }

            if (string.IsNullOrWhiteSpace(host))
            {
                errors.Add($"Line {i + 1}: Host is required.");
            }

            if (protocol is not "TCP" and not "UDP")
            {
                errors.Add($"Line {i + 1}: Protocol must be TCP or UDP.");
            }

            int? port = null;
            if (!string.IsNullOrWhiteSpace(portText))
            {
                if (!int.TryParse(portText, NumberStyles.None, CultureInfo.InvariantCulture, out var parsedPort)
                    || parsedPort < 1
                    || parsedPort > 65535)
                {
                    errors.Add($"Line {i + 1}: Port must be between 1 and 65535.");
                }
                else
                {
                    port = parsedPort;
                }
            }

            if ((protocol == "TCP" || protocol == "UDP") && port is null)
            {
                errors.Add($"Line {i + 1}: {protocol} route requires a port.");
            }

            routes.Add(new RouteDefinition(from, to, protocol, host, port, description));
        }

        return new ParseResult(routes, errors);
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

internal sealed class ParseResult
{
    public ParseResult(
        IReadOnlyList<RouteDefinition> routes,
        IReadOnlyList<string> errors)
    {
        Routes = routes;
        Errors = errors;
    }

    public IReadOnlyList<RouteDefinition> Routes { get; }
    public IReadOnlyList<string> Errors { get; }
}

