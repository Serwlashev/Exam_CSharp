using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Answers")]
    public class Answer : BaseEntity
    {
        [Column]
        [Required]
        public string Text { get; set; }
        [Column]
        [Required]
        [System.Data.Linq.Mapping.Association(Storage = "Questions", ThisKey = "Id", IsForeignKey = true)]
        public int QuestionId { get; set; }
        [Column]
        [Required]
        public bool IsCorrect { get; set; }
        public void Copy(Answer answer)
        {
            Text = answer.Text;
            QuestionId = answer.QuestionId;
            IsCorrect = answer.IsCorrect;
        }
        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
