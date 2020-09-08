using SmartAce.Filters;
using SmartAceLogic;
using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAce.Controllers
{
    [AuthorizeSmartAceUsers]
    public class BooksController : Controller
    {
        private readonly BooksLogic _booksLogic = new BooksLogic();

        public ActionResult Index()
        {
            var books = _booksLogic.GetBooks();
            return View(books);
        }

        [HttpPost]
        public JsonResult _LoadEdit(int id)
        {
            Books books = _booksLogic.FindBooksById(id);
            return Json(books, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Edit(Books books)
        {
            _booksLogic.UpdateBooks(books);
            var users = _booksLogic.GetBooks();
            return PartialView("_list", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Create(Books books)
        {
            _booksLogic.AddBooks(books);
            var users = _booksLogic.GetBooks();
            return PartialView("_list", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Delete(int id)
        {
            _booksLogic.DeleteBooks(id);
            var users = _booksLogic.GetBooks();
            return PartialView("_list", users);
        }
    }
}