using System.Windows.Forms;

namespace Proyecto_G4
{
    partial class Login : Form
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.btnIngresar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocument = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lnkOlvidarContraseña = new System.Windows.Forms.LinkLabel();
            this.btncancelar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkrecordar = new System.Windows.Forms.CheckBox();
            this.btnmostrarclave = new FontAwesome.Sharp.IconButton();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            this.pblogo = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIngresar
            // 
            this.btnIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.FlatAppearance.BorderSize = 0;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Location = new System.Drawing.Point(27, 392);
            this.btnIngresar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(225, 40);
            this.btnIngresar.TabIndex = 0;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.UseVisualStyleBackColor = false;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Número de Documento";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Contraseña";
            // 
            // txtDocument
            // 
            this.txtDocument.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            this.txtDocument.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDocument.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocument.ForeColor = System.Drawing.Color.White;
            this.txtDocument.Location = new System.Drawing.Point(61, 205);
            this.txtDocument.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDocument.Name = "txtDocument";
            this.txtDocument.Size = new System.Drawing.Size(423, 34);
            this.txtDocument.TabIndex = 3;
            this.txtDocument.Tag = "";
            this.txtDocument.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocument_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(54)))), ((int)(((byte)(82)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(61, 294);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(423, 34);
            this.txtPassword.TabIndex = 4;
            // 
            // lnkOlvidarContraseña
            // 
            this.lnkOlvidarContraseña.ActiveLinkColor = System.Drawing.Color.Red;
            this.lnkOlvidarContraseña.AutoSize = true;
            this.lnkOlvidarContraseña.BackColor = System.Drawing.Color.Transparent;
            this.lnkOlvidarContraseña.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkOlvidarContraseña.DisabledLinkColor = System.Drawing.Color.Silver;
            this.lnkOlvidarContraseña.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkOlvidarContraseña.ForeColor = System.Drawing.Color.White;
            this.lnkOlvidarContraseña.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lnkOlvidarContraseña.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(165)))), ((int)(((byte)(250)))));
            this.lnkOlvidarContraseña.Location = new System.Drawing.Point(346, 351);
            this.lnkOlvidarContraseña.Name = "lnkOlvidarContraseña";
            this.lnkOlvidarContraseña.Size = new System.Drawing.Size(178, 20);
            this.lnkOlvidarContraseña.TabIndex = 5;
            this.lnkOlvidarContraseña.TabStop = true;
            this.lnkOlvidarContraseña.Text = "¿Olvidaste tu contraseña?";
            this.lnkOlvidarContraseña.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOlvidarContraseña_LinkClicked);
            // 
            // btncancelar
            // 
            this.btncancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btncancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btncancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancelar.FlatAppearance.BorderSize = 0;
            this.btncancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancelar.ForeColor = System.Drawing.Color.White;
            this.btncancelar.Location = new System.Drawing.Point(299, 392);
            this.btncancelar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(225, 40);
            this.btncancelar.TabIndex = 6;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.UseVisualStyleBackColor = false;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(39)))), ((int)(((byte)(65)))));
            this.groupBox1.Controls.Add(this.chkrecordar);
            this.groupBox1.Controls.Add(this.btnmostrarclave);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtDocument);
            this.groupBox1.Controls.Add(this.iconPictureBox1);
            this.groupBox1.Controls.Add(this.iconPictureBox2);
            this.groupBox1.Controls.Add(this.pblogo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btncancelar);
            this.groupBox1.Controls.Add(this.lnkOlvidarContraseña);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnIngresar);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(220, 60);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(550, 480);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkrecordar
            // 
            this.chkrecordar.AutoSize = true;
            this.chkrecordar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkrecordar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkrecordar.ForeColor = System.Drawing.Color.White;
            this.chkrecordar.Location = new System.Drawing.Point(27, 343);
            this.chkrecordar.Name = "chkrecordar";
            this.chkrecordar.Size = new System.Drawing.Size(139, 32);
            this.chkrecordar.TabIndex = 13;
            this.chkrecordar.Text = "Recordarme";
            this.chkrecordar.UseVisualStyleBackColor = true;
            // 
            // btnmostrarclave
            // 
            this.btnmostrarclave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(39)))), ((int)(((byte)(65)))));
            this.btnmostrarclave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnmostrarclave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmostrarclave.FlatAppearance.BorderSize = 0;
            this.btnmostrarclave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmostrarclave.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.btnmostrarclave.IconColor = System.Drawing.Color.Gainsboro;
            this.btnmostrarclave.IconFont = FontAwesome.Sharp.IconFont.Regular;
            this.btnmostrarclave.IconSize = 40;
            this.btnmostrarclave.Location = new System.Drawing.Point(490, 294);
            this.btnmostrarclave.Name = "btnmostrarclave";
            this.btnmostrarclave.Size = new System.Drawing.Size(34, 34);
            this.btnmostrarclave.TabIndex = 12;
            this.btnmostrarclave.UseVisualStyleBackColor = false;
            this.btnmostrarclave.Click += new System.EventHandler(this.btnmostrarclave_Click);
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(84)))), ((int)(((byte)(102)))));
            this.iconPictureBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.iconPictureBox1.IconColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 34;
            this.iconPictureBox1.Location = new System.Drawing.Point(27, 205);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(34, 34);
            this.iconPictureBox1.TabIndex = 10;
            this.iconPictureBox1.TabStop = false;
            // 
            // iconPictureBox2
            // 
            this.iconPictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(84)))), ((int)(((byte)(102)))));
            this.iconPictureBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Key;
            this.iconPictureBox2.IconColor = System.Drawing.Color.Gainsboro;
            this.iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox2.IconSize = 34;
            this.iconPictureBox2.Location = new System.Drawing.Point(27, 294);
            this.iconPictureBox2.Name = "iconPictureBox2";
            this.iconPictureBox2.Size = new System.Drawing.Size(34, 34);
            this.iconPictureBox2.TabIndex = 11;
            this.iconPictureBox2.TabStop = false;
            // 
            // pblogo
            // 
            this.pblogo.Image = global::Proyecto_G4.Properties.Resources.perfil_del_usuario2;
            this.pblogo.Location = new System.Drawing.Point(243, 19);
            this.pblogo.Name = "pblogo";
            this.pblogo.Size = new System.Drawing.Size(68, 68);
            this.pblogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pblogo.TabIndex = 9;
            this.pblogo.TabStop = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(149, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(278, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Accede a tu usuario para continuar";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(180, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 41);
            this.label3.TabIndex = 7;
            this.label3.Text = "Iniciar Sesión";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.BackgroundImage = global::Proyecto_G4.Properties.Resources.fondo1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(937, 610);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Login";
            this.Text = "Inicio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnIngresar;
        private Label label1;
        private Label label2;
        private TextBox txtDocument;
        private TextBox txtPassword;
        private LinkLabel lnkOlvidarContraseña;
        private Button btncancelar;
        private GroupBox groupBox1;
        private Label label3;
        private Label label4;
        private PictureBox pblogo;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconButton btnmostrarclave;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private CheckBox chkrecordar;
    }
}