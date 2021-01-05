using Quiz.Datatbase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        private const int MAX_NUM_QUESTION = 500;
        private const int MAX_NUM_ANSWERS = 4;

        protected override void Seed(DatabaseContext context)
        {
            base.Seed(context);
            Random rnd = new Random();

            LoadAccounts(context);
            LoadUsers(context);
            List<Category> categories = new List<Category>
            {
            new Category { Name = "History" },
            new Category { Name = "Biology" },
            new Category { Name = "Geography" },
            new Category { Name = "Math" },
            new Category { Name = "Physic" },
            new Category { Name = "Music" },
            new Category { Name = "Sport" },
            new Category { Name = "Languages" },
            new Category { Name = "Science" },
            new Category { Name = "Art" },
            new Category { Name = "Random questions" }
            };
            foreach (var cat in categories)
            {
                context.Categories.Add(cat);
            }

            int countRightAnswer = 0;
            int randCategory;
            for (int i = 0; i < MAX_NUM_QUESTION; i++)
            {
                randCategory = rnd.Next(0, 10);
                context.Questions.Add(new Question
                {
                    CategoryId = randCategory,
                    Text = $"This is question with number {i + 1} from the category {categories[randCategory].Name}"
                });
                for (int j = 0; j < MAX_NUM_ANSWERS; j++)
                {
                    Answer answer = new Answer 
                    {
                        QuestionId = i,
                        Text = $"This is an answer {j + 1} for the question with number {i}",
                        IsCorrect = rnd.Next(0, 2) > 0
                    };
                    if (answer.IsCorrect)
                    {
                        countRightAnswer++;
                    }

                    if(j == 3 && countRightAnswer == 0) 
                    {
                        answer.IsCorrect = true;
                    }

                    if(j == 3 && countRightAnswer == 4)
                    {
                        answer.IsCorrect = false;
                    }
                    context.Answers.Add(answer);
                }
                countRightAnswer = 0;
            }
            LoadStatistic(context);

            context.SaveChanges();
        }
        private void LoadAccounts(DatabaseContext context)
        {
            List<Account> accounts = new List<Account> {
                new Account() {Login = "Marija", Password = "12345678"},
                new Account() {Login = "Vasilij", Password = "11111111"},
                new Account() {Login = "pokopjev", Password = "12121212"},
                new Account() {Login = "bobMarley", Password = "33333333"},
                new Account() {Login = "valya12", Password = "45454545"},
                new Account() {Login = "IgorPetrov", Password = "45645646"},
                new Account() {Login = "illya1", Password = "78787878"},
                new Account() {Login = "billGates", Password = "99999999"},
                new Account() {Login = "gregVine", Password = "88888888"},
                new Account() {Login = "Marta90", Password = "66335544"}

            };

            foreach (var acc in accounts)
            {
                context.Accounts.Add(acc);
            }

            context.SaveChanges();
        }

        private void LoadUsers(DatabaseContext context)
        {
            List<User> users = new List<User>
            {
                new User(){ AccountId = 1, FirstName = "Masha", LastName = "Ivanova", Birth = new DateTime(1990, 12, 8)},
                new User(){ AccountId = 2, FirstName = "Vasya", LastName = "Petrov", Birth = new DateTime(1985, 5, 22)},
                new User(){ AccountId = 3, FirstName = "Petr", LastName = "Prokopjev", Birth = new DateTime(1995, 4, 13)},
                new User(){ AccountId = 4, FirstName = "Bob", LastName = "Marley", Birth = new DateTime(2000, 7, 17)},
                new User(){ AccountId = 5, FirstName = "Valya", LastName = "Smirnova", Birth = new DateTime(1977, 6, 20)},
                new User(){ AccountId = 6, FirstName = "Igor", LastName = "Petrov", Birth = new DateTime(1966, 3, 27)},
                new User(){ AccountId = 7, FirstName = "Illya", LastName = "Sotnik", Birth = new DateTime(1999, 1, 1)},
                new User(){ AccountId = 8, FirstName = "Bill", LastName = "Gates", Birth = new DateTime(1966, 2, 5)},
                new User(){ AccountId = 9, FirstName = "Greg", LastName = "Piters", Birth = new DateTime(1982, 11, 14)},
                new User(){ AccountId = 10, FirstName = "Marta", LastName = "Smith", Birth = new DateTime(1995, 10, 22)},

            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
        private void LoadStatistic(DatabaseContext context)
        {
            Random rnd = new Random();
            for(int i = 0; i < 100; i++)
            {
                context.Statictics.Add(
                    new Statistic()
                    {
                        CategoryId = rnd.Next(1, 12),
                        UserId = rnd.Next(1, 11),
                        NumCorrectAnswer = rnd.Next(0, 20)
                    });
            }
        }
    }
}
