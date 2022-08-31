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

namespace EvoEvents.UnitTests.Business.Events.Extensions
{
    [TestFixture]
    public class GetNoAttendeesTests
    {
        private Event _event;

        [SetUp]
        public void Innit()
        {
            SetupEvent();
        }

        private void SetupEvent()
        {
            _event = new Event
            {
                Id = 1,
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
                Reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1,
                        UserId = 1,
                        EventId = 1
                    },
                    new Reservation
                    {
                        Id = 1,
                        UserId = 14,
                        EventId = 1,
                        AccompanyingPersonId = 1
                    }
                }
            };
        }

        [Test]
        public void ShouldReturnCorrectNoAttendees()
        {
            var CurretNoAttendees = _event.GetNoAttendees();

            CurretNoAttendees.Should().Be(3);
        }

        [Test]
        public void WhenNoReservationsExist_ShouldReturnZero()
        {
            _event.Reservations = new List<Reservation>();
            var CurretNoAttendees = _event.GetNoAttendees();

            CurretNoAttendees.Should().Be(0);
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