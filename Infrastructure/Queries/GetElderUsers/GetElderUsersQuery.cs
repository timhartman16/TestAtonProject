using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries.GetElderUsers
{
    public class GetElderUsersQuery : IGetElderUsersQuery
    {
        private readonly IUserRepository _userRepository;

        public GetElderUsersQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> Execute(int criteria)
        {
            return _userRepository.Table
                                  .Where(x => x.Birthday != null)
                                  .Where(x => x.Birthday.Value < DateTime.Now.AddYears((-1) * criteria))
                                  .ToList();
        }
    }
}
