using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using TestServer;
using Xunit.Abstractions;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests.Seedwork
{
    public class LocalWebTestFixture : WebApplicationFactory<Startup>
    {
        public ITestOutputHelper Output { get; set; }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var builder = WebHost.CreateDefaultBuilder();
            builder.ConfigureLogging(_ =>
            {
                _.ClearProviders();
                _.AddXUnit(Output);
            });

            return builder;
        }
        
    }
}