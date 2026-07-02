using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capa_Negocio
{
    public class CN_Permiso
    {

        private CD_Permiso objcd_Permiso = new CD_Permiso();


        public List<Permiso> Listar(int IdUsuario)
        {
            return objcd_Permiso.Listar(IdUsuario);
        }
    }
}
