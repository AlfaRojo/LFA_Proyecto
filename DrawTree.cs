using LFA_Proyecto.Arbol;
using LFA_Proyecto.Help;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LFA_Proyecto
{
    public partial class DrawTree : Form
    {
        public DrawTree()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private ArbolB _tree;
        void PaintTree()
        {
            if (Datos.Instance.PilaS.Peek() == null)
            {
                return;
            }
            int temp;
            _tree = Datos.Instance.PilaS.Pop();
            pictureBox1.Image = _tree.Draw(out temp);
        }

        private void DrawTree_Load(object sender, EventArgs e)
        {
            PaintTree();
        }
    }
}
