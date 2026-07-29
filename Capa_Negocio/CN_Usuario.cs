using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Capa_Negocio
{
    public class CN_Usuario
    {
        // Instancia a la capa de datos (Principio de Capas)
        private readonly CD_Usuario objCd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objCd_usuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            // Validar campos de negocio antes de llamar a la base de datos
            if (!ValidarUsuario(obj, out Mensaje))
            {
                return 0;
            }

            // Hasheamos la clave en texto plano ANTES de enviarla a la capa de datos
            obj.Clave = BCrypt.Net.BCrypt.HashPassword(obj.Clave);

            return objCd_usuario.Registrar(obj, out Mensaje);
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            // Validar campos de negocio antes de editar
            if (!ValidarUsuario(obj, out Mensaje))
            {
                return false;
            }

            // Solo re-hashear si la clave no es ya un hash BCrypt válido
            // (Los hashes BCrypt siempre empiezan con $2a$, $2b$ o $2y$)
            bool yaEsHashBCrypt = obj.Clave.StartsWith("$2a$")
                               || obj.Clave.StartsWith("$2b$")
                               || obj.Clave.StartsWith("$2y$");

            if (!yaEsHashBCrypt)
            {
                obj.Clave = BCrypt.Net.BCrypt.HashPassword(obj.Clave);
            }

            return objCd_usuario.Editar(obj, out Mensaje);
        }

        #region Recuperación de Contraseña y Correo

        public bool SolicitarCodigoRecuperacion(string correo, out string codigoGenerado, out string mensajeError)
        {
            codigoGenerado = string.Empty;
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(correo))
            {
                mensajeError = "Por favor, ingrese su correo electrónico.";
                return false;
            }

            // Validar formato de correo (Regex)
            string patronCorreo = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(correo.Trim(), patronCorreo))
            {
                mensajeError = "El formato del correo electrónico no es válido.";
                return false;
            }

            // Verificar si el correo existe en la BD
            if (!CD_Usuario.Correo_Existe(correo.Trim()))
            {
                mensajeError = "El correo electrónico ingresado no está registrado.";
                return false;
            }

            // Generar código de 6 dígitos
            codigoGenerado = new Random().Next(100000, 999999).ToString();

            // Enviar correo SMTP
            return EnviarCorreoRecuperacion(correo.Trim(), codigoGenerado, out mensajeError);
        }

        private bool EnviarCorreoRecuperacion(string destino, string codigo, out string mensajeError)
        {
            mensajeError = string.Empty;

            try
            {
                string correoOrigen = ConfigurationManager.AppSettings["CorreoSoporte"];
                string claveApp = ConfigurationManager.AppSettings["ClaveAppCorreo"];

                if (string.IsNullOrEmpty(correoOrigen) || string.IsNullOrEmpty(claveApp))
                {
                    mensajeError = "No se encontró la configuración del servidor de correo.";
                    return false;
                }

                using (MailMessage mensaje = new MailMessage())
                {
                    mensaje.From = new MailAddress(correoOrigen, "Soporte Loboru Sublima");
                    mensaje.To.Add(destino);
                    mensaje.Subject = "Código de recuperación - Loboru Sublima";
                    mensaje.Body = $"Tu código de recuperación es: {codigo}";

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(correoOrigen, claveApp);
                        smtp.EnableSsl = true;
                        smtp.Send(mensaje);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                mensajeError = "Error al enviar el correo: " + ex.Message;
                return false;
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

            // Validar fortaleza de la contraseña
            if (!Validar_Contraseña(nuevaContraseña, out Mensaje))
            {
                return false;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(nuevaContraseña);
            return objCd_usuario.Actualizar_Contraseña(correo, hashedPassword, out Mensaje);
        }

        public bool Validar_Contraseña(string contraseña, out string Mensaje)
        {
            Mensaje = string.Empty;

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

            if (string.IsNullOrEmpty(contraseñaActual))
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(contraseñaAntigua, contraseñaActual);
        }

        #endregion

        #region Bitácora e Intentos

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

        #endregion

        #region Métodos Privados de Validación (DRY / SRP)

        private bool ValidarUsuario(Usuario obj, out string mensaje)
        {
            mensaje = string.Empty;

            // 1. Validar Documento
            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                mensaje += "Es necesario ingresar el documento del usuario.\n";
            }

            // 2. Validar Nombre Completo (Sin números ni caracteres extraños)
            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
            {
                mensaje += "Es necesario ingresar el nombre completo del usuario.\n";
            }
            else
            {
                // Normalizar espacios múltiples
                string nombreLimpio = Regex.Replace(obj.NombreCompleto.Trim(), @"\s+", " ");

                // Expresión Regular: Solo permite letras de cualquier alfabeto (incluyendo tildes y ñ) y espacios
                string patronSoloLetras = @"^[\p{L}\s]+$";

                if (!Regex.IsMatch(nombreLimpio, patronSoloLetras))
                {
                    mensaje += "El nombre completo no puede contener números ni caracteres especiales.\n";
                }
                else
                {
                    obj.NombreCompleto = nombreLimpio; // Asignar el nombre formateado
                }
            }

            // 3. Validar Clave
            if (string.IsNullOrWhiteSpace(obj.Clave))
            {
                mensaje += "Es necesario ingresar la clave del usuario.\n";
            }

            // Si acumuló algún mensaje, retorna false
            return string.IsNullOrEmpty(mensaje);
        }

        #endregion
    }
}   