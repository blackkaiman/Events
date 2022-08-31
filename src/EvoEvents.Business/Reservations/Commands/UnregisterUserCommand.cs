using MediatR;

namespace EvoEvents.Business.Reservations.Commands
{
    public class UnregisterUserCommand : IRequest<bool>
    {
        public int EventId { get; set; }
        public string UserEmail { get; set; }
    }
}