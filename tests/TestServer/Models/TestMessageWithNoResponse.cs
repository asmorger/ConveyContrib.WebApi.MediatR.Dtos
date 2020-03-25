using MediatR;

namespace TestServer.Models
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