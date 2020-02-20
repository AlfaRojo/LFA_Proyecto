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
                        while ((contArchivo = reader.ReadLine()) != null)
                        {
                            string resSETS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("SETS"));
                            string resTOKENS = "";
                            string resACTIONS = "";
                            string resRESERVA = "";
                            try//TOKENS es obligatorio que venga
                            {
                                resTOKENS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("TOKENS"));
                            }
                            catch (InvalidOperationException)
                            {
                                MessageBox.Show("No contiene -TOKEN-");
                                return;
                            }
                            try//ACTIONS es obligatorio que venga
                            {
                                resACTIONS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ACTIONS"));
                                try//RESERVADAS() es obligatorio que venga seguido de ACTIONS
                                {
                                    resRESERVA = File.ReadAllLines(rutaArchivo).First(X => X.Contains("RESERVADAS()"));
                                }
                                catch (InvalidOperationException)
                                {

                                    MessageBox.Show("No contiene -RESERVAS()-");
                                    return;
                                }
                            }
                            catch (InvalidOperationException)
                            {
                                MessageBox.Show("No contiene -ACTIONS-");
                                return;
                            }
                            string resERROR = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ERROR"));

                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                            TOKENlabel.Text = "TOKENS " + ComprobarString(resTOKENS);
                            ACTIONlabel.Text = "ACTIONS " + ComprobarString(resACTIONS);
                            RESERVAlabel.Text = "RESERVA() " + ComprobarString(resRESERVA);
                            ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                        }
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;
            var DelimitadorSETS = Datos.Instance.diccionarioColeccion.ElementAt(thisSET).Key;
            var DelimitadorTOKEN = Datos.Instance.diccionarioColeccion.ElementAt(thisTOKENS).Key;
            var DelimitadorACTION = Datos.Instance.diccionarioColeccion.ElementAt(thisACTION).Key;
            var DelimitadorERROR = Datos.Instance.diccionarioColeccion.ElementAt(thisERROR).Key;

            //NuevoArchivo(contArchivo);

            MessageBox.Show("Archivo leido correctamente",rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
        }
        String ComprobarString(string myString)
        {
            if (String.IsNullOrEmpty(myString))
            {
                return " no se encuentra en el archivo";
            }
            return " se encuentra en el archivo";
        }
        public void AgregarDiccionario()//Para que un usuario lo pueda editar posteriormente
        {
            if (Datos.Instance.diccionarioColeccion.Count != 0)
            {
                return;
            }
            else
            {
                Datos.Instance.diccionarioColeccion.Add("SETS", "SETS");
                Datos.Instance.diccionarioColeccion.Add("TOKENS", "TOKENS");
                Datos.Instance.diccionarioColeccion.Add("ACTIONS", "ACTIONS");
                Datos.Instance.diccionarioColeccion.Add("ERROR", "ERROR");
                return;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Datos.Instance.eTOKEN.Add("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Expresion Expresion = new Expresion();
            Expresion.Show();
        }
    }
}
