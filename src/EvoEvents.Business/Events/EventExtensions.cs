using EvoEvents.Business.Addresses;
using EvoEvents.Business.Events.Commands;
using EvoEvents.Business.Events.Models;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EvoEvents.Business.Events
{
    public static class EventExtensions
    {
        public static Event ToEvent(this CreateEventCommand command)
        {
            return new Event
            {
                EventTypeId = command.EventType,
                Name = command.Name,
                Description = command.Description,
                MaxNoAttendees = command.MaxNoAttendees,
                Address = new Address
                {
                    CityId = command.City,
                    CountryId = command.Country,
                    Location = command.Location
                },
                FromDate = command.FromDate,
                ToDate = command.ToDate,
                Image = command.EventImage
            };
        }

        public static IQueryable<EventInformation> ToEventInformation(this IQueryable<Event> events)
        {
            return events.Include(e => e.Reservations).Select(e => new EventInformation
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                EventType = e.EventTypeId,
                MaxNoAttendees = e.MaxNoAttendees,
                CurrentNoAttendees = e.GetNoAttendees(),
                Address = e.Address.ToAddressInformation(),
                FromDate = e.FromDate,
                ToDate = e.ToDate,
                EventImage = e.Image
            });
        }

        public static EventInformation ToEventInformation(this Event eventToTransform)
        {
            return new EventInformation 
            {
                Id = eventToTransform.Id,
                Name = eventToTransform.Name,
                Description = eventToTransform.Description,
                EventType = eventToTransform.EventTypeId,
                MaxNoAttendees = eventToTransform.MaxNoAttendees,
                CurrentNoAttendees = eventToTransform.GetNoAttendees(),
                Address = eventToTransform.Address.ToAddressInformation(),
                FromDate = eventToTransform.FromDate,
                ToDate = eventToTransform.ToDate,
                EventImage = eventToTransform.Image
            };
        }

        public static IQueryable<EventInformation> ToEventInformation(this IQueryable<Event> events, int descriptionMaxLength)
        {
            return events.Include(e => e.Reservations).Select(e => new EventInformation
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description.Substring(0, descriptionMaxLength),
                EventType = e.EventType.Id,
                MaxNoAttendees = e.MaxNoAttendees,
                CurrentNoAttendees = e.GetNoAttendees(),
                Address = e.Address.ToAddressInformation(),
                FromDate = e.FromDate,
                ToDate = e.ToDate,
                EventImage = e.Image
            });
        }

        public static IQueryable<Event> FilterById(this IQueryable<Event> events, int id)
        {
            return events.Where(e => e.Id == id);
        }

        public static IQueryable<Event> FilterByEventType(this IQueryable<Event> events, EventType eventType)
        {
            return events.Where(e => eventType == EventType.None || e.EventTypeId == eventType);
        }

        public static IQueryable<Event> FilterByUserAttending(this IQueryable<Event> events, User user, bool attending)
        {
            return events.Where(e => user == null || 
                e.Reservations.Any(r => r.UserId == user.Id || r.AccompanyingPersonId == user.Id) == attending);
        }

        public static int GetNoAttendees(this Event eventToCheck)
        {
            return eventToCheck.Reservations.Count() + eventToCheck.Reservations.Count(e => e.AccompanyingPersonId is not null);
        }
    }
}