using DataAccessLayer.Data;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;
using Devsu.Infrastructure;
using Devsu.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.UnitTest
{
    public static class DependencyProvider {
        private static IServiceProvider Provider()
        {
            var services = new ServiceCollection();
            //services.AddScoped<DbFactory>();
            //services.AddScoped<IWorkUnit, WorkUnit>();
            //services.AddScoped<IClientService, ClientService>();
            //services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IMovementService, MovementService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<DevsuContext>(d => d.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            return services.BuildServiceProvider();
        }

        public static T GetRequiredService<T>()
        { 
            var provider = Provider();
            return provider.GetRequiredService<T>();
        }
    }
    
}
