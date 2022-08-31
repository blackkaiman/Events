using EvoEvents.Business.Users;
using EvoEvents.Business.Users.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class CreateUserCommandToUserTests
    {
        [Test]
        public void ShouldReturnCorrectCreateUserCommand()
        {
            var request = new CreateUserCommand
            {
                FirstName = "Radu",
                LastName = "mifdas",
                Company = "fdasfa",
                Email = "asccc@hh.com",
                Password = "kila"
            };

            var result = request.ToUser();

            result.Information.FirstName.Should().Be(request.FirstName);
            result.Information.LastName.Should().Be(request.LastName);
            result.Information.Company.Should().Be(request.Company);
            result.Email.Should().Be(request.Email);
            result.Password.Should().Be(request.Password);
        }
    }
}