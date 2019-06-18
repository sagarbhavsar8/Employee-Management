using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyCart.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        public int ProductName { get; set; }
        [Display(Name = "Product Image")]
        public byte[] ProductImage { get; set; }
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
