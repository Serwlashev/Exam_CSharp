using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Models.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Game
{
    class CategoryStorage
    {
        private UnitOfWork repository;

        public CategoryStorage(UnitOfWork repos)
        {
            repository = repos;
        }

        public int ChooseCategory()
        {
            int result = -1;

            var categories = repository.CategoryRepository.GetAll().ToList();
            ShowCategories(categories);

            if (categories.Count() > 0)
            {
                ConsoleManager.PrintMessage("Please, choose number of category:");
                result = ConsoleManager.GetNumber();

                if (result > categories.Count())
                {
                    result = -1;
                }
            }
            return result;
        }

        private void ShowCategories(List<Category> categories)
        {
            ConsoleManager.PrintMessage("\nAvialable categories: ");

            foreach (var category in categories)
            {
                ConsoleManager.PrintMessage($"\t{category.Id}) - {category.Name}");
            }
        }
    }
}
