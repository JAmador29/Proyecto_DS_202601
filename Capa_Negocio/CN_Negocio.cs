using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Negocio
    {
        private readonly CD_Negocio objCd_Negocio = new CD_Negocio();

        public Negocio ObtenerDatos()
        {
            return objCd_Negocio.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio obj, out string mensaje, out string campoConError)
        {
            mensaje = string.Empty;
            campoConError = string.Empty;

            // --- VALIDACIONES DE NEGOCIO ---
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje = "Es necesario ingresar el nombre.";
                campoConError = "Nombre";
                return false;
            }
            if (obj.Nombre.Trim().Length > 60)
            {
                mensaje = "El campo 'Nombre' no puede superar los 60 caracteres.";
                campoConError = "Nombre";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.RTN))
            {
                mensaje = "Es necesario ingresar el número de RTN.";
                campoConError = "RTN";
                return false;
            }
            if (obj.RTN.Trim().Length > 14)
            {
                mensaje = "El campo 'RTN' no puede superar los 14 caracteres.";
                campoConError = "RTN";
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Direccion))
            {
                mensaje = "Es necesario ingresar la dirección.";
                campoConError = "Direccion";
                return false;
            }
            if (obj.Direccion.Trim().Length > 200)
            {
                mensaje = "El campo 'Dirección' no puede superar los 200 caracteres.";
                campoConError = "Direccion";
                return false;
            }

            // Si pasa todas las validaciones, lo envía a Capa_Datos
            return objCd_Negocio.GuardarDatos(obj, out mensaje);
        }

        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objCd_Negocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return objCd_Negocio.ActualizarLogo(imagen, out mensaje);
        }
    }
}