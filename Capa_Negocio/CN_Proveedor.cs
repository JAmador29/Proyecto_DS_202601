using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
            Mensaje = ValidarProveedor(obj);

            if (Mensaje != string.Empty)
            {
                return 0;
            }

            return objCd_Proveedor.Registrar(obj, out Mensaje);
        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = ValidarProveedor(obj);

            if (Mensaje != string.Empty)
            {
                return false;
            }

            return objCd_Proveedor.Editar(obj, out Mensaje);
        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objCd_Proveedor.Eliminar(obj, out Mensaje);
        }

        private string ValidarProveedor(Proveedor obj)
        {
            string mensaje = string.Empty;

            obj.RTN = obj.RTN.Trim();
            obj.RazonSocial = obj.RazonSocial.Trim();
            obj.Correo = obj.Correo.Trim();
            obj.Telefono = obj.Telefono.Trim();

            if (string.IsNullOrWhiteSpace(obj.RTN))
            {
                mensaje += "Es necesario el RTN del proveedor.\n";
            }
            else if (!TieneLongitudValida(obj.RTN, 14, 14))
            {
                mensaje += "El RTN del proveedor debe contener 14 dígitos.\n";
            }
            else if (!obj.RTN.All(char.IsDigit))
            {
                mensaje += "El RTN del proveedor solo debe contener números.\n";
            }

            if (string.IsNullOrWhiteSpace(obj.RazonSocial))
            {
                mensaje += "Es necesaria la razón social del proveedor.\n";
            }
            else
            {
                bool razonSocialValida = obj.RazonSocial.All(caracter =>
                    char.IsLetterOrDigit(caracter) ||
                    char.IsWhiteSpace(caracter) ||
                    caracter == '&' ||
                    caracter == '.');

                if (!razonSocialValida)
                {
                    mensaje += "La razón social contiene caracteres no permitidos.\n";
                }
            }

            if (string.IsNullOrWhiteSpace(obj.Correo))
            {
                mensaje += "Es necesario el correo del proveedor.\n";
            }
            else if (!CorreoValido(obj.Correo))
            {
                mensaje += "Debe ingresar un correo electrónico válido.\n";
            }

            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                mensaje += "Es necesario el teléfono del proveedor.\n";
            }
            else if (!TieneLongitudValida(obj.Telefono, 8, 8))
            {
                mensaje += "El teléfono debe contener exactamente 8 dígitos.\n";
            }
            else if (!obj.Telefono.All(char.IsDigit))
            {
                mensaje += "El teléfono solo debe contener números.\n";
            }

            return mensaje;
        }

        private bool TieneLongitudValida(string texto, int minimo, int maximo)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            texto = texto.Trim();
            return texto.Length >= minimo && texto.Length <= maximo;
        }

        private bool CorreoValido(string correo)
        {
            string patron = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
            return Regex.IsMatch(correo, patron);
        }
    }
}