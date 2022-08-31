using EvoEvents.Business.Reservations.Commands;
using EvoEvents.Business.Users;
using EvoEvents.Data;
using EvoEvents.Data.Models.Events;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.Business.Reservations.Handlers
{
    public class UnregisterUserCommandHandler : IRequestHandler<UnregisterUserCommand, bool>
    {
        public EvoEventsContext _context;
        private Event _event;

        public UnregisterUserCommandHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UnregisterUserCommand request, CancellationToken cancellationToken)
        {
            ValidateEvent(request);
            ValidateUser(request);
            DeleteUser(request);

            return await _context.SaveChangesAsync() > 0;
        }

        public void DeleteUser (UnregisterUserCommand command)
        {
            var userId = _context.Users
                .FilterByEmail(command.UserEmail)
                .Select(u => u.Id)
                .FirstOrDefault();

            var reservation = _event.Reservations.Where(r => r.UserId == userId || r.AccompanyingPersonId == userId).SingleOrDefault();

            if (reservation is null)
            {
                throw new CustomException(ErrorCode.Reservation_NotFound, ReservationErrorMessage.ReservationNotFound);
            }

            if(reservation.AccompanyingPersonId == userId)
            {
                reservation.AccompanyingPersonId = null;
                _context.Reservations.Update(reservation);
            }
            else 
            {
                _context.Reservations.Remove(reservation);
            }
        }

        private void ValidateEvent(UnregisterUserCommand command)
        {
            _event = _context.Events.Include(e => e.Reservations).SingleOrDefault(e => e.Id == command.EventId);

            if (_event is null)
            {
                throw new CustomException(ErrorCode.Event_NotFound, EventErrorMessage.EventNotFound);
            }
            if (_event.ToDate  < DateTime.UtcNow)
            {
                throw new CustomException(ErrorCode.Reservation_CannotUnregisterToPastEvents, ReservationErrorMessage.CannotUnregisterToPastEvents);
            }
        }

        private void ValidateUser(UnregisterUserCommand command)
        {
            var _user = _context.Users.FilterByEmail(command.UserEmail).FirstOrDefault();

            if (_user is null)
            {
                throw new CustomException(ErrorCode.User_NotFound, UserErrorMessage.UserNotFound);
            }
        }
    }
}