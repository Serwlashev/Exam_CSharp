using Quiz.Datatbase.EntityValidator;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Users")]
    public class User : BaseEntity
    {
        [Column]
        [Association( Storage="Account", ThisKey ="Id", IsForeignKey =true)]
        public int AccountId { get; set; }
        [Column]
        [System.ComponentModel.DataAnnotations.Required]
        public string FirstName { get; set; }
        [Column]
        [System.ComponentModel.DataAnnotations.Required]
        public string LastName { get; set; }
        [Column]
        [System.ComponentModel.DataAnnotations.Required]
        [DateValidator(ErrorMessage = "The birth date shoud be less than current date")]
        public DateTime Birth { get; set; }
        public void Copy(User user)
        {
            AccountId = user.AccountId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Birth = user.Birth;
        }
    }
}
