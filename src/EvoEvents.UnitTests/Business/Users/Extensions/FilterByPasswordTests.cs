using EvoEvents.Business.Users;
using EvoEvents.Data.Models.Users;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EvoEvents.UnitTests.Business.Users.Extensions
{
    [TestFixture]
    public class FilterByPasswordTests
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
            string password = "kila";
            var users = _users.FilterByPassword(password);

            Assert.That(users.Count(), Is.EqualTo(1));
            Assert.That(users.FirstOrDefault().Password, Is.EqualTo(password));
        }

        private void SetupUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "asd@asd.com",
                    Password = "kifdasla",
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