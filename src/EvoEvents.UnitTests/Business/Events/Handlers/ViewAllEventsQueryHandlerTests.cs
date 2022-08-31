using EvoEvents.API.Requests.Events;
using EvoEvents.Business.Events.Handlers;
using EvoEvents.Business.Events.Queries;
using EvoEvents.Data;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
using FluentAssertions;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Business.Events.Handlers
{
    [TestFixture]
    public class ViewAllEventsQueryHandlerTests
    {
        private Mock<EvoEventsContext> _context;
        private ViewAllEventsQueryHandler _handler;
        private ViewAllEventsQuery _query;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string descriptionString = PrimitiveGenerator.Alphanumeric(160);

        [SetUp]
        public void Init()
        {
            _context = new Mock<EvoEventsContext>();
            _handler = new ViewAllEventsQueryHandler(_context.Object);
            _fromDate = DateTime.Now.AddDays(1);
            _toDate = DateTime.Now.AddDays(2);

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
        public async Task WhenViewAllEventsIsCorrect_ShouldReturnCorrectEventInformation()
        {
            var result = await _handler.Handle(_query, new CancellationToken());

            result.TotalNoItems.Should().Be(2);
            result.Items.First().Id.Should().Be(1);
            result.Items.First().Name.Should().Be("EvoEvent");
            result.Items.First().Description.Should().Be(descriptionString.Substring(0, 150));
            result.Items.First().EventType.Should().Be(EventType.Movie);
            result.Items.First().MaxNoAttendees.Should().Be(10);
            result.Items.First().Address.City.Should().Be(City.Milano);
            result.Items.First().Address.Country.Should().Be(Country.Italia);
            result.Items.First().Address.Location.Should().Be("Strada Bisericii Sud");
            result.Items.First().FromDate.Should().Be(_fromDate);
            result.Items.First().ToDate.Should().Be(_toDate);
            result.Items.First().EventImage.Should().Equal(SetupFile().FileToByteArray());
        }

        [Test]
        public async Task WhenEventsAreNotFound_ShouldReturnEmptyList()
        {
            _query.PageNumber = 100000000;
            var result = await _handler.Handle(_query, new CancellationToken());

            result.TotalNoItems.Should().Be(2);
            result.Items.Should().BeEmpty();
        }

        private void SetupContext()
        {
            var events = new List<Event>
            {
               new Event
                {
                    Id=1,
                    Name = "EvoEvent",
                    Description = descriptionString,
                    EventType = new EventTypeLookup
                    {
                        Id = EventType.Movie,
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
                    Description = descriptionString,
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
            _query = new ViewAllEventsQuery
            {
                PageNumber = 1,
                ItemsPerPage = 1
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