using Quiz.Datatbase.Entities;
using Quiz.Datatbase.EntityValidator;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Exceptions;
using Quiz.Models.Console;
using Quiz.Models.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Editor
{
    // Class allows to change user data, questions, answers and add categories
    class DataEditor
    {
        public enum ChangingMode
        {
            CHANGE_PASSWORD,
            CHANGE_BIRTH,
            ADD_CATEGORY,
            ADD_QUESTION,
            ADD_ANSWER,
            CHANGE_QUESTION,
            CHANGE_ANSWER,
            EXIT_TO_MAIN,
            EXIT
        }
        private CategoryStorage categoryStorage;
        private UnitOfWork repository;
        public DataEditor(UnitOfWork repos)
        {
            repository = repos;
            categoryStorage = new CategoryStorage(repos);
        }

        public void ChangeUserData(User user)
        {
            ConsoleManager.ClearConsole();

            while (true)
            {
                ChangingMode mode = ConsoleManager.AskChangingUserMode();

                try
                {
                    RunChangingMode(mode, user);
                }
                catch (ArgumentOutOfRangeException ex)      // Catch exception if we have problem with birth
                {
                    ConsoleManager.PrintMessage(ex.Message);
                    ConsoleManager.PrintMessage("Cannot change user data");
                }
                catch (FormatException ex)
                {
                    ConsoleManager.PrintMessage(ex.Message);
                    ConsoleManager.PrintMessage("Cannot change user data");
                }
            }
        }

        public void ChangeQuiz()
        {
            ConsoleManager.ClearConsole();

            while (true)
            {
                ChangingMode mode = ConsoleManager.AskChangingQuizMode();

                try
                {
                    RunChangingMode(mode);
                }
                catch (FormatException ex)
                {
                    ConsoleManager.PrintMessage(ex.Message);
                    ConsoleManager.PrintMessage("Cannot change user data");
                }
            }
        }

        private void RunChangingMode(ChangingMode mode, User user = null)
        {
            switch (mode)
            {
                case ChangingMode.CHANGE_PASSWORD:
                    ChangeUserPassword(user);
                    break;
                case ChangingMode.CHANGE_BIRTH:
                    ChangeUserBirth(user);
                    break;
                case ChangingMode.ADD_CATEGORY:
                    AddNewCategory();
                    break;
                case ChangingMode.ADD_QUESTION:
                    AddNewQuestion();
                    break;
                case ChangingMode.ADD_ANSWER:
                    AddNewAnswer();
                    break;
                case ChangingMode.CHANGE_QUESTION:
                    ChangeExistingQuestion();
                    break;
                case ChangingMode.CHANGE_ANSWER:
                    ChangeExistingAnswer();
                    break;
                case ChangingMode.EXIT_TO_MAIN:
                    throw new ExitToMenuException();
                case ChangingMode.EXIT:
                    throw new ExitException();
                default:
                    break;
            }
        }

        private void ChangeUserPassword(User user)
        {
            string newPassword = ConsoleManager.GetStringValue("Please, enter a new password");

            var userAccount = repository.AccountRepository.Get(user.AccountId);
            userAccount.Password = newPassword;

            if (ValidateObject(userAccount))
            {
                repository.Save();
            }
        }

        private void ChangeUserBirth(User user)
        {
            DateTime newBirth = ConsoleManager.GetBirth();

            var userBD = repository.UserRepository.Get(user.Id);
            userBD.Birth = newBirth;

            if (ValidateObject(userBD))
            {
                repository.Save();
            }
        }

        private void AddNewCategory()
        {
            string name = ConsoleManager.GetStringValue("Please, enter the name of the new category: ");

            Category category = new Category() { Name = name };

            if (ValidateObject(category))
            {
                repository.CategoryRepository.Create(category);
                repository.Save();
            }
        }

        private void AddNewQuestion() 
        {
            int categoryId = categoryStorage.ChooseCategory();
            string text = ConsoleManager.GetStringValue("Please, enter text of the question");
            Question question = new Question() { CategoryId = categoryId, Text = text };

            if (ValidateObject(question))
            {
                repository.QuestionRepository.Create(question);
                repository.Save();
            }
        }

        private void AddNewAnswer()
        {
            int questionid = ChooseQuestionId();
            if(questionid < 0)
            {
                return;
            }

            string text = ConsoleManager.GetStringValue("Please, enter text of the answer");
            bool isCorrect = EnterIsCorrect();

            Answer answer = new Answer() {Text = text, IsCorrect = isCorrect, QuestionId = questionid};

            if (ValidateObject(answer))
            {
                repository.AnswerRepository.Create(answer);
                repository.Save();
            }

        }

        private void ChangeExistingQuestion()
        {
            int questionId = ChooseQuestionId();

            var question = repository.QuestionRepository.Get(questionId);

            string text = ConsoleManager.GetStringValue("Please, enter new text of the question");
            question.Text = text;

            if (ValidateObject(question))
            {
                repository.Save();
            }
        }

        private void ChangeExistingAnswer()
        {
            int questionId = ChooseQuestionId();
            List<Answer> answers = repository.AnswerRepository.GetAll(ans => ans.QuestionId == questionId).ToList();

            foreach(var ans in answers)
            {
                ConsoleManager.PrintMessage($"id - {ans.Id}, text - {ans.Text}");
            }
            ConsoleManager.PrintMessage("Please, choose answer id: ");
            int id = ConsoleManager.GetNumber();

            Answer answer = answers.FirstOrDefault(a => a.Id == id);

            if (id > 0 && answers.Contains(answer))
            {
                string text = ConsoleManager.GetStringValue("Please, enter text of the answer");
                bool isCorrect = EnterIsCorrect();

                answer.IsCorrect = isCorrect;
                answer.Text = text;

                if (ValidateObject(answer))
                {
                    repository.Save();
                }
            }
        }

        private bool ValidateObject(BaseEntity entity)
        {
            if (EntityValidator.Validate(entity))
            {
                return true;
            }
            List<string> errors = EntityValidator.GetErrors(entity);
            ConsoleManager.PrintMessage("The following has errors occurred during the data validation process:");
            ConsoleManager.ShowList(errors);
            return false;
        }

        private bool EnterIsCorrect()
        {
            bool isCorrect = false;

            if (!bool.TryParse(ConsoleManager.GetStringValue("Please, enter is it answer correct or not (\"false\" or \"true\")"), out isCorrect))
            {
                ConsoleManager.PrintMessage("You entered the wrong parameter, the answer will be marked as false");
            }

            return isCorrect;
        }

        private int ChooseQuestionId()
        {
            int categoryId = categoryStorage.ChooseCategory();

            var questions = repository.QuestionRepository.GetAll(q => q.CategoryId == categoryId).ToList();

            ConsoleManager.PrintMessage("Avialable questions:");

            foreach(var q in questions)
            {
                ConsoleManager.PrintMessage($"id - {q.Id}, text - {q.Text}");
            }

            ConsoleManager.PrintMessage("Please, choose question id:");

            int id = ConsoleManager.GetNumber();

            if(id > 0 && questions.Contains(questions.FirstOrDefault(q => q.Id == id)))
            {
                return id;
            }
            return -1;
        }
    }
}
