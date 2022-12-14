using System;
using System.Collections.Generic;

namespace codename_boquete.Model
{
    public partial class CsrfReporteDeFuga
    {
        public string? NumSerie { get; set; }
        public string? NombreCoil { get; set; }
        public string? Seccion { get; set; }
        public string? Numero { get; set; }
        public string? Area { get; set; }
        public string? Tipo { get; set; }
        public string? Soldador { get; set; }
        public string? CoilRechazado { get; set; }
        public string? FugaFalsa { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Linea { get; set; }
        public int? Turno { get; set; }
        public string? Operador { get; set; }
        public string? Comentarios { get; set; }
        public string? PiezaRetrabajada { get; set; }
        public string? TipoExtra { get; set; }
        public string? CostXunit { get; set; }
    }
}
