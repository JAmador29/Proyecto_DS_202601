using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Negocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_venta = new CD_Venta();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return objcd_venta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return objcd_venta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_venta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_venta.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = objcd_venta.ObtenerVenta(numero);

            if (oVenta == null)
            {
                oVenta = new Venta();
            }

            if (oVenta.IdVenta != 0)
            {
                List<Detalle_Venta> detalles =
                    objcd_venta.ObtenerDetalleVenta(oVenta.IdVenta);

                oVenta.DetalleVenta =
                    detalles ?? new List<Detalle_Venta>();
            }
            else
            {
                oVenta.DetalleVenta = new List<Detalle_Venta>();
            }

            return oVenta;
        }
    }
}

