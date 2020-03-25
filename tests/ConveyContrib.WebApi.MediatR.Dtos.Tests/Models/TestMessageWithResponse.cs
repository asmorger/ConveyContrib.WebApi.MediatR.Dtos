using MediatR;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests.Models
{
    public class TestMessageWithResponse : IRequest<int>
    {
        public TestMessageWithResponse(int theAnswer)
        {
            TheAnswer = theAnswer;
        }

        public int TheAnswer { get; }

        public class Handler : RequestHandler<TestMessageWithResponse, int>
        {
            protected override int Handle(TestMessageWithResponse request) =>
                request.TheAnswer;
        }
    }
}