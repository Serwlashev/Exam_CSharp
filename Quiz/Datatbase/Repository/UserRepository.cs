using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
        public override void Update(User item)
        {
            var srchItem = Get(item.Id);
            srchItem.Copy(item);
        }
    }
}
