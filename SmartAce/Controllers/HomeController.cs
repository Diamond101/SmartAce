using SmartAce.Filters;
using SmartAce.Models;
using SmartAceLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAce.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserLogic _usersLogic = new UserLogic();
        private readonly AuditTrialLogic _logger = new AuditTrialLogic();

        public ActionResult Index()
        {
           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginUser user)

        {
            var response = _usersLogic.LoginUser(user.Username, user.Password);
            if (response)
            {
                _logger.SaveAuditTrail("Login Successful.");
                return RedirectToAction("Dashboard", "Home");
            }
            ViewBag.Message = "Failed";
            _logger.SaveAuditTrail("Login Successful.");
            return View(user);
        }
        [AuthorizeSmartAceUsers]
        public ActionResult Dashboard()
        {
            var  Users = new UserLogic().CountUsers();
            var Books = new UserLogic().CountBooks();
            var Checkin = new UserLogic().CountCheckin();
            var Checkout = new UserLogic().CountCheckout();

            ViewBag.Total = Users;
            ViewBag.TotalBooks = Books;
            ViewBag.TotalCheckin = Checkin;
            ViewBag.TotalCheckout = Checkout;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Contents.RemoveAll();
            Session.Abandon();

            return RedirectToAction("index", "Home");
        }
    }
}