using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Users;
using EvoEvents.Business.Users.Models;
using EvoEvents.Business.Users.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Api.Controllers.UserControllerTests
{
    [TestFixture]
    public class LoginTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private LoginRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new UserController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendLoginQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new UserInformation()));

            var result = await _controller.Login(_request);

            _mediator.Verify(m => m.Send(It.IsAny<LoginQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.Login(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new LoginRequest
            {
                Email = "asccc@hh.com",
                Password = "kila"
            };
        }
    }
}
