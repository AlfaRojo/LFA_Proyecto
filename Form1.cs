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
        string resSETS = "";
        string resTOKENS = "";
        string resACTIONS = "";
        string resRESERVA = "";
        string resERROR = "";
        int repetidos = 0;
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
                        if (fileStream.Length == 0)
                        {
                            MessageBox.Show("El archivo no contiene información");
                            return;
                        }
                        try//SETS no es obligatorio que venga en el archivo
                        {
                            resSETS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("SETS"));
                            SETlabel.Visible = true;
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        catch (InvalidOperationException)
                        {
                            SETlabel.Visible = true;
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        try//TOKENS es obligatorio que venga
                        {
                            resTOKENS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("TOKENS"));
                            TOKENlabel.Visible = true;
                            TOKENlabel.Text = "TOKENS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("No contiene -TOKEN-");
                            return;
                        }
                        try//ACTIONS es obligatorio que venga
                        {
                            resACTIONS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ACTIONS"));
                            ACTIONlabel.Visible = true;
                            ACTIONlabel.Text = "ACTIONS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("No contiene -ACTIONS-");
                            return;
                        }
                        try//ERROR no es obligatorio que venga en el archivo
                        {
                            resERROR = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ERROR"));
                            ERRORlabel.Visible = true;
                            ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                        }
                        catch (InvalidOperationException)
                        {
                            ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                        }
                        var lecturaAux = string.Empty;
                        while (reader.Peek() > 0)
                        {
                            lecturaAux = reader.ReadLine();
                            if (lecturaAux.ToString() == resSETS)
                            {
                                while ((lecturaAux = reader.ReadLine()) != resSETS)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resACTIONS || lecturaAux == null)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaSets.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaSets.Count(); i++)//Agregar al GridView
                                {
                                    this.miDato.Rows.Add(i, Datos.Instance.listaSets.ElementAt(i), "SETS");
                                }
                            }
                            if (lecturaAux.ToString() == resTOKENS)
                            {
                                while ((lecturaAux = reader.ReadLine()) != resTOKENS)
                                {
                                    if (lecturaAux == resSETS || lecturaAux == resERROR || lecturaAux == resACTIONS || lecturaAux == null)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaToken.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaToken.Count(); i++)//Agregar al GridView
                                {
                                    this.miDato.Rows.Add(i, Datos.Instance.listaToken.ElementAt(i), "TOKENS");
                                }
                            }
                            if (lecturaAux.ToString() == resACTIONS)
                            {
                                var seguido = reader.ReadLine();
                                if (seguido.ToString().Replace(" ", "") == "RESERVADAS()")
                                {
                                    var next = reader.ReadLine();
                                    if (next.ToString().Replace(" ", "") == "{")
                                    {
                                        while ((lecturaAux = reader.ReadLine()) != resACTIONS)
                                        {
                                            if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resSETS || lecturaAux == null)//Condicion de salida
                                            {
                                                if (Datos.Instance.listaAction.Last().ToString() != "}")
                                                {
                                                    MessageBox.Show("ACTIONS debe finalizar en -}-");
                                                }
                                                Datos.Instance.listaAction.RemoveAt(Datos.Instance.listaAction.Count() - 1);
                                                break;
                                            }
                                            Datos.Instance.listaAction.Add(lecturaAux);
                                        }
                                        for (int i = 1; i < Datos.Instance.listaAction.Count(); i++)//Agregar al GridView
                                        {
                                            this.miDato.Rows.Add(i, Datos.Instance.listaAction.ElementAt(i), "ACTIONS");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ACTIONS debe iniciar en -{-");
                                    }
                                }
                                else//Sintaxis incorrecto entre ACTIONS & RESERVADAS()
                                {
                                    MessageBox.Show("ACTIONS debe de ir seguido de RESERVADAS()");
                                    return;
                                }
                            }
                            if (lecturaAux.ToString() == resERROR)
                            {
                                Datos.Instance.listaError.Add(lecturaAux);
                                while ((lecturaAux = reader.ReadLine()) != resERROR)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resACTIONS || lecturaAux == resSETS || lecturaAux == null)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaError.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaError.Count(); i++)//Agregar al GridView
                                {
                                    this.miDato.Rows.Add(i, Datos.Instance.listaError.ElementAt(i), "ERROR");
                                }
                            }
                        }
                    }
                }
            }
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;

            //NuevoArchivo(contArchivo);

            MessageBox.Show("Archivo leido correctamente", rutaArchivo, MessageBoxButtons.OK);//Solo confirmación visual
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
        private static IEnumerable<string> GetWordList(string linea)
        {
            return linea.Split(' ').Where(st => !st.Equals(""));
        }
    }
}
