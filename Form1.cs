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
            AgregarDiccionario();
            var contArchivo = string.Empty;
            var rutaArchivo = string.Empty;

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
                    using (StreamReader reader = new StreamReader(fileStream))//----------------Lectura del archivo----------------\\
                    {
                        contArchivo = reader.ReadLine();
                        string resSETS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("SETS"));
                        string resTOKENS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("TOKENS"));
                        string resACTIONS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ACTIONS"));
                        string resRESERVA = File.ReadAllLines(rutaArchivo).First(X => X.Contains("RESERVADAS()"));
                        string resERROR = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ERROR"));

                        SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        TOKENlabel.Text = "TOKENS " + ComprobarString(resTOKENS);
                        ACTIONlabel.Text = "ACTIONS " + ComprobarString(resACTIONS);
                        RESERVAlabel.Text = "RESERVA() " + ComprobarString(resRESERVA);
                        ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;
            var DelimitadorSETS = Datos.Instance.DiccionarioColeccion.ElementAt(thisSET).Key;
            var DelimitadorTOKEN = Datos.Instance.DiccionarioColeccion.ElementAt(thisTOKENS).Key;
            var DelimitadorACTION = Datos.Instance.DiccionarioColeccion.ElementAt(thisACTION).Key;
            var DelimitadorERROR = Datos.Instance.DiccionarioColeccion.ElementAt(thisERROR).Key;

            //NuevoArchivo(contArchivo);

            MessageBox.Show(contArchivo, "Contenido del archivo: " + rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
        }
        String ComprobarString(string myString)
        {
            if (String.IsNullOrEmpty(myString))
            {
                return " no se encuentra en el archivo";
            }
            return " se encuentra en el archivo";
        }
        public void NuevoArchivo(FileStream myFile)
        {
            //myFile.Split(Delimitadores);
            var NuevosValores = new Valores
            {

            };
        }
        public void AgregarDiccionario()
        {
            if (Datos.Instance.DiccionarioColeccion.Count != 0)
            {
                return;
            }
            else
            {
                Datos.Instance.DiccionarioColeccion.Add("SETS", "SETS");
                Datos.Instance.DiccionarioColeccion.Add("TOKENS", "TOKENS");
                Datos.Instance.DiccionarioColeccion.Add("ACTIONS", "ACTIONS");
                Datos.Instance.DiccionarioColeccion.Add("ERROR", "ERROR");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
