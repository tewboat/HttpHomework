using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace HttpHomework.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Author
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Commit
    {
        [JsonPropertyName("author")]
        public Author Author { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    public class PageInfo
    {
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }

        [JsonPropertyName("endCursor")]
        public string EndCursor { get; set; }
    }

    public class History
    {
        [JsonPropertyName("nodes")]
        public List<Commit> Nodes { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo PageInfo { get; set; }
    }

    public class Target
    {
        [JsonPropertyName("history")]
        public History History { get; set; }
    }

    public class DefaultBranchRef
    {
        [JsonPropertyName("target")]
        public Target Target { get; set; }
    }

    public class CommitRepository
    {
        [JsonPropertyName("defaultBranchRef")]
        public DefaultBranchRef DefaultBranchRef { get; set; }
    }

    public class CommitData
    {
        [JsonPropertyName("repository")]
        public CommitRepository Repository { get; set; }
    }

    public class CommitRoot
    {
        [JsonPropertyName("data")]
        public CommitData Data { get; set; }
    }


}