using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Autofac;
using Miningcore.Api;
using Miningcore.Configuration;

namespace Miningcore;

public static class MiningCoreWebHostBuilder
{
    public static IWebHost BuildWebHost<T>(T serverConfig, ClusterConfig clusterConfig, ILogger logger,
        IPEndPoint listenEndpoint, string certificatePassword = null, IComponentContext container = null)
        where T : ApiConfig
    {
        var builder = new WebHostBuilder()
            .Configure(app =>
            {
                // Configure the HTTP request pipeline
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            })
            .ConfigureServices(services =>
            {
                services.AddMvc();
            })
            .UseKestrel(options =>
            {
                options.Listen(listenEndpoint, listenOptions =>
                {
                    if (serverConfig.Tls?.Enabled == true)
                    {
                        var certPath = serverConfig.Tls.TlsPfxFile;
                        var signingCertificate = new X509Certificate2(certPath, certificatePassword ?? string.Empty);
                        
                        listenOptions.UseHttps(signingCertificate);
                    }
                });
            });

        logger.Info(() => $"Launching {(!string.IsNullOrEmpty(clusterConfig.ClusterName) ? clusterConfig.ClusterName : "Miningcore")} ...");

        return builder.Build();
    }
}