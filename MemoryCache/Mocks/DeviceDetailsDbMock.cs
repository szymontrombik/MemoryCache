using System;
using System.Threading;
using System.Threading.Tasks;
using MemoryCache.Models;

namespace MemoryCache.Mocks
{
    public class DeviceDetailsDbMock
    {
        public async Task<DeviceDetails> GetTasks()
        {
            Console.WriteLine("*** START *** Getting data from db");
            await Task.Run(() => Thread.Sleep(5000));
            Console.WriteLine("*** DONE **** Getting data from db");
            return new DeviceDetails { Name = "New" + DateTime.Now.ToLongTimeString() };
        }
    }
}
