using System.Reflection.Metadata;
using System.Text;

namespace HttpHomework.Repository
{
    public static class Query
    {
        public static string GetRepositoriesQuery(string owner, string cursor = null) =>
            $"{{\"query\":\"{{ organization(login: \\\"{owner}\\\") " +
            $"{{ repositories(first: 100" + (cursor != null ? $", after: \\\"{cursor}\\\") " : ")") +
            $"{{ nodes {{ name owner {{ login }} }} pageInfo {{ hasNextPage endCursor }} }} }} }}\"}}";


        public static string GetCommitsQuery(string owner, string repo, string cursor = null) =>
            $"{{\"query\":\"{{repository(owner:\\\"{owner}\\\", name:\\\"{repo}\\\")" +
            "{defaultBranchRef{" +
            "target{ ... on Commit {history(first: 100" +
            (cursor != null ? $", after: \\\"{cursor}\\\")" : ")") +
            "{nodes{author{email} message}pageInfo{hasNextPage endCursor}}}}}}}\"}";
    }
}