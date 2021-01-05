using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext context;
        private DbSet<T> table;
        public BaseRepository(DatabaseContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public virtual void Create(T item)
        {
            table.Add(item);
        }

        public void Delete(int id)
        {
            var srchItem = Get(id);
            table.Remove(srchItem);
        }

        public T Get(int id)
        {
            return table.FirstOrDefault(new Func<T, bool>(item => (item as BaseEntity).Id == id));
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return table.Where(predicate).ToList();
        }
        public abstract void Update(T item);
    }
}
