using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Exceptions
{
    class WrongArgumentException : ArgumentException
    {
        public WrongArgumentException(string message) : base(message) { }

    }
}
