using Microsoft.Extensions.DependencyInjection;
using Sensor.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor.Application.DI
{
    public class AppDependenciesRegistration
    {
        public static void RegisterAll(IServiceCollection services)
        {
            services.AddTransient<ISensorService, SensorService>();

            Infrastructure.DI.AppDependenciesRegistration.RegisterAll(services);
        }
    }
}
