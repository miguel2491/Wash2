using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.Models
{
    public class PaqueteDetalle
    {
        public int id { get;  set; }
        public int id_paquete { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string tipo_vehiculo { get; set; }
        public DateTime duracion { get; set; }
        public decimal precio { get; set; }
        public int status { get; set; }
    }
}
