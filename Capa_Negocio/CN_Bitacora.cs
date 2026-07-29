using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;

namespace Capa_Negocio
{
    public class CN_Bitacora
    {
        private readonly CD_Bitacora _cdBitacora = new CD_Bitacora();

        /// <summary>
        /// Obtiene la lista de registros de auditoría para mostrar en la pantalla de bitácora.
        /// </summary>
        public List<Bitacora> Listar()
        {
            return _cdBitacora.Listar();
        }
    }
}