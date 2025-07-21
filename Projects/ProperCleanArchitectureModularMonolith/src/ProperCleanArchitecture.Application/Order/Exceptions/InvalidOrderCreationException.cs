using System.Collections;

namespace ProperCleanArchitecture.Application.Order.Exceptions;

public class InvalidOrderCreationException(
    string? message, 
    IDictionary? invalidProps
) : Exception($"{message} - {FormatProps(invalidProps)}")
{
    private static string FormatProps(IDictionary? props)
    {
        if (props == null || props.Count == 0) return string.Empty;
        var entries = props.Cast<DictionaryEntry>()
            .Select(e => $"{e.Key}: {e.Value}");
        return $" | Invalid properties: {string.Join(", ", entries)}";
    }
}