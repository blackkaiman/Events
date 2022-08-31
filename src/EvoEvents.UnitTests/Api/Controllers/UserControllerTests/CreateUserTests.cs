using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Users;
using EvoEvents.Business.Users.Commands;
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
    public class CreateUserTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private CreateUserRequest _request;

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
        public async Task ShouldSendCreateUserCommand()
        {
            _mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.CreateUser(_request);

            _mediator.Verify(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.CreateUser(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new CreateUserRequest {
                FirstName = "Radu",
                LastName = "mifdas",
                Company = "fdasfa",
                Email = "asccc@hh.com",
                Password = "kila"
            };
        }
    }
}
