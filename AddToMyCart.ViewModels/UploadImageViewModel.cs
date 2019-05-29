using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AddToMyCart.ViewModels
{
   public class UploadImageViewModel
    {
        public int UserID { get; set; }
        public HttpPostedFileWrapper ImageFile { get; set; }
    }
}
