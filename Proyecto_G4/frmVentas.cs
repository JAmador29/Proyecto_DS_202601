using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using Proyecto_G4.Modales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmVentas : Form
    {
        private readonly Usuario _usuario;
        private readonly CN_Venta _cnVenta = new CN_Venta();
        private readonly CN_Producto _cnProducto = new CN_Producto();
        private int _idClienteSeleccionado = 0;

        public frmVentas(Usuario oUsuario = null)
        {
            _usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            CargarComboTipoDocumento();

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidproducto.Text = "0";
            txtpagocon.Text = string.Empty;
            txtcambio.Text = string.Empty;
            txttotalpagar.Text = "0.00";
        }

        private void CargarComboTipoDocumento()
        {
            cbotipodocumento.Items.Clear();
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK && modal._Cliente != null)
                {
                    _idClienteSeleccionado = modal._Cliente.IdCliente;
                    txtdocumentocliente.Text = modal._Cliente.Documento;
                    txtnombrecliente.Text = modal._Cliente.NombreCompleto;
                    txtcodproducto.Select();
                }
                else
                {
                    txtdocumentocliente.Select();
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK && modal._Producto != null)
                {
                    txtidproducto.Text = modal._Producto.IdProducto.ToString();
                    txtcodproducto.Text = modal._Producto.Codigo;
                    txtproducto.Text = modal._Producto.Nombre;
                    txtprecio.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtstock.Text = modal._Producto.Stock.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodproducto.Select();
                }
            }
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string codigo = txtcodproducto.Text.Trim();

                if (string.IsNullOrEmpty(codigo)) return;

                Producto oProducto = _cnProducto.Listar()
                    .FirstOrDefault(p => p.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && p.Estado);

                if (oProducto != null)
                {
                    txtcodproducto.BackColor = Color.Honeydew;
                    txtidproducto.Text = oProducto.IdProducto.ToString();
                    txtproducto.Text = oProducto.Nombre;
                    txtprecio.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtstock.Text = oProducto.Stock.ToString();
                    txtcantidad.Select();
                }
                else
                {
                    txtcodproducto.BackColor = Color.MistyRose;
                    LimpiarCamposProducto();
                }
            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtidproducto.Text, out int idProducto) || idProducto == 0)
            {
                MessageBox.Show("Debe seleccionar un producto válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtprecio.Text, out decimal precio))
            {
                MessageBox.Show("Precio - Formato de moneda incorrecto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtprecio.Select();
                return;
            }

            if (!int.TryParse(txtstock.Text, out int stockDisponible))
            {
                MessageBox.Show("No se pudo determinar el stock del producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int cantidadAgregar = (int)txtcantidad.Value;

            // Verificar si el producto ya existe en la grilla para sumar o actualizar
            DataGridViewRow filaExistente = null;
            int cantidadActualEnGrilla = 0;

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (Convert.ToInt32(fila.Cells["IdProducto"].Value) == idProducto)
                {
                    filaExistente = fila;
                    cantidadActualEnGrilla = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                    break;
                }
            }

            if ((cantidadActualEnGrilla + cantidadAgregar) > stockDisponible)
            {
                MessageBox.Show($"La cantidad solicitada ({cantidadActualEnGrilla + cantidadAgregar}) supera el stock disponible ({stockDisponible}).", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (filaExistente != null)
            {
                // Actualizar fila existente
                int nuevaCantidad = cantidadActualEnGrilla + cantidadAgregar;
                decimal nuevoSubTotal = nuevaCantidad * precio;

                filaExistente.Cells["Cantidad"].Value = nuevaCantidad;
                filaExistente.Cells["SubTotal"].Value = nuevoSubTotal.ToString("0.00");
            }
            else
            {
                // Agregar nueva fila
                dgvdata.Rows.Add(new object[]
                {
                    idProducto,
                    txtproducto.Text,
                    precio.ToString("0.00"),
                    cantidadAgregar,
                    (cantidadAgregar * precio).ToString("0.00")
                });
            }

            CalcularTotal();
            LimpiarCamposProducto();
            txtcodproducto.Select();
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value ?? 0);
            }

            txttotalpagar.Text = total.ToString("0.00");
        }

        private void LimpiarCamposProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = string.Empty;
            txtcodproducto.BackColor = Color.White;
            txtproducto.Text = string.Empty;
            txtprecio.Text = string.Empty;
            txtstock.Text = string.Empty;
            txtcantidad.Value = 1;
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Asumiendo que la columna 5 o 'btneliminar' es el botón de eliminación
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar" || e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.Basurero25.Width;
                var h = Properties.Resources.Basurero25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.Basurero25, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                dgvdata.Rows.RemoveAt(e.RowIndex);
                CalcularTotal();
                CalcularCambio();
            }
        }

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarEntradaMoneda(sender, e);
        }

        private void txtpagocon_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarEntradaMoneda(sender, e);
        }

        private void ValidarEntradaMoneda(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == '.' && !txt.Text.Contains("."))
            {
                e.Handled = txt.Text.Length == 0; // Evita iniciar con punto
            }
            else
            {
                e.Handled = true;
            }
        }

        private void CalcularCambio()
        {
            if (!decimal.TryParse(txttotalpagar.Text, out decimal total) || total == 0)
            {
                txtcambio.Text = "0.00";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtpagocon.Text))
            {
                txtcambio.Text = "0.00";
                return;
            }

            if (decimal.TryParse(txtpagocon.Text.Trim(), out decimal pagoCon))
            {
                if (pagoCon < total)
                {
                    txtcambio.Text = "0.00";
                }
                else
                {
                    txtcambio.Text = (pagoCon - total).ToString("0.00");
                }
            }
        }

        private void txtpagocon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CalcularCambio();
            }
        }

        private void btncrearventa_Click(object sender, EventArgs e)
        {
            if (_idClienteSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar un cliente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtdocumentocliente.Text))
            {
                MessageBox.Show("Debe ingresar el documento del cliente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtnombrecliente.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del cliente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txttotalpagar.Text, out decimal total) || total <= 0)
            {
                MessageBox.Show("El total de la venta no es válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtpagocon.Text, out decimal pago))
            {
                MessageBox.Show("Debe ingresar un monto de pago válido.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpagocon.Select();
                return;
            }

            if (pago < total)
            {
                MessageBox.Show("El monto pagado no puede ser menor que el total de la venta.", "Pago Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcambio.Text = "0.00";
                txtpagocon.Select();
                return;
            }

            decimal cambio = pago - total;
            txtcambio.Text = cambio.ToString("0.00");

            DataTable detalleVenta = CrearEstructuraDetalleVenta();

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalleVenta.Rows.Add(new object[]
                {
                    Convert.ToInt32(row.Cells["IdProducto"].Value),
                    Convert.ToDecimal(row.Cells["Precio"].Value),
                    Convert.ToInt32(row.Cells["Cantidad"].Value),
                    Convert.ToDecimal(row.Cells["SubTotal"].Value)
                });
            }

            int idCorrelativo = _cnVenta.ObtenerCorrelativo();
            string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IdUsuario = _usuario != null ? _usuario.IdUsuario : 0 },
                oCliente = new Cliente() { IdCliente = _idClienteSeleccionado },
                TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                NumeroDocumento = numeroDocumento,
                MetodoPago = "Efectivo",
                MontoPago = pago,
                MontoCambio = cambio,
                MontoTotal = total
            };

            bool respuesta = _cnVenta.Registrar(oVenta, detalleVenta, out string mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show(
                    $"Número de venta generada:\n{numeroDocumento}\n\n¿Desea copiar al portapapeles?",
                    "Venta Exitosa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);
                }

                ResetearFormularioVenta();
            }
            else
            {
                MessageBox.Show(mensaje, "Error al Registrar Venta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private DataTable CrearEstructuraDetalleVenta()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdProducto", typeof(int));
            dt.Columns.Add("PrecioVenta", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("SubTotal", typeof(decimal));
            return dt;
        }

        private void ResetearFormularioVenta()
        {
            _idClienteSeleccionado = 0;
            txtdocumentocliente.Text = string.Empty;
            txtnombrecliente.Text = string.Empty;
            dgvdata.Rows.Clear();
            CalcularTotal();
            txtpagocon.Text = string.Empty;
            txtcambio.Text = string.Empty;
            LimpiarCamposProducto();
        }

        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}