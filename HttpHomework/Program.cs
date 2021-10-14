using System;
using System.Linq;
using HttpHomework.Repository;
using HttpHomework.View;

namespace HttpHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            var committerStatistics = new TopCommittersStatistics(100);
            var gitHubApi = new GitHubApi();
            var repos = gitHubApi.GetOrganizationRepos("Twitter");
            var commitProgressBar = new ProgressBar("Downloading data:", repos.Count, 50);
            commitProgressBar.DrawEmptyProgressBar();
            var commits = repos
                .Select(repo =>
                {
                    var c = gitHubApi.GetRepoCommits(repo.Owner.Login, repo.Name);
                    commitProgressBar.NotifyValueChanged(commitProgressBar.CurrentValue + 1);
                    return c;
                })
                .SelectMany(list => list.Where(commit => commit.Message.StartsWith("Merge pull request")));
            var statistics = committerStatistics.MakeStatistics(commits);
            Console.WriteLine();
            foreach (var (email, score) in statistics)
                Console.WriteLine($"{email}: {score}");
        }
    }
}