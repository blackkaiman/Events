using EvoEvents.API.Requests.Events;
using EvoEvents.Business.Addresses.Models;
using EvoEvents.Business.Events;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EvoEvents.UnitTests.Business.Events.Extensions
{
    [TestFixture]
    public class EventToEventInformationTests
    {
        private IQueryable<Event> _events;

        [SetUp]
        public void Innit()
        {
            SetupEvents();
        }

        private void SetupEvents()
        {
            var events = new List<Event>
            {
                new Event
                {
                    Id=1,
                    Name = "EvoEvent",
                    Description = "super",
                    EventTypeId = EventType.Movie,
                    MaxNoAttendees = 10,
                    Address = new Address
                    {
                        Location = "Strada Bisericii Sud",
                        CityId = City.Milano,
                        CountryId = Country.Italia
                    },
                    Image = SetupFile().FileToByteArray(),
                    Reservations = new List<Reservation>()
                }
            };

            _events = events.AsQueryable();
        }

        [Test]
        public void ShouldReturnCorrectEventInformation()
        {   
            var eventInformation = _events.ToEventInformation().FirstOrDefault();

            eventInformation.Id.Should().Be(1);
            eventInformation.Name.Should().Be("EvoEvent");
            eventInformation.Description.Should().Be("super");
            eventInformation.EventType.Should().Be(EventType.Movie);
            eventInformation.MaxNoAttendees.Should().Be(10);
            eventInformation.Address.Should().BeEquivalentTo(
                new AddressInformation
                {
                    Location = "Strada Bisericii Sud",
                    City = City.Milano,
                    Country = Country.Italia
                });
            eventInformation.EventImage.Should().Equal(SetupFile().FileToByteArray());
            eventInformation.Attending.Should().Be(false);
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