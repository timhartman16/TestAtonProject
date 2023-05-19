using Domain.UserAggregate;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries.GetElderUsers
{
    public interface IGetElderUsersQuery : IQuery<int, IEnumerable<User>>
    {
    }
}
