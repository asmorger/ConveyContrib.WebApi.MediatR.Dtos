using Convey;
using Convey.WebApi;
using ConveyContrib.WebApi.MediatR.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestServer.Models;

namespace TestServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services
                .Configure<KestrelServerOptions>(_ => _.AllowSynchronousIO = true)
                .AddConvey()
                .AddWebApi()
                .AddDtos()
                .Build();
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => 
            app
                .UseDtoEndpoints(e => e
                    .Get<TestDto, TestMessageWithResponse, int>("")
                    .Post<TestDto, TestMessageWithResponse, int>("")
                    .Put<TestDto, TestMessageWithResponse, int>("")
                );
    }
}