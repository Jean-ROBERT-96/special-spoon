using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class Pen
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Code { get; set; }
    }
}
