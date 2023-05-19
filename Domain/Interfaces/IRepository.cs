using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        IEnumerable<T> GetAll();
        void Create(T item);
        void Update(T item);
        void Delete(string login);
        void Save();
    }
}
