using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Negocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Proyecto_G4
{
    public partial class frmDetalle_Compra : Form
    {
        private const string FormatoMoneda = "0.00";

        private readonly CN_Compra compraNegocio;
        private readonly CN_Negocio negocioNegocio;

        public frmDetalle_Compra()
        {
            InitializeComponent();

            compraNegocio = new CN_Compra();
            negocioNegocio = new CN_Negocio();
        }

        // ================================================================
        // CARGA DEL FORMULARIO
        // ================================================================

        private void frmDetalle_Compra_Load(object sender, EventArgs e)
        {
            LimpiarDetalle();
            txtbusqueda.Focus();
        }

        // ================================================================
        // BÚSQUEDA DE COMPRA
        // ================================================================

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            BuscarCompra();
        }

        private void BuscarCompra()
        {
            string numeroDocumento = txtbusqueda.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeroDocumento))
            {
                MostrarAdvertencia(
                    "Ingrese el número de documento de la compra.",
                    "Número requerido");

                txtbusqueda.Focus();
                return;
            }

            Compras compra = compraNegocio.ObtenerCompra(numeroDocumento);

            if (!CompraValida(compra))
            {
                MostrarInformacion(
                    "No se encontró ninguna compra con ese número de documento.",
                    "Sin resultados");

                LimpiarDetalle();
                txtbusqueda.Focus();
                return;
            }

            MostrarCompra(compra);
        }

        private static bool CompraValida(Compras compra)
        {
            return compra != null &&
                   compra.IdCompra > 0;
        }

        private void MostrarCompra(Compras compra)
        {
            txtnumerodocumento.Text =
                compra.NumeroDocumento ?? string.Empty;

            txtfecha.Text =
                compra.FechaRegistro ?? string.Empty;

            txttipodocumento.Text =
                compra.TipoDocumento ?? string.Empty;

            txtusuario.Text =
                compra.oUsuario?.NombreCompleto ?? string.Empty;

            txtdocproveedor.Text =
                compra.oProveedor?.RTN ?? string.Empty;

            txtnombreproveedor.Text =
                compra.oProveedor?.RazonSocial ?? string.Empty;

            CargarDetalleCompra(compra);

            txtmontototal.Text =
                compra.MontoTotal.ToString(FormatoMoneda);
        }

        private void CargarDetalleCompra(Compras compra)
        {
            dgvdata.Rows.Clear();

            if (compra.DetalleCompra == null)
                return;

            foreach (Detalle_Compra detalle in compra.DetalleCompra)
            {
                AgregarDetalleAlGrid(detalle);
            }
        }

        private void AgregarDetalleAlGrid(Detalle_Compra detalle)
        {
            dgvdata.Rows.Add(
                detalle.oProducto?.Nombre ?? string.Empty,
                detalle.PrecioCompra.ToString(FormatoMoneda),
                detalle.Cantidad,
                detalle.MontoTotal.ToString(FormatoMoneda)
            );
        }

        // ================================================================
        // LIMPIEZA
        // ================================================================

        private void btnborrar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtbusqueda.Clear();
            LimpiarDetalle();
            txtbusqueda.Focus();
        }

        private void LimpiarDetalle()
        {
            txtnumerodocumento.Clear();
            txtfecha.Clear();
            txttipodocumento.Clear();
            txtusuario.Clear();
            txtdocproveedor.Clear();
            txtnombreproveedor.Clear();

            dgvdata.Rows.Clear();

            txtmontototal.Text = FormatoMoneda;
        }

        // ================================================================
        // GENERACIÓN DEL PDF
        // ================================================================

        private void btndescargar_Click(object sender, EventArgs e)
        {
            if (!HayCompraCargada())
            {
                MostrarAdvertencia(
                    "Primero debe buscar y seleccionar una compra.",
                    "Compra requerida");

                txtbusqueda.Focus();
                return;
            }

            try
            {
                GenerarDocumentoPdf();
            }
            catch (Exception ex)
            {
                MostrarError(
                    "No se pudo generar el documento PDF.\n\n" +
                    ex.Message,
                    "Error al generar PDF");
            }
        }

        private bool HayCompraCargada()
        {
            return !string.IsNullOrWhiteSpace(txttipodocumento.Text) &&
                   !string.IsNullOrWhiteSpace(txtnumerodocumento.Text);
        }

        private void GenerarDocumentoPdf()
        {
            string textoHtml = CrearContenidoHtml();

            using (SaveFileDialog dialogoGuardar = CrearDialogoGuardar())
            {
                if (dialogoGuardar.ShowDialog() != DialogResult.OK)
                    return;

                CrearArchivoPdf(
                    dialogoGuardar.FileName,
                    textoHtml);
            }

            MostrarInformacion(
                "El documento PDF se generó correctamente.",
                "Documento generado");
        }

        private SaveFileDialog CrearDialogoGuardar()
        {
            string numeroDocumento =
                LimpiarNombreArchivo(txtnumerodocumento.Text);

            return new SaveFileDialog
            {
                FileName = $"Compra_{numeroDocumento}.pdf",
                Filter = "Archivos PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                OverwritePrompt = true,
                Title = "Guardar detalle de compra"
            };
        }

        private string CrearContenidoHtml()
        {
            string textoHtml =
                Properties.Resources.PlantillaCompra.ToString();

            Negocio datosNegocio =
                negocioNegocio.ObtenerDatos();

            textoHtml = ReemplazarDatosNegocio(
                textoHtml,
                datosNegocio);

            textoHtml = ReemplazarDatosCompra(textoHtml);

            textoHtml = textoHtml.Replace(
                "@filas",
                CrearFilasHtml());

            textoHtml = textoHtml.Replace(
                "@montototal",
                CodificarHtml(txtmontototal.Text));

            return textoHtml;
        }

        private static string ReemplazarDatosNegocio(
            string textoHtml,
            Negocio negocio)
        {
            string nombre =
                negocio?.Nombre ?? string.Empty;

            string rtn =
                negocio?.RTN ?? string.Empty;

            string direccion =
                negocio?.Direccion ?? string.Empty;

            return textoHtml
                .Replace(
                    "@nombrenegocio",
                    CodificarHtml(nombre.ToUpperInvariant()))
                .Replace(
                    "@docnegocio",
                    CodificarHtml(rtn))
                .Replace(
                    "@direcnegocio",
                    CodificarHtml(direccion));
        }

        private string ReemplazarDatosCompra(string contenidoHtml)
        {
            return contenidoHtml
                .Replace(
                    "@tipodocumento",
                    CodificarHtml(
                        txttipodocumento.Text.ToUpperInvariant()))
                .Replace(
                    "@numerodocumento",
                    CodificarHtml(txtnumerodocumento.Text))
                .Replace(
                    "@docproveedor",
                    CodificarHtml(txtdocproveedor.Text))
                .Replace(
                    "@nombreproveedor",
                    CodificarHtml(txtnombreproveedor.Text))
                .Replace(
                    "@fecharegistro",
                    CodificarHtml(txtfecha.Text))
                .Replace(
                    "@usuarioregistro",
                    CodificarHtml(txtusuario.Text));
        }

        private string CrearFilasHtml()
        {
            StringBuilder filasHtml = new StringBuilder();

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                filasHtml.AppendLine("<tr>");

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "Producto"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "PrecioCompra"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "Cantidad"));

                filasHtml.AppendFormat(
                    "<td>{0}</td>",
                    ObtenerValorHtml(fila, "SubTotal"));

                filasHtml.AppendLine("</tr>");
            }

            return filasHtml.ToString();
        }

        private static string ObtenerValorHtml(
            DataGridViewRow fila,
            string nombreColumna)
        {
            string valor =
                fila.Cells[nombreColumna].Value?.ToString()
                ?? string.Empty;

            return CodificarHtml(valor);
        }

        private void CrearArchivoPdf(string rutaArchivo, string textoHtml)
        {
            // 1. REGISTRAR ENCODINGS
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // 2. PARCHE CONTRA EL NULLREFERENCEEXCEPTION EN Version.GetInstance()
            // Forzamos la inicialización de la versión del producto para iTextSharp
            iTextSharp.text.Version.GetInstance(); // Si revienta aquí, usamos el bloque try a continuación

            using (FileStream stream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (Document documento = new Document(PageSize.A4, 25, 25, 25, 25))
                {
                    PdfWriter escritor = PdfWriter.GetInstance(documento, stream);
                    documento.Open();

                    AgregarLogo(documento);

                    using (StringReader lector = new StringReader(textoHtml))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(
                            escritor,
                            documento,
                            lector
                        );
                    }

                    documento.Close();
                }
            }
        }

        private void AgregarLogo(Document documento)
        {
            byte[] logo =
                negocioNegocio.ObtenerLogo(
                    out bool obtenido);

            if (!obtenido ||
                logo == null ||
                logo.Length == 0)
            {
                return;
            }

            iTextSharp.text.Image imagen =
                iTextSharp.text.Image.GetInstance(logo);

            imagen.ScaleToFit(60, 60);
            imagen.Alignment =
                iTextSharp.text.Image.UNDERLYING;

            imagen.SetAbsolutePosition(
                documento.Left,
                documento.GetTop(51));

            documento.Add(imagen);
        }

        private static string CodificarHtml(string valor)
        {
            return WebUtility.HtmlEncode(
                valor ?? string.Empty);
        }

        private static string LimpiarNombreArchivo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return "SinNumero";

            foreach (char caracterInvalido
                     in Path.GetInvalidFileNameChars())
            {
                nombre = nombre.Replace(
                    caracterInvalido,
                    '_');
            }

            return nombre.Trim();
        }

        // ================================================================
        // VALIDACIÓN DE TECLADO
        // ================================================================

        private void SoloNumeros_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            bool esNumero =
                char.IsDigit(e.KeyChar);

            bool esTeclaControl =
                char.IsControl(e.KeyChar);

            if (!esNumero && !esTeclaControl)
                e.Handled = true;
        }

        // ================================================================
        // MENSAJES
        // ================================================================

        private static void MostrarAdvertencia(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void MostrarInformacion(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void MostrarError(
            string mensaje,
            string titulo)
        {
            MessageBox.Show(
                mensaje,
                titulo,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}