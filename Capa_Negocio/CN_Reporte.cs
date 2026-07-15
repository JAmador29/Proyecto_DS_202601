using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Reporte
    {
        private CD_Reporte objcd_reporte = new CD_Reporte();

        public List<ReporteCompra> Compra(DateTime fechainicio, DateTime fechafin, int idproveedor)
        {
            if (fechainicio.Date > fechafin.Date)
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin.");

            return objcd_reporte.Compra(fechainicio, fechafin, idproveedor);
        }

        public List<ReporteVenta> Venta(DateTime fechainicio, DateTime fechafin)
        {
            if (fechainicio.Date > fechafin.Date)
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin.");

            return objcd_reporte.Venta(fechainicio, fechafin);
        }
    }
}