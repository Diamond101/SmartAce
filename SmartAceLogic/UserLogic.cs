using SmartAceData.Interface;
using SmartAceData.Repository;
using SmartAceModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartAceLogic
{
  public  class UserLogic
    {
        private readonly IUser _usersRepository = new UserRepository();
        private readonly AuditTrialLogic _logger = new AuditTrialLogic();
        public Users FindUserById(int id)
        {
            var user = _usersRepository.FindUserById(id);
            return user;
        }

        public ICollection<Users> GetUsers()
        {
            return _usersRepository.GetUsers();
        }

        public void AddUser(Users user)
        {
            var fullname = user.FirstName + " " + user.Surname;
            user.Deleted = false;
            user.ContactAddress = "uu";
            user.DateCreated = DateTime.UtcNow;
            _usersRepository.AddUser(user);
            _logger.SaveAuditTrail("New User: " + user.FirstName + " " + user.Surname + " created.");
        }

        public void UpdateUser(Users user)
        {
            var currUser = FindUserById(user.ID);
            currUser.FirstName = user.FirstName;
            currUser.Surname = user.Surname;
            currUser.Gender = user.Gender;
            currUser.ContactAddress = user.ContactAddress;
            currUser.Email = user.Email;
            currUser.Phone = user.Phone;
            currUser.Role = user.Role ?? currUser.Role;
            currUser.Status = user.Status;
            currUser.Image = user.Image ?? currUser.Image;
            currUser.Password = user.Password ?? currUser.Password;
            _usersRepository.UpdateUser(currUser);
             _logger.SaveAuditTrail("User record: " + user.FirstName + " " + user.Surname + " updated.");
        }

        public bool LoginUser(string username, string password)
        {
            Users user = _usersRepository.LoginUser(username, password);
            if (user != null)
            {
                CreateUserSession(user);
                return true;
            }

            HttpContext.Current.Session["FailedUser"] = username;
            return false;

        }

        void CreateUserSession(Users user)
        {
            UserSession userSession = new UserSession()
            {
                UserID = user.ID,
                Fullname = user.Fullname,
                Email = user.Email,
                Role = user.Role,
                Image = user.Image
            };
            HttpContext.Current.Session["UserSession"] = userSession;
        }

        public void DeleteUser(int id)
        {
            var user = FindUserById(id);
            user.Deleted = true;
            _usersRepository.UpdateUser(user);
             _logger.SaveAuditTrail("User record deleted.");
        }
        public int CountUsers()
        {
            return _usersRepository.CountUsers();
        }
        public int CountBooks()
        {
            return _usersRepository.CountBooks();
        }
        public int CountCheckin()
        {
            return _usersRepository.CountCheckin();
        }
        public int CountCheckout()
        {
            return _usersRepository.CountCheckout();
        }
    }
}