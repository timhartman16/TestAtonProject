using Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestAtonDbContext _db;

        public IQueryable<User> Table => _db.User;

        public UserRepository(TestAtonDbContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return _db.User.ToList();
        }

        public void Create(User item)
        {
            _db.User.Add(item);     
        }

        public void Update(User item)
        {
            _db.User.Update(item);
        }

        public void Delete(string login)
        {
            var user = _db.User.FirstOrDefault(x => x.Login == login);
            if (user != null)
            {
                _db.User.Remove(user);
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
