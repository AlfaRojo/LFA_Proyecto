﻿namespace LFA_Proyecto
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
            this.FLFN_Data = new System.Windows.Forms.DataGridView();
            this.TOKEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.First = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nuller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Generar = new System.Windows.Forms.Button();
            this.TextBoxER = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.Label();
            this.EstadoData = new System.Windows.Forms.DataGridView();
            this.FollowData = new System.Windows.Forms.DataGridView();
            this.Simbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Exportar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.miDato)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FLFN_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstadoData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FollowData)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Lime;
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
            this.rutaLabel.ForeColor = System.Drawing.Color.Lime;
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
            this.miDato.BackgroundColor = System.Drawing.Color.Black;
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
            this.miDato.Size = new System.Drawing.Size(604, 150);
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
            this.SETlabel.ForeColor = System.Drawing.Color.Lime;
            this.SETlabel.Location = new System.Drawing.Point(12, 538);
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
            this.TOKENlabel.ForeColor = System.Drawing.Color.Lime;
            this.TOKENlabel.Location = new System.Drawing.Point(12, 555);
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
            this.ACTIONlabel.ForeColor = System.Drawing.Color.Lime;
            this.ACTIONlabel.Location = new System.Drawing.Point(12, 572);
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
            this.ERRORlabel.ForeColor = System.Drawing.Color.Lime;
            this.ERRORlabel.Location = new System.Drawing.Point(12, 589);
            this.ERRORlabel.Name = "ERRORlabel";
            this.ERRORlabel.Size = new System.Drawing.Size(105, 17);
            this.ERRORlabel.TabIndex = 7;
            this.ERRORlabel.Text = "LABEL ERROR";
            this.ERRORlabel.Visible = false;
            // 
            // FLFN_Data
            // 
            this.FLFN_Data.AllowUserToAddRows = false;
            this.FLFN_Data.AllowUserToDeleteRows = false;
            this.FLFN_Data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FLFN_Data.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.FLFN_Data.BackgroundColor = System.Drawing.Color.Black;
            this.FLFN_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FLFN_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TOKEN,
            this.First,
            this.Last,
            this.Nuller});
            this.FLFN_Data.GridColor = System.Drawing.Color.Lime;
            this.FLFN_Data.Location = new System.Drawing.Point(12, 241);
            this.FLFN_Data.Name = "FLFN_Data";
            this.FLFN_Data.RowHeadersWidth = 51;
            this.FLFN_Data.RowTemplate.Height = 24;
            this.FLFN_Data.Size = new System.Drawing.Size(604, 150);
            this.FLFN_Data.TabIndex = 10;
            this.FLFN_Data.Visible = false;
            // 
            // TOKEN
            // 
            this.TOKEN.HeaderText = "TOKEN";
            this.TOKEN.MinimumWidth = 6;
            this.TOKEN.Name = "TOKEN";
            this.TOKEN.Width = 85;
            // 
            // First
            // 
            this.First.HeaderText = "First";
            this.First.MinimumWidth = 6;
            this.First.Name = "First";
            this.First.Width = 64;
            // 
            // Last
            // 
            this.Last.HeaderText = "Last";
            this.Last.MinimumWidth = 6;
            this.Last.Name = "Last";
            this.Last.Width = 64;
            // 
            // Nuller
            // 
            this.Nuller.HeaderText = "Nuller";
            this.Nuller.MinimumWidth = 6;
            this.Nuller.Name = "Nuller";
            this.Nuller.Width = 74;
            // 
            // Generar
            // 
            this.Generar.BackColor = System.Drawing.Color.Transparent;
            this.Generar.Enabled = false;
            this.Generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Generar.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Generar.ForeColor = System.Drawing.Color.Lime;
            this.Generar.Location = new System.Drawing.Point(871, 553);
            this.Generar.Name = "Generar";
            this.Generar.Size = new System.Drawing.Size(142, 50);
            this.Generar.TabIndex = 11;
            this.Generar.Text = "Generar";
            this.Generar.UseVisualStyleBackColor = false;
            this.Generar.Click += new System.EventHandler(this.Generar_Click);
            // 
            // TextBoxER
            // 
            this.TextBoxER.Location = new System.Drawing.Point(179, 12);
            this.TextBoxER.Name = "TextBoxER";
            this.TextBoxER.Size = new System.Drawing.Size(834, 22);
            this.TextBoxER.TabIndex = 12;
            this.TextBoxER.Text = "ExpresionRegular";
            // 
            // txtTime
            // 
            this.txtTime.AutoEllipsis = true;
            this.txtTime.AutoSize = true;
            this.txtTime.ForeColor = System.Drawing.Color.Lime;
            this.txtTime.Location = new System.Drawing.Point(179, 45);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(120, 17);
            this.txtTime.TabIndex = 13;
            this.txtTime.Text = "Tiempo Ejecución";
            // 
            // EstadoData
            // 
            this.EstadoData.AllowUserToAddRows = false;
            this.EstadoData.AllowUserToDeleteRows = false;
            this.EstadoData.BackgroundColor = System.Drawing.Color.Black;
            this.EstadoData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EstadoData.Location = new System.Drawing.Point(613, 241);
            this.EstadoData.Name = "EstadoData";
            this.EstadoData.RowHeadersWidth = 51;
            this.EstadoData.RowTemplate.Height = 24;
            this.EstadoData.Size = new System.Drawing.Size(390, 150);
            this.EstadoData.TabIndex = 14;
            this.EstadoData.Visible = false;
            // 
            // FollowData
            // 
            this.FollowData.AllowUserToAddRows = false;
            this.FollowData.AllowUserToDeleteRows = false;
            this.FollowData.BackgroundColor = System.Drawing.Color.Black;
            this.FollowData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FollowData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Simbolo,
            this.TransData});
            this.FollowData.Location = new System.Drawing.Point(613, 85);
            this.FollowData.Name = "FollowData";
            this.FollowData.RowHeadersWidth = 51;
            this.FollowData.RowTemplate.Height = 24;
            this.FollowData.Size = new System.Drawing.Size(390, 150);
            this.FollowData.TabIndex = 15;
            this.FollowData.Visible = false;
            // 
            // Simbolo
            // 
            this.Simbolo.HeaderText = "Simbolo";
            this.Simbolo.MinimumWidth = 6;
            this.Simbolo.Name = "Simbolo";
            this.Simbolo.Width = 125;
            // 
            // TransData
            // 
            this.TransData.HeaderText = "Follow";
            this.TransData.MinimumWidth = 6;
            this.TransData.Name = "TransData";
            this.TransData.Width = 125;
            // 
            // Exportar
            // 
            this.Exportar.BackColor = System.Drawing.Color.Transparent;
            this.Exportar.Enabled = false;
            this.Exportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exportar.Font = new System.Drawing.Font("Modern No. 20", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exportar.ForeColor = System.Drawing.Color.Lime;
            this.Exportar.Location = new System.Drawing.Point(723, 553);
            this.Exportar.Name = "Exportar";
            this.Exportar.Size = new System.Drawing.Size(142, 50);
            this.Exportar.TabIndex = 16;
            this.Exportar.Text = "Exportar";
            this.Exportar.UseVisualStyleBackColor = false;
            this.Exportar.Click += new System.EventHandler(this.Exportar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1025, 615);
            this.Controls.Add(this.Exportar);
            this.Controls.Add(this.FollowData);
            this.Controls.Add(this.EstadoData);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.TextBoxER);
            this.Controls.Add(this.Generar);
            this.Controls.Add(this.FLFN_Data);
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
            ((System.ComponentModel.ISupportInitialize)(this.FLFN_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstadoData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FollowData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label rutaLabel;
        private System.Windows.Forms.DataGridView miDato;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fila;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.Label SETlabel;
        private System.Windows.Forms.Label TOKENlabel;
        private System.Windows.Forms.Label ACTIONlabel;
        private System.Windows.Forms.Label ERRORlabel;
        private System.Windows.Forms.DataGridView FLFN_Data;
        private System.Windows.Forms.Button Generar;
        private System.Windows.Forms.TextBox TextBoxER;
        private System.Windows.Forms.Label txtTime;
        private System.Windows.Forms.DataGridView EstadoData;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOKEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn First;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nuller;
        private System.Windows.Forms.DataGridView FollowData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Simbolo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransData;
        private System.Windows.Forms.Button Exportar;
    }
}

