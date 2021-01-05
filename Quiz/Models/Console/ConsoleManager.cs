using Quiz.Models.Application;
using Quiz.Models.Authorization;
using Quiz.Models.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Console
{
    // This class separate logic working with console from the other classes, only it can print or read from the console
    static class ConsoleManager     
    {
        public static void PrintMessage(string msg)
        {
            System.Console.WriteLine(msg);
        }
        public static string GetLogin()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Please, enter login (login should be from 6 to 20 letters, can contain letters, numbers, @ and points):    ");
            string login = System.Console.ReadLine();
            return login;
        }

        public static string GetPassword()
        {
            System.Console.WriteLine("Please, enter password (password should be from 8 to 20 symbols and can contain any symbols): ");
            string password = System.Console.ReadLine();
            return password;
        }

        public static DateTime GetBirth()
        {
            System.Console.Write("\nPlease, enter your year of birth:  ");
            int year = int.Parse(System.Console.ReadLine());
            System.Console.Write("Please, enter your month of birth: ");
            int month = int.Parse(System.Console.ReadLine());
            System.Console.Write("Please, enter your day of birth: ");
            int day = int.Parse(System.Console.ReadLine());

            return new DateTime(year, month, day);
        }

        public static int GetNumber()
        {
            int number = -1;
            int.TryParse(System.Console.ReadLine(), out number);
            return number;
        }

        public static void ShowList(List<string> values)
        {
            foreach(var val in values)
            {
                System.Console.WriteLine(val);
            }
        }

        public static QuizApplication.MenuActions AskMainMenuAction()
        {
            QuizApplication.MenuActions action = QuizApplication.MenuActions.EXIT;

            System.Console.WriteLine("\n\tPlease choose action: ");
            System.Console.WriteLine("1 - start a new game");
            System.Console.WriteLine("2 - show user statistics");
            System.Console.WriteLine("3 - show top 20 statistics by category");
            System.Console.WriteLine("4 - change information about the account");
            System.Console.WriteLine("5 - start questions change mode");
            System.Console.WriteLine("0 - exit");

            string result = System.Console.ReadLine();
            System.Console.WriteLine();

            switch (result)
            {
                case "1":
                    action = QuizApplication.MenuActions.START_GAME;
                    break;
                case "2":
                    action = QuizApplication.MenuActions.SHOW_USER_STATISTICS;
                    break;
                case "3":
                    action = QuizApplication.MenuActions.SHOW_TOP20_BY_CATEGORY;
                    break;
                case "4":
                    action = QuizApplication.MenuActions.CHANGE_USER_DATA;
                    break;
                case "5":
                    action = QuizApplication.MenuActions.CHANGE_QUIZ_MODE;
                    break;
                case "0":
                    action = QuizApplication.MenuActions.EXIT;
                    break;
                default:
                    System.Console.WriteLine("You entered the wrong parameter, the program will be closed");
                    break;
            }

            return action;
        }

        public static DataEditor.ChangingMode AskChangingUserMode()
        {
            DataEditor.ChangingMode action = DataEditor.ChangingMode.EXIT;

            System.Console.WriteLine("\n\tPlease choose action: ");
            System.Console.WriteLine("1 - change user password");
            System.Console.WriteLine("2 - change user birth date");
            System.Console.WriteLine("3 - exit to the main menu");
            System.Console.WriteLine("0 - exit");

            string result = System.Console.ReadLine();
            System.Console.WriteLine();

            switch (result)
            {
                case "1":
                    action = DataEditor.ChangingMode.CHANGE_PASSWORD;
                    break;
                case "2":
                    action = DataEditor.ChangingMode.CHANGE_BIRTH;
                    break;
                case "3":
                    action = DataEditor.ChangingMode.EXIT_TO_MAIN;
                    break;
                case "0":
                    action = DataEditor.ChangingMode.EXIT;
                    break;
                default:
                    System.Console.WriteLine("You entered the wrong parameter, the program will be closed");
                    break;
            }

            return action;
        }

        public static DataEditor.ChangingMode AskChangingQuizMode()
        {
            DataEditor.ChangingMode action = DataEditor.ChangingMode.EXIT;

            System.Console.WriteLine("\n\tPlease choose action: ");
            System.Console.WriteLine("1 - add a new category");
            System.Console.WriteLine("2 - add a new question");
            System.Console.WriteLine("3 - add a new answer");
            System.Console.WriteLine("4 - change an existing  question");
            System.Console.WriteLine("5 - change an existing  answer");
            System.Console.WriteLine("6 - exit to the main menu");
            System.Console.WriteLine("0 - exit");

            string result = System.Console.ReadLine();
            System.Console.WriteLine();

            switch (result)
            {
                case "1":
                    action = DataEditor.ChangingMode.ADD_CATEGORY;
                    break;
                case "2":
                    action = DataEditor.ChangingMode.ADD_QUESTION;
                    break;
                case "3":
                    action = DataEditor.ChangingMode.ADD_ANSWER;
                    break;
                case "4":
                    action = DataEditor.ChangingMode.CHANGE_QUESTION;
                    break;
                case "5":
                    action = DataEditor.ChangingMode.CHANGE_ANSWER;
                    break;
                case "6":
                    action = DataEditor.ChangingMode.EXIT_TO_MAIN;
                    break;
                case "0":
                    action = DataEditor.ChangingMode.EXIT;
                    break;
                default:
                    System.Console.WriteLine("You entered the wrong parameter, the program will be closed");
                    break;
            }

            return action;
        }

        public static Logger.LoggerAction WantContinue()
        {
            Logger.LoggerAction action = Logger.LoggerAction.EXIT;

            System.Console.WriteLine("\n\tPlease choose action: ");
            System.Console.WriteLine("1 - create a new account");
            System.Console.WriteLine("2 - login into an existing account");
            System.Console.WriteLine("0 - exit");

            string result = System.Console.ReadLine();
            System.Console.WriteLine();

            switch (result)
            {
                case "1":
                    action = Logger.LoggerAction.CREATE_NEW;
                    break;
                case "2":
                    action = Logger.LoggerAction.ENTER_EXISTED;
                    break;
                default:
                    System.Console.WriteLine("You entered the wrong parameter, the program will be closed");
                    break;
            }

            return action;
        }
        public static void ClearConsole()
        {
            System.Console.Clear();
        }
        public static List<int> AskQuestion(int numRightQuestion)
        {
            List<int> answers = new List<int>(numRightQuestion);
            int choosenAnswer = -1;

            string question = "Please, choose ";
            question += numRightQuestion == 1 ? "a right answer: " : $" {numRightQuestion} right answers: ";
            System.Console.WriteLine(question);

            for(int i = 0; i < numRightQuestion; i++)
            {
                System.Console.Write($"Choose answer {i + 1}: ");
                int.TryParse(System.Console.ReadLine(), out choosenAnswer);
                answers.Add(choosenAnswer);
            }

            return answers;
        }
        
        public static (string, string) GetFullName()
        {
            System.Console.Write("Please, enter first name: ");
            string firstname = System.Console.ReadLine();
            System.Console.Write("Please, enter last name: ");
            string lastname = System.Console.ReadLine();

            return (firstname, lastname);
        }

        public static string GetStringValue(string message)
        {
            System.Console.WriteLine(message);
            return System.Console.ReadLine();
        }
    }
}
