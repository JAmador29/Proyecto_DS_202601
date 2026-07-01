using System;
using System.Collections.Generic;
using System.Text;

namespace Capa_Entidad
{
    public class Compras
    {
        public int IdCompra { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public List<Detalle_Compra> DetalleCompra { get; set; }
        public string FechaRegistro { get; set; }
        
    }
}
