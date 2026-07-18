using Capa_Entidad;
using Capa_Entidad.Utilidades;
using Capa_Negocio;
using Proyecto_G4.Modales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_G4
{
    public partial class frmVentas : Form
    {
        private Usuario _Usuario;
        private int idClienteSeleccionado = 0;

        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cbotipodocumento.Items.Add(
                new OpcionCombo()
                {
                    Valor = "Boleta",
                    Texto = "Boleta"
                }
            );

            cbotipodocumento.Items.Add(
                new OpcionCombo()
                {
                    Valor = "Factura",
                    Texto = "Factura"
                }
            );

            cbotipodocumento.DisplayMember = "Texto";
            cbotipodocumento.ValueMember = "Valor";
            cbotipodocumento.SelectedIndex = 0;

            txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtidproducto.Text = "0";

            txtpagocon.Text = "";
            txtcambio.Text = "";
            txttotalpagar.Text = "0";
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    idClienteSeleccionado = modal._Cliente.IdCliente;
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

                if (result == DialogResult.OK)
                {
                    txtidproducto.Text =
                        modal._Producto.IdProducto.ToString();

                    txtcodproducto.Text =
                        modal._Producto.Codigo;

                    txtproducto.Text =
                        modal._Producto.Nombre;

                    txtprecio.Text =
                        modal._Producto.PrecioVenta.ToString("0.00");

                    txtstock.Text =
                        modal._Producto.Stock.ToString();

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
                Producto oProducto = new CN_Producto()
                    .Listar()
                    .Where(p =>
                        p.Codigo == txtcodproducto.Text &&
                        p.Estado == true
                    )
                    .FirstOrDefault();

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
                    txtidproducto.Text = "0";
                    txtproducto.Text = "";
                    txtprecio.Text = "";
                    txtstock.Text = "";
                    txtcantidad.Value = 1;
                }
            }
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool producto_existe = false;

            if (int.Parse(txtidproducto.Text) == 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un producto",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            if (!decimal.TryParse(txtprecio.Text, out precio))
            {
                MessageBox.Show(
                    "Precio - Formato moneda incorrecto",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                txtprecio.Select();
                return;
            }

            if (Convert.ToInt32(txtstock.Text) <
                Convert.ToInt32(txtcantidad.Value.ToString()))
            {
                MessageBox.Show(
                    "La cantidad no puede ser mayor al stock",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            foreach (DataGridViewRow fila in dgvdata.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() ==
                    txtidproducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }

            if (!producto_existe)
            {
                dgvdata.Rows.Add(new object[]
                {
                    txtidproducto.Text,
                    txtproducto.Text,
                    precio.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * precio).ToString("0.00")
                });

                calcularTotal();
                limpiarProducto();
                txtcodproducto.Select();
            }
            else
            {
                MessageBox.Show(
                    "El producto ya fue agregado a la venta",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
        }

        private void calcularTotal()
        {
            decimal total = 0;

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    total += Convert.ToDecimal(
                        row.Cells["SubTotal"].Value.ToString()
                    );
                }
            }

            txttotalpagar.Text = total.ToString("0.00");
        }

        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtcodproducto.BackColor = Color.White;
            txtproducto.Text = "";
            txtprecio.Text = "";
            txtstock.Text = "";
            txtcantidad.Value = 1;
        }

        private void dgvdata_CellPainting(
            object sender,
            DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 5)
            {
                e.Paint(
                    e.CellBounds,
                    DataGridViewPaintParts.All
                );

                var w = Properties.Resources.Basurero25.Width;
                var h = Properties.Resources.Basurero25.Height;

                var x =
                    e.CellBounds.Left +
                    (e.CellBounds.Width - w) / 2;

                var y =
                    e.CellBounds.Top +
                    (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(
                    Properties.Resources.Basurero25,
                    new Rectangle(x, y, w, h)
                );

                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                dgvdata.Rows.RemoveAt(e.RowIndex);
                calcularTotal();
            }
        }

        private void txtprecio_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtprecio.Text.Trim().Length == 0 &&
                    e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) ||
                        e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtpagocon_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtpagocon.Text.Trim().Length == 0 &&
                    e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) ||
                        e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void calcularcambio()
        {
            if (txttotalpagar.Text.Trim() == "")
            {
                MessageBox.Show(
                    "No existen productos en la venta",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            decimal pagacon;
            decimal total =
                Convert.ToDecimal(txttotalpagar.Text);

            if (txtpagocon.Text.Trim() == "")
            {
                txtpagocon.Text = "0";
            }

            if (decimal.TryParse(
                txtpagocon.Text.Trim(),
                out pagacon))
            {
                if (pagacon < total)
                {
                    txtcambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagacon - total;
                    txtcambio.Text = cambio.ToString("0.00");
                }
            }
        }

        private void txtpagocon_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularcambio();
            }
        }

        private void btncrearventa_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show(
                    "Debe seleccionar un cliente",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            if (txtdocumentocliente.Text == "")
            {
                MessageBox.Show(
                    "Debe ingresar documento del cliente",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            if (txtnombrecliente.Text == "")
            {
                MessageBox.Show(
                    "Debe ingresar el nombre del cliente",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show(
                    "Debe ingresar productos en la venta",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            decimal total;
            decimal pago;

            if (!decimal.TryParse(txttotalpagar.Text, out total))
            {
                MessageBox.Show(
                    "El total de la venta no es válido",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                return;
            }

            if (!decimal.TryParse(txtpagocon.Text, out pago))
            {
                MessageBox.Show(
                    "Debe ingresar un monto de pago válido",
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                txtpagocon.Select();
                return;
            }

            if (pago < total)
            {
                MessageBox.Show(
                    "El monto pagado no puede ser menor que el total de la venta",
                    "Pago insuficiente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );

                txtcambio.Text = "0.00";
                txtpagocon.Select();
                return;
            }

            decimal cambio = pago - total;
            txtcambio.Text = cambio.ToString("0.00");

            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add(
                "IdProducto",
                typeof(int)
            );

            detalle_venta.Columns.Add(
                "PrecioVenta",
                typeof(decimal)
            );

            detalle_venta.Columns.Add(
                "Cantidad",
                typeof(int)
            );

            detalle_venta.Columns.Add(
                "SubTotal",
                typeof(decimal)
            );

            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                detalle_venta.Rows.Add(new object[]
                {
                    Convert.ToInt32(
                        row.Cells["IdProducto"].Value
                    ),

                    Convert.ToDecimal(
                        row.Cells["Precio"].Value
                    ),

                    Convert.ToInt32(
                        row.Cells["Cantidad"].Value
                    ),

                    Convert.ToDecimal(
                        row.Cells["SubTotal"].Value
                    )
                });
            }

            if (detalle_venta.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se generaron filas para el detalle de la venta.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            int idcorrelativo =
                new CN_Venta().ObtenerCorrelativo();

            string numeroDocumento =
                string.Format(
                    "{0:00000}",
                    idcorrelativo
                );

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario()
                {
                    IdUsuario = _Usuario.IdUsuario
                },

                oCliente = new Cliente()
                {
                    IdCliente = idClienteSeleccionado
                },

                TipoDocumento =
                    ((OpcionCombo)
                    cbotipodocumento.SelectedItem).Texto,

                NumeroDocumento = numeroDocumento,
                MetodoPago = "Efectivo",
                MontoPago = pago,
                MontoCambio = cambio,
                MontoTotal = total
            };

            string mensaje = string.Empty;

            bool respuesta = new CN_Venta().Registrar(
                oVenta,
                detalle_venta,
                out mensaje
            );

            if (respuesta)
            {
                var result = MessageBox.Show(
                    "Numero de venta generada:\n" +
                    numeroDocumento +
                    "\n\n¿Desea copiar al portapapeles?",
                    "Mensaje",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numeroDocumento);
                }

                idClienteSeleccionado = 0;
                txtdocumentocliente.Text = "";
                txtnombrecliente.Text = "";
                dgvdata.Rows.Clear();
                calcularTotal();
                txtpagocon.Text = "";
                txtcambio.Text = "";
            }
            else
            {
                MessageBox.Show(
                    mensaje,
                    "Mensaje",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation
                );
            }
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