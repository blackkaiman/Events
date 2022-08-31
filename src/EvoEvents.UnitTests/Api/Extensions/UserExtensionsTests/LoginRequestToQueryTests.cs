using EvoEvents.API.Requests.Users;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Extensions.UserExtensionsTests
{
    [TestFixture]
    public class LoginRequestToQueryTests
    {
        [Test]
        public void ShouldReturnCorrectLoginQuery()
        {
            var request = new LoginRequest
            {
                Email = "asccc@hh.com",
                Password = "kila"
            };

            var result = request.ToQuery();

            result.Password.Should().Be(request.Password);
            result.Email.Should().Be(request.Email);
        }
    }
}
