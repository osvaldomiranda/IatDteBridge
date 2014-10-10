namespace IatDteBridge
{
    partial class FormEmisor
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
            this.label_rutempresa = new System.Windows.Forms.Label();
            this.textBox_rutempresa = new System.Windows.Forms.TextBox();
            this.label_razonsocial = new System.Windows.Forms.Label();
            this.textBox_razonsocial = new System.Windows.Forms.TextBox();
            this.label_giro = new System.Windows.Forms.Label();
            this.textBox_giro = new System.Windows.Forms.TextBox();
            this.label_telefono = new System.Windows.Forms.Label();
            this.textBox_telefono = new System.Windows.Forms.TextBox();
            this.labelcorreoemisor = new System.Windows.Forms.Label();
            this.textBoxcorreoemisor = new System.Windows.Forms.TextBox();
            this.labelsucursales = new System.Windows.Forms.Label();
            this.textBoxsucursales = new System.Windows.Forms.TextBox();
            this.button_guardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_rutempresa
            // 
            this.label_rutempresa.AutoSize = true;
            this.label_rutempresa.Location = new System.Drawing.Point(13, 15);
            this.label_rutempresa.Name = "label_rutempresa";
            this.label_rutempresa.Size = new System.Drawing.Size(68, 13);
            this.label_rutempresa.TabIndex = 0;
            this.label_rutempresa.Text = "Rut Empresa";
            this.label_rutempresa.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox_rutempresa
            // 
            this.textBox_rutempresa.Location = new System.Drawing.Point(87, 13);
            this.textBox_rutempresa.Name = "textBox_rutempresa";
            this.textBox_rutempresa.Size = new System.Drawing.Size(263, 20);
            this.textBox_rutempresa.TabIndex = 1;
            // 
            // label_razonsocial
            // 
            this.label_razonsocial.AutoSize = true;
            this.label_razonsocial.Location = new System.Drawing.Point(13, 48);
            this.label_razonsocial.Name = "label_razonsocial";
            this.label_razonsocial.Size = new System.Drawing.Size(70, 13);
            this.label_razonsocial.TabIndex = 2;
            this.label_razonsocial.Text = "Razón Social";
            // 
            // textBox_razonsocial
            // 
            this.textBox_razonsocial.Location = new System.Drawing.Point(87, 48);
            this.textBox_razonsocial.Name = "textBox_razonsocial";
            this.textBox_razonsocial.Size = new System.Drawing.Size(263, 20);
            this.textBox_razonsocial.TabIndex = 3;
            // 
            // label_giro
            // 
            this.label_giro.AutoSize = true;
            this.label_giro.Location = new System.Drawing.Point(15, 82);
            this.label_giro.Name = "label_giro";
            this.label_giro.Size = new System.Drawing.Size(26, 13);
            this.label_giro.TabIndex = 4;
            this.label_giro.Text = "Giro";
            // 
            // textBox_giro
            // 
            this.textBox_giro.Location = new System.Drawing.Point(88, 82);
            this.textBox_giro.Name = "textBox_giro";
            this.textBox_giro.Size = new System.Drawing.Size(260, 20);
            this.textBox_giro.TabIndex = 5;
            // 
            // label_telefono
            // 
            this.label_telefono.AutoSize = true;
            this.label_telefono.Location = new System.Drawing.Point(14, 117);
            this.label_telefono.Name = "label_telefono";
            this.label_telefono.Size = new System.Drawing.Size(49, 13);
            this.label_telefono.TabIndex = 6;
            this.label_telefono.Text = "Telefono";
            // 
            // textBox_telefono
            // 
            this.textBox_telefono.Location = new System.Drawing.Point(88, 117);
            this.textBox_telefono.Name = "textBox_telefono";
            this.textBox_telefono.Size = new System.Drawing.Size(260, 20);
            this.textBox_telefono.TabIndex = 7;
            // 
            // labelcorreoemisor
            // 
            this.labelcorreoemisor.AutoSize = true;
            this.labelcorreoemisor.Location = new System.Drawing.Point(17, 158);
            this.labelcorreoemisor.Name = "labelcorreoemisor";
            this.labelcorreoemisor.Size = new System.Drawing.Size(38, 13);
            this.labelcorreoemisor.TabIndex = 8;
            this.labelcorreoemisor.Text = "Correo";
            // 
            // textBoxcorreoemisor
            // 
            this.textBoxcorreoemisor.Location = new System.Drawing.Point(87, 153);
            this.textBoxcorreoemisor.Name = "textBoxcorreoemisor";
            this.textBoxcorreoemisor.Size = new System.Drawing.Size(260, 20);
            this.textBoxcorreoemisor.TabIndex = 9;
            // 
            // labelsucursales
            // 
            this.labelsucursales.AutoSize = true;
            this.labelsucursales.Location = new System.Drawing.Point(17, 192);
            this.labelsucursales.Name = "labelsucursales";
            this.labelsucursales.Size = new System.Drawing.Size(59, 13);
            this.labelsucursales.TabIndex = 10;
            this.labelsucursales.Text = "Sucursales";
            // 
            // textBoxsucursales
            // 
            this.textBoxsucursales.Location = new System.Drawing.Point(87, 188);
            this.textBoxsucursales.Name = "textBoxsucursales";
            this.textBoxsucursales.Size = new System.Drawing.Size(260, 20);
            this.textBoxsucursales.TabIndex = 11;
            // 
            // button_guardar
            // 
            this.button_guardar.Location = new System.Drawing.Point(87, 283);
            this.button_guardar.Name = "button_guardar";
            this.button_guardar.Size = new System.Drawing.Size(75, 23);
            this.button_guardar.TabIndex = 12;
            this.button_guardar.Text = "Guarda";
            this.button_guardar.UseVisualStyleBackColor = true;
            this.button_guardar.Click += new System.EventHandler(this.button_guardar_Click);
            // 
            // FormEmisor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 335);
            this.Controls.Add(this.button_guardar);
            this.Controls.Add(this.textBoxsucursales);
            this.Controls.Add(this.labelsucursales);
            this.Controls.Add(this.textBoxcorreoemisor);
            this.Controls.Add(this.labelcorreoemisor);
            this.Controls.Add(this.textBox_telefono);
            this.Controls.Add(this.label_telefono);
            this.Controls.Add(this.textBox_giro);
            this.Controls.Add(this.label_giro);
            this.Controls.Add(this.textBox_razonsocial);
            this.Controls.Add(this.label_razonsocial);
            this.Controls.Add(this.textBox_rutempresa);
            this.Controls.Add(this.label_rutempresa);
            this.Name = "FormEmisor";
            this.Text = "Datos Emisor";
            this.Load += new System.EventHandler(this.FormEmisor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_rutempresa;
        private System.Windows.Forms.TextBox textBox_rutempresa;
        private System.Windows.Forms.Label label_razonsocial;
        private System.Windows.Forms.TextBox textBox_razonsocial;
        private System.Windows.Forms.Label label_giro;
        private System.Windows.Forms.TextBox textBox_giro;
        private System.Windows.Forms.Label label_telefono;
        private System.Windows.Forms.TextBox textBox_telefono;
        private System.Windows.Forms.Label labelcorreoemisor;
        private System.Windows.Forms.TextBox textBoxcorreoemisor;
        private System.Windows.Forms.Label labelsucursales;
        private System.Windows.Forms.TextBox textBoxsucursales;
        private System.Windows.Forms.Button button_guardar;
    }
}