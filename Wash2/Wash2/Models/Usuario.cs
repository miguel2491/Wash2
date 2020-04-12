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
        public int idB { set; get; }
        public int idWasher { set; get; }
        [MaxLength(200)]
        public string nombre { set; get; }
        public string app { set; get; }
        public string apm { set; get; }
        public string fecha { set; get; }
        public string telefono { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string email { set; get; }
        public string remember_token { set; get; }
        public string name { set; get; }
        public string google_id { set; get; }
        public string token { set; get; }
        public string foto { set; get; }
        public int status { set; get; }
    }
}
