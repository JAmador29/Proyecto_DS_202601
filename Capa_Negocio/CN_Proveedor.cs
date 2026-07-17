using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Linq;

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

            if (string.IsNullOrWhiteSpace(obj.RTN))
            {
                mensaje += "Es necesario el RTN del proveedor.\n";
            }
            else if (obj.RTN.Length != 14)
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

            return mensaje;
        }
    }
}