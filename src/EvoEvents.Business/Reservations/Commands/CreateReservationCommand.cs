using MediatR;

namespace EvoEvents.Business.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public int EventId { get; set; }
        public string AccompanyingPersonEmail { get; set; }
    }
}