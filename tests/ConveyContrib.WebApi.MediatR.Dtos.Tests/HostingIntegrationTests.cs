using System.Threading.Tasks;
using Alba;
using Convey;
using Convey.WebApi;
using ConveyContrib.WebApi.MediatR.Dtos.Tests.Models;
using Microsoft.AspNetCore.Hosting;
using Xunit;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests
{
    public class HostingIntegrationTests
    {
        [Fact]
        public async Task An_http_status_of_200_is_returned_when_validation_succeeds()
        {
            using var system = GetSystem();
            await system.Scenario(s =>
            {
                s.Post.Json(new TestDto {TheAnswer = 42}).ToUrl("/");
                s.StatusCodeShouldBe(200);
            });
        }

        [Fact]
        public async Task An_http_status_of_422_is_returned_when_validation_fails()
        {
            using var system = GetSystem();
            await system.Scenario(s =>
            {
                s.Post.Json(new TestDto {TheAnswer = -1}).ToUrl("/");
                s.StatusCodeShouldBe(422);
            });
        }

        [Fact]
        public async Task The_expected_response_is_returned_when_validation_succeeds()
        {
            using var system = GetSystem();
            await system.Scenario(s =>
            {
                s.Post.Json(new TestDto {TheAnswer = 42}).ToUrl("/");
                s.ContentShouldBe("42");
            });
        }
        
        private static SystemUnderTest GetSystem() =>
            SystemUnderTest.For(_ =>
            {
                _.ConfigureServices(services =>
                        services
                            .AddConvey()
                            .AddWebApi()
                            .AddDtos()
                            .Build()
                    )
                    .Configure(app =>
                    {
                        app
                            .UseDtoEndpoints(e => e
                                .Post<TestDto, TestMessageWithResponse, int>("")
                            );
                    });
            });
    }
}