using EvoEvents.Business.Reservations.Commands;
using EvoEvents.Business.Reservations.Handlers;
using EvoEvents.Business.Users;
using EvoEvents.Data;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Business.Reservations.Handlers
{
    [TestFixture]
    public class CreateReservationCommandHandlerTests
    {
        private Mock<EvoEventsContext> _context;
        private CreateReservationCommandHandler _handler;
        private CreateReservationCommand _command;
        private List<User> _users;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new CreateReservationCommandHandler(_context.Object);

            SetupContext();
            SetupRequest();
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
            var result = await _handler.Handle(_command, new CancellationToken());
            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task ShouldAddReservationAsync()
        {
            var user = _users.Where(u => u.Email == _command.UserEmail).FirstOrDefault();
            var accompanyPersonId = _users.Where(u => u.Email == _command.AccompanyingPersonEmail).Select(u => u.Id).FirstOrDefault();

            var result = await _handler.Handle(_command, new CancellationToken());

            _context.Verify(c => c.Reservations
                .AddAsync(It.Is<Reservation>(obj =>
                    obj.EventId == _command.EventId &&
                    obj.AccompanyingPersonId == accompanyPersonId &&
                    obj.UserId == user.Id), It.IsAny<CancellationToken>()),Times.Once);
        }

        [Test]
        public async Task WhenEventIsNotFound_ShouldThrowException()
        {
            _command.EventId = 100000;
            var exceptionMessage = new CustomException(ErrorCode.Event_NotFound, EventErrorMessage.EventNotFound).Message;
            Func<Task> act = async () => await _handler.Handle(_command, new CancellationToken());

            await act.Should().ThrowAsync<CustomException>()
                 .WithMessage(exceptionMessage);
        }

        [Test]
        public async Task WhenUserIsNotFound_ShouldThrowException()
        {
            _command.UserEmail = "asd@kal.com";
            var exceptionMessage = new CustomException(ErrorCode.User_NotFound, UserErrorMessage.UserNotFound).Message;
            Func<Task> act = async () => await _handler.Handle(_command, new CancellationToken());

            await act.Should().ThrowAsync<CustomException>()
                 .WithMessage(exceptionMessage);
        }

        private void SetupContext()
        {
            var events = new List<Event>
            {
               new Event
                {
                    Id=1,
                    Name = "EvoEvent",
                    Description = "super",
                    EventType = new EventTypeLookup
                    {
                        Id = EventType.Movie,
                        Name = EventType.Movie.ToString()
                    },
                    MaxNoAttendees = 10,
                    Reservations = new List<Reservation>()
                }
            };

            _users = new List<User>
            {
                new User
                {   
                    Id = 1,
                    Email = "asd@asd.com",
                    Password = "123ASDF",
                    Information = new UserDetail
                    {   
                        FirstName = "absc",
                        LastName = "asdfg",
                        Company = "comnapi"
                    }
                },
                 new User
                {
                    Id = 2,
                    Email = "abcd@aaa.com",
                    Password = "123ASDF",
                    Information = new UserDetail
                    {
                        FirstName = "aaaaaa",
                        LastName = "bbbbb",
                        Company = "comnapi"
                    }
                }
            };
            _context.Setup(c => c.Reservations).ReturnsDbSet(new List<Reservation> { });
            _context.Setup(c => c.Users).ReturnsDbSet(_users);
            _context.Setup(c => c.Events).ReturnsDbSet(events);
        }

        private void SetupRequest()
        {
            _command = new CreateReservationCommand
            {
                EventId = 1,
                UserEmail = "asd@asd.com",
                AccompanyingPersonEmail = "abcd@aaa.com"
            };
        }
    }
}