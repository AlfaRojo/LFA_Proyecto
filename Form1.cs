using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        #region Valores
        int thisSET = 0;
        int thisTOKENS = 1;
        int thisACTION = 2;
        int thisERROR = 3;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Datos.Instance.DiccionarioColeccion.Clear();
            var contArchivo = string.Empty;
            var rutaArchivo = string.Empty;

            using (OpenFileDialog actuArchivo = new OpenFileDialog())
            {
                actuArchivo.InitialDirectory = "c:\\";
                actuArchivo.Filter = "txt files (*.txt)|*.txt";
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;

                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivo = actuArchivo.FileName;
                    var fileStream = actuArchivo.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        contArchivo = reader.ReadToEnd();
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;
            NuevoArchivo(contArchivo);

            MessageBox.Show(contArchivo, "Contenido del archivo: " + rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
        }
        public void NuevoArchivo(string myFile)
        {
            var NuevosValores = new Valores
            {
                //Agregar todos los valores del archivo
            };
        }
    }
}
