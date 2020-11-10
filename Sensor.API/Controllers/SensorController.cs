using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sensor.Application.Services;
using Sensor.Domain;

namespace Sensor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<SensorController> _logger;
        private readonly ISensorService _sensorService;

        public SensorController(ILogger<SensorController> logger, ISensorService sensorService)
        {
            _logger = logger;
            _sensorService = sensorService;
        }

        /// <summary>
        /// 1) Sensor store the temperature from the TemperatureCaptor component.
        /// </summary>
        /// <param name="temperatureValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TemperatureHistory>> PostAsync(int temperatureValue, CancellationToken cancellationToken = default)
        {
            var temperatureHistory = await _sensorService.AddTemperatureHistoryAsync(temperatureValue, cancellationToken);

            if (temperatureHistory.Id == 0)
                return StatusCode(500, "Internal server error, Please check the server logs or contact your adminstrator !");

            return Ok(temperatureHistory);
        }

        /// <summary>
        /// 5) Get latest temperature requests
        /// </summary>
        /// <param name="count">Number Of latest requests, default value is 15</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TemperatureHistory>>> GetAsync(int count = 15, CancellationToken cancellationToken = default)
        {
            var temperatureHistories = await _sensorService.GetTemperatureHistoriesAsync(count, cancellationToken);
            return Ok(temperatureHistories);
        }

        /// <summary>
        /// 2-3-4) Get Sensor satate
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("state")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<String>> GetAsync(CancellationToken cancellationToken = default)
        {

            var sensorState = await _sensorService.GetSensorStateAsync(cancellationToken);
            if (string.IsNullOrEmpty(sensorState))
            {
                return NotFound();
            }
            return Ok(sensorState);
        }
    }
}
