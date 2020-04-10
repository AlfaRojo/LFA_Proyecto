using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using LFA_Proyecto.Help;
using System.Text;
using System.Diagnostics;
using LFA_Proyecto.Modelos;

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        #region Valores
        int Max;
        string resSETS = "";
        string resTOKENS = "";
        string resACTIONS = "";
        string resERROR = "";
        char[] Delimitadores = { ' ', '\t', '\n', '\r' };
        char[] AlfabetoMinuscula;
        char[] AlfabetoMayuscula;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
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
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        catch (InvalidOperationException)
                        {
                            SETlabel.Text = "SETS " + ComprobarString(resSETS);
                        }
                        try//TOKENS es obligatorio que venga
                        {
                            resTOKENS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("TOKENS"));
                            TOKENlabel.Text = "TOKENS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene -TOKENS- o se encuentra mal escrito");
                            return;
                        }
                        try//ACTIONS es obligatorio que venga
                        {
                            resACTIONS = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ACTIONS"));
                            ACTIONlabel.Text = "ACTIONS se encuentra en el archivo";
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene -ACTIONS- o se encuentra mal escrito");
                            return;
                        }
                        try//ERROR no es obligatorio que venga en el archivo
                        {
                            resERROR = File.ReadAllLines(rutaArchivo).First(X => X.Contains("ERROR"));
                            ERRORlabel.Text = "ERROR " + ComprobarString(resERROR);
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("El archivo no contiene almenos un -ERROR- o se encuentra mal escrito");
                            return;
                        }
                        #endregion
                        var lecturaAux = string.Empty;
                        while (reader.Peek() > 0)
                        {
                            lecturaAux = reader.ReadLine();
                            #region All SETS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("\t", "").Replace(" ", "") == "SETS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resSETS)
                                {
                                    if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == resACTIONS)//Condicion de salida
                                    {
                                        break;
                                    }
                                    Datos.Instance.listaSets.Add(lecturaAux);
                                }
                                if (Datos.Instance.listaSets.Contains(""))
                                {
                                    Datos.Instance.listaSets.RemoveAll(X => X == "");
                                }
                                if (Datos.Instance.listaSets.Count() < 1)//Sii existe SETS en el archivo, debe tener almenos uno
                                {
                                    MessageBox.Show("Si el archivo contiene SETS, debe de llevar almenos un SET");
                                    return;
                                }
                                for (int i = 0; i < Datos.Instance.listaSets.Count(); i++)//Agregar al GridView
                                {
                                    if (Datos.Instance.listaSets.ElementAt(i).Contains("=") && Datos.Instance.listaSets.ElementAt(i).Contains("'"))
                                    {
                                        string[] SeparadorIgual = Datos.Instance.listaSets.ElementAt(i).Trim(Delimitadores).Replace(" ", "").Split('=');//Hacer split y comprobar derecha
                                        SpliterIgual(SeparadorIgual, sender, e);
                                    }
                                    if (!(Datos.Instance.listaSets.ElementAt(i).Contains("'")))
                                    {
                                        string[] SeparadorChar = Datos.Instance.listaSets.ElementAt(i).Trim(Delimitadores).Replace(" ", "").Split('=');//Hacer split y comprobar derecha
                                        SpliterChar(SeparadorChar, sender, e);
                                    }
                                    if (Datos.Instance.listaSets.ElementAt(i).Contains("+"))//Comprobar concatenación con signo +
                                    {
                                        string[] Delimitado = Datos.Instance.listaSets.ElementAt(i).Split('+');//Hacer split y comprobar derecha e izquierda
                                        SpliterMas(Delimitado, sender, e);
                                    }
                                    this.miDato.Rows.Add(i, Datos.Instance.listaSets.ElementAt(i).Replace(" ", "").Trim(Delimitadores), "SETS");
                                    string[] toList = Datos.Instance.listaSets.ElementAt(i).Split('=');
                                    Datos.Instance.TiposSETS.Add(toList[0].Replace("\t", "").Replace(" ", ""));
                                    if (Datos.Instance.listaSets.ElementAt(i) == "LETRA")
                                    {
                                        GetLETRA(Datos.Instance.listaSets.ElementAt(i));
                                    }
                                }
                            }
                            #endregion
                            #region All TOKENS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("( |\t)", "").Replace(" ", "") == "TOKENS")
                            {
                                while ((lecturaAux = reader.ReadLine()) != resTOKENS)
                                {
                                    if (lecturaAux == "SETS" || lecturaAux == resERROR || lecturaAux == resACTIONS)//Condicion de salida
                                    {
                                        break;
                                    }
                                    if (lecturaAux == "\t" || lecturaAux == " ")
                                    {
                                        lecturaAux = reader.ReadLine();
                                    }
                                    Datos.Instance.listaToken.Add(lecturaAux);
                                }
                                if (Datos.Instance.listaToken.Contains(""))
                                {
                                    Datos.Instance.listaToken.RemoveAll(X => X == "");
                                }
                                for (int i = 0; i < Datos.Instance.listaToken.Count(); i++)//Agregar al GridView y comprobar sintaxis
                                {
                                    try
                                    {
                                        var Delimitador = Datos.Instance.listaToken.ElementAt(i).Trim(Delimitadores);
                                        if (Delimitador == "")
                                        {
                                            Datos.Instance.listaToken.RemoveAt(i);
                                        }
                                        string myText = Datos.Instance.listaToken.ElementAt(i);
                                        myText = Regex.Replace(myText, @"\s+", "");
                                        if (!myText.Trim(Delimitadores).Contains("TOKEN"))//Error de escritura
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene TOKEN o se encunetra mal escrito.");
                                            return;
                                        }
                                        if (myText.All(char.IsDigit))//No contiene dígito----NO FUNCIONA, REVISAR!
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene numeración válida.");
                                            return;
                                        }
                                        if (!myText.Contains("="))//Error de igualación
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene =.");
                                            return;
                                        }
                                        else//Enviar split de igual para comprobar Sintaxis
                                        {
                                            string[] Linea = myText.Split('=');
                                            SpliterTOKEN(Linea, sender, e);
                                        }
                                        //Agregar la comprobación de expresión regular
                                        this.miDato.Rows.Add(i, myText, "TOKENS");
                                        if (Datos.Instance.SimbolosTerminales.Count() == 0)//Inicio Simbolos Terminales
                                        {
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0, "("));
                                        }
                                        if (myText.Contains("'"))
                                        {
                                            string[] toList = myText.Split(new char[] { '=' }, 2);
                                            if (toList[1].Contains("="))
                                            {
                                                toList[1] = toList[1].Trim('\'');
                                                if (toList[1].Contains("'"))
                                                {
                                                    var txtAuxiliar = toList[1].Split('\'');
                                                    var aux = txtAuxiliar[0] + '.';
                                                    aux = aux + txtAuxiliar[2];
                                                    toList[1] = aux;
                                                }
                                                var numero = Regex.Match(toList[0], @"\d+").Value;
                                                int numAgregado = Int32.Parse(numero);
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, toList[1]));
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0,"|"));
                                            }
                                            else if (toList[1].Length > 4 && !(toList[1].Contains("DIGITO")) && !(toList[1].Contains("LETRA")) && !(toList[1].Contains("CHARSET")))
                                            {
                                                var listaAuxiliar = new List<string>();
                                                for (int j = 0; j < toList[1].Length; j++)
                                                {
                                                    listaAuxiliar.Add(toList[1].Substring(j, 1));//Guarda caracter por caracteres
                                                }
                                                var txtAgregado = myText.Split('=');
                                                var numero = Regex.Match(txtAgregado[0], @"\d+").Value;
                                                int numAgregado = Int32.Parse(numero);
                                                try
                                                {
                                                    txtAgregado[1] = listaAuxiliar[1] + "." + listaAuxiliar[4] + "." + listaAuxiliar[7];
                                                }
                                                catch (Exception)
                                                {
                                                    txtAgregado[1] = listaAuxiliar[1] + "." + listaAuxiliar[4];
                                                }
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txtAgregado[1]));
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0, "|"));
                                            }
                                            else
                                            {
                                                var txtAgregado = myText.Split('=');
                                                var numero = Regex.Match(txtAgregado[0], @"\d+").Value;
                                                int numAgregado = Int32.Parse(numero);
                                                if (numAgregado > Max)//Para el ultimo Simbolo Terminal
                                                {
                                                    Max = numAgregado;
                                                }
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txtAgregado[1]));
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0, "|"));
                                            }
                                        }
                                        else
                                        {
                                            var listaSETS = new List<string>();
                                            for (int z = 0; z < Datos.Instance.listaSets.Count; z++)
                                            {
                                                var spliter = Datos.Instance.listaSets.ElementAt(z).Split('=');
                                                listaSETS.Add(spliter[0].Replace("\t", "").Replace(" ", ""));
                                            }
                                            var txtAgregado = myText.Split('=');
                                            var txtAuxiliar = Datos.Instance.listaToken.ElementAt(i);
                                            var splitauxiliar = txtAuxiliar.Split('=');
                                            bool Contains = false;
                                            string tipo = string.Empty;
                                            int totalLETRA = 0;
                                            var final = string.Empty;
                                            bool a = false;
                                            bool b = false;
                                            bool c = false;
                                            for (int z = 0; z < listaSETS.Count; z++)//Comprueba sii existe la palabra en el área SETs
                                            {
                                                if (txtAgregado[1].Contains(listaSETS.ElementAt(z)))//Confirma que se encuentra
                                                {
                                                    Contains = true;
                                                    tipo = listaSETS.ElementAt(z);
                                                    int LETRA = Regex.Matches(myText, "LETRA").Count;
                                                    if (LETRA > 0)
                                                    {
                                                        a = true;
                                                    }
                                                    int DIGITO = Regex.Matches(myText, "DIGITO").Count;
                                                    if (DIGITO > 0)
                                                    {
                                                        b = true;
                                                    }
                                                    int CHARSET = Regex.Matches(myText, "CHARSET").Count;
                                                    if (CHARSET > 0)
                                                    {
                                                        c = true;
                                                    }
                                                    totalLETRA = LETRA + DIGITO + CHARSET;
                                                    if (splitauxiliar[1].EndsWith("*") || splitauxiliar[1].EndsWith("+"))
                                                    {
                                                        final = "*";
                                                        if (splitauxiliar[1].EndsWith("+"))
                                                        {
                                                            final = "+";
                                                        }
                                                    }
                                                    break;//Fin ciclo
                                                }
                                            }
                                            if (!Contains)
                                            {
                                                MessageBox.Show(myText + "\nNo se encuentra definido en SETS");
                                                return;
                                            }
                                            if ((a && !b && !c )|| (!a && b && !c )||( !a && !b && c))
                                            {
                                                if (totalLETRA == 1)
                                                {
                                                    txtAgregado[1] = tipo + final;
                                                }
                                                else
                                                {
                                                    txtAgregado[1] = string.Empty;
                                                    for (int z = 0; z < totalLETRA; z++)
                                                    {
                                                        txtAgregado[1] = txtAgregado[1] + tipo + " ";
                                                    }
                                                    txtAgregado[1] = txtAgregado[1].TrimEnd();
                                                    txtAgregado[1] = txtAgregado[1] + final;
                                                    txtAgregado[1] = txtAgregado[1].Replace(" ", ".");
                                                }
                                            }
                                            txtAgregado[1] = txtAgregado[1].Replace("{RESERVADAS()}", string.Empty);
                                            txtAgregado[1] = txtAgregado[1].TrimEnd();
                                            var numero = Regex.Match(txtAgregado[0], @"\d+").Value;
                                            int numAgregado = Int32.Parse(numero);
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txtAgregado[1]));
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0, "|"));
                                        }
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        i = i - 2;
                                    }
                                }
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(Max + 1, ").#"));//Fin de SimbolosTerminales
                            }
                            #endregion
                            #region All ACTIONS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("( |\t)", "").Replace(" ", "") == "ACTIONS")
                            {
                                lecturaAux = reader.ReadLine();//Siguiente linea
                                if (lecturaAux.ToString().Replace(" ", "").Replace("\t", "") == "RESERVADAS()")//Comprobar que le siga RESERVADAS()
                                {
                                    lecturaAux = reader.ReadLine();//Siguiente linea
                                    if (lecturaAux.ToString().Replace(" ", "").Replace("\t", "") == "{")//Comprobar 3ra linea abra con llave {
                                    {
                                        while ((lecturaAux = reader.ReadLine()) != "}")
                                        {
                                            if (lecturaAux == "}")
                                            {
                                                lecturaAux = reader.ReadLine();
                                                break;
                                            }
                                            if (lecturaAux == resTOKENS || lecturaAux == resERROR || lecturaAux == "SETS")//Condicion de salida
                                            {
                                                if (Datos.Instance.listaAction.Last().Replace(" ", "").Replace("\t", "") != "}")//Sii el ultimo no es de cerrar llave }
                                                {
                                                    MessageBox.Show("ACTIONS debe finalizar en -}-");
                                                    return;
                                                }
                                                Datos.Instance.listaAction.RemoveAt(Datos.Instance.listaAction.Count() - 1);//Sii lo contiene, se elimina, inesesario
                                                break;
                                            }
                                            Datos.Instance.listaAction.Add(lecturaAux.Replace(" ", "").Replace("\t", ""));//Agregar a su lista respectiva
                                        }
                                        if (Datos.Instance.listaAction.Contains(""))
                                        {
                                            Datos.Instance.listaAction.RemoveAll(X => X == "");
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
                                            this.miDato.Rows.Add(i, Datos.Instance.listaAction.ElementAt(i), "ACTIONS");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("ACTIONS debe iniciar en -{-");
                                        RebootList();
                                        return;
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
                            if (lecturaAux.Contains("ERROR"))
                            {
                                Datos.Instance.listaError.Add(lecturaAux.Replace("\t", "").Replace(" ", ""));
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
                                if (Datos.Instance.listaError.Contains(""))
                                {
                                    Datos.Instance.listaError.RemoveAll(X => X == "");
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
                else
                {
                    return;
                }
            }
            #region Labels
            rutaLabel.Text = rutaArchivo;
            miDato.Visible = true;
            SETlabel.Visible = true;
            TOKENlabel.Visible = true;
            ACTIONlabel.Visible = true;
            ERRORlabel.Visible = true;
            #endregion
            sw.Stop();
            txtTime.Text = "Tiempo de ejeccion en lectura de Archivo: " + sw.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
            MessageBox.Show("Archivo leido correctamente", rutaArchivo);//Solo confirmación visual
        }
        String ComprobarString(string myString)
        {
            if (String.IsNullOrEmpty(myString))
            {
                return " no se encuentra en el archivo";
            }
            return " se encuentra en el archivo";
        }
        private void Form1_Load(object sender, EventArgs e)//Expresiones Regulares generadas manualmente
        {

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
            Datos.Instance.SimbolosTerminales.Clear();
            miDato.Rows.Clear();
        }//Reiniciar listas sin necesidad de cerrar el programa
        private void SpliterMas(string[] Linea, object sender, EventArgs e)
        {
            for (int i = 0; i < Linea.Length; i++)//Recorrer todo el arreglo en busca de errores de Sintaxis
            {
                if (Linea[i] == "")//Donde encuentre una posición vacia
                {
                    if ((i % 2) == 0)
                    {
                        MessageBox.Show("Antes del signo + debe contener una definición.");
                        RebootList();
                        button1_Click(sender, e);
                    }
                    if ((i % 2) != 0)
                    {
                        MessageBox.Show("Después del signo + debe contener una definición.");
                        RebootList();
                        button1_Click(sender, e);
                    }
                }
            }
            return;
        }
        private void SpliterIgual(string[] Linea, object sender, EventArgs e)
        {
            if (Linea[1] == "''")
            {
                MessageBox.Show("En " + Linea[0] + ":\nDentro de las comillas debe ir una definición.");
                RebootList();
                button1_Click(sender, e);
            }
            string[] splitComillas = Linea[1].Split(new string[] { "'" }, StringSplitOptions.None);
            if ((splitComillas.Count() % 2) == 0)
            {
                MessageBox.Show("En " + Linea[1] + "\ndebe empezar y terminar con comillas.");
                RebootList();
                button1_Click(sender, e);
            }
            if (Linea[1].Count() < 2)
            {
                MessageBox.Show("En " + Linea[0] + " debe empezar o terminar con comillas.");
                RebootList();
                button1_Click(sender, e);
            }
            return;
        }
        private void SpliterChar(string[] Linea, object sender, EventArgs e)
        {
            for (int i = 0; i < Linea.Length; i++)
            {
                Linea[i] = Linea[i].Replace(" ", "").Replace("\t", "");
            }
            if (!Linea[1].Contains("CHR"))
            {
                MessageBox.Show("En " + Linea[0] + " debe empezar con CHR.");
                RebootList();
                button1_Click(sender, e);
            }
            else
            {
                string[] SplitParentesis = Linea[1].Split(new string[] { "(", ")" }, StringSplitOptions.None);
                for (int j = 0; j < SplitParentesis.Length; j++)
                {
                    if (SplitParentesis[j].Contains(".."))
                    {
                        SplitParentesis[j] = SplitParentesis[j].Replace("..", "");
                    }
                    if ((j % 2) != 0)
                    {
                        try
                        {
                            if (!(int.TryParse(SplitParentesis[j], out int x)))
                            {
                                MessageBox.Show("SETS en la linea " + j + " no contiene valor numérico encerrado en parentesis.");
                                RebootList();
                                button1_Click(sender, e);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show("CHR en la linea " + j + " debe contener un valor numérico.");
                            RebootList();
                            button1_Click(sender, e);
                        }
                    }
                }
            }
            return;
        }
        private void SpliterTOKEN(string[] Linea, object sender, EventArgs e)
        {
            for (int i = 0; i < Linea.Length; i++)
            {
                Linea[i] = Linea[i].Replace(" ", "").Replace("\t", "");
                if (Linea.Count() > 2)
                {
                    if (!Linea[1].StartsWith("'") || !Linea[2].EndsWith("'"))
                    {
                        MessageBox.Show("Luego de " + Linea[i] + " debe iniciar y finalizar con comillas.");
                        RebootList();
                        button1_Click(sender, e);
                    }
                }
            }
            try
            {
                string[] SplitToken = Linea[0].Split(new string[] { "TOKEN" }, StringSplitOptions.None);//Buscar el TOKEN de la linea
                if (!(int.TryParse(SplitToken[1], out int x)))
                {
                    MessageBox.Show("Luego de " + Linea[0] + " debe ir un valor numérico.");
                    RebootList();
                    button1_Click(sender, e);
                }
                string[] splitComillas = SplitToken[1].Split(new string[] { "'" }, StringSplitOptions.None);//Buscar las comillas del TOKEN
                if ((splitComillas.Count() % 2) == 0)//Count MOD 2, usado para buscar irregularidades en comillas(cantida de comillas impares)
                {
                    MessageBox.Show("En " + Linea[0] + "\ndebe empezar y terminar con comillas.");
                    RebootList();
                    button1_Click(sender, e);
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("TOKEN no se encuentra bien escrito\no no se encuentra.");
                RebootList();
                button1_Click(sender, e);
            }
        }
        private void GetLETRA(string Linea)
        {
            string[] SplitInicial = Linea.Split('=');
            string[] Mas = SplitInicial[1].Split('+');
            if ((Mas.Length % 2) != 0)
            {
                for (int i = 0; i < Mas.Length; i++)
                {
                    string[] Puntos = Mas[i].Split(new string[] { ".." }, StringSplitOptions.None);
                    byte[] Inicial;
                    byte[] Final;
                    int Rango;
                    if ((Puntos.Length % 2) == 0)
                    {
                        Puntos[0] = Puntos[0].Replace("'", "").Replace(" ", "");
                        Puntos[1] = Puntos[1].Replace("'", "").Replace(" ", "");
                        Inicial = Encoding.ASCII.GetBytes(Puntos[0]);
                        Final = Encoding.ASCII.GetBytes(Puntos[1]);
                        Rango = Final[0] - Inicial[0] + 1;
                    }
                    else
                    {
                        Puntos[0] = Puntos[0].Replace("'", "").Replace(" ", "");
                        Inicial = Encoding.ASCII.GetBytes(Puntos[0]);
                        Final = Encoding.ASCII.GetBytes(Puntos[0]);
                        Rango = Final[0] - Inicial[0] + 1;
                    }
                    if (Inicial[0] >= 65 && Final[0] <= 90)
                    {
                        AlfabetoMayuscula = Enumerable.Range(Inicial[0], Rango).Select(x => (char)x).ToArray();
                    }
                    else if (Inicial[0] >= 97 && Final[0] <= 122)
                    {
                        AlfabetoMinuscula = Enumerable.Range(Inicial[0], Rango).Select(x => (char)x).ToArray();
                    }
                }
            }
        }//Obtiene alfabetos
        private void miDato_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Generar_Click(object sender, EventArgs e)
        {
            TextBoxER.Clear();
            var newLista = Datos.Instance.SimbolosTerminales;
            List<Datos.AllData> SortedList = newLista.OrderBy(o => o.IntegerData).ToList();
            for (int i = 1; i < SortedList.Count; i++)
            {
                TextBoxER.Text = TextBoxER.Text + SortedList[i].StringData + " | ";
            }
            ER_ET ArbolExpresiones = new ER_ET();
            ArbolExpresiones.CrearArbol(Datos.Instance.SimbolosTerminales);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TransicionesData.Visible = true;
            sw.Stop();
            txtTime.Text = "Tiempo de ejeccion en creación del arbol y ER: " + sw.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
        }
    }
}
