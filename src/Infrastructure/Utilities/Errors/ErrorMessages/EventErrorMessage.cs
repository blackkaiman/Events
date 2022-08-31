namespace Infrastructure.Utilities.Errors.ErrorMessages
{
    public static class EventErrorMessage
    {
        public static readonly string NameFormat = "Name should have between 2 and 100 alphanumeric characters";

        public static readonly string DescriptionFormat = "Description should have maximum 2000 alphanumeric characters";

        public static readonly string EventTypeNull = "Event type can't be null";

        public static readonly string MaxNoAttendeesFormat = "Max number of attendees should be an integer between 1 and 100.000";

        public static readonly string EventNotFound = "Event with that id does not exist";

        public static readonly string FromDateValue = "Starting Date can't be less than today's Date";
        
        public static readonly string CapacityExceeded = "There are no more available places for this event";

        public static readonly string FileSizeTooLarge = "File size is larger than allowed";

        public static readonly string InvalidFileType = "File sent was not an image";

        public static readonly string ToDateValue = "Ending Date can't be less then today's Date";

        public static readonly string FromDateNull = "Starting Date must exist";

        public static readonly string ToDateNull = "Ending Date must exist";

        public static readonly string FromDateGraterThenToDate = "Ending date can't be before the starting date";

        public static readonly string EventAlreadyCreated = "This event was already created";
        
    }
}