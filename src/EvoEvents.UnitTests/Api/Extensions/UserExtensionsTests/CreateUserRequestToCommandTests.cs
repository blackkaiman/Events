using EvoEvents.API.Requests.Users;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class CreateUserRequestToCommandTests
    {
        [Test]
        public void ShouldReturnCorrectCreateUserCommand()
        {
            var request = new CreateUserRequest
            {
                FirstName = "Radu",
                LastName = "mifdas",
                Company = "fdasfa",
                Email = "asccc@hh.com",
                Password = "kila"
            };

            var result = request.ToCommand();

            result.Password.Should().Be(request.Password);
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);  
            result.Company.Should().Be(request.Company);    
            result.Email.Should().Be(request.Email);    
        }
    }
}