using System;

namespace Capa_Entidad
{
    /// <summary>
    /// Entidad que representa la categoría de un producto.
    /// Incluye la referencia al usuario activo para mantener la trazabilidad de auditoría y bitácora.
    /// </summary>
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

        /// <summary>
        /// Usuario que ejecuta la operación (Requerido para la bitácora/auditoría).
        /// </summary>
        public Usuario oUsuario { get; set; }
    }
}