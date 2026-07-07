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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuusuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menugestor = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuCategory = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuProducts = new FontAwesome.Sharp.IconMenuItem();
            this.menuventas = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuRegistrar = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuDV = new FontAwesome.Sharp.IconMenuItem();
            this.menucompras = new FontAwesome.Sharp.IconMenuItem();
            this.SubmenuRegistrarC = new FontAwesome.Sharp.IconMenuItem();
            this.SubMenuDC = new FontAwesome.Sharp.IconMenuItem();
            this.menuproveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuclientes = new FontAwesome.Sharp.IconMenuItem();
            this.menureportes = new FontAwesome.Sharp.IconMenuItem();
            this.menuacercade = new FontAwesome.Sharp.IconMenuItem();
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contenedor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuusuarios,
            this.menugestor,
            this.menuventas,
            this.menucompras,
            this.menuproveedores,
            this.menuclientes,
            this.menureportes,
            this.menuacercade});
            this.menu.Location = new System.Drawing.Point(0, 87);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1158, 78);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // menuusuarios
            // 
            this.menuusuarios.AutoSize = false;
            this.menuusuarios.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuusuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuusuarios.IconColor = System.Drawing.Color.MediumPurple;
            this.menuusuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuusuarios.IconSize = 55;
            this.menuusuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuusuarios.Name = "menuusuarios";
            this.menuusuarios.Size = new System.Drawing.Size(90, 74);
            this.menuusuarios.Text = "Usuarios";
            this.menuusuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuusuarios.Click += new System.EventHandler(this.menuusuarios_Click);
            // 
            // menugestor
            // 
            this.menugestor.AutoSize = false;
            this.menugestor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuCategory,
            this.SubMenuProducts});
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
            // subMenuCategory
            // 
            this.subMenuCategory.IconChar = FontAwesome.Sharp.IconChar.None;
            this.subMenuCategory.IconColor = System.Drawing.Color.Black;
            this.subMenuCategory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.subMenuCategory.Name = "subMenuCategory";
            this.subMenuCategory.Size = new System.Drawing.Size(158, 26);
            this.subMenuCategory.Text = "Categoria";
            this.subMenuCategory.Click += new System.EventHandler(this.subMenuCategory_Click);
            // 
            // SubMenuProducts
            // 
            this.SubMenuProducts.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuProducts.IconColor = System.Drawing.Color.Black;
            this.SubMenuProducts.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuProducts.Name = "SubMenuProducts";
            this.SubMenuProducts.Size = new System.Drawing.Size(158, 26);
            this.SubMenuProducts.Text = "Productos";
            this.SubMenuProducts.Click += new System.EventHandler(this.SubMenuProducts_Click);
            // 
            // menuventas
            // 
            this.menuventas.AutoSize = false;
            this.menuventas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRegistrar,
            this.SubMenuDV});
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
            // SubMenuRegistrar
            // 
            this.SubMenuRegistrar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuRegistrar.IconColor = System.Drawing.Color.Black;
            this.SubMenuRegistrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuRegistrar.Name = "SubMenuRegistrar";
            this.SubMenuRegistrar.Size = new System.Drawing.Size(181, 26);
            this.SubMenuRegistrar.Text = "Registrar";
            this.SubMenuRegistrar.Click += new System.EventHandler(this.SubMenuRegistrar_Click);
            // 
            // SubMenuDV
            // 
            this.SubMenuDV.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuDV.IconColor = System.Drawing.Color.Black;
            this.SubMenuDV.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuDV.Name = "SubMenuDV";
            this.SubMenuDV.Size = new System.Drawing.Size(181, 26);
            this.SubMenuDV.Text = "Detalle Venta";
            this.SubMenuDV.Click += new System.EventHandler(this.SubMenuDV_Click);
            // 
            // menucompras
            // 
            this.menucompras.AutoSize = false;
            this.menucompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubmenuRegistrarC,
            this.SubMenuDC});
            this.menucompras.ForeColor = System.Drawing.Color.MediumPurple;
            this.menucompras.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
            this.menucompras.IconColor = System.Drawing.Color.MediumPurple;
            this.menucompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menucompras.IconSize = 55;
            this.menucompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menucompras.Name = "menucompras";
            this.menucompras.Size = new System.Drawing.Size(122, 74);
            this.menucompras.Text = "Compras";
            this.menucompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // SubmenuRegistrarC
            // 
            this.SubmenuRegistrarC.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubmenuRegistrarC.IconColor = System.Drawing.Color.Black;
            this.SubmenuRegistrarC.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubmenuRegistrarC.Name = "SubmenuRegistrarC";
            this.SubmenuRegistrarC.Size = new System.Drawing.Size(224, 26);
            this.SubmenuRegistrarC.Text = "Registar ";
            this.SubmenuRegistrarC.Click += new System.EventHandler(this.SubmenuRegistrarC_Click);
            // 
            // SubMenuDC
            // 
            this.SubMenuDC.IconChar = FontAwesome.Sharp.IconChar.None;
            this.SubMenuDC.IconColor = System.Drawing.Color.Black;
            this.SubMenuDC.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.SubMenuDC.Name = "SubMenuDC";
            this.SubMenuDC.Size = new System.Drawing.Size(224, 26);
            this.SubMenuDC.Text = "Detalle Compra";
            this.SubMenuDC.Click += new System.EventHandler(this.SubMenuDC_Click);
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
            this.menuproveedores.Click += new System.EventHandler(this.menuproveedores_Click);
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
            this.menuclientes.Click += new System.EventHandler(this.menuclientes_Click);
            // 
            // menureportes
            // 
            this.menureportes.AutoSize = false;
            this.menureportes.ForeColor = System.Drawing.Color.MediumPurple;
            this.menureportes.IconChar = FontAwesome.Sharp.IconChar.ClipboardList;
            this.menureportes.IconColor = System.Drawing.Color.MediumPurple;
            this.menureportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menureportes.IconSize = 55;
            this.menureportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menureportes.Name = "menureportes";
            this.menureportes.Size = new System.Drawing.Size(90, 74);
            this.menureportes.Text = "Reportes";
            this.menureportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menureportes.Click += new System.EventHandler(this.menureportes_click);
            // 
            // menuacercade
            // 
            this.menuacercade.AutoSize = false;
            this.menuacercade.ForeColor = System.Drawing.Color.MediumPurple;
            this.menuacercade.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.menuacercade.IconColor = System.Drawing.Color.MediumPurple;
            this.menuacercade.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuacercade.IconSize = 55;
            this.menuacercade.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuacercade.Name = "menuacercade";
            this.menuacercade.Size = new System.Drawing.Size(90, 74);
            this.menuacercade.Text = "Acerca de ";
            this.menuacercade.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuacercade.Click += new System.EventHandler(this.menuacercade_Click);
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
            this.label1.Size = new System.Drawing.Size(312, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "Loboru Sublima";
            // 
            // contenedor
            // 
            this.contenedor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("contenedor.BackgroundImage")));
            this.contenedor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 165);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1158, 479);
            this.contenedor.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MediumPurple;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(949, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuario:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.MediumPurple;
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(1027, 39);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(94, 22);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "lblUsuario";
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1158, 644);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.label2);
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
            this.Text = "Menu Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menu;
        private MenuStrip menutitulo;
        private Label label1;
        private FontAwesome.Sharp.IconMenuItem menuacercade;
        private FontAwesome.Sharp.IconMenuItem menuusuarios;
        private FontAwesome.Sharp.IconMenuItem menugestor;
        private FontAwesome.Sharp.IconMenuItem menuventas;
        private FontAwesome.Sharp.IconMenuItem menucompras;
        private FontAwesome.Sharp.IconMenuItem menuproveedores;
        private FontAwesome.Sharp.IconMenuItem menuclientes;
        private Panel contenedor;
        private FontAwesome.Sharp.IconMenuItem menureportes;
        private Label label2;
        private Label lblUsuario;
        private FontAwesome.Sharp.IconMenuItem subMenuCategory;
        private FontAwesome.Sharp.IconMenuItem SubMenuProducts;
        private FontAwesome.Sharp.IconMenuItem SubMenuRegistrar;
        private FontAwesome.Sharp.IconMenuItem SubMenuDV;
        private FontAwesome.Sharp.IconMenuItem SubmenuRegistrarC;
        private FontAwesome.Sharp.IconMenuItem SubMenuDC;
    }
}