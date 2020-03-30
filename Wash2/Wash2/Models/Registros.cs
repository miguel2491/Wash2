using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Wash2.Models
{
    public class Registros
    {
        [PrimaryKey, AutoIncrement]
        public int id { set; get; }
        public string nombre { set; get; }
        public string app { set; get; }
        public string apm { set; get; }
        public string fecha { set; get; }
        public string telefono { set; get; }
        public string correo { set; get; }
        public string password { set; get; }
        public int paquete { set; get; }
    }
}
