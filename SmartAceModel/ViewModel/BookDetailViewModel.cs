using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceModel.ViewModel
{
  public  class BookDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string National_ID { get; set; }
        public string Date_Check_Out { get; set; }
        public string Date_Return { get; set; }
        public string ISBN { get; set; }        
        public string Book_title { get; set; }
        public string Publish_Year { get; set; }
        public string Cover_Price { get; set; }
        public string Status { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}
