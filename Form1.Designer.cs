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
            this.SETS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOKENS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTIONS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ERROR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETlabel = new System.Windows.Forms.Label();
            this.TOKENlabel = new System.Windows.Forms.Label();
            this.RESERVAlabel = new System.Windows.Forms.Label();
            this.ACTIONlabel = new System.Windows.Forms.Label();
            this.ERRORlabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
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
            this.miDato.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.miDato.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SETS,
            this.TOKENS,
            this.ACTIONS,
            this.ERROR});
            this.miDato.Location = new System.Drawing.Point(12, 85);
            this.miDato.Name = "miDato";
            this.miDato.RowHeadersWidth = 51;
            this.miDato.RowTemplate.Height = 24;
            this.miDato.Size = new System.Drawing.Size(463, 150);
            this.miDato.TabIndex = 2;
            this.miDato.Visible = false;
            // 
            // SETS
            // 
            this.SETS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SETS.HeaderText = "SETS";
            this.SETS.MinimumWidth = 6;
            this.SETS.Name = "SETS";
            // 
            // TOKENS
            // 
            this.TOKENS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TOKENS.HeaderText = "TOKENS";
            this.TOKENS.MinimumWidth = 6;
            this.TOKENS.Name = "TOKENS";
            // 
            // ACTIONS
            // 
            this.ACTIONS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ACTIONS.HeaderText = "ACTIONS";
            this.ACTIONS.MinimumWidth = 6;
            this.ACTIONS.Name = "ACTIONS";
            // 
            // ERROR
            // 
            this.ERROR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ERROR.HeaderText = "ERROR";
            this.ERROR.MinimumWidth = 6;
            this.ERROR.Name = "ERROR";
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
            // 
            // RESERVAlabel
            // 
            this.RESERVAlabel.AutoSize = true;
            this.RESERVAlabel.BackColor = System.Drawing.Color.Transparent;
            this.RESERVAlabel.ForeColor = System.Drawing.Color.Red;
            this.RESERVAlabel.Location = new System.Drawing.Point(12, 289);
            this.RESERVAlabel.Name = "RESERVAlabel";
            this.RESERVAlabel.Size = new System.Drawing.Size(149, 17);
            this.RESERVAlabel.TabIndex = 6;
            this.RESERVAlabel.Text = "LABEL RESERVADA()";
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
            // 
            // ERRORlabel
            // 
            this.ERRORlabel.AutoSize = true;
            this.ERRORlabel.BackColor = System.Drawing.Color.Transparent;
            this.ERRORlabel.ForeColor = System.Drawing.Color.Red;
            this.ERRORlabel.Location = new System.Drawing.Point(12, 306);
            this.ERRORlabel.Name = "ERRORlabel";
            this.ERRORlabel.Size = new System.Drawing.Size(105, 17);
            this.ERRORlabel.TabIndex = 7;
            this.ERRORlabel.Text = "LABEL ERROR";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ERRORlabel);
            this.Controls.Add(this.RESERVAlabel);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn SETS;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOKENS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACTIONS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ERROR;
        private System.Windows.Forms.Label SETlabel;
        private System.Windows.Forms.Label TOKENlabel;
        private System.Windows.Forms.Label RESERVAlabel;
        private System.Windows.Forms.Label ACTIONlabel;
        private System.Windows.Forms.Label ERRORlabel;
        private System.Windows.Forms.Button button2;
    }
}

