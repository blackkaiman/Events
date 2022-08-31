using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoEvents.Business.Users.Commands
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string Email { get; set; }   
        public string Company { get; set; }
        public string FirstName{ get; set; } 
        public string LastName{ get; set; } 
        public string OldPassword{ get; set; } 
        public string NewPassword { get; set; } 
    }
}