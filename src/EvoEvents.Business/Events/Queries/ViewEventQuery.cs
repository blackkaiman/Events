using EvoEvents.Business.Events.Models;
using MediatR;

namespace EvoEvents.Business.Events.Queries
{
    public class ViewEventQuery : IRequest<EventInformation>
    {
        public int Id { get; set; }
        public string UserEmail{ get; set; }    
    }
}