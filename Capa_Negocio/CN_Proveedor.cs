using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Capa_Negocio
{
    /// <summary>
    /// Capa de Negocio encargada de gestionar la lógica y validaciones de Proveedores.
    /// Cumple con el principio SRP manteniendo las reglas de negocio aisladas de la UI.
    /// </summary>
    public class CN_Proveedor
    {
        private readonly CD_Proveedor objCd_Proveedor = new CD_Proveedor();

        /// <summary>
        /// Obtiene la lista completa de proveedores registrados.
        /// </summary>
        public List<Proveedor> Listar()
        {
            return objCd_Proveedor.Listar();
        }

        /// <summary>
        /// Registra un nuevo proveedor previa validación de reglas de negocio.
        /// </summary>
        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = ValidarProveedor(obj, esEdicion: false);

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0; // Se retorna 0 para indicar que la operación falló
            }

            return objCd_Proveedor.Registrar(obj, out Mensaje);
        }

        /// <summary>
        /// Edita la información de un proveedor existente previa validación.
        /// </summary>
        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = ValidarProveedor(obj, esEdicion: true);

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false; // Se retorna false para indicar fallo en validación
            }

            return objCd_Proveedor.Editar(obj, out Mensaje);
        }

        /// <summary>
        /// Elimina un proveedor del sistema.
        /// </summary>
        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objCd_Proveedor.Eliminar(obj, out Mensaje);
        }

        #region Métodos Privados de Validación (Clean Code / DRY / SOLID)

        /// <summary>
        /// Unifica y evalúa todas las reglas de negocio aplicables a un proveedor.
        /// </summary>
        private string ValidarProveedor(Proveedor obj, bool esEdicion)
        {
            string mensaje = string.Empty;

            // Limpieza y formateo de datos de entrada
            obj.RTN = obj.RTN?.Trim() ?? string.Empty;
            obj.RazonSocial = Regex.Replace(obj.RazonSocial?.Trim() ?? string.Empty, @"\s+", " ");
            obj.Correo = obj.Correo?.Trim() ?? string.Empty;
            obj.Telefono = obj.Telefono?.Trim() ?? string.Empty;

            // 1. Validación de RTN (Obligatorio, 14 dígitos numéricos)
            if (string.IsNullOrWhiteSpace(obj.RTN))
            {
                mensaje += "Es necesario ingresar el RTN del proveedor.\n";
            }
            else if (obj.RTN.Length != 14 || !obj.RTN.All(char.IsDigit))
            {
                mensaje += "El RTN del proveedor debe contener exactamente 14 dígitos numéricos.\n";
            }

            // 2. Validación de Razón Social (Obligatorio, caracteres permitidos, máx 50 y NO duplicados)
            if (string.IsNullOrWhiteSpace(obj.RazonSocial))
            {
                mensaje += "Es necesaria la razón social del proveedor.\n";
            }
            else
            {
                if (obj.RazonSocial.Length > 50)
                {
                    mensaje += "La Razón Social no puede superar los 50 caracteres.\n";
                }

                // Permite letras, números, espacios, '&' y '.'
                bool razonSocialValida = obj.RazonSocial.All(caracter =>
                    char.IsLetterOrDigit(caracter) ||
                    char.IsWhiteSpace(caracter) ||
                    caracter == '&' ||
                    caracter == '.');

                if (!razonSocialValida)
                {
                    mensaje += "La Razón Social contiene caracteres no permitidos.\n";
                }
                else
                {
                    // VALIDACIÓN DE DUPLICIDAD EN RAZÓN SOCIAL
                    List<Proveedor> listaProveedores = objCd_Proveedor.Listar();
                    bool existeDuplicado = listaProveedores.Any(p =>
                        p.RazonSocial.Equals(obj.RazonSocial, StringComparison.OrdinalIgnoreCase) &&
                        (!esEdicion || p.IdProveedor != obj.IdProveedor));

                    if (existeDuplicado)
                    {
                        mensaje += $"La Razón Social '{obj.RazonSocial}' ya se encuentra registrada en el sistema.\n";
                    }
                }
            }

            // 3. Validación de Correo (Obligatorio, Máx 50, formato y lista blanca de dominios)
            if (string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje += "Es necesario ingresar el correo del proveedor.\n";
            }
            else if (obj.Correo.Length > 50)
            {
                mensaje += "El correo electrónico no puede superar los 50 caracteres.\n";
            }
            else if (!CorreoYDominioValido(obj.Correo))
            {
                mensaje += "El correo electrónico no es válido. Solo se permiten dominios de: gmail.com, yahoo.com, outlook.com y hotmail.com.\n";
            }

            // 4. Validación de Teléfono (Obligatorio, exactamente 8 dígitos numéricos)
            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                mensaje += "Es necesario ingresar el teléfono del proveedor.\n";
            }
            else if (obj.Telefono.Length != 8 || !obj.Telefono.All(char.IsDigit))
            {
                mensaje += "El teléfono debe contener exactamente 8 dígitos numéricos.\n";
            }

            return mensaje;
        }

        /// <summary>
        /// Evalúa si el correo cumple con el formato estándar y pertenece a los dominios autorizados.
        /// </summary>
        private bool CorreoYDominioValido(string correo)
        {
            string patronRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(correo, patronRegex))
            {
                return false;
            }

            string[] dominiosValidos = { "gmail.com", "yahoo.com", "outlook.com", "hotmail.com" };
            string dominio = correo.Split('@').LastOrDefault()?.ToLower();

            return dominiosValidos.Contains(dominio);
        }

        #endregion
    }
}