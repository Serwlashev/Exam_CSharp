using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Datatbase.Entities
{
    [Table(Name = "Categories")]
    public class Category : BaseEntity
    {
        [Column]
        [System.Data.Linq.Mapping.Association(IsUnique = true)]
        public string Name { get; set; }

        public void Copy(Category category)
        {
            Name = category.Name;
        }
    }
}
