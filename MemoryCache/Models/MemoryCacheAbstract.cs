using System;
using System.Threading.Tasks;

namespace MemoryCache.Models
{
    public abstract class MemoryCacheAbstract<T>
    {
        protected readonly Func<Task<T>> _fetchData;

        protected MemoryCacheAbstract(Func<Task<T>> fetchData)
        {
            _fetchData = fetchData;
        }

        public abstract Task<T> GetAsync();
        public abstract void Clean();
    }
}
