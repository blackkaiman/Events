using EvoEvents.Business.Events;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using EvoEvents.Data.Models.Reservations;
using EvoEvents.Data.Models.Users;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EvoEvents.UnitTests.Business.Events.Extensions
{
    [TestFixture]
    public class FilterByUserAttendingTests
    {
        private IQueryable<Event> _events;

        [SetUp]
        public void Init()
        {
            SetupEvents();
        }

        [Test]
        public void ShouldReturnAttendingEvents()
        {
            var user = new User
            {
                Id = 1
            };

            bool attending = true;

            var events = _events.FilterByUserAttending(user, attending);

            Assert.That(events.Count(), Is.EqualTo(1));
            Assert.That(events.FirstOrDefault().Id, Is.EqualTo(1));
        }

        [Test]
        public void ShouldReturnUnattendingEvents()
        {
            var user = new User
            {
                Id = 1
            };
            bool attending = false;

            var events = _events.FilterByUserAttending(user, attending);

            Assert.That(events.Count(), Is.EqualTo(1));
            Assert.That(events.FirstOrDefault().Id, Is.EqualTo(2));
        }

        [Test]
        public void WhenUserIdIsNull_ShouldReturnAllEvents()
        {
            User user = null;
            bool attending = false;

            var events = _events.FilterByUserAttending(user, attending);

            Assert.That(events.Count(), Is.EqualTo(2));
        }

        private void SetupEvents()
        {
            var events = new List<Event>
            {
                new Event
                {
                    Id=1,
                    Name = "EvoEvent",
                    Description = "super",
                    EventTypeId = EventType.Concert,
                    MaxNoAttendees = 10,
                    Address = new Address
                    {
                        Location = "Strada Bisericii Sud",
                        CityId = City.Milano,
                        CountryId = Country.Italia
                    },
                    Reservations = new List<Reservation>()
                    {
                        new Reservation
                        {
                            Id = 1,
                            UserId = 1,
                            EventId = 1
                        }
                    }
                },
                new Event
                {
                    Id=2,
                    Name = "EvoEvent",
                    Description = "super",
                    EventTypeId = EventType.Concert,
                    MaxNoAttendees = 10,
                    Address = new Address
                    {
                        Location = "Strada Bisericii Sud",
                        CityId = City.Milano,
                        CountryId = Country.Italia
                    },
                    Reservations = new List<Reservation>()
                }
            };

            _events = events.AsQueryable();
        }
    }
}