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

        public MemoryCache(Func<Task<T>> fetchData, int validThroughMiliseconds)
            : base(fetchData, validThroughMiliseconds)
        {

        }

        public override async Task<T> GetAsync()
        {
            T newDepositeValue;

            if (Deposite == default
               || (_validTo != default && _validTo < DateTime.Now))
            {
                newDepositeValue = await _fetchData();

                lock (_lock)
                {
                    Console.WriteLine("is null");
                    Deposite = newDepositeValue;
                    if (_validThroughMiliseconds != default)
                    {
                        _validTo = DateTime.Now.AddMinutes(_validThroughMiliseconds);
                    }
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
