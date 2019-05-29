using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyCart.ViewModels
{
    public class AddCalendarViewModel
    {
        public int CalendarID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 Characters allowed", MinimumLength = 6)]
        public string CalendarText { get; set; }

        [Required]
        public string CalendarDateTime { get; set; }

        [Required]
        public string PriorityID { get; set; }

        public string UserID { get; set; }
    }
}
