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
  public  class BorrowLogic
    {
        private readonly IBorrow _booksdetailRepository = new BorrowRepository();
        private readonly AuditTrialLogic _logger = new AuditTrialLogic();
        public BookDetailViewModel FindBooksDetailById(int id)
        {
            var user = _booksdetailRepository.FindBookDetailById(id);
            return user;
        }


        public void AddBorrow(Borrow borrow)
        {
            borrow.Date_Check_Out = DateTime.UtcNow;
            borrow.Date_Return = DateTime.UtcNow.AddDays(19);
            _booksdetailRepository.AddBookDetailViewModel(borrow);
            _logger.SaveAuditTrail("Borrow Books: " + borrow.Name + " " + borrow.Mobile + " created.");
        }

        public void UpdateBookDetailViewModel (Borrow borrow)
        {
            _booksdetailRepository.UpdateBookDetailViewModel(borrow);
        }
    }
}
