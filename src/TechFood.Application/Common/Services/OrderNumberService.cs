using System;
using System.Threading;
using System.Threading.Tasks;
using TechFood.Application.Common.Services.Interfaces;

namespace TechFood.Application.Common.Services
{
    public class OrderNumberService : IOrderNumberService
    {
        private int _count;
        private DateTime _lastResetTime = DateTime.UtcNow.Date;

        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<int> GetNumberAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_lastResetTime.Date != DateTime.UtcNow.Date)
                {
                    _count = 0;
                    _lastResetTime = DateTime.UtcNow.Date;
                }

                _count++;
                return _count;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
