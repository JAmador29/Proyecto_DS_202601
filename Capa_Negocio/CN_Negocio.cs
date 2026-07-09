using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Negocio
    {
        private CD_Negocio objCd_Negocio = new CD_Negocio();

        public Negocio ObtenerDatos()
        {
            return objCd_Negocio.ObtenerDatos();
        }

        public bool  GuardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario el nombre.\n";
            }

            if (obj.RTN == "")
            {
                Mensaje += "Es necesario el número de RTN.\n";
            }

            if (obj.Direccion == "")
            {
                Mensaje += "Es necesario la dirección.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_Negocio.GuardarDatos(obj, out Mensaje);
            }
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objCd_Negocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen,out string mensaje)
        {
            return objCd_Negocio.ActualizarLogo(imagen, out mensaje);
        }
    }
}
