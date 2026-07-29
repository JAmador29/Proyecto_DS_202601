namespace Proyecto_G4
{
    partial class frmCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbTipoDocumento = new System.Windows.Forms.ComboBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIdProveedor = new System.Windows.Forms.TextBox();
            this.btnBuscarProveedor = new FontAwesome.Sharp.IconButton();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.txtDocProveedor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrecioVenta = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.btnBuscarProducto = new FontAwesome.Sharp.IconButton();
            this.txtIdProducto = new System.Windows.Forms.TextBox();
            this.txtProducto = new System.Windows.Forms.TextBox();
            this.txtCodProducto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvdata = new System.Windows.Forms.DataGridView();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTotalaPagar = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new FontAwesome.Sharp.IconButton();
            this.btnAgregarProducto = new FontAwesome.Sharp.IconButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(89, 12);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1063, 559);
            this.label10.TabIndex = 120;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(115, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 38);
            this.label1.TabIndex = 22;
            this.label1.Text = "Registrar Compra";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            this.groupBox1.Controls.Add(this.cbTipoDocumento);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(123, 69);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(461, 99);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Compra";
            // 
            // cbTipoDocumento
            // 
            this.cbTipoDocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.cbTipoDocumento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbTipoDocumento.ForeColor = System.Drawing.Color.White;
            this.cbTipoDocumento.FormattingEnabled = true;
            this.cbTipoDocumento.Location = new System.Drawing.Point(220, 57);
            this.cbTipoDocumento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbTipoDocumento.Name = "cbTipoDocumento";
            this.cbTipoDocumento.Size = new System.Drawing.Size(191, 28);
            this.cbTipoDocumento.TabIndex = 3;
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFecha.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFecha.ForeColor = System.Drawing.Color.White;
            this.txtFecha.Location = new System.Drawing.Point(9, 57);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(177, 27);
            this.txtFecha.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(217, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tipo Documento:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            this.groupBox2.Controls.Add(this.txtIdProveedor);
            this.groupBox2.Controls.Add(this.btnBuscarProveedor);
            this.groupBox2.Controls.Add(this.txtNombreProveedor);
            this.groupBox2.Controls.Add(this.txtDocProveedor);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(609, 69);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(497, 99);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información Proveedor";
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.Location = new System.Drawing.Point(426, 18);
            this.txtIdProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.Size = new System.Drawing.Size(41, 27);
            this.txtIdProveedor.TabIndex = 5;
            this.txtIdProveedor.Visible = false;
            // 
            // btnBuscarProveedor
            // 
            this.btnBuscarProveedor.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarProveedor.IconColor = System.Drawing.Color.Black;
            this.btnBuscarProveedor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProveedor.IconSize = 16;
            this.btnBuscarProveedor.Location = new System.Drawing.Point(193, 56);
            this.btnBuscarProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscarProveedor.Name = "btnBuscarProveedor";
            this.btnBuscarProveedor.Size = new System.Drawing.Size(53, 23);
            this.btnBuscarProveedor.TabIndex = 4;
            this.btnBuscarProveedor.UseVisualStyleBackColor = true;
            this.btnBuscarProveedor.Click += new System.EventHandler(this.btnBuscarProveedor_Click);
            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtNombreProveedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombreProveedor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombreProveedor.ForeColor = System.Drawing.Color.White;
            this.txtNombreProveedor.Location = new System.Drawing.Point(272, 56);
            this.txtNombreProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreProveedor.MaxLength = 50;
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.Size = new System.Drawing.Size(196, 27);
            this.txtNombreProveedor.TabIndex = 3;
            // 
            // txtDocProveedor
            // 
            this.txtDocProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtDocProveedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocProveedor.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocProveedor.ForeColor = System.Drawing.Color.White;
            this.txtDocProveedor.Location = new System.Drawing.Point(10, 57);
            this.txtDocProveedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDocProveedor.MaxLength = 16;
            this.txtDocProveedor.Name = "txtDocProveedor";
            this.txtDocProveedor.Size = new System.Drawing.Size(177, 27);
            this.txtDocProveedor.TabIndex = 2;
            this.txtDocProveedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SoloNumeros_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(269, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Razón Social:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Número de Documento: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtPrecioVenta);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtCantidad);
            this.groupBox3.Controls.Add(this.txtPrecioCompra);
            this.groupBox3.Controls.Add(this.btnBuscarProducto);
            this.groupBox3.Controls.Add(this.txtIdProducto);
            this.groupBox3.Controls.Add(this.txtProducto);
            this.groupBox3.Controls.Add(this.txtCodProducto);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(122, 181);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(867, 94);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información Producto";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.Location = new System.Drawing.Point(755, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Cantidad:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.Location = new System.Drawing.Point(645, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Precio Venta:";
            // 
            // txtPrecioVenta
            // 
            this.txtPrecioVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtPrecioVenta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioVenta.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrecioVenta.ForeColor = System.Drawing.Color.White;
            this.txtPrecioVenta.Location = new System.Drawing.Point(648, 53);
            this.txtPrecioVenta.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecioVenta.Name = "txtPrecioVenta";
            this.txtPrecioVenta.Size = new System.Drawing.Size(103, 27);
            this.txtPrecioVenta.TabIndex = 11;
            this.txtPrecioVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioVenta_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.Location = new System.Drawing.Point(536, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Precio Compra:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidad.ForeColor = System.Drawing.Color.White;
            this.txtCantidad.Location = new System.Drawing.Point(757, 55);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCantidad.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(79, 27);
            this.txtCantidad.TabIndex = 9;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtPrecioCompra.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrecioCompra.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrecioCompra.ForeColor = System.Drawing.Color.White;
            this.txtPrecioCompra.Location = new System.Drawing.Point(539, 53);
            this.txtPrecioCompra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(103, 27);
            this.txtPrecioCompra.TabIndex = 7;
            this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCompra_KeyPress);
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscarProducto.IconColor = System.Drawing.Color.Black;
            this.btnBuscarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscarProducto.IconSize = 16;
            this.btnBuscarProducto.Location = new System.Drawing.Point(220, 51);
            this.btnBuscarProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(53, 23);
            this.btnBuscarProducto.TabIndex = 6;
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // txtIdProducto
            // 
            this.txtIdProducto.Location = new System.Drawing.Point(165, 20);
            this.txtIdProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIdProducto.Name = "txtIdProducto";
            this.txtIdProducto.Size = new System.Drawing.Size(43, 27);
            this.txtIdProducto.TabIndex = 5;
            this.txtIdProducto.Visible = false;
            // 
            // txtProducto
            // 
            this.txtProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProducto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtProducto.ForeColor = System.Drawing.Color.White;
            this.txtProducto.Location = new System.Drawing.Point(289, 52);
            this.txtProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtProducto.MaxLength = 50;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.ReadOnly = true;
            this.txtProducto.Size = new System.Drawing.Size(231, 27);
            this.txtProducto.TabIndex = 3;
            // 
            // txtCodProducto
            // 
            this.txtCodProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtCodProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodProducto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCodProducto.ForeColor = System.Drawing.Color.White;
            this.txtCodProducto.Location = new System.Drawing.Point(9, 51);
            this.txtCodProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCodProducto.MaxLength = 50;
            this.txtCodProducto.Name = "txtCodProducto";
            this.txtCodProducto.Size = new System.Drawing.Size(199, 27);
            this.txtCodProducto.TabIndex = 2;
            this.txtCodProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodProducto_KeyDown);
            this.txtCodProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SoloNumeros_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(285, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Producto:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(5, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Cod. Producto";
            // 
            // dgvdata
            // 
            this.dgvdata.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(132)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvdata.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdata.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dgvdata.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvdata.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(132)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvdata.ColumnHeadersHeight = 40;
            this.dgvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdProducto,
            this.Producto,
            this.PrecioCompra,
            this.PrecioVenta,
            this.Cantidad,
            this.SubTotal,
            this.btnEliminar});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(132)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdata.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvdata.EnableHeadersVisualStyles = false;
            this.dgvdata.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(62)))), ((int)(((byte)(78)))));
            this.dgvdata.Location = new System.Drawing.Point(122, 289);
            this.dgvdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvdata.Name = "dgvdata";
            this.dgvdata.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdata.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvdata.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvdata.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvdata.RowTemplate.Height = 24;
            this.dgvdata.Size = new System.Drawing.Size(867, 263);
            this.dgvdata.TabIndex = 26;
            this.dgvdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdata_CellContentClick);
            this.dgvdata.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvdata_CellPainting);
            // 
            // IdProducto
            // 
            this.IdProducto.HeaderText = "Id Producto";
            this.IdProducto.MinimumWidth = 6;
            this.IdProducto.Name = "IdProducto";
            this.IdProducto.ReadOnly = true;
            this.IdProducto.Visible = false;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.MinimumWidth = 6;
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.HeaderText = "Precio Compra";
            this.PrecioCompra.MinimumWidth = 6;
            this.PrecioCompra.Name = "PrecioCompra";
            this.PrecioCompra.ReadOnly = true;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.MinimumWidth = 6;
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            this.PrecioVenta.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "Sub Total";
            this.SubTotal.MinimumWidth = 6;
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.ReadOnly = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.MinimumWidth = 6;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(1001, 445);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 20);
            this.label12.TabIndex = 28;
            this.label12.Text = "Total a Pagar:";
            // 
            // txtTotalaPagar
            // 
            this.txtTotalaPagar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTotalaPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtTotalaPagar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalaPagar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTotalaPagar.ForeColor = System.Drawing.Color.White;
            this.txtTotalaPagar.Location = new System.Drawing.Point(1005, 467);
            this.txtTotalaPagar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotalaPagar.Name = "txtTotalaPagar";
            this.txtTotalaPagar.ReadOnly = true;
            this.txtTotalaPagar.Size = new System.Drawing.Size(124, 27);
            this.txtTotalaPagar.TabIndex = 29;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRegistrar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.IconChar = FontAwesome.Sharp.IconChar.Tag;
            this.btnRegistrar.IconColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRegistrar.IconSize = 40;
            this.btnRegistrar.Location = new System.Drawing.Point(1005, 504);
            this.btnRegistrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(124, 48);
            this.btnRegistrar.TabIndex = 30;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAgregarProducto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this.btnAgregarProducto.IconColor = System.Drawing.Color.LimeGreen;
            this.btnAgregarProducto.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAgregarProducto.IconSize = 30;
            this.btnAgregarProducto.Location = new System.Drawing.Point(1005, 181);
            this.btnAgregarProducto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(109, 94);
            this.btnAgregarProducto.TabIndex = 27;
            this.btnAgregarProducto.Text = "Agregar";
            this.btnAgregarProducto.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAgregarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregarProducto.UseVisualStyleBackColor = false;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // frmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1229, 831);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.txtTotalaPagar);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAgregarProducto);
            this.Controls.Add(this.dgvdata);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCompras";
            this.Text = "frmCompras";
            this.Load += new System.EventHandler(this.frmCompras_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbTipoDocumento;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private FontAwesome.Sharp.IconButton btnBuscarProveedor;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.TextBox txtDocProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIdProveedor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtIdProducto;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.TextBox txtCodProducto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPrecioVenta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private FontAwesome.Sharp.IconButton btnBuscarProducto;
        private System.Windows.Forms.DataGridView dgvdata;
        private FontAwesome.Sharp.IconButton btnAgregarProducto;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTotalaPagar;
        private FontAwesome.Sharp.IconButton btnRegistrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
    }
}