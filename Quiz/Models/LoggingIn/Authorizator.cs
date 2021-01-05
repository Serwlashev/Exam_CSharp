using Quiz.Datatbase.Entities;
using Quiz.Datatbase.EntityValidator;
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
    // Class for authorization in the app
    public class Authorizator
    {
        private UnitOfWork repository;

        public Authorizator(UnitOfWork repos)
        {
            repository = repos;
        }
        public User Authorize()
        {
            ConsoleManager.ClearConsole();
            ConsoleManager.PrintMessage("\n\tAuthorization");
            Account account = null;
            User user = null;

            account = EnterAccount();       // Enter account data

            if (account != null)            // If with some reason we didn't get an account
            {
                // Load all accounts with the same login 
                List<Account> accounts = repository.AccountRepository.GetAll(acc => acc.Login.Equals(account.Login)).ToList();

                if (accounts.Count() != 1)  // If we cannot find the account
                {
                    throw new AccountNotFoundException();
                }

                if (!accounts[0].Password.Equals(account.Password)) // If user entered the wrong password
                {
                    throw new WrongPasswordException();
                }

                user = GetUser(accounts[0].Id);                     // Load user according to the account id
            }

            return user;
        }

        private Account EnterAccount()      // Method get input from the console and check passed data, in case wrong input it throws an error to the logger
        {
            string login = ConsoleManager.GetLogin();
            string password = ConsoleManager.GetPassword();
            Account account = new Account() { Login = login, Password = password };

            CheckAccount(account);

            return account;
        }

        private void CheckAccount(Account account)  
        {
            if (!EntityValidator.Validate(account))
            {
                ConsoleManager.PrintMessage("\nThe following errors occurred during the authorization process:");
                ConsoleManager.ShowList(EntityValidator.GetErrors(account));

                throw new WrongArgumentException("Cannot authorize  an account!");
            }
        }

        private User GetUser(int id)
        {
            return repository.UserRepository.Get(id);
        }
    }
}
