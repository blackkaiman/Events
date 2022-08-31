namespace Infrastructure.Utilities.Errors.ErrorMessages
{
    public static class ReservationErrorMessage
    {
        public static readonly string UserAlreadyRegistered = "User is already registered to that event";
        public static readonly string DuplicateEmail = "User and accompanying person email should not be equal";
        public static readonly string ReservationNotFound = "Reservation was not found";
        public static readonly string AccompanyingPersonAlreadyRegistered = "Accompanying person is already registered to this event";
        public static readonly string CannotUnregisterToPastEvents = "You can not unregister to past events";
    }
}