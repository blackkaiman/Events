namespace Infrastructure.Utilities.Errors
{
    public enum ErrorCode
    {
        User_UniqueEmail = 100,
        User_WrongCredentials = 101,
        User_NotFound = 102,

        Event_NotFound = 200,
        Event_AlreadyCreated = 201,
        Event_CapacityExceeded = 202,

        Reservation_UserAlreadyRegistered = 300,
        Reservation_NotFound = 301,
        Reservation_CannotUnregisterToPastEvents = 303,
        Reservation_AccompanyingPersonAlreadyRegistered = 302
    }
}