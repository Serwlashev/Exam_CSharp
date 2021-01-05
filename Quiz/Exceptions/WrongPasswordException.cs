using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Exceptions
{
    class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("You have passed the wrong password!") { }
    }
}
