using AutoMapper;
using FluentValidation;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests.Models
{
    public class TestDto
    {
        public int TheAnswer { get; set; }
    }

    public class TestDtoValidator : AbstractValidator<TestDto>
    {
        public TestDtoValidator()
        {
            RuleFor(x => x.TheAnswer).GreaterThan(0);
        }
    }

    public class TestDtoMapper : Profile
    {
        public TestDtoMapper()
        {
            CreateMap<TestDto, TestMessageWithNoResponse>();
            CreateMap<TestDto, TestMessageWithResponse>();
        }
    }
}