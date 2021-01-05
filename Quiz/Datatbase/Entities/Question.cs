using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationAttribute = System.Data.Linq.Mapping.AssociationAttribute;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Questions")]
    public class Question : BaseEntity
    {
        [Column]
        [Required]
        public string Text { get; set; }
        [Column]
        [Required]
        [Association(Storage = "Categories", ThisKey = "Id", IsForeignKey = true)]
        public int CategoryId { get; set; }
        public void Copy(Question question)
        {
            Text = question.Text;
            CategoryId = question.CategoryId;
        }
        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
