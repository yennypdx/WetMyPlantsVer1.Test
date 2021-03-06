﻿/*using System;
using System.Web.Mvc;
using DBHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using WebApp.Models.AccountViewModels;

namespace WebApp.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        // Is this okay? Constructor takes a database
        private readonly DBHelper.IDbHelper db = new DBHelper.DbHelper();

        [TestMethod]
        public void Login_ShouldReturnTheLoginViewModel()
        {
            var controller = new AccountController(db);
            var result = controller.Login() as ViewResult;
            var model = result.Model as LoginViewModel;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void Register_ShouldReturnTheRegistrationViewModel()
        {
            var controller = new AccountController(db);
            var result = controller.Register() as ViewResult;
            var model = result.Model as RegistrationViewModel;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void ForgotPassword_ShouldReturnTheForgotPasswordViewModel()
        {
            var controller = new AccountController(db);
            var result = controller.ForgotPassword() as ViewResult;
            var model = result.Model as ForgotPasswordViewModel;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
*/