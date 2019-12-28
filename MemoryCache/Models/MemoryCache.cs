using System;
using System.Threading.Tasks;

namespace MemoryCache.Models
{
    public class MemoryCache<T> : MemoryCacheAbstract<T>
    {
        protected readonly object _lock = new object();

        private T Deposite { get; set; }

        public MemoryCache(Func<Task<T>> fetchData)
            : base(fetchData)
        {
        }

        public override async Task<T> GetAsync()
        {
            T newDepositeValue;

            if (Deposite == default)
            {
                newDepositeValue = await _fetchData();

                lock (_lock)
                {
                    Console.WriteLine("is null");
                    Deposite = newDepositeValue;
                }
            }
            else
            {
                Console.WriteLine("not null");
            }
            return Deposite;
        }

        public override void Clean()
        {
            lock (_lock)
            {
                Deposite = default;
            }
        }
    }
}
