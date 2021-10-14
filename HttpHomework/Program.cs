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
            Console.WriteLine("Enter your github token:");
            var token = Console.ReadLine();
            Console.WriteLine("Enter organization name:");
            var owner = Console.ReadLine();

            var committerStatistics = new TopCommittersStatistics(100);
            var gitHubApi = new GitHubApi(token);
            var repos = gitHubApi.GetOrganizationRepos(owner);
            var commitProgressBar = new ProgressBar("Downloading data:", repos.Count, 50);
            commitProgressBar.DrawEmptyProgressBar();
            var commits = repos
                .Select(repo =>
                {
                    var c = gitHubApi.GetRepoCommits(repo.Owner.Login, repo.Name);
                    commitProgressBar.NotifyValueChanged(commitProgressBar.CurrentValue + 1);
                    return c;
                })
                .SelectMany(list => list.Where(commit => !commit.Message.StartsWith("Merge pull request")));
            var statistics = committerStatistics.MakeStatistics(commits);
            Console.WriteLine("\nCommits amount: Email");
            foreach (var (score, email) in statistics.Reverse())
                Console.WriteLine($"{score}: {email}");
        }
    }
}