using MediatR;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests.Models
{
    public class TestMessageWithNoResponse : IRequest
    {
        public TestMessageWithNoResponse(int theAnswer)
        {
            TheAnswer = theAnswer;
        }

        public int TheAnswer { get; }

        public class Handler : RequestHandler<TestMessageWithNoResponse>
        {
            public int LifeTheUniverseAndEverything { get; private set; }

            protected override void Handle(TestMessageWithNoResponse request) =>
                LifeTheUniverseAndEverything = request.TheAnswer;
        }
    }
}