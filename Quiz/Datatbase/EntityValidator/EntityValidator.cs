using Quiz.Models.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.EntityValidator
{
    // Class for validation entities and for getting errors in case they occurred
    public static class EntityValidator
    {
        public static bool Validate<T>(T obj)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj);

            if (Validator.TryValidateObject(obj, context, validationResults, true))
            {
                return true;
            }
            return false;
        }

        public static List<string> GetErrors<T>(T obj)
        {
            List<string> errors = null;
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(obj);

            if (!Validator.TryValidateObject(obj, context, validationResults, true))
            {
                errors = validationResults.Select(v => v.ErrorMessage).ToList();
            }
            return errors;
        }
    }
}
