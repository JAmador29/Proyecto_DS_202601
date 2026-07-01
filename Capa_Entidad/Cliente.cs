using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Docuemento { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
