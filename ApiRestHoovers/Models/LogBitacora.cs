using System;
using System.Collections.Generic;

#nullable disable

namespace ApiRestHoovers.Models
{
    public partial class LogBitacora
    {
        public int Id { get; set; }
        public int? IdModule { get; set; }
        public int? IdMethod { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaOperacion { get; set; }
        public string Usuario { get; set; }

        public virtual HttpMethod IdMethodNavigation { get; set; }
        public virtual Module IdModuleNavigation { get; set; }
    }
}
