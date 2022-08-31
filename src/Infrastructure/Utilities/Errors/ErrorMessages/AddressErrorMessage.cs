namespace Infrastructure.Utilities.Errors.ErrorMessages
{
    public static class AddressErrorMessage
    {
        public static readonly string LocationFormat = "Address location should have between 10 and 50 characters";
        public static readonly string CityNull = "Address city can't be null";
        public static readonly string CountryNull = "Address country can't be null";
    }
}