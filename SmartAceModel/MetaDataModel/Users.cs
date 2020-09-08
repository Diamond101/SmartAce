using System;
using System.ComponentModel.DataAnnotations;


namespace SmartAceModel
{
    [MetadataType(typeof(UsersMetadata))]
  
    public partial class Users
    {
        public string Fullname { get { return Surname + " " + FirstName; } }
    }

    public class UsersMetadata
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string ContactAddress { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string Status { get; set; }
    }
}
