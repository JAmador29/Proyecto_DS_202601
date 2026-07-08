using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCd_Cliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objCd_Cliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es Necesario el nombre completo del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCd_Cliente.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el documento del Cliente\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es Necesario el nombre completo del Cliente\n";
            }

            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_Cliente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objCd_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
