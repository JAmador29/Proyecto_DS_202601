using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Data;

namespace Capa_Negocio
{
    /// <summary>
    /// Capa de Negocio encargada de gestionar el flujo y validaciones operativas de Ventas.
    /// </summary>
    public class CN_Venta
    {
        private readonly CD_Venta _objcdVenta = new CD_Venta();

        public bool RestarStock(int idproducto, int cantidad)
        {
            return _objcdVenta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad)
        {
            return _objcdVenta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return _objcdVenta.ObtenerCorrelativo();
        }

        public bool Registrar(Venta obj, DataTable detalleVenta, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj == null)
            {
                mensaje = "El objeto de venta no puede ser nulo.";
                return false;
            }

            if (obj.MontoPago < obj.MontoTotal)
            {
                mensaje = "El monto pagado no puede ser menor que el total de la venta.";
                return false;
            }

            if (detalleVenta == null || detalleVenta.Rows.Count == 0)
            {
                mensaje = "Debe ingresar al menos un producto en la venta.";
                return false;
            }

            // Asignación explícita del cambio
            obj.MontoCambio = obj.MontoPago - obj.MontoTotal;

            return _objcdVenta.Registrar(obj, detalleVenta, out mensaje);
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = _objcdVenta.ObtenerVenta(numero) ?? new Venta();

            if (oVenta.IdVenta != 0)
            {
                List<Detalle_Venta> detalles = _objcdVenta.ObtenerDetalleVenta(oVenta.IdVenta);
                oVenta.DetalleVenta = detalles ?? new List<Detalle_Venta>();
            }
            else
            {
                oVenta.DetalleVenta = new List<Detalle_Venta>();
            }

            return oVenta;
        }
    }
}