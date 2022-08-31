using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Users;

namespace EvoEvents.Data.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int? AccompanyingPersonId { get; set; }

        public User User { get; set; }
        public Event Event { get; set; }
    }
}