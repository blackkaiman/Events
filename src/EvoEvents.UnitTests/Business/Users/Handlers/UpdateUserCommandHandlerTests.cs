using EvoEvents.Business.Users.Commands;
using EvoEvents.Business.Users.Handlers;
using EvoEvents.Data;
using EvoEvents.Data.Models.Users;
using FluentAssertions;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Business.Users.Handlers
{
    [TestFixture]
    public class UpdateUserCommandTests
    {
        private Mock<EvoEventsContext> _context;
        private UpdateUserCommandHandler _handler;
        private UpdateUserCommand _request;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new UpdateUserCommandHandler(_context.Object);

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
        public async Task WhenUserIsNotFound_ShouldThrowException()
        {
            _request.Email = "maria245@yahoo.com";
            var exceptionMessage = new CustomException(ErrorCode.User_WrongCredentials, UserErrorMessage.WrongCredentials).Message;
            Func<Task> act = async () => await _handler.Handle(_request, new CancellationToken());

            await act.Should().ThrowAsync<CustomException>()
                 .WithMessage(exceptionMessage);
        }

        [Test]
        public async Task WhenPasswordIsNull_ShouldCallSaveChangesAsync()
        {
            _request.OldPassword = null;
            _request.NewPassword = null;
            var result = await _handler.Handle(_request, new CancellationToken());
            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldCallSaveChangesAsync()
        {
            var result = await _handler.Handle(_request, new CancellationToken());
            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private void SetupContext()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "maria234@yahoo.com",
                    Password = "maria1234",
                    Information = new UserDetail
                    {
                        FirstName = "Maria",
                        LastName = "Oltean",
                        Company = "Evozon"
                    }
                }
            };

            _context.Setup(c => c.Users).ReturnsDbSet(users);
        }

        private void SetupValidRequest()
        {
            _request = new UpdateUserCommand
            {
                Email = "maria234@yahoo.com",
                FirstName = "Maria",
                LastName = "Oltean",
                Company = "Evozon",
                OldPassword = "maria1234",
                NewPassword = "maria1234"
            };
        }
    }
}