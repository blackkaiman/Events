using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Reservations;
using EvoEvents.Business.Reservations.Commands;
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
    public class UnregisterUserTests
    {
        private EventController _controller;
        private Mock<IMediator> _mediator;
        private UnregisterUserRequest _request;

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
        public async Task ShouldSendUnregisterUserCommand()
        {
            _mediator.Setup(m => m.Send(It.IsAny<UnregisterUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.UnregisterUser(_request);

            _mediator.Verify(m => m.Send(It.IsAny<UnregisterUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.UnregisterUser(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new UnregisterUserRequest
            {
                EventId = 2,
                EmailModel = new EmailModel
                {
                    UserEmail = "adelap@yahoo.com",
                }
            };
        }
    }
}