using Domain.UserAggregate;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IHandle<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public UpdateUserCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(UpdateUserCommand command)
        {
            User user = _getUserByLoginQuery.Execute(command.Login);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            user.UpdateUserProperties(command.Name, command.Gender, command.Birthday, command.ModifiedBy);
            _userRepository.Update(user);
            _userRepository.Save();
        }
    }
}
