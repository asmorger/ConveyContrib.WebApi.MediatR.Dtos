using System.Net.Http;
using System.Text;
using FakeItEasy.Core;
using Newtonsoft.Json;

namespace ConveyContrib.WebApi.MediatR.Dtos.Tests
{
    public static class Extensions
    {
        public static bool MatchesEndpointMethodByName(this IFakeObjectCall fake, string name) =>
            fake.Method.Name == name;
        
        public static StringContent AsJson(this object o) =>
            new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }
}