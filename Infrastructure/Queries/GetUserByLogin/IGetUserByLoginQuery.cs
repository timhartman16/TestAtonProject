using Domain.UserAggregate;
using Infrastructure.Interfaces;

namespace Infrastructure.Queries.GetUserByLogin
{
    public interface IGetUserByLoginQuery : IQuery<string, User>
    {
    }
}
