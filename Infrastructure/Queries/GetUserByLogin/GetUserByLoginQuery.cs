using Domain.UserAggregate;
using Infrastructure.Exceptions;
using System.Linq;

namespace Infrastructure.Queries.GetUserByLogin
{
    public class GetUserByLoginQuery : IGetUserByLoginQuery
    {
        private readonly IUserRepository _userRepository;

        public GetUserByLoginQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Execute(string login)
        {
            return _userRepository.Table
                                  .Where(x => x.RevokedOn == null)
                                  .FirstOrDefault(x => x.Login == login);
        }
    }
}
