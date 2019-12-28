using System;
using System.Threading.Tasks;

namespace MemoryCache.Models
{
    public abstract class MemoryCacheAbstract<T>
    {
        protected readonly Func<Task<T>> _fetchData;

        protected readonly int _validThroughMiliseconds;

        protected DateTime _validTo;

        protected MemoryCacheAbstract(Func<Task<T>> fetchData)
        {
            _fetchData = fetchData;
        }

        protected MemoryCacheAbstract(Func<Task<T>> fetchData, int validThroughMiliseconds)
        {
            _fetchData = fetchData;
            _validThroughMiliseconds = validThroughMiliseconds;
        }

        public abstract Task<T> GetAsync();

        public abstract void Clean();
    }
}
