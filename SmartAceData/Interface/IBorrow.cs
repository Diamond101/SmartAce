using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData.Interface
{
   public interface IBorrow:IDisposable
    {
        BookDetailViewModel FindBookDetailById(int id);
        //BookDetailViewModel FindBookDetailViewModelById(int id);
        //ICollection<BookDetailViewModel> GetBookDetailViewModel();
        void AddBookDetailViewModel(Borrow borrow);
        void UpdateBookDetailViewModel(Borrow borrow);
    }
}
