using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sensor.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor.Infrastructure.DI
{
    public class AppDependenciesRegistration
    {
        public static void RegisterAll(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<SensorDBContext>(options => options.UseSqlite("Data Source=Sensor.db"));
        }
    }
}
