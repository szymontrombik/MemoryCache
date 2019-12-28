using System;
using System.Threading.Tasks;
using MemoryCache.Mocks;
using MemoryCache.Models;

namespace MemoryCache
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var detailsDbMock = new DeviceDetailsDbMock();
            var _deviceCache =
                new MemoryCache<DeviceDetails>(
                    async () => await detailsDbMock.GetTasks(), 500);
            var name = await _deviceCache.GetAsync();

            Console.WriteLine("name: " + name.Name);

            var name2 = await _deviceCache.GetAsync();
            Console.WriteLine("name2: " + name2.Name);

            _deviceCache.Clean();

            var name3 = await _deviceCache.GetAsync();
            Console.WriteLine("name3: " + name3.Name);

            _deviceCache =
                new MemoryCache<DeviceDetails>(
                    async () => await Task.Run(
                        () => new DeviceDetails { Name = "NewSuperName" })); // for test purpose

            var name4 = await _deviceCache.GetAsync();
            Console.WriteLine("name4: " + name4.Name);
        }
    }
}
 