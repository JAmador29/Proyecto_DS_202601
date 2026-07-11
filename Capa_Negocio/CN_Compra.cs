using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Compra
    {

        private CD_Compra objcd_compra = new CD_Compra();

        public int ObtenerCorrelativo()
        {
            return objcd_compra.ObtenerCorrelativo();
        }

        public bool Registrar(Compras obj,DataTable DetalleCompra, out string Mensaje)
        {
            return objcd_compra.Registrar(obj,DetalleCompra, out Mensaje);
        }

        public Compras ObtenerCompra(string numero)
        {

            Compras oCompra = objcd_compra.ObtenerCompra(numero);

            if (oCompra.IdCompra != 0)
            {
                List<Detalle_Compra> oDetalleCompra = objcd_compra.ObtenerDetalleCompra(oCompra.IdCompra);

                oCompra.DetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }

    }
}
