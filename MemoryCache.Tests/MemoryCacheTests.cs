using System;
using Xunit;
using MemoryCache.Models;
using System.Threading.Tasks;

namespace MemoryCache.Tests
{
    public class MemoryCacheTests : IDisposable
    {
        MemoryCache<int> memoryCache;
        readonly Random random;

        public MemoryCacheTests()
        {
            random = new Random();
        }

        public void Dispose()
        {
            memoryCache.Clean();
            memoryCache = null;
        }

        [Fact]
        public async Task Verify_If_Cache_Value_Equals_Inserted()
        {
            var testValue = random.Next();

            memoryCache = new MemoryCache<int>(async () => await Task.Run(() => testValue));

            var retrievedValue = await memoryCache.GetAsync();

            Assert.Equal(testValue, retrievedValue);
        }

        [Fact]
        public async Task Verify_If_Cache_Value_Is_The_Same_For_50_Miliseconds()
        {
            var testValue = random.Next();

            memoryCache = new MemoryCache<int>(async () => await Task.Run(() => testValue), 50);

            var retrievedValue = await memoryCache.GetAsync();

            Assert.Equal(testValue, retrievedValue);
        }

        [Fact]
        public async Task Verify_If_Cache_Value_Is_New_After_500_Miliseconds_Plus_10()
        {
            var validThrough = 500;

            memoryCache = new MemoryCache<int>(async () => await Task.Run(random.Next), validThrough);
            var testValue = await memoryCache.GetAsync();

            await Task.Delay(validThrough + 10);

            var retrievedValue = await memoryCache.GetAsync();

            Assert.NotEqual(testValue, retrievedValue);
        }

        [Fact]
        public async Task Verify_If_Cache_Value_Is_The_Same_After_490_Miliseconds()
        {
            var validThrough = 500;

            memoryCache = new MemoryCache<int>(async () => await Task.Run(random.Next), validThrough);
            var testValue = await memoryCache.GetAsync();

            await Task.Delay(validThrough - 10);

            var retrievedValue = await memoryCache.GetAsync();

            Assert.Equal(testValue, retrievedValue);
        }
    }
}
