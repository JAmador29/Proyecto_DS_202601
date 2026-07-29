using System;

namespace Capa_Entidad
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public string TablaAfectada { get; set; }
        public string Accion { get; set; }
        public int? IdUsuario { get; set; } // Permite nulos por si el evento fue del sistema
        public string Detalle { get; set; }
        public string FechaRegistro { get; set; }

        // Propiedad de navegación para mostrar el nombre del usuario en las grillas de consulta
        public Usuario oUsuario { get; set; }
    }
}