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
    }
}
