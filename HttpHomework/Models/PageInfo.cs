using System.Text.Json.Serialization;

public class PageInfo
{
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }

    [JsonPropertyName("endCursor")]
    public string EndCursor { get; set; }
}