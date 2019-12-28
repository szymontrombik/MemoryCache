using System;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryCache.Mocks
{
    public class DeviceDetailsDbMock
    {
        public async Task<DeviceDetails> GetTasks()
        {
            await Task.Run(() => Thread.Sleep(5000));

            return new DeviceDetails { Name = "New" + DateTime.Now.ToLongTimeString() };
        }
    }
}
