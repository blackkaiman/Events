using EvoEvents.Business.Addresses.Models;
using EvoEvents.Data.Models.Addresses;

namespace EvoEvents.Business.Addresses
{
    public static class AddressExtensions
    {
        public static AddressInformation ToAddressInformation(this Address address)
        {
            return new AddressInformation
            {
                Location = address.Location,
                City = address.CityId,
                Country = address.CountryId
            };
        }
    }
}