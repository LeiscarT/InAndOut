using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models
{
    public class Expense
    {
        [Key]
        public int Id{ get; set; }
        [DisplayName("Expense")]
        public string ExpenseName{ get; set; }
        public int Amount { get; set; }
    }
}
