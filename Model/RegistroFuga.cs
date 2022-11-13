using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codename_boquete.Model
{
    public class RegistroFuga
    {
        
        public string NumSerie { get; set; }
        public bool CoilParaScrap { get; set; }
        public bool FugaFalsa { get; set; }
        public int Linea { get; set; }
        public int Turno { get; set; }
        public string RetrabajadorSelect { get; set; }
        public string NombreCoil { get; set; }
    }
}
