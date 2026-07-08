using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objCd_Proveedor = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            return objCd_Proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.RTN == "")
            {
                Mensaje += "Es Necesario el RTN del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es Necesario la razon social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCd_Proveedor.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.RTN == "")
            {
                Mensaje += "Es Necesario el RTN del Proveedor\n";
            }

            if (obj.RazonSocial == "")
            {
                Mensaje += "Es Necesario la razon social del Proveedor\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_Proveedor.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objCd_Proveedor.Eliminar(obj, out Mensaje);
        }
    
    }
}
