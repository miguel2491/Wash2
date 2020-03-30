using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int id { set; get; }
        public string nombre { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string email { set; get; }
        public string remember_token { set; get; }
        public string name { set; get; }
        public string google_id { set; get; }
        public string token { set; get; }
        public int status { set; get; }
    }
}
