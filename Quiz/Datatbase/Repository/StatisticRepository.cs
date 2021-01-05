using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Quiz.Datatbase.Repository
{
    public class StatisticRepository : BaseRepository<Statistic>
    {
        public StatisticRepository(DatabaseContext context) : base(context)
        {
        }
        
        public override void Update(Statistic item)
        {
            throw new NotImplementedException();
        }
        public List<Statistic> GetAllEager()
        {
            return context.Statictics.Include(stat => stat.User).Include(stat => stat.Category).OrderByDescending(stat => stat.NumCorrectAnswer).ToList();
        }

        public List<Statistic> GetAllEager(Func<Statistic, bool> predicate)
        {
            return context.Statictics.Include(stat => stat.User).Include(stat => stat.Category).Where(predicate).OrderByDescending(stat => stat.NumCorrectAnswer).ToList();
        }
    }
}
