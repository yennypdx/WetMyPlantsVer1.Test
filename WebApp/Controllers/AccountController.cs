﻿
using System.Web.Mvc;
using Models;
using WebApp.Models.AccountViewModels;
using WebApp.Models.HomeViewModels;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly DBHelper.IDbHelper _db;

        // Inject Dependency
        public AccountController(DBHelper.IDbHelper db) => _db = db;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            var user = Session["User"];
            return View(user);
        }

        //POST: Account/RegisterUser
        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel uModel)
        {
            var result = _db.CreateNewUser(
                uModel.FirstName,
                uModel.LastName,
                uModel.Phone,
                uModel.Email,
                uModel.Password);

            if (!result) return RedirectToAction("Register");

            var token = _db.LoginAndGetToken(uModel.Email, uModel.Password);

            if (token == null) return RedirectToAction("Register");

            //ViewBag.User = result;
            ViewBag.Token = token;

            // set the session
            var user = _db.FindUserByEmail(uModel.Email);
            //Session["User"] = _db.FindUserByEmail(uModel.Email);
            Session["User"] = user;
            Session["Email"] = user.Email;

            return RedirectToAction("Index", "Home", user);
        }

        [HttpPost]
        public ActionResult LoginUser(LoginViewModel model)
        {
            // Merge errors for name change from result to token!
            var token = _db.LoginAndGetToken(model.Email, model.Password);

            if (token != null)
            {
                var user = _db.FindUserByEmail(model.Email);
                ViewBag.User = user;
                ViewBag.Token = token;

                // set the session 
                Session["User"] = user;
                Session["Email"] = user.Email;

                // Create a ViewModel to pass the user to the view
                var userViewModel = new RegistrationViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };

                // store token as a cookie here
                return RedirectToAction("Index", "Home", user);
            }

            return View("Login");
        }

        [HttpPost]
        public ActionResult UpdateUser(User user)
        {
            // update the session
            Session["User"] = user;
            _db.UpdateUser(user);
            return RedirectToAction("MyAccount", "Account", user);
        }

        public ActionResult DeleteUser(string email)
        {
            var user = _db.FindUserByEmail(email);
            var userViewModel = new DeleteUserViewModel
            {
                Email = email
            };
            
            return View(userViewModel);
        }

        public ActionResult ConfirmDeletion(DeleteUserViewModel model)
        {
            if (_db.AuthenticateUser(model.Email, model.Password))
            {
                _db.DeleteUser(model.Email);
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["Error"] = "Incorrect Password";
                return RedirectToAction("DeleteUser", "Account", new { email = model.Email });
            }
        }
     
    }
}