using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public String Documento { get; set; }
        public String NombreCompleto { get; set; }
        public String Correo { get; set; }
        public String Clave { get; set; }
        public Rol oRol { get; set; }
        public bool Estado { get; set; }
        public  string FechaRegistro { get; set; }
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime? FechaBloqueo { get; set; }
    }
}
