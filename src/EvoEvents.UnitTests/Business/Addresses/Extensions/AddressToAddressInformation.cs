using EvoEvents.Business.Addresses;
using EvoEvents.Business.Addresses.Models;
using EvoEvents.Data.Models.Addresses;
using FluentAssertions;
using NUnit.Framework;

namespace EvoEvents.UnitTests.Business.Addresses.Extensions
{
    [TestFixture]
    public class AddressToAddressInformationTests
    {
        private Address _address;

        [SetUp]
        public void Innit()
        {
            SetupAddress();
        }

        private void SetupAddress()
        {
            _address = new Address
            {
                Location = "Strada Bisericii Sud",
                CityId = City.Milano,
                CountryId = Country.Italia
            };
        }
        [Test]
        public void ShouldReturnCorrectAddressInformation()
        {
            var addressInformation = _address.ToAddressInformation();
            
            addressInformation.Should().BeEquivalentTo(
                new AddressInformation
                {
                    Location = _address.Location,
                    City = _address.CityId,
                    Country = _address.CountryId
                });
        }
    }
}