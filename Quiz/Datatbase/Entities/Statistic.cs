using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Statistics")]
    public class Statistic : BaseEntity
    {
        [Column]
        [Required]
        [System.Data.Linq.Mapping.Association(Storage = "Users", ThisKey = "Id", IsForeignKey = true)]
        public int? UserId { get; set; }
        public User User { get; set; }

        [Column]
        [Required]
        [System.Data.Linq.Mapping.Association(Storage = "Categories", ThisKey = "Id", IsForeignKey = true)]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        [Column]
        [Required]
        public int NumCorrectAnswer { get; set; }

    }
}
