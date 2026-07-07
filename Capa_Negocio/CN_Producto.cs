using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Producto
    {
        // Método para instanciar el Producto
        private CD_Producto objCd_Producto = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCd_Producto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario ingresar el código del Producto.\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario ingresar el nombre del Producto.\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario ingresar la descripción del Producto.\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCd_Producto.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es necesario ingresar el código del Producto.\n";
            }

            if (obj.Nombre == "")
            {
                Mensaje += "Es necesario ingresar el nombre del Producto.\n";
            }

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario ingresar la descripción del Producto.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_Producto.Editar(obj, out Mensaje);
            }
        }
    }
}
