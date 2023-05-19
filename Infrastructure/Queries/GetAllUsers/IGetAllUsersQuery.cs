using Domain.UserAggregate;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.Queries.GetAllUsers
{
    public interface IGetAllUsersQuery : IQuery<IEnumerable<User>>
    {
    }
}
