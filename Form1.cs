using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        #region Valores
        string pathToFile = "";
        int thisSET = 0;
        int thisTOKENS = 1;
        int thisACTION = 2;
        int thisERROR = 3;
        char[] Delimitadores = { '.', ' ', '\n' };
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

            Datos.Instance.DiccionarioColeccion.Add("SETS", "SETS");

            using (OpenFileDialog actuArchivo = new OpenFileDialog())
            {
                actuArchivo.InitialDirectory = @"~/LFA_Proyecto/PRUEBAS";
                actuArchivo.Filter = "txt files (*.txt)|*.txt";
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;

                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {
                    rutaArchivo = actuArchivo.FileName;
                    var fileStream = actuArchivo.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (reader.Peek() >= 0)
                        {
                            contArchivo = reader.ReadLine();
                            if (contArchivo.Contains("SETS"))
                            {
                                Datos.Instance.listaSets.Add(contArchivo);
                            }
                            else if (contArchivo.Contains("TOKENS"))
                            {
                                Datos.Instance.listaToken.Add(contArchivo);
                            }
                            else if (contArchivo.Contains("ACTIONS"))
                            {
                                Datos.Instance.listaAction.Add(contArchivo);
                            }
                            else if (contArchivo.Contains("ERROR"))
                            {
                                Datos.Instance.listaError.Add(contArchivo);
                            }
                        }
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;

            var OperacionSintaxis = new Sintaxis
            {
                SintaxisEscrita = contArchivo
            };
            var DelimitadorSETS = Datos.Instance.DiccionarioColeccion.ElementAt(thisSET).Key;
            string[] ArreglOperaciones = Regex.Split(OperacionSintaxis.SintaxisEscrita, DelimitadorSETS);

            //NuevoArchivo(contArchivo);

            MessageBox.Show(contArchivo, "Contenido del archivo: " + rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
        }
        public void NuevoArchivo(FileStream myFile)
        {
            //myFile.Split(Delimitadores);


            var NuevosValores = new Valores
            {

            };
        }
    }
}
