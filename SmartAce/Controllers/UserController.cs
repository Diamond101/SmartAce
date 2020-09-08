using SmartAce.Filters;
using SmartAce.Models;
using SmartAceLogic;
using SmartAceModel;
using SmartAceModel.SecurityLayer;
using System;
using System.Web.Mvc;

namespace SmartAce.Controllers
{
    [AuthorizeSmartAceUsers]
    public class UserController : Controller
    {
        private readonly UserLogic _usersLogic = new UserLogic();
        private readonly AuditTrialLogic _logger = new AuditTrialLogic();

        public ActionResult Index()
        {
            var users = _usersLogic.GetUsers();
            return View(users);
        }

        [HttpPost]
        public JsonResult _LoadEdit(int id)
        {
            Users user = _usersLogic.FindUserById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Edit(Users user)
        {
            if (!String.IsNullOrEmpty(user.Password))
            {
                string encryptedPassword = new SecurityLayer().Encrypt(user.Password);
                user.Password = encryptedPassword;
            }

            _usersLogic.UpdateUser(user);
            var users = _usersLogic.GetUsers();
            return PartialView("_list", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Create(Users user)
        {
            _usersLogic.AddUser(user);
            var users = _usersLogic.GetUsers();
            return PartialView("_list", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Delete(int id)
        {
            _usersLogic.DeleteUser(id);
            var users = _usersLogic.GetUsers();
            return PartialView("_list", users);
        }

       
    }
}