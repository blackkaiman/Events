using EvoEvents.API.Requests.Events;
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
    public class ViewAllEventsTruncateDescriptionTests
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
                    Description = "A wonderful serenity has taken possession of my entire soul, " +
                    "like these sweet mornings of spring which " +
                    "I enjoy with my whole heart. I am alone, and " +
                    "feel the charm of existence in this spot, which was.",
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
                    Image = SetupFile().FileToByteArray(),
                    Reservations = new List<Reservation>()     
                }
            };

            _events = events.AsQueryable();
        }

        [Test]
        public void ShouldReturnCorrectTruncatedDescription()
        {
            var events =  _events.ToEventInformation(150);
            
            events.First().Description.Should().Be("A wonderful serenity has taken possession of my entire soul, " +
                "like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and fe");
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