using System;
using System.Collections.Generic;
using System.Text;

using Capa_Datos;
using Capa_Entidad;


namespace Capa_Negocio
{
    public class CN_Usuario
    {
        // Método para instanciar el usuario
        private CD_Usuario objCd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objCd_usuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es Necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es Necesario la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCd_usuario.Registrar(obj, out Mensaje);
            }

           
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el documento del usuario\n";
            }

            if (obj.NombreCompleto == "")
            {
                Mensaje += "Es Necesario el nombre completo del usuario\n";
            }

            if (obj.Clave == "")
            {
                Mensaje += "Es Necesario la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_usuario.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objCd_usuario.Eliminar(obj, out Mensaje);
        }
    }
}
