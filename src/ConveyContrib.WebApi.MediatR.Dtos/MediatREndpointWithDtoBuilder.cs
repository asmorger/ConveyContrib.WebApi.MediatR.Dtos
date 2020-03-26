using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Convey.WebApi;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace ConveyContrib.WebApi.MediatR.Dtos
{
    public interface IDispatcherWithDtoEndpointsBuilder
    {
        IDispatcherWithDtoEndpointsBuilder Get(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null,
            params string[] policies);
        IDispatcherWithDtoEndpointsBuilder Get<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<TResult>;

        IDispatcherWithDtoEndpointsBuilder Post(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null,
            params string[] policies);

        IDispatcherWithDtoEndpointsBuilder Post<TDto, TQuery>(string path, Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>;

        IDispatcherWithDtoEndpointsBuilder Post<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<TResult>;

        IDispatcherWithDtoEndpointsBuilder Put(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null,
            params string[] policies);

        IDispatcherWithDtoEndpointsBuilder Put<TDto, TQuery>(string path, Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>;

        IDispatcherWithDtoEndpointsBuilder Put<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<TResult>;

        IDispatcherWithDtoEndpointsBuilder Delete(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null,
            params string[] policies);

        IDispatcherWithDtoEndpointsBuilder Delete<TDto, TQuery>(string path, Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false, string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>;

        IDispatcherWithDtoEndpointsBuilder Delete<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false,
            string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<TResult>;
    }
    public class MediatREndpointWithDtoBuilder : IDispatcherWithDtoEndpointsBuilder
    {
        private readonly IEndpointsBuilder _builder;

        public MediatREndpointWithDtoBuilder(IEndpointsBuilder builder)
        {
            _builder = builder;
        }

        public IDispatcherWithDtoEndpointsBuilder Get(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null, params string[] policies)
        {
            _builder.Get(path, context, endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Get<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false, string? roles = null,
            params string[] policies) where TQuery : class, IRequest<TResult> where TDto : class
        {
            _builder.Get<TDto>(path,
                (dto, ctx) => BuildCommandContext<TDto, TQuery, TResult>(dto, ctx, beforeDispatch, afterDispatch),
                endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Post(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null, params string[] policies)
        {
            _builder.Post(path, context, endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Post<TDto, TQuery>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false,
            string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>
        {
            _builder.Post<TDto>(path, (cmd, ctx) => BuildCommandContext<TDto, TQuery, Unit>(cmd, ctx, beforeDispatch, afterDispatch), endpoint,
                auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Post<TDto, TQuery, TResult>(string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false, string? roles = null,
            params string[] policies) where TQuery : class, IRequest<TResult> where TDto : class
        {
            _builder.Post<TDto>(path,
                (dto, ctx) => BuildCommandContext<TDto, TQuery, TResult>(dto, ctx, beforeDispatch, afterDispatch),
                endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Put(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null, params string[] policies)
        {
            _builder.Put(path, context, endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Put<TDto, TQuery>(string path, Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null, params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>
        {
            _builder.Put<TDto>(path, (cmd, ctx) => BuildCommandContext<TDto, TQuery, Unit>(cmd, ctx, beforeDispatch, afterDispatch), endpoint,
                auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Put<TDto, TQuery, TResult>(
            string path, 
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null, Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false, string? roles = null,
            params string[] policies) where TDto : class where TQuery : class, IRequest<TResult>
        {
            _builder.Put<TDto>(path,
                (dto, ctx) => BuildCommandContext<TDto, TQuery, TResult>(dto, ctx, beforeDispatch, afterDispatch),
                endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Delete(string path, Func<HttpContext, Task>? context = null,
            Action<IEndpointConventionBuilder>? endpoint = null, bool auth = false,
            string? roles = null, params string[] policies)
        {
            _builder.Delete(path, context, endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Delete<TDto, TQuery>(
            string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, Unit, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false,
            string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<Unit>
        {
            _builder.Delete<TDto>(path,
                (dto, ctx) => BuildCommandContext<TDto, TQuery, Unit>(dto, ctx, beforeDispatch, afterDispatch),
                endpoint, auth, roles, policies);
            return this;
        }

        public IDispatcherWithDtoEndpointsBuilder Delete<TDto, TQuery, TResult>(
            string path,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null,
            Action<IEndpointConventionBuilder>? endpoint = null,
            bool auth = false,
            string? roles = null,
            params string[] policies)
            where TDto : class
            where TQuery : class, IRequest<TResult>
        {
            _builder.Delete<TDto>(path,
                (dto, ctx) => BuildCommandContext<TDto, TQuery, TResult>(dto, ctx, beforeDispatch, afterDispatch),
                endpoint, auth, roles, policies);
            return this;
        }

        private static async Task BuildCommandContext<TDto, TQuery, TResult>(
            TDto dto,
            HttpContext context,
            Func<TDto, HttpContext, Task>? beforeDispatch = null,
            Func<TDto, TResult, HttpContext, Task>? afterDispatch = null)
            where TDto : class
            where TQuery : class, IRequest<TResult>
        {
            EnableAsyncIO(context);

            if (beforeDispatch != null)
                await beforeDispatch(dto, context);

            if (HasFailedValidation(dto, context, out var failedMessages))
            {
                context.Response.StatusCode = 422;

                var errorViewModel = new
                {
                    Errors = failedMessages.Select(x => x.ErrorMessage)
                };
                
                await context.Response.WriteJsonAsync(errorViewModel);
                return;
            }

            var mapper = context.RequestServices.GetRequiredService<IMapper>();
            var command = mapper.Map<TQuery>(dto);

            var handler = context.RequestServices.GetRequiredService<IRequestHandler<TQuery, TResult>>();
            var result = await handler.Handle(command, CancellationToken.None);

            if (afterDispatch is null)
            {
                context.Response.StatusCode = 200;

                if (result is null || typeof(TResult) == typeof(Unit)) return;

                await context.Response.WriteJsonAsync(result);
                return;
            }

            await afterDispatch(dto, result, context);
        }

        // breaks standard bool naming conventions due to the out parameter/usage
        private static bool HasFailedValidation<TDto>(TDto dto, HttpContext context,
            out IEnumerable<ValidationFailure> failedMessages)
        {
            var validators = context.RequestServices.GetService<IEnumerable<IValidator<TDto>>>();

            if (validators == null)
            {
                failedMessages = new ValidationFailure[0];
                return false;
            }

            var failures = validators
                .Select(v => v.Validate(dto))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            var isValid = failures.Count != 0;
            failedMessages = failures;

            return isValid;
        }

        private static void EnableAsyncIO(HttpContext context)
        {
            // BUG: https://github.com/dotnet/aspnetcore/issues/7644
            var syncIOFeature = context.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }
        }
    }
}