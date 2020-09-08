using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData.Interface
{
   public interface IBooks : IDisposable
    {
        Books FindBooksById(int id);
        ICollection<Books> GetBooks();
        ICollection<BooksViewModel> GetBookList();
        void AddBooks(Books books);
        void UpdateBooks(Books books);
    }
}
