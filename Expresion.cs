using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LFA_Proyecto.Help;

namespace LFA_Proyecto
{
    public partial class Expresion : Form
    {
        public Expresion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Datos.Instance.eTOKEN.Clear();//Elimina los datos previos
            Datos.Instance.eTOKEN.Add(textBox1.Text);//Agrega nuevos datos
            MessageBox.Show("Expresión regular cambiada a " + textBox1.Text);//Confirmación
            this.Hide();
        }
    }
}
