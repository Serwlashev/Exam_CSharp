using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Repository
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(DatabaseContext context) : base(context)
        {
        }
        public override void Update(Account item)
        {
            var srchItem = Get(item.Id);
            srchItem.Copy(item);
        }

    }
}
