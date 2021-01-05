using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Exceptions
{
    class AccountNotFoundException : ApplicationException
    {
        public AccountNotFoundException() : base("Account not found!") { }
    }
}
