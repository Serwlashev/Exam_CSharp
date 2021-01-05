using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.EntityValidator
{
    // This class checks if the date is bigger than the current date
    class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime curentDate = DateTime.Now;
            DateTime passedDate;
            bool success = DateTime.TryParse(value.ToString(), out passedDate);

            if (success && passedDate <= curentDate)
            {
                return true;
            }
            return false;
        }
    }
}
