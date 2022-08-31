using EvoEvents.Business.Users;
using EvoEvents.Data.Models.Users;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EvoEvents.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class UserToUserInformationTests
    {
        private IQueryable<User> _users;

        [SetUp]
        public void Innit()
        {
            SetupUsers();
        }

        private void SetupUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "asd@asd.com",
                    Password = "123ASDF",
                    Information = new UserDetail
                    {
                        FirstName = "absc",
                        LastName = "asdfg",
                        Company = "comnapi"
                    }
                }
            };

            _users = users.AsQueryable();
        }

        [Test]
        public void ShouldReturnCorrectUserInformation()
        {
            var userInformation = _users.ToUserInformation().FirstOrDefault();

            userInformation.FirstName.Should().Be("absc");
            userInformation.LastName.Should().Be("asdfg");
            userInformation.Company.Should().Be("comnapi");
            userInformation.Email.Should().Be("asd@asd.com");
        }
    }
}