using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


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

        public bool Actualizar_Contraseña(string correo, string nuevaContraseña, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(nuevaContraseña))
            {
                Mensaje = "La nueva contraseña no puede estar vacía.";
                return false;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);
            return objCd_usuario.Actualizar_Contraseña(correo, hashedPassword, out Mensaje);
        }

        public bool Validar_Contraseña(string contraseña, out string Mensaje)
        {
            Mensaje = "";

            if (contraseña.Length < 8)
            {
                Mensaje = "La contraseña debe tener al menos 8 caracteres.";
                return false;
            }

            if (!contraseña.Any(char.IsUpper))
            {
                Mensaje = "La contraseña debe contener al menos una letra mayúscula.";
                return false;
            }

            if (!contraseña.Any(char.IsLower))
            {
                Mensaje = "La contraseña debe contener al menos una letra minúscula.";
                return false;
            }

            if (!contraseña.Any(char.IsDigit))
            {
                Mensaje = "La contraseña debe contener al menos un número.";
                return false;
            }

            if (!contraseña.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                Mensaje = "La contraseña debe contener al menos un carácter especial.";
                return false;
            }

            return true;
        }

        public bool Validar_ContraseñaAntigua(string correo, string contraseñaAntigua)
        {
            string contraseñaActual = objCd_usuario.Obtener_ClaveCorreo(correo);

            if(string.IsNullOrEmpty(contraseñaActual))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(contraseñaAntigua, contraseñaActual);
        }

        public bool Registrar_Bitacora(int idUsuario, string accion, string detalle, out string Mensaje)
        {
            return objCd_usuario.Registrar_Bitacora(idUsuario, accion, detalle, out Mensaje);
        }

        public bool Registrar_BitacoraSinUsuario(int idUsuario, string accion, string detalle, out string Mensaje)
        {
            return objCd_usuario.Registrar_BitacoraSinUsuario(idUsuario, accion, detalle, out Mensaje);
        }

        public bool Usuario_Bloqueado(int idUsuario)
        {
            return objCd_usuario.Usuario_Bloqueado(idUsuario);
        }

        public int Intentos_Fallidos(int idUsuario)
        {
            return objCd_usuario.Aumentar_Intentos(idUsuario);
        }

        public void Bloquear_Usuario(int idUsuario)
        {
            objCd_usuario.Bloquear_Usuario(idUsuario);
        }

        public bool Desbloquear_Usuario(int idUsuario, out string Mensaje)
        {
            return objCd_usuario.Desbloquear_Usuario(idUsuario, out Mensaje);
        }
    }
}