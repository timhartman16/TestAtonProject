using Domain.UserAggregate;
using Infrastructure.Commands.UpdateUserPassword;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUserLogin
{
    public class UpdateUserLoginCommandHandler : IHandle<UpdateUserLoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public UpdateUserLoginCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(UpdateUserLoginCommand command)
        {
            User user = _getUserByLoginQuery.Execute(command.Login);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            User user1 = _getUserByLoginQuery.Execute(command.NewLogin);
            if (user1 == null)
            {
                user.UpdateUserLogin(command.NewLogin, command.ModifiedBy);
                _userRepository.Update(user);
                _userRepository.Save();
            }
            else
            {
                throw new HttpErrorException(403, ErrorText.UserAlreadyExists);
            }
        }
    }
}
