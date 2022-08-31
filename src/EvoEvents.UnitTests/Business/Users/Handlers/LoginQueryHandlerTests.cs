using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using EvoEvents.Data;
using EvoEvents.Business.Users.Handlers;
using EvoEvents.Data.Models.Users;
using System;
using EvoEvents.Business.Users.Queries;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;

namespace EvoEvents.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class LoginQueryHandlerTests
    {
        private Mock<EvoEventsContext> _context;
        private LoginQueryHandler _handler;
        private LoginQuery _query;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new LoginQueryHandler(_context.Object);

            SetupQuery();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task WhenUserIsFound_ShouldReturnCorrectUserInformation()
        {
            var result = await _handler.Handle(_query, new CancellationToken());
 
            result.Email.Should().Be("asd@asd.com");
            result.FirstName.Should().Be("absc");
            result.LastName.Should().Be("asdfg");
            result.Company.Should().Be("comnapi");
        }

        [Test]
        public async Task WhenUserIsNotFound_ShouldThrowException()
        {
            _query.Email = "asd@kal.com";
            var exceptionMessage = new CustomException(ErrorCode.User_WrongCredentials, UserErrorMessage.WrongCredentials).Message;
            Func<Task> act = async () => await _handler.Handle(_query, new CancellationToken());

            await act.Should().ThrowAsync<CustomException>()
                 .WithMessage(exceptionMessage);
        }

        private void SetupContext()
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

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void SetupQuery()
        {
            _query = new LoginQuery
            {
                Email = "asd@asd.com",
                Password = "123ASDF",
            };
        }
    }
}
