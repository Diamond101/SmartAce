using SmartAce.Filters;
using SmartAceLogic;
using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAce.Controllers
{
    [AuthorizeSmartAceUsers]
    public class CheckinCheckOutController : Controller
    {
        private readonly BooksLogic _booksLogic = new BooksLogic();
        private readonly BorrowLogic _borrowLogic = new BorrowLogic();
        public ActionResult Index()
        {
            LoadDropDown();
            var books = _booksLogic.GetBooks();
            return View(books);
        }
       

        [HttpPost]
        public JsonResult Index11(int id)
        {
            BookDetailViewModel bookDetailViewModel = _borrowLogic.FindBooksDetailById(id);
            return Json(bookDetailViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Index101(int id)
        {
            BookDetailViewModel bookDetailViewModel = _borrowLogic.FindBooksDetailById(id);
            return Json(bookDetailViewModel, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Create(Borrow borrow)
        {
            LoadDropDown();
            _borrowLogic.AddBorrow(borrow);
            var users = _booksLogic.GetBooks();
            return PartialView("_list", users);
        }
        private void LoadDropDown()
        {
            ViewBag.books = new SelectList(new BooksLogic().GetBookList(), "ISBN", "List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _Edit(Borrow borrow)

        {
            _borrowLogic.UpdateBookDetailViewModel(borrow);
            var users = _booksLogic.GetBooks();
            return PartialView("_list", users);
        }
    }
}