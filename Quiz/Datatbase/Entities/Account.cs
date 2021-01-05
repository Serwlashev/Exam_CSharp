using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Accounts")]
    public class Account : BaseEntity
    {
        [Required]
        [RegularExpression("^[a-zA-Z][a-zA-Z0-9\\.@]{5,20}$", 
            ErrorMessage ="Login should be from 6 to 20 symbols, can contain points, @ and should start from a letter")]

        [Column]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password can be from 8 to 20 symbols")]
        [Column]
        public string Password { get; set;}
        public void Copy(Account acc)
        {
            Login = acc.Login;
            Password = acc.Password;
        }
    }
}
