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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.miDato);
            this.Controls.Add(this.rutaLabel);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Lenguajes Formales & Autómatas";
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
    }
}

