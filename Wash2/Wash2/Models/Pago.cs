using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.Models
{
    public class Pago
    {
        public int id_pago { get; set; }
        public int id_usuario { get; set; }
        public int id_solicitud { get; set; }
        public int id_washer { get; set; }
        public decimal monto { get; set; }
        public decimal cambio { get; set; }
        public int tipo_pago { get; set; }
        public int status { get; set; }
        public DateTime created_at { get; set; }
        public string comentario { get; set; }
        //
        public decimal ganancias { get; set; }
    }
}
