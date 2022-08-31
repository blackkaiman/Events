using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Events.Queries;
using EvoEvents.Business.Shared;
using EvoEvents.Business.Shared.Models;
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
    public class ViewAllEventsQueryHandler : IRequestHandler<ViewAllEventsQuery, PageInfo<EventInformation>>
    {
        private readonly EvoEventsContext _context;
        private User _user;

        public ViewAllEventsQueryHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<PageInfo<EventInformation>> Handle(ViewAllEventsQuery query, CancellationToken cancellationToken)
        {
            ValidateUser(query.Email);

            var events = GetEvents();

            events = events
                .FilterByEventType(query.EventType)
                .FilterByUserAttending(_user, query.Attending);

            var data = await events
                .GetPage(query.PageNumber, query.ItemsPerPage)
                .ToEventInformation(150)
                .ToListAsync();

            var totalNoEvents = events.Count();

            return new PageInfo<EventInformation>(data, totalNoEvents);
        }

        private void ValidateUser(string email)
        {
            if (email is null)
            {
                _user = null;
                return;
            }

            _user = _context.Users.FilterByEmail(email).FirstOrDefault();

            if (_user is null)
            {
                throw new CustomException(ErrorCode.User_NotFound, UserErrorMessage.UserNotFound);
            }
        }

        private IQueryable<Event> GetEvents()
        {
            return _context.Events;
        }
    }
}