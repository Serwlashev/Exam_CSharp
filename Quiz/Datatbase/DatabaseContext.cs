using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users {get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Statistic> Statictics { get; set; }

        public DatabaseContext() : base("DefaultConnection") { }
    }
}
