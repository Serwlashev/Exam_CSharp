using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        T Get(int id);
        void Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}
