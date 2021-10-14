using System.Collections.Generic;
using HttpHomework.Models;

namespace HttpHomework.Repository
{
    public class TopCommittersStatistics : IStatistics<Commit, IEnumerable<(int, string)>>
    {
        public int Count { get; }

        public TopCommittersStatistics(int count)
        {
            Count = count;
        }

        public IEnumerable<(int, string)> MakeStatistics(IEnumerable<Commit> commits)
        {
            var committersResults = new Dictionary<string, int>();
            foreach (var commit in commits)
            {
                var email = commit.Author.Email;
                if (!committersResults.ContainsKey(email))
                    committersResults[email] = 0;
                committersResults[email] += 1;
            }

            var priorityQueue = new PriorityQueue<(int, string)>(Count);
            foreach (var (key, value) in committersResults)
                priorityQueue.Enqueue((value, key));
            return priorityQueue;
        }
    }
}