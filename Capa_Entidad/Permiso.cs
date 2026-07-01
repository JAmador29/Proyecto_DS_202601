using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public Rol oROl { get; set; }
        public string NombreMenu { get; set; }
        public string FechaRegsitro { get; set; }
    }
}
