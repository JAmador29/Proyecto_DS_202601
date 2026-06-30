using System.Windows.Forms;

namespace Proyecto_G4
{
    partial class MenuPrincipal
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuusuario = new FontAwesome.Sharp.IconMenuItem();
            this.menugestor = new FontAwesome.Sharp.IconMenuItem();
            this.menuventas = new FontAwesome.Sharp.IconMenuItem();
            this.menucompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuproveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuclientes = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem1 = new FontAwesome.Sharp.IconMenuItem();
            this.menureportes = new FontAwesome.Sharp.IconMenuItem();
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuusuario,
            this.menugestor,
            this.menuventas,
            this.menucompras,
            this.menuproveedores,
            this.menuclientes,
            this.iconMenuItem1,
            this.menureportes});
            this.menu.Location = new System.Drawing.Point(0, 87);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1158, 78);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuusuario
            // 
            this.menuusuario.AutoSize = false;
            this.menuusuario.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuusuario.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuusuario.IconColor = System.Drawing.Color.MediumPurple;
            this.menuusuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuusuario.IconSize = 55;
            this.menuusuario.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuusuario.Name = "menuusuario";
            this.menuusuario.Size = new System.Drawing.Size(90, 74);
            this.menuusuario.Text = "Usuarios";
            this.menuusuario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menugestor
            // 
            this.menugestor.AutoSize = false;
            this.menugestor.ForeColor = System.Drawing.Color.MediumPurple;
            this.menugestor.IconChar = FontAwesome.Sharp.IconChar.Gears;
            this.menugestor.IconColor = System.Drawing.Color.MediumPurple;
            this.menugestor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menugestor.IconSize = 55;
            this.menugestor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menugestor.Name = "menugestor";
            this.menugestor.Size = new System.Drawing.Size(90, 74);
            this.menugestor.Text = "Gestor";
            this.menugestor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuventas
            // 
            this.menuventas.AutoSize = false;
            this.menuventas.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuventas.IconChar = FontAwesome.Sharp.IconChar.HandHoldingUsd;
            this.menuventas.IconColor = System.Drawing.Color.MediumPurple;
            this.menuventas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuventas.IconSize = 55;
            this.menuventas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuventas.Name = "menuventas";
            this.menuventas.Size = new System.Drawing.Size(90, 74);
            this.menuventas.Text = "Ventas";
            this.menuventas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menucompras
            // 
            this.menucompras.AutoSize = false;
            this.menucompras.ForeColor = System.Drawing.Color.MediumPurple;
            this.menucompras.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
            this.menucompras.IconColor = System.Drawing.Color.MediumPurple;
            this.menucompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menucompras.IconSize = 55;
            this.menucompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menucompras.Name = "menucompras";
            this.menucompras.Size = new System.Drawing.Size(90, 74);
            this.menucompras.Text = "Compras";
            this.menucompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuproveedores
            // 
            this.menuproveedores.AutoSize = false;
            this.menuproveedores.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuproveedores.IconChar = FontAwesome.Sharp.IconChar.Dolly;
            this.menuproveedores.IconColor = System.Drawing.Color.MediumPurple;
            this.menuproveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuproveedores.IconSize = 55;
            this.menuproveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuproveedores.Name = "menuproveedores";
            this.menuproveedores.Size = new System.Drawing.Size(90, 74);
            this.menuproveedores.Text = "Proveedores";
            this.menuproveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menuclientes
            // 
            this.menuclientes.AutoSize = false;
            this.menuclientes.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuclientes.IconChar = FontAwesome.Sharp.IconChar.UserTag;
            this.menuclientes.IconColor = System.Drawing.Color.MediumPurple;
            this.menuclientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuclientes.IconSize = 55;
            this.menuclientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuclientes.Name = "menuclientes";
            this.menuclientes.Size = new System.Drawing.Size(90, 74);
            this.menuclientes.Text = "Clientes";
            this.menuclientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // iconMenuItem1
            // 
            this.iconMenuItem1.AutoSize = false;
            this.iconMenuItem1.ForeColor = System.Drawing.Color.MediumPurple;
            this.iconMenuItem1.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.iconMenuItem1.IconColor = System.Drawing.Color.MediumPurple;
            this.iconMenuItem1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem1.IconSize = 55;
            this.iconMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem1.Name = "iconMenuItem1";
            this.iconMenuItem1.Size = new System.Drawing.Size(90, 74);
            this.iconMenuItem1.Text = "Reportes";
            this.iconMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // menureportes
            // 
            this.menureportes.AutoSize = false;
            this.menureportes.ForeColor = System.Drawing.Color.MediumPurple;
            this.menureportes.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.menureportes.IconColor = System.Drawing.Color.MediumPurple;
            this.menureportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menureportes.IconSize = 55;
            this.menureportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menureportes.Name = "menureportes";
            this.menureportes.Size = new System.Drawing.Size(90, 74);
            this.menureportes.Text = "Acerca de ";
            this.menureportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menureportes.Click += new System.EventHandler(this.menureportes_Click);
            // 
            // menutitulo
            // 
            this.menutitulo.AutoSize = false;
            this.menutitulo.BackColor = System.Drawing.Color.MediumPurple;
            this.menutitulo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menutitulo.Location = new System.Drawing.Point(0, 0);
            this.menutitulo.Name = "menutitulo";
            this.menutitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menutitulo.Size = new System.Drawing.Size(1158, 87);
            this.menutitulo.TabIndex = 1;
            this.menutitulo.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.MediumPurple;
            this.label1.Font = new System.Drawing.Font("Arial Black", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Loboru Sublima";
            // 
            // contenedor
            // 
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 165);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1158, 479);
            this.contenedor.TabIndex = 3;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1158, 644);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.menutitulo);
            this.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrincipal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menu;
        private MenuStrip menutitulo;
        private Label label1;
        private FontAwesome.Sharp.IconMenuItem menureportes;
        private FontAwesome.Sharp.IconMenuItem menuusuario;
        private FontAwesome.Sharp.IconMenuItem menugestor;
        private FontAwesome.Sharp.IconMenuItem menuventas;
        private FontAwesome.Sharp.IconMenuItem menucompras;
        private FontAwesome.Sharp.IconMenuItem menuproveedores;
        private FontAwesome.Sharp.IconMenuItem menuclientes;
        private Panel contenedor;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem1;
    }
}