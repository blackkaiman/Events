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
    public class UpdateUserTests
    {
        private UserController _controller;
        private Mock<IMediator> _mediator;
        private UpdateUserRequest _request;

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
        public async Task ShouldSendUpdateUserCommand()
        {
            _mediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new bool()));

            var result = await _controller.UpdateUser(_request);

            _mediator.Verify(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.UpdateUser(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new UpdateUserRequest
            {
                Email = "maria234@yahoo.com",
                FirstName = "Maria",
                LastName = "Oltean",
                Company = "Evozon",
                OldPassword = "maria123",
                NewPassword = "maria1234"
            };
        }
    }
}