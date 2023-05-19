using Domain.UserAggregate;
using Infrastructure.Commands.UpdateUser;
using Infrastructure.Exceptions;
using Infrastructure.Hash;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IHandle<UpdateUserPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public UpdateUserPasswordCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(UpdateUserPasswordCommand command)
        {
            User user = _getUserByLoginQuery.Execute(command.Login);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            user.UpdateUserPassword(HashConverter.ConvertPasswordToHash(command.NewPassword), command.ModifiedBy);
            _userRepository.Update(user);
            _userRepository.Save();
        }
    }
}
