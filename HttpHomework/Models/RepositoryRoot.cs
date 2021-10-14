using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Owner
{
    [JsonPropertyName("login")] public string Login { get; set; }
}

public class Repository
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("owner")] public Owner Owner { get; set; }
}

public class Repositories
{
    [JsonPropertyName("nodes")] public List<Repository> Nodes { get; set; }

    [JsonPropertyName("pageInfo")] public PageInfo PageInfo { get; set; }
}

public class Organization
{
    [JsonPropertyName("repositories")] public Repositories Repositories { get; set; }
}

public class RepositoryData
{
    [JsonPropertyName("organization")] public Organization Organization { get; set; }
}

public class RepositoryRoot
{
    [JsonPropertyName("data")] public RepositoryData Data { get; set; }
}