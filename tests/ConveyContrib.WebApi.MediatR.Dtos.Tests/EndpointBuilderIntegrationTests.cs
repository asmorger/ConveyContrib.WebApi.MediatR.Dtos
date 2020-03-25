using Convey.WebApi;
using ConveyContrib.WebApi.MediatR.Dtos.Tests.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests
{
    public class EndpointBuilderIntegrationTests
    {
        private readonly IEndpointsBuilder _endpointsBuilder;
        private readonly MediatREndpointWithDtoBuilder _systemUnderTest;
        
        public EndpointBuilderIntegrationTests()
        {
            _endpointsBuilder = A.Fake<IEndpointsBuilder>();
            _systemUnderTest = new MediatREndpointWithDtoBuilder(_endpointsBuilder);
        }
        
        [Fact]
        public void Get_with_no_inputs_should_call_get_on_builder()
        {
            _systemUnderTest.Get("", ctx => ctx.Response.WriteAsync("Hello"));

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Get)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Get_with_one_input_and_one_output_should_call_get_on_builder()
        {
            _systemUnderTest.Get<TestDto, TestMessageWithResponse, int>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Get)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Post_with_no_inputs_should_call_post_on_builder()
        {
            _systemUnderTest.Post("", ctx => ctx.Response.WriteAsync("Hello"));

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Post)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Post_with_one_input_and_no_output_should_call_post_on_builder()
        {
            _systemUnderTest.Post<TestDto, TestMessageWithNoResponse>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Post)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Post_with_one_input_and_one_output_should_call_post_on_builder()
        {
            _systemUnderTest.Post<TestDto, TestMessageWithResponse, int>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Post)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Put_with_no_inputs_should_call_put_on_builder()
        {
            _systemUnderTest.Put("", ctx => ctx.Response.WriteAsync("Hello"));

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Put)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Put_with_one_input_and_no_output_should_call_put_on_builder()
        {
            _systemUnderTest.Put<TestDto, TestMessageWithNoResponse>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Put)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Put_with_one_input_and_one_output_should_call_put_on_builder()
        {
            _systemUnderTest.Put<TestDto, TestMessageWithResponse, int>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Put)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Delete_with_no_inputs_should_call_delete_on_builder()
        {
            _systemUnderTest.Delete("", ctx => ctx.Response.WriteAsync("Hello"));

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Delete)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Delete_with_one_input_and_no_output_should_call_delete_on_builder()
        {
            _systemUnderTest.Delete<TestDto, TestMessageWithNoResponse>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Delete)))
                .MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Delete_with_one_input_and_one_output_should_call_delete_on_builder()
        {
            _systemUnderTest.Delete<TestDto, TestMessageWithResponse, int>("");

            A
                .CallTo(_endpointsBuilder)
                .Where(call => call.MatchesEndpointMethodByName(nameof(IEndpointsBuilder.Delete)))
                .MustHaveHappenedOnceExactly();
        }
    }
}