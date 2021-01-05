using Quiz.Datatbase;
using Quiz.Datatbase.Entities;
using Quiz.Datatbase.EntityValidator;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Models.Console;
using Quiz.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.StatisticControl
{
    // This class works with all playing statistics
    class StatisticsManager
    {
        private UnitOfWork repository;
        private CategoryStorage categoryStorage;
        public StatisticsManager(UnitOfWork repos)
        {
            repository = repos;
            categoryStorage = new CategoryStorage(repos);
        }

        public void AddStatistic(int userId, int categoryId, int numCorrectAnswers)
        {
            Datatbase.Entities.Statistic stat = new Datatbase.Entities.Statistic() { UserId = userId, CategoryId = categoryId, NumCorrectAnswer = numCorrectAnswers };
            if (EntityValidator.Validate(stat))
            {
                repository.StatisticRepository.Create(stat);
                repository.Save();
            }
        }

        public void ShowAllStatistic(int userId)
        {
            List<Statistic> statistics = repository.StatisticRepository.GetAllEager();

            PrintStatistic(statistics);
        }

        public void ShowTop20ByCategory()
        {
            int categoryId = categoryStorage.ChooseCategory();
            List<Statistic> statistics = repository.StatisticRepository.GetAllEager(stat => stat.CategoryId == categoryId).Take(20).ToList();

            PrintStatistic(statistics);
        }

        public void ShowUserStatistic(int userId)
        {
            List<Statistic> statistics = repository.StatisticRepository.GetAllEager(stat => stat.UserId == userId);

            PrintStatistic(statistics);
        }

        private void PrintStatistic(List<Statistic> statistics)
        {
            int counter = 1;

            ConsoleManager.PrintMessage("\n\tStatistics\n");
            foreach (var stat in statistics)
            {
                ConsoleManager.PrintMessage($"{counter++}) User - {stat.User.FirstName} {stat.User.LastName}, " +
                    $"category - {stat.Category.Name}, correct answers {stat.NumCorrectAnswer}");
            }
        }
    }
}
