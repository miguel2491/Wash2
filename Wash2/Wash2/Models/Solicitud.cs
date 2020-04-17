using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.Models
{
    public class Solicitud
    {
        public int id_solicitud { get; set; }
        public int id_washer { get; set; }
        public int id_usuario { get; set; }
        public int id_auto { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string foto { get; set; }
        public string foto_washer { get; set; }
        public DateTime fecha { get; set; }
        public int calificacion { get; set; }
        public string comentario { get; set; }
        public string nombre { get; set; }
        public string app { get; set; }
        public string apm { get; set; }
        public int status { get; set; }
        //JOINS
        public string placas { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string ann { get; set; }
        public string paquete { get; set; }
        public string precio { get; set; }
        public string usuario { get; set; }
        public string tipo_vehiculo { get; set; }
    }
}
