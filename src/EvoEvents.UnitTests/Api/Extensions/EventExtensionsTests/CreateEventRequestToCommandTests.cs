using EvoEvents.API.Requests.Events;
using EvoEvents.API.Shared.Models;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace EvoEvents.UnitTests.Api.Extensions.EventExtensionsTests
{
    [TestFixture]
    public class CreateEventRequestToCommandTests
    {
        [Test]
        public void ShouldReturnCorrectCreateEventCommand()
        {
            var request = new CreateEventRequest
            {
                Name = "EvoEvents",
                Description = "fain",
                EventType = (EventType)2,
                MaxNoAttendees = 15,
                DateRangeModel = new DateRangeModel
                {
                    FromDate = DateTime.UtcNow.AddDays(1),
                    ToDate = DateTime.UtcNow.AddDays(2)
                },
                City = (City)1,
                Country = (Country)1,
                EventImage = SetupFile()
            };

            var result = request.ToCommand();

            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.EventType.Should().Be(request.EventType);
            result.MaxNoAttendees.Should().Be(request.MaxNoAttendees);
            result.FromDate.Should().Be(request.DateRangeModel.FromDate);
            result.ToDate.Should().Be(request.DateRangeModel.ToDate);
            result.EventImage.Should().Equal(request.EventImage.FileToByteArray());
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