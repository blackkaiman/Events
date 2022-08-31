namespace EvoEvents.Data.Models.Addresses
{
    public class Address
    {
        public int Id { get; set; } 
        public int EventId { get; set; }
        public string Location { get; set; }    
        public City CityId { get; set; }
        public Country CountryId { get; set; }

        public virtual CountryLookup Country { get; set; }    
        public virtual CityLookup City { get; set; }
    }
}