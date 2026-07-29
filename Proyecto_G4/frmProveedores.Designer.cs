namespace Proyecto_G4
{
    partial class frmProveedores
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
            this.label9 = new System.Windows.Forms.Label();
            this.btnlimpiar = new FontAwesome.Sharp.IconButton();
            this.btnguardar = new FontAwesome.Sharp.IconButton();
            this.cmbestado = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcorreo = new System.Windows.Forms.TextBox();
            this.txtrazonsocial = new System.Windows.Forms.TextBox();
            this.txtdocumento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnlimpiarbuscador = new FontAwesome.Sharp.IconButton();
            this.btnbuscar = new FontAwesome.Sharp.IconButton();
            this.cmbbusqueda = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIndice = new System.Windows.Forms.TextBox();
            this.dgvdata = new System.Windows.Forms.DataGridView();
            this.btnSeleccionar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtbusqueda = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txttelefono = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(30, 18);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(253, 38);
            this.label9.TabIndex = 69;
            this.label9.Text = "Detalle Proveedor";
            // 
            // btnlimpiar
            // 
            this.btnlimpiar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnlimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnlimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlimpiar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnlimpiar.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnlimpiar.IconColor = System.Drawing.Color.White;
            this.btnlimpiar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlimpiar.IconSize = 16;
            this.btnlimpiar.Location = new System.Drawing.Point(169, 498);
            this.btnlimpiar.Margin = new System.Windows.Forms.Padding(4);
            this.btnlimpiar.Name = "btnlimpiar";
            this.btnlimpiar.Size = new System.Drawing.Size(104, 28);
            this.btnlimpiar.TabIndex = 68;
            this.btnlimpiar.Text = "Limpiar";
            this.btnlimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnlimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnlimpiar.UseVisualStyleBackColor = false;
            this.btnlimpiar.Click += new System.EventHandler(this.btnlimpiar_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.BackColor = System.Drawing.Color.Green;
            this.btnguardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnguardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnguardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnguardar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnguardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnguardar.IconColor = System.Drawing.Color.White;
            this.btnguardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnguardar.IconSize = 16;
            this.btnguardar.Location = new System.Drawing.Point(48, 498);
            this.btnguardar.Margin = new System.Windows.Forms.Padding(4);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(104, 28);
            this.btnguardar.TabIndex = 67;
            this.btnguardar.Text = "Guardar";
            this.btnguardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnguardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnguardar.UseVisualStyleBackColor = false;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // cmbestado
            // 
            this.cmbestado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.cmbestado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbestado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbestado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbestado.ForeColor = System.Drawing.Color.White;
            this.cmbestado.FormattingEnabled = true;
            this.cmbestado.Location = new System.Drawing.Point(52, 350);
            this.cmbestado.Margin = new System.Windows.Forms.Padding(4);
            this.cmbestado.Name = "cmbestado";
            this.cmbestado.Size = new System.Drawing.Size(204, 28);
            this.cmbestado.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(48, 323);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 23);
            this.label8.TabIndex = 65;
            this.label8.Text = "Estado:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(48, 259);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 23);
            this.label5.TabIndex = 63;
            this.label5.Text = "Teléfono:";
            // 
            // txtcorreo
            // 
            this.txtcorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtcorreo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtcorreo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcorreo.ForeColor = System.Drawing.Color.White;
            this.txtcorreo.Location = new System.Drawing.Point(52, 225);
            this.txtcorreo.Margin = new System.Windows.Forms.Padding(4);
            this.txtcorreo.MaxLength = 50;
            this.txtcorreo.Name = "txtcorreo";
            this.txtcorreo.Size = new System.Drawing.Size(204, 27);
            this.txtcorreo.TabIndex = 62;
            // 
            // txtrazonsocial
            // 
            this.txtrazonsocial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtrazonsocial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtrazonsocial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrazonsocial.ForeColor = System.Drawing.Color.White;
            this.txtrazonsocial.Location = new System.Drawing.Point(52, 164);
            this.txtrazonsocial.Margin = new System.Windows.Forms.Padding(4);
            this.txtrazonsocial.MaxLength = 55;
            this.txtrazonsocial.Name = "txtrazonsocial";
            this.txtrazonsocial.Size = new System.Drawing.Size(204, 27);
            this.txtrazonsocial.TabIndex = 61;
            this.txtrazonsocial.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtrazonsocial_KeyPress);
            // 
            // txtdocumento
            // 
            this.txtdocumento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtdocumento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtdocumento.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdocumento.ForeColor = System.Drawing.Color.White;
            this.txtdocumento.Location = new System.Drawing.Point(52, 103);
            this.txtdocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtdocumento.MaxLength = 16;
            this.txtdocumento.Name = "txtdocumento";
            this.txtdocumento.Size = new System.Drawing.Size(204, 27);
            this.txtdocumento.TabIndex = 60;
            this.txtdocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdocumento_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(48, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 23);
            this.label4.TabIndex = 59;
            this.label4.Text = "Correo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(48, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 23);
            this.label3.TabIndex = 58;
            this.label3.Text = "Razón Social:";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 585);
            this.label1.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(65, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 57;
            this.label2.Text = "No Documento:";
            // 
            // btnlimpiarbuscador
            // 
            this.btnlimpiarbuscador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnlimpiarbuscador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlimpiarbuscador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnlimpiarbuscador.FlatAppearance.BorderSize = 0;
            this.btnlimpiarbuscador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlimpiarbuscador.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnlimpiarbuscador.IconChar = FontAwesome.Sharp.IconChar.Brush;
            this.btnlimpiarbuscador.IconColor = System.Drawing.Color.White;
            this.btnlimpiarbuscador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnlimpiarbuscador.IconSize = 20;
            this.btnlimpiarbuscador.Location = new System.Drawing.Point(1297, 49);
            this.btnlimpiarbuscador.Margin = new System.Windows.Forms.Padding(4);
            this.btnlimpiarbuscador.Name = "btnlimpiarbuscador";
            this.btnlimpiarbuscador.Size = new System.Drawing.Size(48, 28);
            this.btnlimpiarbuscador.TabIndex = 76;
            this.btnlimpiarbuscador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnlimpiarbuscador.UseVisualStyleBackColor = false;
            this.btnlimpiarbuscador.Click += new System.EventHandler(this.btnlimpiarbuscador_Click);
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.btnbuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnbuscar.FlatAppearance.BorderSize = 0;
            this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnbuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnbuscar.IconColor = System.Drawing.Color.White;
            this.btnbuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnbuscar.IconSize = 20;
            this.btnbuscar.Location = new System.Drawing.Point(1235, 49);
            this.btnbuscar.Margin = new System.Windows.Forms.Padding(4);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(55, 28);
            this.btnbuscar.TabIndex = 75;
            this.btnbuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // cmbbusqueda
            // 
            this.cmbbusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.cmbbusqueda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbbusqueda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbusqueda.ForeColor = System.Drawing.Color.White;
            this.cmbbusqueda.FormattingEnabled = true;
            this.cmbbusqueda.Location = new System.Drawing.Point(892, 48);
            this.cmbbusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.cmbbusqueda.Name = "cmbbusqueda";
            this.cmbbusqueda.Size = new System.Drawing.Size(167, 28);
            this.cmbbusqueda.TabIndex = 74;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(788, 50);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 23);
            this.label11.TabIndex = 73;
            this.label11.Text = "Buscar por:";
            // 
            // txtid
            // 
            this.txtid.Location = new System.Drawing.Point(245, 71);
            this.txtid.Margin = new System.Windows.Forms.Padding(4);
            this.txtid.Name = "txtid";
            this.txtid.Size = new System.Drawing.Size(27, 22);
            this.txtid.TabIndex = 72;
            this.txtid.Text = "0";
            this.txtid.Visible = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(357, 37);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1023, 49);
            this.label10.TabIndex = 71;
            this.label10.Text = "Lista de Proveedores:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIndice
            // 
            this.txtIndice.Location = new System.Drawing.Point(211, 71);
            this.txtIndice.Margin = new System.Windows.Forms.Padding(4);
            this.txtIndice.Name = "txtIndice";
            this.txtIndice.Size = new System.Drawing.Size(27, 22);
            this.txtIndice.TabIndex = 78;
            this.txtIndice.Text = "-1";
            this.txtIndice.Visible = false;
            // 
            // dgvdata
            // 
            this.dgvdata.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(132)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvdata.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdata.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvdata.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
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
            this.btnSeleccionar,
            this.Id,
            this.Documento,
            this.RazonSocial,
            this.Correo,
            this.Telefono,
            this.EstadoValor,
            this.Estado});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(132)))), ((int)(((byte)(214)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvdata.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvdata.EnableHeadersVisualStyles = false;
            this.dgvdata.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(62)))), ((int)(((byte)(78)))));
            this.dgvdata.Location = new System.Drawing.Point(355, 100);
            this.dgvdata.Margin = new System.Windows.Forms.Padding(4);
            this.dgvdata.MultiSelect = false;
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
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvdata.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvdata.RowTemplate.Height = 28;
            this.dgvdata.Size = new System.Drawing.Size(1025, 427);
            this.dgvdata.TabIndex = 70;
            this.dgvdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvdata_CellContentClick);
            this.dgvdata.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvdata_CellFormatting);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.FillWeight = 6F;
            this.btnSeleccionar.HeaderText = "";
            this.btnSeleccionar.MinimumWidth = 6;
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Documento
            // 
            this.Documento.FillWeight = 60.58911F;
            this.Documento.HeaderText = "No Documento";
            this.Documento.MinimumWidth = 6;
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            // 
            // RazonSocial
            // 
            this.RazonSocial.FillWeight = 60.58911F;
            this.RazonSocial.HeaderText = "Razon Social";
            this.RazonSocial.MinimumWidth = 6;
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.ReadOnly = true;
            // 
            // Correo
            // 
            this.Correo.FillWeight = 60.58911F;
            this.Correo.HeaderText = "Correo";
            this.Correo.MinimumWidth = 6;
            this.Correo.Name = "Correo";
            this.Correo.ReadOnly = true;
            // 
            // Telefono
            // 
            this.Telefono.HeaderText = "Telefono";
            this.Telefono.MinimumWidth = 6;
            this.Telefono.Name = "Telefono";
            this.Telefono.ReadOnly = true;
            // 
            // EstadoValor
            // 
            this.EstadoValor.HeaderText = "EstadoValor";
            this.EstadoValor.MinimumWidth = 6;
            this.EstadoValor.Name = "EstadoValor";
            this.EstadoValor.ReadOnly = true;
            this.EstadoValor.Visible = false;
            // 
            // Estado
            // 
            this.Estado.FillWeight = 60.58911F;
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            // 
            // txtbusqueda
            // 
            this.txtbusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txtbusqueda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbusqueda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbusqueda.ForeColor = System.Drawing.Color.White;
            this.txtbusqueda.Location = new System.Drawing.Point(1067, 49);
            this.txtbusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.txtbusqueda.Name = "txtbusqueda";
            this.txtbusqueda.Size = new System.Drawing.Size(160, 27);
            this.txtbusqueda.TabIndex = 77;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(48, 76);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 23);
            this.label6.TabIndex = 79;
            this.label6.Text = "No Documento:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(227, 75);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(27, 22);
            this.textBox1.TabIndex = 80;
            this.textBox1.Text = "0";
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(191, 75);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(27, 22);
            this.textBox2.TabIndex = 81;
            this.textBox2.Text = "-1";
            this.textBox2.Visible = false;
            // 
            // txttelefono
            // 
            this.txttelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.txttelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txttelefono.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttelefono.ForeColor = System.Drawing.Color.White;
            this.txttelefono.Location = new System.Drawing.Point(52, 286);
            this.txttelefono.Margin = new System.Windows.Forms.Padding(4);
            this.txttelefono.MaxLength = 10;
            this.txttelefono.Name = "txttelefono";
            this.txttelefono.Size = new System.Drawing.Size(204, 27);
            this.txttelefono.TabIndex = 82;
            this.txttelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttelefono_KeyPress);
            // 
            // frmProveedores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 585);
            this.Controls.Add(this.txtbusqueda);
            this.Controls.Add(this.txttelefono);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnlimpiar);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.cmbestado);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcorreo);
            this.Controls.Add(this.txtrazonsocial);
            this.Controls.Add(this.txtdocumento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnlimpiarbuscador);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.cmbbusqueda);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtid);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtIndice);
            this.Controls.Add(this.dgvdata);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmProveedores";
            this.Text = "frmProveedores";
            this.Load += new System.EventHandler(this.frmProveedores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private FontAwesome.Sharp.IconButton btnlimpiar;
        private FontAwesome.Sharp.IconButton btnguardar;
        private System.Windows.Forms.ComboBox cmbestado;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtcorreo;
        private System.Windows.Forms.TextBox txtrazonsocial;
        private System.Windows.Forms.TextBox txtdocumento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnlimpiarbuscador;
        private FontAwesome.Sharp.IconButton btnbuscar;
        private System.Windows.Forms.ComboBox cmbbusqueda;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtid;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIndice;
        private System.Windows.Forms.DataGridView dgvdata;
        private System.Windows.Forms.TextBox txtbusqueda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewButtonColumn btnSeleccionar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Correo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txttelefono;
    }
}