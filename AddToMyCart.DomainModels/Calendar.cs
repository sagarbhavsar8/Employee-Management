using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AddToMyCart.DomainModels
{
    public class Calendar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CalendarID { get; set; }
        public string CalendarText { get; set; }
        public DateTime CalendarDateTime  { get; set; }
        public int PriorityID { get; set; }
        public int UserID { get; set; }

        [ForeignKey("PriorityID")]
        public virtual SetPriority SetPriority { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
