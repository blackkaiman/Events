using System.Threading.Tasks;
using EvoEvents.API.Requests.Events;
using EvoEvents.API.Requests.Events.Reservations;
using EvoEvents.API.Requests.Reservations;
using EvoEvents.API.Requests.Users;
using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EvoEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-event")]
        public async Task<ActionResult<bool>> CreateEvent([FromForm] CreateEventRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<EventInformation>> ViewEvent(ViewEventRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [HttpPost("view-all-events")]
        public async Task<ActionResult<PageInfo<EventInformation>>> ViewAllEvents([FromBody] ViewAllEventsRequest request)
        {
            var result = await _mediator.Send(request.ToQuery());

            return Ok(result);
        }

        [HttpPost("{eventid}/register")]
        public async Task<ActionResult<bool>> RegisterEvent(CreateReservationRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }

        [HttpPost("{eventid}/unregister")]
        public async Task<ActionResult<bool>> UnregisterUser(UnregisterUserRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());

            return Ok(result);
        }
    }
}