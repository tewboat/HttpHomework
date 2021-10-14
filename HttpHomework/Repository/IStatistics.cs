using System.Collections.Generic;

namespace HttpHomework.Repository
{
    public interface IStatistics<in TIn, out TOut>
    {
        TOut MakeStatistics(IEnumerable<TIn> data);
    }
}