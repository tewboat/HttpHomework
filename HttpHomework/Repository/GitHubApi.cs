using System;
using System.Collections.Generic;
using System.Text.Json;
using HttpHomework.Models;
using HttpHomework.View;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;

namespace HttpHomework.Repository
{
    public class GitHubApi
    {
        public Uri BaseAddress { get; } = new("https://api.github.com");
        private HttpRequestHelper requestHelper;
        private string token;

        public GitHubApi() : this("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")
        {
        }


        public GitHubApi(string token)
        {
            this.token = token;
            requestHelper = new HttpRequestHelper(BaseAddress);
            requestHelper.AddAuthorization(token);
        }

        public List<global::Repository> GetOrganizationRepos(string owner)
        {
            var result = new List<global::Repository>();
            string endCursor = null;
            bool hasNextPage;
            do
            {
                var response = requestHelper.Post("/graphql", Query.GetRepositoriesQuery(owner, endCursor));
                var repositoryRoot = JsonConvert.DeserializeObject<RepositoryRoot>(response);
                if (repositoryRoot == null)
                    throw new JsonException($"Can't deserialize response: \n{response}");
                var repositories = repositoryRoot.Data.Organization.Repositories;
                hasNextPage = repositories.PageInfo.HasNextPage;
                endCursor = repositories.PageInfo.EndCursor;
                result.AddRange(repositories.Nodes);
            } while (hasNextPage);

            return result;
        }

        public List<Commit> GetRepoCommits(string owner, string repo)
        {
            var result = new List<Commit>();
            string endCursor = null;
            bool hasNextPage;
            do
            {
                var response = requestHelper.Post("/graphql", Query.GetCommitsQuery(owner, repo, endCursor));
                var commitRoot = JsonConvert.DeserializeObject<CommitRoot>(response);
                if (commitRoot == null)
                    throw new JsonException($"Can't deserialize response: \n{response}");
                var history = commitRoot.Data.Repository.DefaultBranchRef.Target.History;
                hasNextPage = history.PageInfo.HasNextPage;
                endCursor = history.PageInfo.EndCursor;
                result.AddRange(history.Nodes);
            } while (hasNextPage);
        
            return result;
        }
    }
}