using Domain.UserAggregate;
using Infrastructure.Commands.UpdateUserLogin;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.RestoreUser
{
    public class RestoreUserCommandHandler : IHandle<RestoreUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public RestoreUserCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(RestoreUserCommand command)
        {
            User user = _userRepository.Table
                                       .FirstOrDefault(x => x.Login == command.Login);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            user.RestoreUser(command.ModifiedBy);
            _userRepository.Save();
        }
    }
}
