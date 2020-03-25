using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ConveyContrib.WebApi.MediatR.Dtos.Tests.Models;
using ConveyContrib.WebApi.MediatR.Dtos.Tests.Seedwork;
using Xunit;
using Xunit.Abstractions;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests
{
    public class HostingIntegrationTests : IClassFixture<LocalWebTestFixture>, IDisposable
    {
        public HostingIntegrationTests(LocalWebTestFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _fixture.Output = output;

            _client = fixture.CreateClient();
        }

        public void Dispose() => _fixture.Output = null;

        private readonly LocalWebTestFixture _fixture;
        private readonly HttpClient _client;

        [Fact]
        public async Task An_http_status_of_422_is_returned_when_validation_fails()
        {
            var result = await _client.PostAsync("", new TestDto {TheAnswer = -1}.AsJson());
            
            Assert.Equal(HttpStatusCode.UnprocessableEntity, result.StatusCode);
        }
        
        [Fact]
        public async Task An_http_status_of_200_is_returned_when_validation_succeeds()
        {
            var result = await _client.PostAsync("", new TestDto {TheAnswer = 42}.AsJson());
            
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        
        [Fact]
        public async Task The_expected_response_body_is_returned_when_validation_succeeds()
        {
            var result = await _client.PostAsync("", new TestDto {TheAnswer = 42}.AsJson());
            
            Assert.Equal("42", await result.Content.ReadAsStringAsync());
        }
    }
}