using Domain.UserAggregate;
using Infrastructure.Commands.UpdateUser;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetUserByLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands.DeleteUserByLogin
{
    public class DeleteUserByLoginCommandHandler : IHandle<DeleteUserByLoginCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;

        public DeleteUserByLoginCommandHandler(IUserRepository userRepository, IGetUserByLoginQuery getUserByLoginQuery)
        {
            _userRepository = userRepository;
            _getUserByLoginQuery = getUserByLoginQuery;
        }

        public void Handle(DeleteUserByLoginCommand command)
        {
            User user = _getUserByLoginQuery.Execute(command.Login);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);
            if (command.IsSoftDelete)
                user.SoftDelete(command.RevokedByLogin);
            else
                _userRepository.Delete(command.Login);
            _userRepository.Save();
        }
    }
}
