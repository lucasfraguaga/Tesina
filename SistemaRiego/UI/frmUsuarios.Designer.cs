namespace UI
{
    partial class frmUsuarios
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
            this.cmdGuardarFamilia = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.grpPatentes = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cboFamilias = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cboPatentes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdConfigurar = new System.Windows.Forms.Button();
            this.cboUsuarios = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grpPatentes.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdGuardarFamilia
            // 
            this.cmdGuardarFamilia.Location = new System.Drawing.Point(278, 228);
            this.cmdGuardarFamilia.Margin = new System.Windows.Forms.Padding(2);
            this.cmdGuardarFamilia.Name = "cmdGuardarFamilia";
            this.cmdGuardarFamilia.Size = new System.Drawing.Size(110, 22);
            this.cmdGuardarFamilia.TabIndex = 10;
            this.cmdGuardarFamilia.Text = "Guardar cambios";
            this.cmdGuardarFamilia.UseVisualStyleBackColor = true;
            this.cmdGuardarFamilia.Click += new System.EventHandler(this.cmdGuardarFamilia_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(278, 11);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(276, 213);
            this.treeView1.TabIndex = 9;
            // 
            // grpPatentes
            // 
            this.grpPatentes.Controls.Add(this.button2);
            this.grpPatentes.Controls.Add(this.cboFamilias);
            this.grpPatentes.Controls.Add(this.label3);
            this.grpPatentes.Controls.Add(this.button1);
            this.grpPatentes.Controls.Add(this.cboPatentes);
            this.grpPatentes.Controls.Add(this.label1);
            this.grpPatentes.Controls.Add(this.cmdConfigurar);
            this.grpPatentes.Controls.Add(this.cboUsuarios);
            this.grpPatentes.Controls.Add(this.label2);
            this.grpPatentes.Location = new System.Drawing.Point(6, 1);
            this.grpPatentes.Margin = new System.Windows.Forms.Padding(2);
            this.grpPatentes.Name = "grpPatentes";
            this.grpPatentes.Padding = new System.Windows.Forms.Padding(2);
            this.grpPatentes.Size = new System.Drawing.Size(256, 249);
            this.grpPatentes.TabIndex = 8;
            this.grpPatentes.TabStop = false;
            this.grpPatentes.Text = "Usuarios";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 219);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 26);
            this.button2.TabIndex = 13;
            this.button2.Text = "Agregar >>";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cboFamilias
            // 
            this.cboFamilias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFamilias.FormattingEnabled = true;
            this.cboFamilias.Location = new System.Drawing.Point(11, 194);
            this.cboFamilias.Margin = new System.Windows.Forms.Padding(2);
            this.cboFamilias.Name = "cboFamilias";
            this.cboFamilias.Size = new System.Drawing.Size(234, 21);
            this.cboFamilias.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Agregar Familias";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 151);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 25);
            this.button1.TabIndex = 10;
            this.button1.Text = "Agregar >>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboPatentes
            // 
            this.cboPatentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatentes.FormattingEnabled = true;
            this.cboPatentes.Location = new System.Drawing.Point(11, 127);
            this.cboPatentes.Margin = new System.Windows.Forms.Padding(2);
            this.cboPatentes.Name = "cboPatentes";
            this.cboPatentes.Size = new System.Drawing.Size(234, 21);
            this.cboPatentes.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Agregar patentes";
            // 
            // cmdConfigurar
            // 
            this.cmdConfigurar.Location = new System.Drawing.Point(11, 63);
            this.cmdConfigurar.Margin = new System.Windows.Forms.Padding(2);
            this.cmdConfigurar.Name = "cmdConfigurar";
            this.cmdConfigurar.Size = new System.Drawing.Size(83, 28);
            this.cmdConfigurar.TabIndex = 7;
            this.cmdConfigurar.Text = "Configurar";
            this.cmdConfigurar.UseVisualStyleBackColor = true;
            this.cmdConfigurar.Click += new System.EventHandler(this.cmdConfigurar_Click);
            // 
            // cboUsuarios
            // 
            this.cboUsuarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuarios.FormattingEnabled = true;
            this.cboUsuarios.Location = new System.Drawing.Point(11, 39);
            this.cboUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.cboUsuarios.Name = "cboUsuarios";
            this.cboUsuarios.Size = new System.Drawing.Size(234, 21);
            this.cboUsuarios.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Todos los usuarios";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(444, 228);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 22);
            this.button3.TabIndex = 11;
            this.button3.Text = "Volver";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 415);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Lenguaje";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(95, 414);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // frmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmdGuardarFamilia);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.grpPatentes);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "frmUsuarios";
            this.Text = "frmPatentesFamilias";
            this.Load += new System.EventHandler(this.frmUsuarios_Load);
            this.grpPatentes.ResumeLayout(false);
            this.grpPatentes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdGuardarFamilia;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox grpPatentes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cboFamilias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboPatentes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdConfigurar;
        private System.Windows.Forms.ComboBox cboUsuarios;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}