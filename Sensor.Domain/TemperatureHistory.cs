using System;

namespace Sensor.Domain
{
    public class TemperatureHistory : EntityBase
    { 
        public DateTime DateTime { get; set; }
        public int TemperatureValues { get; set; }
    }
}
