using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common
{
    public static class AppExtensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var address1 = configuration.GetValue<string>("Consul:Host");
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration.GetValue<string>("Consul:Host");
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app,IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //if (!(app.Properties["server.Features"] is FeatureCollection features)) return app;
            //var port = new Uri(features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault()).Port;
            //var addresses = features.Get<IServerAddressesFeature>();
            //var address1 = addresses.Addresses.First();

            //Console.WriteLine($"address={address}");

            //var uri = new Uri(address);
            var n = configuration["Consul:ServiceId"];
            var registration = new AgentServiceRegistration()
            {
                ID = configuration["Consul:ServiceId"],
                Name = configuration["Consul:ServiceName"],
                Address = configuration["Consul:ServiceHost"],
                Port = int.Parse(configuration["Consul:ServicePort"]),
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}