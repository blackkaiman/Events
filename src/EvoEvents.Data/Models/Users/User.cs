using System;

namespace EvoEvents.Data.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }

        public UserDetail Information { get; set; }
    }
}