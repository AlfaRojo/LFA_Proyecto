namespace LFA_Proyecto
{
    partial class Form1
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

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.rutaLabel = new System.Windows.Forms.Label();
            this.miDato = new System.Windows.Forms.DataGridView();
            this.Fila = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETlabel = new System.Windows.Forms.Label();
            this.TOKENlabel = new System.Windows.Forms.Label();
            this.ACTIONlabel = new System.Windows.Forms.Label();
            this.ERRORlabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.ER_TOKEN = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.miDato)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 50);
            this.button1.TabIndex = 0;
            this.button1.Text = "Examinar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rutaLabel
            // 
            this.rutaLabel.AutoEllipsis = true;
            this.rutaLabel.AutoSize = true;
            this.rutaLabel.ForeColor = System.Drawing.Color.White;
            this.rutaLabel.Location = new System.Drawing.Point(12, 65);
            this.rutaLabel.Name = "rutaLabel";
            this.rutaLabel.Size = new System.Drawing.Size(38, 17);
            this.rutaLabel.TabIndex = 1;
            this.rutaLabel.Text = "Ruta";
            // 
            // miDato
            // 
            this.miDato.AllowUserToAddRows = false;
            this.miDato.AllowUserToDeleteRows = false;
            this.miDato.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.miDato.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.miDato.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.miDato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.miDato.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fila,
            this.VALOR,
            this.TIPO});
            this.miDato.GridColor = System.Drawing.Color.Lime;
            this.miDato.Location = new System.Drawing.Point(12, 85);
            this.miDato.Name = "miDato";
            this.miDato.RowHeadersWidth = 51;
            this.miDato.RowTemplate.Height = 24;
            this.miDato.Size = new System.Drawing.Size(776, 150);
            this.miDato.TabIndex = 2;
            this.miDato.Visible = false;
            // 
            // Fila
            // 
            this.Fila.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Fila.HeaderText = "FILA";
            this.Fila.MinimumWidth = 6;
            this.Fila.Name = "Fila";
            this.Fila.Width = 65;
            // 
            // VALOR
            // 
            this.VALOR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.VALOR.HeaderText = "VALOR";
            this.VALOR.MinimumWidth = 6;
            this.VALOR.Name = "VALOR";
            this.VALOR.Width = 84;
            // 
            // TIPO
            // 
            this.TIPO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TIPO.HeaderText = "TIPO";
            this.TIPO.MinimumWidth = 6;
            this.TIPO.Name = "TIPO";
            this.TIPO.Width = 69;
            // 
            // SETlabel
            // 
            this.SETlabel.AutoSize = true;
            this.SETlabel.BackColor = System.Drawing.Color.Transparent;
            this.SETlabel.ForeColor = System.Drawing.Color.Red;
            this.SETlabel.Location = new System.Drawing.Point(12, 238);
            this.SETlabel.Name = "SETlabel";
            this.SETlabel.Size = new System.Drawing.Size(82, 17);
            this.SETlabel.TabIndex = 3;
            this.SETlabel.Text = "LABEL SET";
            this.SETlabel.Visible = false;
            // 
            // TOKENlabel
            // 
            this.TOKENlabel.AutoSize = true;
            this.TOKENlabel.BackColor = System.Drawing.Color.Transparent;
            this.TOKENlabel.ForeColor = System.Drawing.Color.Red;
            this.TOKENlabel.Location = new System.Drawing.Point(12, 255);
            this.TOKENlabel.Name = "TOKENlabel";
            this.TOKENlabel.Size = new System.Drawing.Size(103, 17);
            this.TOKENlabel.TabIndex = 4;
            this.TOKENlabel.Text = "LABEL TOKEN";
            this.TOKENlabel.Visible = false;
            // 
            // ACTIONlabel
            // 
            this.ACTIONlabel.AutoSize = true;
            this.ACTIONlabel.BackColor = System.Drawing.Color.Transparent;
            this.ACTIONlabel.ForeColor = System.Drawing.Color.Red;
            this.ACTIONlabel.Location = new System.Drawing.Point(12, 272);
            this.ACTIONlabel.Name = "ACTIONlabel";
            this.ACTIONlabel.Size = new System.Drawing.Size(115, 17);
            this.ACTIONlabel.TabIndex = 5;
            this.ACTIONlabel.Text = "LABEL ACTIONS";
            this.ACTIONlabel.Visible = false;
            // 
            // ERRORlabel
            // 
            this.ERRORlabel.AutoSize = true;
            this.ERRORlabel.BackColor = System.Drawing.Color.Transparent;
            this.ERRORlabel.ForeColor = System.Drawing.Color.Red;
            this.ERRORlabel.Location = new System.Drawing.Point(12, 289);
            this.ERRORlabel.Name = "ERRORlabel";
            this.ERRORlabel.Size = new System.Drawing.Size(105, 17);
            this.ERRORlabel.TabIndex = 7;
            this.ERRORlabel.Text = "LABEL ERROR";
            this.ERRORlabel.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(561, 352);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 86);
            this.button2.TabIndex = 8;
            this.button2.Text = "Modificar Expresión";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ER_TOKEN
            // 
            this.ER_TOKEN.AutoSize = true;
            this.ER_TOKEN.ForeColor = System.Drawing.Color.White;
            this.ER_TOKEN.Location = new System.Drawing.Point(9, 424);
            this.ER_TOKEN.Name = "ER_TOKEN";
            this.ER_TOKEN.Size = new System.Drawing.Size(178, 17);
            this.ER_TOKEN.TabIndex = 9;
            this.ER_TOKEN.Text = "<ID>.\" \"*.\"=\".\" \"*.\"\'\".<c>.\"\'\".#";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ER_TOKEN);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ERRORlabel);
            this.Controls.Add(this.ACTIONlabel);
            this.Controls.Add(this.TOKENlabel);
            this.Controls.Add(this.SETlabel);
            this.Controls.Add(this.miDato);
            this.Controls.Add(this.rutaLabel);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Lenguajes Formales & Autómatas";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.miDato)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label rutaLabel;
        private System.Windows.Forms.DataGridView miDato;
        private System.Windows.Forms.Label SETlabel;
        private System.Windows.Forms.Label TOKENlabel;
        private System.Windows.Forms.Label ACTIONlabel;
        private System.Windows.Forms.Label ERRORlabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fila;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.Label ER_TOKEN;
    }
}

