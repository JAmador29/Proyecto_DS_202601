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

        public int Registrar(Producto obj, out string mensaje)
        {
            if (!ValidarProducto(obj, requiereIdProducto: false, out mensaje))
            {
                return 0;
            }

            return objCd_Producto.Registrar(obj, out mensaje);
        }

        public bool Editar(Producto obj, out string mensaje)
        {
            if (!ValidarProducto(obj, requiereIdProducto: true, out mensaje))
            {
                return false;
            }

            return objCd_Producto.Editar(obj, out mensaje);
        }

        #region Métodos Privados de Validación (SOLID / DRY)

        private bool ValidarProducto(Producto obj, bool requiereIdProducto, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj == null)
            {
                mensaje = "No se recibieron los datos del producto.";
                return false;
            }

            if (requiereIdProducto && obj.IdProducto <= 0)
            {
                mensaje += "No se seleccionó un producto válido para modificar.\n";
            }

            if (obj.oUsuario == null || obj.oUsuario.IdUsuario <= 0)
            {
                mensaje += "No se pudo identificar al usuario que realiza la operación.\n";
            }

            if (obj.oCategoria == null || obj.oCategoria.IdCategoria <= 0)
            {
                mensaje += "Debe seleccionar una categoría válida.\n";
            }

            // 1. Validaciones para el CÓDIGO
            if (string.IsNullOrWhiteSpace(obj.Codigo))
            {
                mensaje += "Es necesario ingresar el código del producto.\n";
            }
            else
            {
                obj.Codigo = obj.Codigo.Trim();
                if (obj.Codigo.Length > 20)
                {
                    mensaje += "El código del producto no puede superar los 20 caracteres.\n";
                }
            }

            // 2. Validaciones para el NOMBRE
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                mensaje += "Es necesario ingresar el nombre del producto.\n";
            }
            else
            {
                string nombreLimpio = Regex.Replace(obj.Nombre.Trim(), @"\s+", " ");

                if (nombreLimpio.Length > 30)
                {
                    mensaje += "El nombre del producto no puede superar los 30 caracteres.\n";
                }

                string patronNombreProducto = @"^(?=.{2,100}$)[\p{L}\p{M}\p{N} .,'’()\-_/#+%&°:]+$";
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
                mensaje += "Es necesario ingresar la descripción del producto.\n";
            }
            else
            {
                string descripcionLimpia = Regex.Replace(obj.Descripcion.Trim(), @"\s+", " ");

                if (descripcionLimpia.Length > 30)
                {
                    mensaje += "La descripción del producto no puede superar los 30 caracteres.\n";
                }

                string patronDescripcionValida = @"^.*[\p{L}\p{N}]{2,}.*$";
                if (!Regex.IsMatch(descripcionLimpia, patronDescripcionValida))
                {
                    mensaje += "La descripción debe contener palabras o términos descriptivos válidos.\n";
                }
                else
                {
                    obj.Descripcion = descripcionLimpia;
                }
            }

            return string.IsNullOrEmpty(mensaje);
        }

        #endregion
    }
}