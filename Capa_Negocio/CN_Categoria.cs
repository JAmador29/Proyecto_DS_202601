using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Negocio
{
    public class CN_Categoria
    {
        // Método para instanciar el Categoria
        private CD_Categoria objCd_Categoria = new CD_Categoria();

        public List<Categoria> Listar()
        {
            return objCd_Categoria.Listar();
        }

        public int Registrar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario ingresar la descripción de la categoría.\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objCd_Categoria.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Descripcion == "")
            {
                Mensaje += "Es necesario ingresar la descripción de la categoría.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objCd_Categoria.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Categoria obj, out string Mensaje)
        {
            return objCd_Categoria.Eliminar(obj, out Mensaje);
        }
    }
}
