using EvoEvents.Business.Users;
using EvoEvents.Data.Models.Users;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EvoEvents.UnitTests.Business.Users.Extensions
{
    [TestFixture]
    public class FilterByEmailTests
    {
        private IQueryable<User> _users;

        [SetUp]
        public void Init()
        {
            SetupUsers();
        }

        [Test]
        public void ShouldReturnFilteredQueryable()
        {
            string email = "asd@asd.com";
            var users = _users.FilterByEmail(email);

            Assert.That(users.Count(), Is.EqualTo(1));
            Assert.That(users.FirstOrDefault().Email, Is.EqualTo(email));
        }

        private void SetupUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "asd@asd.com",
                    Password = "kila",
                    Information = new UserDetail
                    {
                        FirstName = "absc",
                        LastName = "asdfg",
                        Company = "comnapi"
                    }
                },
                new User
                {
                    Email = "asd@aoosd.com",
                    Password = "kila",
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
    }
}