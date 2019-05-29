using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace AddToMyCart.ViewModels
{
    public class CalendarViewModel
    {
        public int CalendarID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 Characters allowed", MinimumLength = 6)]
        public string CalendarText { get; set; }

        [Required]
        public DateTime CalendarDateTime { get; set; }

        [Required]
        public int PriorityID { get; set; }

        public int UserID { get; set; }

        public virtual SetPriorityViewModel SetPriority { get; set; }
    }
}
