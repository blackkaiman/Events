using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Events.Queries;
using EvoEvents.Business.Users;
using EvoEvents.Data;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Users;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.Business.Events.Handlers
{
    public class ViewEventQueryHandler : IRequestHandler<ViewEventQuery, EventInformation>
    {
        private readonly EvoEventsContext _context;
        private User _user;

        public ViewEventQueryHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<EventInformation> Handle(ViewEventQuery query, CancellationToken cancellationToken)
        {
            ValidateUser(query.UserEmail);
            var _event = await GetEvent(query);

            ValidateEventId(_event);
            var eventInformation = _event.ToEventInformation();
            eventInformation.Attending = IsRegistered(_event);

            return eventInformation;
        }

        private void ValidateUser(string email)
        {
            _user = _context.Users.FilterByEmail(email).FirstOrDefault();

            if (_user is null)
            {
                throw new CustomException(ErrorCode.User_NotFound, UserErrorMessage.UserNotFound);
            }
        }

        private async Task<Event> GetEvent(ViewEventQuery query)
        {
            return await _context.Events
                .Include(e => e.Reservations)
                .Include(e => e.Address)
                .FilterById(query.Id)
                .FirstOrDefaultAsync();
        }

        private static void ValidateEventId(Event _event)
        {
            if (_event == null)
            {
                throw new CustomException(ErrorCode.Event_NotFound, EventErrorMessage.EventNotFound);
            }
        }

        private bool IsRegistered(Event _event)
        {
            return _event.Reservations.Any(r => r.UserId == _user.Id || r.AccompanyingPersonId == _user.Id);
        }
    }
}