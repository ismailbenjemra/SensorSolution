using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sensor.Application.Constants;
using Sensor.Domain;
using Sensor.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sensor.Application.Services
{
    public class SensorService : ISensorService
    {
        private readonly IRepository<TemperatureHistory> _temperatureHistoryRepository;
        private readonly ILogger<SensorService> _logger;

        private readonly int HOT_MIN_VALUE;
        private readonly int COLD_MAX_VALUE;

        public SensorService(IRepository<TemperatureHistory> temperatureHistoryRepository, ILogger<SensorService> logger, IConfiguration configuration)
        {
            _temperatureHistoryRepository = temperatureHistoryRepository;
            _logger = logger;


            HOT_MIN_VALUE = int.Parse(configuration.GetSection("TemperatureLimits")["HOT_Minimal_Value"]);
            COLD_MAX_VALUE = int.Parse(configuration.GetSection("TemperatureLimits")["COLD_Maximal_Value"]);
        }

        public async Task<IEnumerable<TemperatureHistory>> GetTemperatureHistoriesAsync(int count, CancellationToken cancellationToken = default)
        {
            IEnumerable<TemperatureHistory> temperatureHistories = await _temperatureHistoryRepository.GetLatestRecordsAsync(count, cancellationToken);

            return temperatureHistories;
        }

        public async Task<string> GetSensorStateAsync(CancellationToken cancellationToken = default)
        {
            string sensorState = string.Empty;
            TemperatureHistory temperatureHistory = await _temperatureHistoryRepository.GetLatestRecordAsync( cancellationToken);

            if(temperatureHistory != null)
            {
                if (temperatureHistory.TemperatureValues >= HOT_MIN_VALUE)
                    sensorState = SensorState.HOT;
                else if (temperatureHistory.TemperatureValues < COLD_MAX_VALUE)
                    sensorState = SensorState.COLD;
                else
                    sensorState = SensorState.WARM;
            }

            return sensorState;
        }

        public async Task<TemperatureHistory> AddTemperatureHistoryAsync(int temperatureValue, CancellationToken cancellationToken = default)
        {
            var temperatureHistory = new TemperatureHistory()
            {
                TemperatureValues = temperatureValue,
                DateTime = DateTime.Now
            };

            try
            {
                await _temperatureHistoryRepository.AddAsync(temperatureHistory, cancellationToken);
                await _temperatureHistoryRepository.SaveAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error when saving new temperature {ex}");
            }

            return temperatureHistory;
        }
    }
}
