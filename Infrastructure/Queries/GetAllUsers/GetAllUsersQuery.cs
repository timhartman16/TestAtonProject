using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IGetAllUsersQuery
    {
        private readonly IUserRepository _userRepository;
        
        public GetAllUsersQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Execute()
        {
            return _userRepository.Table
                                  .Where(x => x.RevokedOn == null)
                                  .OrderBy(x => x.CreatedOn)
                                  .ToList();
        }
    }
}
