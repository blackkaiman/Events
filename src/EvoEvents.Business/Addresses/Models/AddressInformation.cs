using EvoEvents.Data.Models.Addresses;

namespace EvoEvents.Business.Addresses.Models
{
    public class AddressInformation
    {
        public string Location { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
    }
}