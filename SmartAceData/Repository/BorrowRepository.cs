using SmartAceData.Interface;
using SmartAceModel;
using SmartAceModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using static SmartAceData.SmartAceDbContextww;

namespace SmartAceData.Repository
{
    public class BorrowRepository : IBorrow
    {
        private readonly SmartAceDBContext _db = new SmartAceDBContext();
        private static SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(SmartAceConnection.GetConnectionString());
            conn.Close();

            return conn;
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



        public BookDetailViewModel FindBookDetailById(int id)
        {
            SqlConnection con = OpenConnection();
            BookDetailViewModel books = new BookDetailViewModel();
            {
                SqlCommand cmd = new SqlCommand("Book_Detail_FindByID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    books.Book_title = dr["Book_title"].ToString();
                    books.Cover_Price = dr["Cover_Price"].ToString();
                    books.Date_Check_Out = dr["Date_Check_Out"].ToString();
                    books.Date_Return = dr["Date_Return"].ToString();
                    books.ISBN = dr["ISBN"].ToString();
                    books.Mobile = dr["Mobile"].ToString();
                    books.Name = dr["Name"].ToString();
                    books.National_ID = dr["National_ID"].ToString();
                    books.Publish_Year = dr["Publish_Year"].ToString();
                    books.Status = dr["Status"].ToString();
                }
                con.Close();
            }
            return books;

        }


        public void AddBookDetailViewModel(Borrow borrow)
        {
            SqlConnection con = OpenConnection();
            {
                SqlCommand cmd = new SqlCommand("Borrow_Add", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", borrow.Name);
                cmd.Parameters.AddWithValue("@Mobile", borrow.Mobile);
                cmd.Parameters.AddWithValue("@National_ID", borrow.National_ID);
                cmd.Parameters.AddWithValue("@Date_Check_Out", borrow.Date_Check_Out);
                cmd.Parameters.AddWithValue("@Date_Return", borrow.Date_Return);
                cmd.Parameters.AddWithValue("@ISBN", borrow.ISBN);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateBookDetailViewModel(Borrow borrow)
        {
            SqlConnection con = OpenConnection();
            {
                SqlCommand cmd = new SqlCommand("Check_In", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ISBN", borrow.ISBN);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
