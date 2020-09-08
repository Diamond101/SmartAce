using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData.Interface
{
  public  interface IUser :IDisposable
    {
        Users FindUserById(int id);
        ICollection<Users> GetUsers();
        void AddUser(Users user);
        void UpdateUser(Users user);
        Users LoginUser(string Email, string password);
        int CountUsers();
        int CountBooks();
        int CountCheckin();
        int CountCheckout();
    }
}
