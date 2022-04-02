using System;
using System.Collections.Generic;

#nullable disable

namespace ApiRestHoovers.Models
{
    public partial class HttpMethod
    {
        public HttpMethod()
        {
            LogBitacoras = new HashSet<LogBitacora>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<LogBitacora> LogBitacoras { get; set; }
    }
}
