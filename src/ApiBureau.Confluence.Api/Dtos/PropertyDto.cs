namespace ApiBureau.Confluence.Api.Dtos;

public class PropertyDto
{
    public string Id { get; set; } = null!;
    public string Key { get; set; } = null!;
    public JsonElement Value { get; set; }

    /// <summary>
    /// Gets the value as a string if it's a string type, otherwise returns null.
    /// Useful for cases like "emoji-title-published" key.
    /// </summary>
    public string? ValueAsString => Value.ValueKind == JsonValueKind.String ? Value.GetString() : null;

    /// <summary>
    /// Checks if the value is a string type.
    /// </summary>
    public bool IsStringValue => Value.ValueKind == JsonValueKind.String;

    /// <summary>
    /// Checks if the value is an object type.
    /// </summary>
    public bool IsObjectValue => Value.ValueKind == JsonValueKind.Object;

    /// <summary>
    /// Gets the raw JSON string representation of the value.
    /// Useful for debugging or when you need the exact JSON.
    /// </summary>
    public string ValueAsJsonString => Value.GetRawText();

    /// <summary>
    /// Tries to deserialize the value to a specific type T.
    /// Returns true if successful, false otherwise.
    /// </summary>
    public bool TryGetValueAs<T>(out T? result)
    {
        try
        {
            result = Value.Deserialize<T>();
            return result != null;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
