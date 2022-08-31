using EvoEvents.Business.Events;
using EvoEvents.Data.Models.Addresses;
using EvoEvents.Data.Models.Events;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace EvoEvents.UnitTests.Business.Events.Extensions
{
    [TestFixture]
    public class FilterByIdTests
    {
        private IQueryable<Event> _events;

        [SetUp]
        public void Init()
        {
            SetupEvents();
        }

        [Test]
        public void ShouldReturnFilteredQueryable()
        {
            int id = 1;
            var events = _events.FilterById(id);

            Assert.That(events.Count(), Is.EqualTo(1));
            Assert.That(events.FirstOrDefault().Id, Is.EqualTo(id));
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
                    }
                }
            };

            _events = events.AsQueryable();
        }
    }
}