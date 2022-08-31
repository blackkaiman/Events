using EvoEvents.Business.Users.Commands;
using EvoEvents.Business.Users.Models;
using EvoEvents.Data.Models.Users;
using System;
using System.Linq;

namespace EvoEvents.Business.Users
{
    public static class UserExtensions
    {
        public static User ToUser(this CreateUserCommand command)
        {
            return new User
            {
                Email = command.Email,
                Password = command.Password,
                Created = DateTime.Now,

                Information = new UserDetail
                {
                    LastName = command.LastName,
                    FirstName = command.FirstName,
                    Company = command.Company
                }
            };
        }

        public static IQueryable<UserInformation> ToUserInformation(this IQueryable<User> user)
        {
            return user.Select(u => new UserInformation
            {
                Email = u.Email,
                FirstName = u.Information.FirstName,
                LastName = u.Information.LastName,
                Company = u.Information.Company
            });
        }

        public static IQueryable<User> FilterByEmail(this IQueryable<User> users, string email)
        {
            return users.Where(u => u.Email == email);
        }

        public static IQueryable<User> FilterByPassword(this IQueryable<User> users, string password)
        {
            return users.Where(u => u.Password == password);
        }
    }
}
