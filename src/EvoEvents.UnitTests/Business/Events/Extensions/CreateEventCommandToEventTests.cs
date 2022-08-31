using EvoEvents.API.Requests.Events;
using EvoEvents.Business.Events;
using EvoEvents.Business.Events.Commands;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace EvoEvents.UnitTests.Business.Events.Extensions
{
    [TestFixture]
    public class CreateUserCommandToUserTest
    {
        [Test]
        public void ShouldReturnCorrectCreateUserCommand()
        {
            var request = new CreateEventCommand
            {
                Name = "EvoEvent",
                Description = "foarte fain",
                EventType = (EventType)2,
                MaxNoAttendees = 10,
                Location = "Strada Bisericii Sud",
                City = City.Milano,
                Country = Country.Italia,
                FromDate = DateTime.Now.AddDays(1),
                ToDate = DateTime.Now.AddDays(2),
                EventImage = SetupFile().FileToByteArray()
            };

            var result = request.ToEvent();

            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.EventTypeId.Should().Be(request.EventType);
            result.MaxNoAttendees.Should().Be(request.MaxNoAttendees);
            result.Address.Location.Should().Be(request.Location);
            result.Address.CountryId.Should().Be(request.Country);
            result.Address.CityId.Should().Be(request.City);
            result.FromDate.Should().Be(request.FromDate);
            result.ToDate.Should().Be(request.ToDate);
            result.Image.Should().Equal(SetupFile().FileToByteArray());
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