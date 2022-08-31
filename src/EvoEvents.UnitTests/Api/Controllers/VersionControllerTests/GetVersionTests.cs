using EvoEvents.API.Controllers;
using EvoEvents.API.Requests.Versions;
using EvoEvents.Business.Versions.Models;
using EvoEvents.Business.Versions.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.UnitTests.Api.Controllers.VersionControllerTests
{
    [TestFixture]
    public class GetVersionTests
    {
        private VersionController _controller;
        private Mock<IMediator> _mediator;
        private GetVersionRequest _request;

        [SetUp]
        public void Init()
        {
            _mediator = new Mock<IMediator>();

            _controller = new VersionController(_mediator.Object);

            CreateRequest();
        }

        [TearDown]
        public void Clean()
        {
            _controller = null;
        }

        [Test]
        public async Task ShouldSendGetVersionQuery()
        {
            _mediator.Setup(m => m.Send(It.IsAny<GetVersionQuery>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new VersionCode()));

            var result = await _controller.GetVersion(_request);

            _mediator.Verify(m => m.Send(It.IsAny<GetVersionQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task WhenRequestCompletes_ReturnStatusOk()
        {
            var result = await _controller.GetVersion(_request);

            result.Result.Should().BeOfType<OkObjectResult>();
        }

        private void CreateRequest()
        {
            _request = new GetVersionRequest { Name = "version1" };
        }
    }
}
