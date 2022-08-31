using EvoEvents.Business.Users.Models;
using MediatR;

namespace EvoEvents.Business.Users.Queries
{
    public class LoginQuery : IRequest<UserInformation>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
