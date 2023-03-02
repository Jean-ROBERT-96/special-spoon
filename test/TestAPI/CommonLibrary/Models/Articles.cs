using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    [Table("Article")]
    public class Articles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int FamilyId { get; set; }
        [ForeignKey("FamilyId")]
        public virtual Family Family { get; set; }
    }
}
