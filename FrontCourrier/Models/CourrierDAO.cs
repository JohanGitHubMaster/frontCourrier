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

        public List<Object> FindObject(string Myclass,Dictionary<string,string> condition,int? skip,int? take)
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

            if (condition != null)
            {
                queryStringModified += " WHERE ";
                int countcond=0;
                foreach (var item in condition)
                {
                    if (countcond++ > 0 || countcond < condition.Count-1)
                        queryStringModified += " AND ";
                    //if(Islike)
                        queryStringModified += $"([{item.Key}] Like '{item.Value}')";
                    //else
                    //    queryStringModified += $"[{ item.Key }] = '{item.Value }'";
                }
            }

            if (skip!=null && take!=null)
            {
                queryStringModified += " order by [Id] OFFSET "+ skip + " ROWS" +
                    " FETCH NEXT "+ take + " ROWS ONLY;";
            }

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
                        if(reader[count].GetType().FullName.ToLower().Contains("null"))
                            mi.SetValue(o, null);                      
                        else 
                            mi.SetValue(o, reader[count]);
                        
                        count++;
                    }
                    catch(Exception ex)
                    {
                        //mi.SetValue(o, reader[count]);
                    }
                 
                }               
                courriers.Add(o);
            }
            Connexion.con.Close();
            return courriers;
        }

        public object ElementCourrier(string element,int Id)
        {
            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("Id", Id.ToString());
            var result = FindObject(element, condition,null,null).FirstOrDefault();
            return result;
        }
    }
}