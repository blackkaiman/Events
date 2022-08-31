using System;

namespace EvoEvents.Business.Users.Models
{
    public record UserInformation
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string Company { get; init; }
    }
}
