using EvoEvents.API.Requests.Events;
using EvoEvents.Business.Events.Commands;
using EvoEvents.Business.Events.Handlers;
using EvoEvents.Data;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
using FluentAssertions;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Business.Events.Handlers
{
    [TestFixture]
    public class CreateEventCommandHandlerTests
    {
        private Mock<EvoEventsContext> _context;
        private CreateEventCommandHandler _handler;
        private CreateEventCommand _command;
        private DateTime _fromDate;
        private DateTime _toDate;

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new CreateEventCommandHandler(_context.Object);
            _fromDate = DateTime.UtcNow.AddDays(1);
            _toDate = DateTime.UtcNow.AddDays(2);

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
        public async Task WhenEventIsNew_ShouldCallSaveChangesAsync()
        {
            _command.Name = "EvoEvent3";
            var result = await _handler.Handle(_command, new CancellationToken());
            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenCreateAlreadyExistingEvent_ShouldThrowException()
        {
            var exceptionMessage = new CustomException(ErrorCode.Event_AlreadyCreated, EventErrorMessage.EventAlreadyCreated).Message;
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
                    Description = "foarte fain",
                    EventType = new EventTypeLookup
                    {
                        Id = (EventType)2,
                        Name = EventType.Movie.ToString()
                    },
                    MaxNoAttendees = 10,
                    Address = new Address
                    {
                        Location = "Strada Bisericii Sud",
                        CityId = City.Milano,
                        CountryId = Country.Italia
                    },
                    FromDate = _fromDate,
                    ToDate = _toDate,
                    Image = SetupFile().FileToByteArray(),
                    Reservations = new List<Reservation>()
                },
               new Event
                {
                    Id=2,
                    Name = "EvoEvent2",
                    Description = "super fain",
                    EventType = new EventTypeLookup
                    {
                        Id = EventType.Movie,
                        Name = EventType.Movie.ToString()
                    },
                    MaxNoAttendees = 15,
                    Address = new Address
                    {
                        Location = "Strada Bisericii Sud2",
                        CityId = City.Milano,
                        CountryId = Country.Italia
                    },
                    FromDate = _fromDate,
                    ToDate = _toDate,
                    Image = SetupFile().FileToByteArray(),
                    Reservations = new List<Reservation>()
                }
            };

            _context.Setup(c => c.Events).ReturnsDbSet(events);
        }

        private void SetupRequest()
        {
            _command = new CreateEventCommand
            {
                Name = "EvoEvent",
                Description = "foarte fain",
                EventType = (EventType)2,
                MaxNoAttendees = 10,
                Location = "Strada Bisericii Sud",
                City = City.Milano,
                FromDate = _fromDate,
                ToDate = _toDate,
                Country = Country.Italia,
                EventImage = SetupFile().FileToByteArray()
            };
        }

        private byte[] SetupByteArray()
        {
            var base64Image = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNkqAcAAIUAgUW0RjgAAAAASUVORK5CYII=";
            byte[] content = Convert.FromBase64String(base64Image);
            return content;
        }

        private IFormFile SetupFile()
        {
            var fileMock = new Mock<IFormFile>();
            byte[] content = SetupByteArray();
            var fileName = "test.png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
        }
    }
}