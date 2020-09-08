using SmartAceData.Interface;
using SmartAceData.Repository;
using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceLogic
{
  public  class BooksLogic
    {
        private readonly IBooks _booksRepository = new BooksRepository();
        private readonly AuditTrialLogic _logger = new AuditTrialLogic();
        public Books FindBooksById(int id)
        {
            var user = _booksRepository.FindBooksById(id);
            return user;
        }

        public ICollection<Books> GetBooks()
        {
            return _booksRepository.GetBooks();
        }

        public ICollection<BooksViewModel> GetBookList()
        {
            return _booksRepository.GetBookList();
        }
        public void AddBooks(Books books)
        {
            books.Deleted = false;
            _booksRepository.AddBooks(books);
            _logger.SaveAuditTrail("New Books: " + books.Book_title + " " + books.Cover_Price + " created.");
        }

        public void UpdateBooks(Books books)
        {
            var currUser = FindBooksById(books.ID);
            currUser.Book_title = books.Book_title ?? currUser.Book_title;
            currUser.Cover_Price = books.Cover_Price?? currUser.Cover_Price;
            currUser.ISBN = books.ISBN ?? currUser.ISBN;
            currUser.Publish_Year = books.Publish_Year ?? currUser.Publish_Year;
            currUser.Status = books.Status?? currUser.Status;            
            _booksRepository.UpdateBooks(currUser);
            _logger.SaveAuditTrail("Books record: "  + books.Book_title + " " + books.Cover_Price +  " updated.");
        }

        public void DeleteBooks(int id)
        {
            var user = FindBooksById(id);
            user.Deleted = true;
            _booksRepository.UpdateBooks(user);
            _logger.SaveAuditTrail("Books record deleted.");
        }
    }
}