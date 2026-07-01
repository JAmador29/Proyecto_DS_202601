using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string RTN { get; set; }
        public string RazonSocial { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
