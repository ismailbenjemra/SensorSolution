using Microsoft.EntityFrameworkCore;
using Sensor.Domain;
using System;

namespace Sensor.Infrastructure
{
    public class SensorDBContext : DbContext
    {
        public SensorDBContext(DbContextOptions<SensorDBContext> options):base(options)
        { 

        }

        public DbSet<TemperatureHistory> TemperatureHistories { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite("Data Source=Sensor.db");
    }
}
