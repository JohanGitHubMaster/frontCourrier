using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FrontCourrier.Models
{
    public static class Connexion
    {
        

        public static SqlConnection con;

        public static void connection()
        {
            string costr = "Server = (localdb)\\MSSQLLocalDB; Database =CourrierContext; Trusted_Connection = true";
            con = new SqlConnection(costr);
        }
    }
}