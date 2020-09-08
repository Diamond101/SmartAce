using SmartAceData.Interface;
using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SmartAceData.SmartAceDbContextww;

namespace SmartAceData.Repository
{
  public  class BooksRepository:IBooks
    {
        private readonly SmartAceDBContext _db = new SmartAceDBContext();
        private static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(SmartAceConnection.GetConnectionString());
            conn.Close();

            return conn;
        }
        public void AddBooks(Books books)
        {         
            _db.Books.Add(books);
            Save();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool Disposing)
        {
            if (!this.disposed)
            {
                if (Disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Books FindBooksById(int id)
        {
            return _db.Books.FirstOrDefault(u => u.Deleted == false && u.ID == id);
        }

        public ICollection<Books> GetBooks()
        {
            return _db.Books
             .Where(u => u.Deleted == false)
               .ToList();
        }

        
        public void UpdateBooks(Books books)
        {
            _db.Entry(books).State = EntityState.Modified;
            Save();
        }
        void Save()
        {
            _db.SaveChanges();
        }

        public ICollection<BooksViewModel> GetBookList()
        {
            List<BooksViewModel> booklist = new List<BooksViewModel>();
            SqlConnection con = OpenConnection();
            {
                SqlCommand cmd = new SqlCommand("Book_GetList", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    BooksViewModel booksViewModel = new BooksViewModel();
                    booksViewModel.ISBN = Convert.ToInt32(dr["ISBN"].ToString());
                    booksViewModel.List = dr["List"].ToString();
                    booklist.Add(booksViewModel);
                }
                con.Close();
            }
            return booklist;
        }
    }
}

