using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AddToMyCart.ViewModels
{
    public partial class EditUserDetailsViewModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        //[FileExtensions(Extensions = )]
        public dynamic ProfilePicture { get; set; }
    }
}
