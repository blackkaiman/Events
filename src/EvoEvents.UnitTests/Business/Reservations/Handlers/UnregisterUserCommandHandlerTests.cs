using EvoEvents.Business.Reservations.Commands;
using EvoEvents.Business.Reservations.Handlers;
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
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Business.Reservations.Handlers
{
    [TestFixture]
    public class UnregisterUserCommandHandlerTests
    {
        private Mock<EvoEventsContext> _context;
        private UnregisterUserCommandHandler _handler;
        private UnregisterUserCommand _command;
        private List<User> _users;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new UnregisterUserCommandHandler(_context.Object);

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
            _command.UserEmail = "bianca@yahoo.com";
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
                    FromDate = DateTime.UtcNow.AddDays(2),
                    ToDate = DateTime.UtcNow.AddDays(5),
                    Reservations = new List<Reservation>
                    {
                        new Reservation
                        {
                            Id = 3,
                            UserId = 2,
                            EventId = 1,
                            AccompanyingPersonId = null
                        }
                    }
                }
            };

            _users = new List<User>
            {
                new User
                {
                    Id = 2,
                    Email = "adelap@yahoo.com",
                    Password = "adela123",
                    Information = new UserDetail
                    {
                        FirstName = "Adela",
                        LastName = "Popescu",
                        Company = "Evozon"
                    }
                }
            };

            var reservation = new List<Reservation>
            {
               new Reservation
                {
                    Id = 3,
                    UserId = 2,
                    EventId = 1,
                    AccompanyingPersonId = null
                }
            };
            _context.Setup(c => c.Reservations).ReturnsDbSet(reservation);
            _context.Setup(c => c.Users).ReturnsDbSet(_users);
            _context.Setup(c => c.Events).ReturnsDbSet(events);
        }

        private void SetupRequest()
        {
            _command = new UnregisterUserCommand
            {
                EventId = 1,
                UserEmail = "adelap@yahoo.com",
            };
        }
    }
}