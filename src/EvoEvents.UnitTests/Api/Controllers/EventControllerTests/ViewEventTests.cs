using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Events;
using EvoEvents.API.Requests.Reservations;
using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Events.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Api.Controllers.EventControllerTests
{
    [TestFixture]
    public class ViewEventTests
    {
        private EventController _controller;
        private Mock<IMediator> _mediator;
        private ViewEventRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new EventController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendViewEventQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ViewEventQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new EventInformation()));

            var result = await _controller.ViewEvent(_request);

            _mediator.Verify(m => m.Send(It.IsAny<ViewEventQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.ViewEvent(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new ViewEventRequest
            {
                Id = 1,
                EmailModel = new EmailModel
                {
                    UserEmail = "paulac@yahoo.com"
                }
            };
        }
    }
}