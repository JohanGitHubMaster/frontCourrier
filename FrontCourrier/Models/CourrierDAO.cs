using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Xml.Linq;

namespace FrontCourrier.Models
{
    public class CourrierDAO
    {
        public CourrierDAO() { }

        static object caller(string myclass)
        {
            // Get a type from the string 
            Type type = Type.GetType(myclass);
            // Create an instance of that type
            Object obj = Activator.CreateInstance(type);
            return obj;
        }

        public List<String> getProperties(string Myclass)
        {
            Type type = Type.GetType(Myclass);
            List<String> list = new List<String>();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (PropertyInfo mi in propertyInfos)
            {
                list.Add(mi.Name);
            }
            return list;
        }

        public List<Object> FindObject(string Myclass)
        {                                            
            Connexion.connection();
            List<Object> courriers = new List<Object>();
            var properties = getProperties("FrontCourrier.Models."+Myclass);

            string queryStringModified = "SELECT ";
            int c = 0;
            foreach (string pi in properties)
            {
                if(c++ == properties.Count-1) queryStringModified += " [" + pi + "]";
                else queryStringModified += " [" + pi + "],";
            
            }
            if (Myclass == "Courriers")
                queryStringModified += " FROM [CourrierContext].[dbo]." + Myclass.Substring(0, Myclass.Length - 1);
            else
                queryStringModified += " FROM [CourrierContext].[dbo]." + Myclass;

            SqlCommand cmd = new SqlCommand(queryStringModified, Connexion.con);
            Connexion.con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            PropertyInfo readercount;
            while (reader.Read())
            {
                int count = 0;
                object o = caller("FrontCourrier.Models." + Myclass);
                PropertyInfo[] propertyInfos = o.GetType().GetProperties();
                foreach (PropertyInfo mi in propertyInfos)
                {
                    try
                    {
                        readercount = mi;
                        mi.SetValue(o, reader[count]);
                        count++;
                    }
                    catch(Exception ex)
                    {
                        mi.SetValue((DateTime?)o, reader[count]);
                    }
                 
                }               
                courriers.Add(o);
            }
            Connexion.con.Close();
            return courriers;
        }
    }
}