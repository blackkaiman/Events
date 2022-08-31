using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using EvoEvents.Data;
using EvoEvents.Business.Users.Handlers;
using EvoEvents.Business.Users.Commands;
using EvoEvents.Data.Models.Users;
using System;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using System.Text.Json;
using Infrastructure.Utilities.Errors.ErrorMessages;

namespace EvoEvents.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class CreateUserCommandTests
    {
        private Mock<EvoEventsContext> _context;
        private CreateUserCommandHandler _handler;
        private CreateUserCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new CreateUserCommandHandler(_context.Object);

            SetupValidRequest();
            SetupContext();
        }

        [TearDown]
        public void Clean()
        {
            _context = null;
            _handler = null;
        }

        [Test]
        public async Task ShouldCallSaveChangesAsync()
        {
            var result = await _handler.Handle(_request, new CancellationToken());
            _context.Verify( m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenDuplicateEmail_ShouldThrowException()
        {
            _request.Email = "asd@asd.com";
            var exceptionMessage = new CustomException(ErrorCode.User_UniqueEmail, UserErrorMessage.UniqueEmail).Message;
            Func<Task> act = async () => await _handler.Handle(_request, new CancellationToken());

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

        private void SetupValidRequest()
        {
            _request = new CreateUserCommand {
                FirstName = "Ion",
                LastName = "Snow",
                Email = "ion@snow.com",
                Company = "Accessa",
                Password = "tradator"
            };
        }
    }
}
