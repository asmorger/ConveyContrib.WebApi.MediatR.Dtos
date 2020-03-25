using FakeItEasy.Core;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests
{
    public static class Extensions
    {
        public static bool MatchesEndpointMethodByName(this IFakeObjectCall fake, string name)
        {
            return fake.Method.Name == name;
        }
    }
}