using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Events;
using EvoEvents.API.Shared.Models;
using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Events.Queries;
using EvoEvents.Business.Shared.Models;
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
    public class ViewAllEventsTests
    {
        private EventController _controller;
        private Mock<IMediator> _mediator;
        private ViewAllEventsRequest _request;

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
        public async Task ShouldGetEventsQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<ViewAllEventsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new PageInfo<EventInformation>()));

            var result = await _controller.ViewAllEvents(_request);

            _mediator.Verify(m => m.Send(It.IsAny<ViewAllEventsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.ViewAllEvents(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new ViewAllEventsRequest
            {
                PaginationModel = new PaginationModel
                {
                    PageNumber = 1,
                    ItemsPerPage = 1
                }
            };
        }
    }
}