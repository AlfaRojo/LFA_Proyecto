namespace LFA_Proyecto
{
    partial class PostOrden
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
            this.MyPost = new System.Windows.Forms.DataGridView();
            this.Post = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MyPost)).BeginInit();
            this.SuspendLayout();
            // 
            // MyPost
            // 
            this.MyPost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MyPost.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Post});
            this.MyPost.Location = new System.Drawing.Point(12, 12);
            this.MyPost.Name = "MyPost";
            this.MyPost.RowHeadersWidth = 51;
            this.MyPost.RowTemplate.Height = 24;
            this.MyPost.Size = new System.Drawing.Size(180, 426);
            this.MyPost.TabIndex = 0;
            this.MyPost.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Post
            // 
            this.Post.HeaderText = "PostOrden";
            this.Post.MinimumWidth = 6;
            this.Post.Name = "Post";
            this.Post.Width = 125;
            // 
            // PostOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(205, 450);
            this.Controls.Add(this.MyPost);
            this.Name = "PostOrden";
            this.Text = "PostOrden";
            this.Load += new System.EventHandler(this.PostOrden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MyPost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView MyPost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Post;
    }
}