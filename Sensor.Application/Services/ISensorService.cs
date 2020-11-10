using Sensor.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Application.Services
{
    public interface ISensorService
    {
        Task<TemperatureHistory> AddTemperatureHistoryAsync(int temperatureValue, CancellationToken cancellationToken = default);
        Task<IEnumerable<TemperatureHistory>> GetTemperatureHistoriesAsync(int count, CancellationToken cancellationToken = default);
        Task<string> GetSensorStateAsync(CancellationToken cancellationToken = default);
    }
}