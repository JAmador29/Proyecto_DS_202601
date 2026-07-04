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
                // Hasheamos la clave en texto plano ANTES de enviarla a la capa de datos
                obj.Clave = BCrypt.Net.BCrypt.HashPassword(obj.Clave);

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
                // Solo re-hashear si la clave no es ya un hash BCrypt válido
                // (los hashes BCrypt siempre empiezan con $2a$, $2b$ o $2y$)
                bool yaEsHashBCrypt = obj.Clave.StartsWith("$2a$")
                                   || obj.Clave.StartsWith("$2b$")
                                   || obj.Clave.StartsWith("$2y$");

                if (!yaEsHashBCrypt)
                {
                    obj.Clave = BCrypt.Net.BCrypt.HashPassword(obj.Clave);
                }

                return objCd_usuario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objCd_usuario.Eliminar(obj, out Mensaje);
        }
    }
}