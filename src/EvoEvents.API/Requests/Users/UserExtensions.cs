using EvoEvents.Business.Users.Commands;
using EvoEvents.Business.Users.Queries;

namespace EvoEvents.API.Requests.Users
{
    public static class UserExtensions
    {
        public static CreateUserCommand ToCommand(this CreateUserRequest request)
        {
            return new CreateUserCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                Company = request.Company
            };
        }

        public static UpdateUserCommand ToCommand(this UpdateUserRequest request)
        {
            return new UpdateUserCommand
            {
                Email = request.Email,
                Company = request.Company,
                FirstName = request.FirstName,
                LastName = request.LastName,
                OldPassword = request.OldPassword,
                NewPassword = request.NewPassword
            };
        }

        public static LoginQuery ToQuery(this LoginRequest request)
        {
            return new LoginQuery
            {
                Email = request.Email,
                Password = request.Password,
            };
        }
    }
}