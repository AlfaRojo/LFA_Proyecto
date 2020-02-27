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
        string resERROR = "";
        char[] Delimitadores = { '.', ' ', '\n' };
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RebootList();
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
                        #region EXISTENCIAS SETS/TOKENS/ACTIONS/ERROR
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
                            MessageBox.Show("El archivo debe llevar -TOKENS-");
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
                        #endregion
                        var lecturaAux = string.Empty;
                        while (reader.Peek() > 0)
                        {
                            lecturaAux = reader.ReadLine();
                            #region All SETS Sintaxis //Falta comprobar concatenación con signo +
                            if (lecturaAux.ToString().Replace(" ", "") == "SETS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resSETS)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resACTIONS || lecturaAux == null)//Condicion de salida
                                    {
                                        if (Datos.Instance.listaSets.Count() < 1)//Sii existe SETS en el archivo, debe tener almenos uno
                                        {
                                            MessageBox.Show("Si el archivo contiene SETS, debe de llevar almenos un SET");
                                            return;
                                        }
                                        break;
                                    }
                                    Datos.Instance.listaSets.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaSets.Count(); i++)//Agregar al GridView
                                {
                                    this.miDato.Rows.Add(i, Datos.Instance.listaSets.ElementAt(i).Replace(" ", ""), "SETS");
                                }
                            }
                            #endregion
                            #region All TOKENS Sintaxis //Falta comprobar la expresion regular(F)
                            if (lecturaAux.ToString().Replace(" ", "") == "TOKENS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resTOKENS)
                                {
                                    if (lecturaAux == resSETS || lecturaAux == resERROR || lecturaAux == resACTIONS || lecturaAux == null)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaToken.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaToken.Count(); i++)//Agregar al GridView y comprobar sintaxis
                                {
                                    if (!Datos.Instance.listaToken.ElementAt(i).Contains("TOKEN"))//Error de escritura
                                    {
                                        MessageBox.Show("TOKEN en la linea " + i + " no contiene TOKEN o se encunetra mal escrito");
                                        return;
                                    }
                                    if ((Datos.Instance.listaToken.ElementAt(i)).All(char.IsDigit))//No contiene dígito----NO FUNCIONA, REVISAR!
                                    {
                                        MessageBox.Show("TOKEN en la linea " + i + " no contiene numeración válida");
                                        return;
                                    }
                                    if (!Datos.Instance.listaToken.ElementAt(i).Contains("="))//Error de igualación
                                    {
                                        MessageBox.Show("TOKEN en la linea " + i + " no contiene =");
                                        return;
                                    }
                                    //Agregar la comprobación de expresión regular
                                    this.miDato.Rows.Add(i, Datos.Instance.listaToken.ElementAt(i), "TOKENS");
                                }
                            }
                            #endregion
                            #region All ACTIONS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace(" ", "") == "ACTIONS")
                            {
                                lecturaAux = reader.ReadLine();//Siguiente linea
                                if (lecturaAux.ToString().Replace(" ", "") == "RESERVADAS()")//Comprobar que le siga RESERVADAS()
                                {
                                    lecturaAux = reader.ReadLine();//Siguiente linea
                                    if (lecturaAux.ToString().Replace(" ", "") == "{")//Comprobar 3ra linea abra con llave {
                                    {
                                        while ((lecturaAux = reader.ReadLine()) != resACTIONS)
                                        {
                                            if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resSETS || lecturaAux == null)//Condicion de salida
                                            {
                                                if (Datos.Instance.listaAction.Last().ToString() != "}")//Sii el ultimo no es de cerrar llave }
                                                {
                                                    MessageBox.Show("ACTIONS debe finalizar en -}-");
                                                }
                                                Datos.Instance.listaAction.RemoveAt(Datos.Instance.listaAction.Count() - 1);//Sii lo contiene, se elimina, inesesario
                                                break;
                                            }
                                            Datos.Instance.listaAction.Add(lecturaAux);//Agregar a su lista respectiva
                                        }
                                        for (int i = 0; i < Datos.Instance.listaAction.Count(); i++)//Agregar al GridView y comprobar Sintaxis
                                        {
                                            string[] compERROR = Datos.Instance.listaAction.ElementAt(i).Split('=');
                                            try
                                            {
                                                if (!(int.TryParse(compERROR[0], out int x)))
                                                {
                                                    MessageBox.Show("ACTIONS en la linea " + i + " no inicia con valor numérico");
                                                    return;
                                                }
                                                if (!(compERROR[1].StartsWith("'")) && !(compERROR[1].EndsWith("'")))
                                                {
                                                    MessageBox.Show("ACTIONS en la linea " + i + " no inicia o finaliza correctamente");
                                                    return;
                                                }
                                            }
                                            catch (IndexOutOfRangeException)
                                            {
                                                MessageBox.Show("ACTIONS en la linea " + i + " no inicia correctamente");
                                                return;
                                            }
                                            //Agregar comprobación de número
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
                            #endregion
                            #region All ERROR Sintaxis //COMPLETO
                            if (lecturaAux.ToString() == resERROR)
                            {
                                Datos.Instance.listaError.Add(lecturaAux);
                                while ((lecturaAux = reader.ReadLine()) != resERROR)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resACTIONS || lecturaAux == resSETS || lecturaAux == null)//Condicion de salida
                                    {
                                        if (Datos.Instance.listaError.Count() < 1)
                                        {
                                            MessageBox.Show("Archivo debe contener almenos un ERROR");
                                            return;
                                        }
                                        break;
                                    }
                                    Datos.Instance.listaError.Add(lecturaAux);
                                }
                                for (int i = 0; i < Datos.Instance.listaError.Count(); i++)//Agregar al GridView y comprobar Sintaxis
                                {    
                                    string[] compERROR = Datos.Instance.listaError.ElementAt(i).Split('=');
                                    try
                                    {
                                        if (!(compERROR[0].Contains("ERROR")))
                                        {
                                            MessageBox.Show("ERROR en la linea " + i + " no inicia correctamente");
                                            return;
                                        }
                                        if (!(int.TryParse(compERROR[1], out int x)))
                                        {
                                            MessageBox.Show("ERROR en la linea " + i + " no contiene valor numérico");
                                            return;
                                        }
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        MessageBox.Show("ERROR en la linea " + i + " no inicia correctamente");
                                        return;
                                    }
                                    this.miDato.Rows.Add(i, Datos.Instance.listaError.ElementAt(i), "ERROR");
                                }
                            }
                            #endregion
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
        private void AgregarDiccionario()//Para que un usuario lo pueda editar posteriormente
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
        private void Form1_Load(object sender, EventArgs e)//Expresiones Regulares generadas manualmente
        {
            string thisTOKEN = ER_TOKEN.Text;
            for (int i = 0; i < thisTOKEN.Length; i++)
            {
                Datos.Instance.eTOKEN.Add(ER_TOKEN.Text.Substring(i, 1));//Guarda caracter por caracter para la ER_ET
            }
        }
        private void button2_Click(object sender, EventArgs e)//Poder cambiar la Expresión Regular manualmente(NO USAR)
        {
            Expresion Expresion = new Expresion();
            Expresion.Show();
        }
        private static IEnumerable<string> GetWordList(string linea)
        {
            return linea.Split(' ').Where(st => !st.Equals(""));
        }//Comprobar cantidad de caracteres en una linea
        private void RebootList()
        {
            Datos.Instance.listaAction.Clear();
            Datos.Instance.listaError.Clear();
            Datos.Instance.listaSets.Clear();
            Datos.Instance.listaToken.Clear();
        }//Reiniciar listas sin necesidad de cerrar el programa
    }
}
