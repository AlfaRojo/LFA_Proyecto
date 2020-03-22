using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LFA_Proyecto.Arbol.PostOrden;

namespace LFA_Proyecto
{
    public partial class PostOrden : Form
    {
        public PostOrden()
        {
            InitializeComponent();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PostOrden_Load(object sender, EventArgs e)
        {
            DataGridViewUpdater dvu = new DataGridViewUpdater();
            dvu.UpdateDataGridView(MyPost);
        }
    }
}
