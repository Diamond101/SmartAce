using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData
{
   public class SmartAceDbContextww

    {
        public static class SmartAceConnection
        {
            public static string GetConnectionString()
            {
                return
                    ConfigurationManager.ConnectionStrings["SmartAceConnection"].ConnectionString;
            }
        }
    }
}

