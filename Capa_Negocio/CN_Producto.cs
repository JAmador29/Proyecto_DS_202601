using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Capa_Negocio
{
    public class CN_Producto
    {
        private readonly CD_Producto objCd_Producto = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCd_Producto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            if (!ValidarProducto(obj, out Mensaje))
            {
                return 0;
            }

            return objCd_Producto.Registrar(obj, out Mensaje);
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            if (!ValidarProducto(obj, out Mensaje))
            {
                return false;
            }

            return objCd_Producto.Editar(obj, out Mensaje);
        }

        #region Métodos Privados de Validación (SOLID / DRY)

        private bool ValidarProducto(Producto obj, out string mensaje)
        {
            mensaje = string.Empty;

            // 1. Validaciones para el CÓDIGO
            if (string.IsNullOrWhiteSpace(obj.Codigo))
            {
                mensaje += "Es necesario ingresar el código del Producto.\n";
            }
            else if (obj.Codigo.Trim().Length > 50)
            {
                mensaje += "El código del Producto no puede superar los 50 caracteres.\n";
            }

            // 2. Validaciones para el NOMBRE
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje += "Es necesario ingresar el nombre del Producto.\n";
            }
            else
            {
                if (obj.Nombre.Trim().Length > 50)
                {
                    mensaje += "El nombre del Producto no puede superar los 50 caracteres.\n";
                }

                string patronNombreProducto = @"^(?=.{2,100}$)[\p{L}\p{M}\p{N} .,'’()\-_/#+%&°:]+$";
                string nombreLimpio = Regex.Replace(obj.Nombre.Trim(), @"\s+", " ");

                if (!Regex.IsMatch(nombreLimpio, patronNombreProducto))
                {
                    mensaje += "El nombre del producto contiene caracteres no permitidos.\n";
                }
                else
                {
                    obj.Nombre = nombreLimpio;
                }
            }

            // 3. Validaciones para la DESCRIPCIÓN
            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                mensaje += "Es necesario ingresar la descripción del Producto.\n";
            }
            else
            {
                if (obj.Descripcion.Trim().Length > 50)
                {
                    mensaje += "La descripción del Producto no puede superar los 50 caracteres.\n";
                }

                // Normalización de espacios
                string descripcionLimpia = Regex.Replace(obj.Descripcion.Trim(), @"\s+", " ");

                // Expresión Regular: Exige al menos una palabra de 2 o más letras/números consecutivos
                // Esto rechaza entradas inválidas como "A a", "b c", "x y", etc.
                string patronDescripcionValida = @"^.*[\p{L}\p{N}]{2,}.*$";

                if (!Regex.IsMatch(descripcionLimpia, patronDescripcionValida))
                {
                    mensaje += "La descripción ingresada no es válida (debe incluir palabras o términos descriptivos reales).\n";
                }
                else
                {
                    obj.Descripcion = descripcionLimpia; // Guardamos la descripción con espacios formateados
                }
            }

            // Retorna 'true' si no hubo errores acumulados
            return string.IsNullOrEmpty(mensaje);
        }

        #endregion
    }
}