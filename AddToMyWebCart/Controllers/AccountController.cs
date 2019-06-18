using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddToMyCart.ViewModels;
using AddToMyCart.ServiceLayer;
using System.IO;
using System.Security.AccessControl;

namespace AddToMyWebCart.Controllers
{
    public class AccountController : Controller
    {
        IUsersService us;

        public AccountController(IUsersService us)
        {
            this.us = us;
        }

        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel lvm = CheckCookie();
            if (lvm == null)
            {
                return View(lvm);
            }
            else
            {
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.FirstName + " " + uvm.LastName;
                    Session["CurrentUserEmail"] = uvm.Email;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Data");
                    return View();
                }
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                UserViewModel uvm = this.us.GetUsersByEmailAndPassword(lvm.Email, lvm.Password);
                if (uvm != null)
                {
                    Session["CurrentUserID"] = uvm.UserID;
                    Session["CurrentUserName"] = uvm.FirstName + " " + uvm.LastName;
                    Session["CurrentUserEmail"] = uvm.Email;

                    if (lvm.RememberMe)
                    {
                        HttpCookie ckEmail = new HttpCookie("UserEmail");
                        ckEmail.Expires.AddMinutes(5);
                        ckEmail.Value = lvm.Email;
                        Response.Cookies.Add(ckEmail);

                        HttpCookie ckPassword = new HttpCookie("UserPassword");
                        ckEmail.Expires.AddMinutes(5);
                        ckPassword.Value = lvm.Password;
                        Response.Cookies.Add(ckPassword);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Data");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }
        }

        public ActionResult Register()
        {
            RegisterViewModel rvm = new RegisterViewModel();
            return View(rvm);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                int uid = us.InsertUser(rvm);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }
        }

        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            if (Response.Cookies["UserEmail"] != null)
            {
                HttpCookie ckEmail = new HttpCookie("UserEmail");
                ckEmail.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckEmail);
            }
            if (Response.Cookies["UserPassword"] != null)
            {
                HttpCookie ckPassword = new HttpCookie("UserPassword");
                ckPassword.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(ckPassword);
            }
            return RedirectToAction("Login", "Account");
        }


        public LoginViewModel CheckCookie()
        {
            LoginViewModel lvm = null;
            string email = string.Empty;
            string password = string.Empty;
            if (Request.Cookies["UserEmail"] != null)
            {
                email = Request.Cookies["UserEmail"].Value;
            }
            if (Request.Cookies["UserPassword"] != null)
            {
                password = Request.Cookies["UserPassword"].Value;
            }
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                lvm = new LoginViewModel() { Email = email, Password = password };
            }
            return lvm;
        }

        public ActionResult ChangeProfile()
        {
            int UserID = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel uvm = us.GetUsersByUserID(Convert.ToInt32(Session["CurrentUserID"]));
            return View(uvm);
        }

        [HttpPost]
        public ActionResult ChangeProfile(EditUserDetailsViewModel euvm, HttpPostedFileBase ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePicture != null)
                {
                    euvm.ProfilePicture = new byte[ProfilePicture.ContentLength];
                    ProfilePicture.InputStream.Read(euvm.ProfilePicture, 0, ProfilePicture.ContentLength);
                }
                us.UpdateUserDetails(euvm);
                Session["CurrentUserName"] = euvm.FirstName + " " + euvm.LastName;
                return RedirectToAction("ChangeProfile");
            }
            else
            {
                ModelState.AddModelError("X", "Invalid Data");
                return RedirectToAction("ChangeProfile");
            }
        }

        public ActionResult Photos()
        {
            if (Session["CurrentUserID"] != null)
            {
                string filepath = "~/UploadedImage/" + Session["CurrentUserID"].ToString() + "/";
                if (Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(Server.MapPath(filepath));
                    string[] str = Directory.GetFiles(Server.MapPath(filepath));
                    ViewBag.userFiles = str;
                }
                else
                {
                    ViewBag.userFiles = null;
                }
            }
            else
            {
                ViewBag.userFiles = null;
            }
            return View();
        }

        [HttpPost]
        public JsonResult UploadPhotos(UploadImageViewModel model)
        {
            var file = model.ImageFile;
            string finalFileName = "";
            string currentPath = @"~/UploadedImage/" + Session["CurrentUserID"].ToString() + "/";
            if (file != null)
            {
                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(Server.MapPath(currentPath));
                }
                file.SaveAs(Server.MapPath(currentPath + "/" + file.FileName));
                string str = "/UploadedImage/" + Session["CurrentUserID"].ToString() + "/" + file.FileName;
                finalFileName = "<div class='thumbnail border-2 ml - 2'>" + "<img src='" + str + "' class='img-thumbnail' height='150' width='150'/>" + "</div>";
            }
            return Json(finalFileName, JsonRequestBehavior.AllowGet);
        }

    }
}