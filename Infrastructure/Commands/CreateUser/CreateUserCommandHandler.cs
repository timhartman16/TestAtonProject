using Domain.UserAggregate;
using Infrastructure.Exceptions;
using Infrastructure.Hash;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.CreateUser
{
    public class CreateUserCommandHandler : IHandle<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public CreateUserCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(CreateUserCommand command)
        {
            User usr = _getUserByLoginQuery.Execute(command.Login);
            if (usr != null)
                throw new HttpErrorException(403, ErrorText.UserAlreadyExists);

            User user = new User(Guid.NewGuid(), command.Login, HashConverter.ConvertPasswordToHash(command.Password), 
                                 command.Name, command.Gender, command.Birthday, command.IsAdmin,
                                 command.CreatedOn, command.CreatedBy);
            _userRepository.Create(user);
            _userRepository.Save();
        }
    }
}
