using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Categoria oCategoria { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Estado { get; set; }
        public string FechaRegistro { get; set; }

        // >>> PROPIEDAD AGREGADA PARA LA BITÁCORA <<<
        public Usuario oUsuario { get; set; }
    }
}