using System;
using System.Collections.Generic;
using System.Text;

namespace Wash2.Models
{
    public class Autos
    {
        public int id_auto { get; set; }
        public int id_usuario { get; set; }
        public string placas { get; set; }
        public string modelo { get; set; }
        public DateTime ann { get; set; }
        public string marca { get; set; }
        public string color { get; set; }
        public string imagen { get; set; }
    }
}
