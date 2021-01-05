using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Exceptions;
using Quiz.Models.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Models.Authorization
{
    // Class allows to log in or create a new account
    class Logger
    {
        public enum LoggerAction
        {
            CREATE_NEW,
            ENTER_EXISTED,
            EXIT
        }
        private Authorizator authorizator;
        private Registrator registrator;

        public Logger(UnitOfWork repository)
        {
            authorizator = new Authorizator(repository);
            registrator = new Registrator(repository);
        }
        public User StartLoggingIn()
        {
            User user = null;
            LoggerAction action;

            while (user == null)
            {
                action = ConsoleManager.WantContinue();

                switch (action)
                {
                    case LoggerAction.CREATE_NEW:
                        CreateNewAccount();
                        break;
                    case LoggerAction.ENTER_EXISTED:
                        user = AuthorizeUser();
                        break;
                    case LoggerAction.EXIT:
                        throw new ExitException();
                }
            }

            return user;
        }

        private User AuthorizeUser()
        {
            User user = null;
            try
            {
                user = authorizator.Authorize();
            }
            catch (WrongArgumentException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }
            catch (WrongPasswordException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }
            catch(AccountNotFoundException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }

            return user;
        }
        private void CreateNewAccount()
        {
            if (registrator.Registrate())
            {
                ConsoleManager.PrintMessage("The account was successfully created");
            }
        }
    }
}
