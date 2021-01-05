using Quiz.Datatbase;
using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Exceptions;
using Quiz.Models.Authorization;
using Quiz.Models.Console;
using Quiz.Models.Editor;
using Quiz.Models.Game;
using Quiz.Models.StatisticControl;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Application
{
    // It is the main class which works with all other classes
    class QuizApplication
    {
        public enum MenuActions
        {
            START_GAME,
            SHOW_USER_STATISTICS,
            SHOW_TOP20_BY_CATEGORY,
            CHANGE_USER_DATA,
            CHANGE_QUIZ_MODE,
            EXIT
        }

        private CategoryStorage categoryStorage;
        private UnitOfWork repository;
        private Logger logger;
        private QuizGame game;
        private StatisticsManager statisticManager;
        private DataEditor editor;

        private User currentUser;
        public QuizApplication()
        {
            Database.SetInitializer(new DatabaseInitializer());
            repository = new UnitOfWork();
            logger = new Logger(repository);
            game = new QuizGame(repository);
            statisticManager = new StatisticsManager(repository);
            currentUser = null;
            categoryStorage = new CategoryStorage(repository);
            editor = new DataEditor(repository);
        }
        public void Start()
        {
            try
            {
                currentUser = logger.StartLoggingIn();

                if(currentUser != null)
                {
                    ConsoleManager.PrintMessage($"\n\tHello, {currentUser.FirstName} {currentUser.LastName}");
                    StartMenuWork();
                }
                else
                {
                    ConsoleManager.PrintMessage("Cannot start the application, goodbay!");
                }

            }
            catch(ExitException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }
        }

        private void StartMenuWork()
        {
            while (true)
            {
                MenuActions action = ConsoleManager.AskMainMenuAction();

                try
                {
                    RunMenuAction(action);
                }
                catch (ExitToMenuException)
                {
                    ConsoleManager.ClearConsole();
                    ConsoleManager.PrintMessage($"\n\tHello, {currentUser.FirstName} {currentUser.LastName}");
                }
            }
        }

        private void RunMenuAction(MenuActions action)
        {
            switch (action)
            {
                case MenuActions.START_GAME:
                    StartGame();
                    break;
                case MenuActions.SHOW_USER_STATISTICS:
                    ShowAllUserStatistics();
                    break;
                case MenuActions.SHOW_TOP20_BY_CATEGORY:
                    ShowTop20();
                    break;
                case MenuActions.CHANGE_USER_DATA:
                    ChangeUserData();
                    break;
                case MenuActions.CHANGE_QUIZ_MODE:
                    ChangeQuiz();
                    break;
                case MenuActions.EXIT:
                    throw new ExitException();
            }
        }

        private void ChangeQuiz()
        {
            editor.ChangeQuiz();
        }

        private void ChangeUserData()
        {
            editor.ChangeUserData(currentUser);
        }

        private void StartGame()
        {
            int categoryId = categoryStorage.ChooseCategory();

            int numCorrectAnswers = game.Start(categoryId);
            statisticManager.AddStatistic(currentUser.Id, categoryId, numCorrectAnswers);

            ConsoleManager.ClearConsole();
            ConsoleManager.PrintMessage($"\n\t{currentUser.FirstName}, you have answered {numCorrectAnswers} correct answers.\n");
            ShowStatistics();
        }

        private void ShowTop20()
        {
            statisticManager.ShowTop20ByCategory();
        }

        private void ShowStatistics()
        {
            statisticManager.ShowAllStatistic(currentUser.Id);
        }

        private void ShowAllUserStatistics()
        {
            statisticManager.ShowUserStatistic(currentUser.Id);
        }

    }
}
