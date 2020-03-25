using System;
using System.Linq;
using AutoMapper;
using Convey;
using Convey.WebApi;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ConveyContrib.WebApi.MediatR.Dtos
{
    public static class Extensions
    {
        public static IConveyBuilder AddDtos(this IConveyBuilder builder)
        {
            // adding this restriction due to FluentValidators
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic).ToArray();
            
            builder.Services.AddMediatR(assemblies);
            builder.Services.AddAutoMapper(assemblies);
            builder.Services.AddValidatorsFromAssemblies(assemblies);
            return builder;
        }
        
        public static IApplicationBuilder UseDtoEndpoints(this IApplicationBuilder app,
            Action<IDispatcherWithDtoEndpointsBuilder> builder, bool useAuthorization = true)
        {
            var definitions = app.ApplicationServices.GetService<WebApiEndpointDefinitions>();
            app.UseRouting();
            if (useAuthorization) app.UseAuthorization();

            app
                .UseEndpoints(router =>
                    builder(new MediatREndpointWithDtoBuilder(new EndpointsBuilder(router, definitions))));

            return app;
        }
    }
}