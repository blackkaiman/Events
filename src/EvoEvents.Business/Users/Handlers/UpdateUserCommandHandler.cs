using EvoEvents.Business.Users.Commands;
using EvoEvents.Data;
using EvoEvents.Data.Models.Users;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.Business.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly EvoEventsContext _context;

        public UpdateUserCommandHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = GetUser(command);
            ValidateUserInformation(user);
            UpdateUser(command, user);

            return await _context.SaveChangesAsync() > 0;
        }

        private User GetUser(UpdateUserCommand command)
        {
            return _context.Users
                .Include(u => u.Information) 
                .FirstOrDefault(u => u.Email == command.Email && ((command.OldPassword != null) ? u.Password == command.OldPassword : true));
        }

        private void UpdateUser(UpdateUserCommand request, User user)
        {
            user.Password = (request.OldPassword is not null) ? request.NewPassword : user.Password;
            user.Information = new UserDetail
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Company = request.Company
            };
        }

        private void ValidateUserInformation(User user)
        {
            if (user == null)
            {
                throw new CustomException(ErrorCode.User_WrongCredentials, UserErrorMessage.WrongCredentials);
            }
        }
    }
}