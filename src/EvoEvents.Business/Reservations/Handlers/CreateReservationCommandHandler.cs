using EvoEvents.Business.Events;
using EvoEvents.Business.Reservations.Commands;
using EvoEvents.Business.Users;
using EvoEvents.Data;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
using EvoEvents.Data.Models.Users;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.Business.Reservations.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, bool>
    {
        public EvoEventsContext _context;
        private User _user;
        private User _accompanyingPerson;
        private Event _event;

        public CreateReservationCommandHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
        {
            ValidateReservation(command);

            await CreateReservation();

            return await _context.SaveChangesAsync() > 0;
        }

        private void ValidateReservation(CreateReservationCommand command)
        {
            ValidateEvent(command);
            ValidateCapacity(command);
            ValidateUser(command);
            ValidateAccompanyingPerson(command);
            IsUserAlreadyRegistered();
            IsAccompanyingPersonAlreadyRegistered();
        }

        private async Task CreateReservation()
        {
            await _context.Reservations.AddAsync(new Reservation
            {
                EventId = _event.Id,
                UserId = _user.Id,
                AccompanyingPersonId = _accompanyingPerson is null ? null : _accompanyingPerson.Id
            });
        }

        private void ValidateEvent(CreateReservationCommand command)
        {
            _event = _context.Events.Include(e => e.Reservations).SingleOrDefault(e => e.Id == command.EventId);

            if (_event is null)
            {
                throw new CustomException(ErrorCode.Event_NotFound, EventErrorMessage.EventNotFound);
            }
        }

        private void ValidateCapacity(CreateReservationCommand command)
        {
            var CurrentNoAttendees = _event.GetNoAttendees();
            var AttendeesToAdd = command.AccompanyingPersonEmail is null ? 1 : 2;

            if (CurrentNoAttendees + AttendeesToAdd > _event.MaxNoAttendees)
            {
                throw new CustomException(ErrorCode.Event_CapacityExceeded, EventErrorMessage.CapacityExceeded);
            }
        }

        private void ValidateUser(CreateReservationCommand command)
        {
            _user = _context.Users.FilterByEmail(command.UserEmail).FirstOrDefault();

            if (_user is null)
            {
                throw new CustomException(ErrorCode.User_NotFound, UserErrorMessage.UserNotFound);
            }
        }

        private void ValidateAccompanyingPerson(CreateReservationCommand command)
        {
            if (command.AccompanyingPersonEmail is null)
            {
                return;
            }
            _accompanyingPerson = _context.Users.FilterByEmail(command.AccompanyingPersonEmail).FirstOrDefault();

            if (_accompanyingPerson is null)
            {
                throw new CustomException(ErrorCode.User_NotFound, UserErrorMessage.AccompanyingPersonNotFound);
            }
        }

        private void IsUserAlreadyRegistered()
        {
            if (_event.Reservations.Any(r => r.UserId == _user.Id || r.AccompanyingPersonId == _user.Id))
            {
                throw new CustomException(ErrorCode.Reservation_UserAlreadyRegistered, ReservationErrorMessage.UserAlreadyRegistered);
            }
        }

        private void IsAccompanyingPersonAlreadyRegistered()
        {
            if (_accompanyingPerson is null)
            {
                return;
            }

            if (_event.Reservations.Any(r => r.AccompanyingPersonId == _accompanyingPerson.Id || r.UserId == _accompanyingPerson.Id))
            {
                throw new CustomException(ErrorCode.Reservation_AccompanyingPersonAlreadyRegistered, ReservationErrorMessage.AccompanyingPersonAlreadyRegistered);
            }
        }
    }
}