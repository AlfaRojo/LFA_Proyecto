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

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var contArchivo = string.Empty;
            var rutaArchivo = string.Empty;

            using (OpenFileDialog actuArchivo = new OpenFileDialog())
            {
                actuArchivo.InitialDirectory = "c:\\";
                actuArchivo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;

                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    rutaArchivo = actuArchivo.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = actuArchivo.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        contArchivo = reader.ReadToEnd();
                    }
                }
            }

            MessageBox.Show(contArchivo, "File Content at path: " + rutaArchivo, MessageBoxButtons.OK);
        }
    }
}
