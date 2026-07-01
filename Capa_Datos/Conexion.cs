using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Capa_Datos
{
    public class Conexion
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["CadenaConexion"].ConnectionString;
    }
}
