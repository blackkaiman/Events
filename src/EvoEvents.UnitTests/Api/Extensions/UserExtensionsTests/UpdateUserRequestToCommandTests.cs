using EvoEvents.API.Requests.Users;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Extensions.UserExtensionsTests
{
    [TestFixture]
    public class UpdateUserRequestToCommandTests
    {
        [Test]
        public void ShouldReturnCorrectUpdateUserCommand()
        {
            var request = new UpdateUserRequest
            {
                Email = "maria234@yahoo.com",
                FirstName = "Maria",
                LastName = "Oltean",
                Company = "Evozon",
                OldPassword = "maria123",
                NewPassword = "maria1234"
            };

            var result = request.ToCommand();

            result.Email.Should().Be(request.Email);
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Company.Should().Be(request.Company);
            result.OldPassword.Should().Be(request.OldPassword);
            result.NewPassword.Should().Be(request.NewPassword);
        }
    }
}