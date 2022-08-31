using EvoEvents.Business.Users.Models;
using EvoEvents.Business.Users.Queries;
using EvoEvents.Data;
using Infrastructure.Utilities.CustomException;
using Infrastructure.Utilities.Errors;
using Infrastructure.Utilities.Errors.ErrorMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EvoEvents.Business.Users.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserInformation>
    {
        private readonly EvoEventsContext _context;

        public LoginQueryHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<UserInformation> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var userInformation = await GetUserInformation(query);

            ValidateUserInformation(userInformation);

            return userInformation;
        }

        private async Task<UserInformation> GetUserInformation(LoginQuery query)
        {
            return await _context.Users
                .FilterByEmail(query.Email)
                .FilterByPassword(query.Password)
                .ToUserInformation()
                .FirstOrDefaultAsync();
        }

        private static void ValidateUserInformation(UserInformation userInformation)
        {
            if (userInformation == null)
            {
                throw new CustomException(ErrorCode.User_WrongCredentials, UserErrorMessage.WrongCredentials);
            }
        }
    }
}
