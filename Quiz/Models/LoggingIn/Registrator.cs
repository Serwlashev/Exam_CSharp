using Quiz.Datatbase.Entities;
using Quiz.Datatbase.UnitOfWork;
using Quiz.Models.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Datatbase.EntityValidator;
using Quiz.Exceptions;

namespace Quiz.Models.Authorization
{
    // Class for registration in the app
    class Registrator
    {
        private UnitOfWork repository;

        public Registrator(UnitOfWork repos)
        {
            repository = repos;
        }

        public bool Registrate()
        {
            ConsoleManager.ClearConsole();
            ConsoleManager.PrintMessage("\n\tRegistration a new user:\n");
            try
            {
                Account account = GetCreatedAccount();
                
                if (account != null)
                {
                    CreateUser(account.Id);
                    return true;
                }
                else{
                    ConsoleManager.PrintMessage("Cannot registrate a new account, please, try again\n");
                }
            }
            catch (WrongArgumentException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }

            return false;
        }

        private Account GetCreatedAccount()
        {
            Account account = CreateAccount();

            repository.AccountRepository.Create(account);
            repository.Save();

            var accounts = repository.AccountRepository.GetAll(ac => ac.Login.Equals(account.Login));

            if (accounts.Count() > 0)
            {
                return accounts.FirstOrDefault(ac => ac.Password.Equals(account.Password));
            }
            throw new WrongArgumentException("Cannot create a new account, please, try again\n");
        }

        private Account CreateAccount()
        {
            string login = ConsoleManager.GetLogin();
            string password = ConsoleManager.GetPassword();

            if(repository.AccountRepository.GetAll(acc => acc.Login.Equals(login)).Count() > 0){
                throw new WrongArgumentException("Account already exists in the database\n");
            }

            Account account = new Account() { Login = login, Password = password };
            if (EntityValidator.Validate(account))
            {
                return account;
            }

            ShowErrors(EntityValidator.GetErrors(account));
            throw new WrongArgumentException("Please, try again\n");
        }
        private void CreateUser(int id)
        {
            try
            {
                (string, string) FullName = ConsoleManager.GetFullName();
                DateTime birth = ConsoleManager.GetBirth();
                User user = null;

                user = new User() { AccountId = id, FirstName = FullName.Item1, LastName = FullName.Item2, Birth = birth };
                if (EntityValidator.Validate(user))
                {
                    repository.UserRepository.Create(user);
                    repository.Save();
                    return;
                }

                ShowErrors(EntityValidator.GetErrors(user));
            }
            catch (ArgumentOutOfRangeException ex)      // Catch exception if we have problem with birth
            {
                ConsoleManager.PrintMessage(ex.Message);
            }
            catch(FormatException ex)
            {
                ConsoleManager.PrintMessage(ex.Message);
            }

            repository.AccountRepository.Delete(id);    // Delete created account, which exists without user
            repository.Save();

            throw new WrongArgumentException("Please, try again\n");
        }

        private void ShowErrors(List<string> errors)
        {
            ConsoleManager.PrintMessage("\nDuring the registration process has occured next errors:");
            ConsoleManager.ShowList(errors);
        }
    }
}
