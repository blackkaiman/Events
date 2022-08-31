using EvoEvents.Business.Events.Models;
using EvoEvents.Business.Shared.Models;
using EvoEvents.Data.Models.Events;
using MediatR;

namespace EvoEvents.Business.Events.Queries
{
    public class ViewAllEventsQuery : IRequest<PageInfo<EventInformation>>
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public string Email { get; set; }
        public bool Attending { get; set; }
        public EventType EventType { get; set; }
    }
}