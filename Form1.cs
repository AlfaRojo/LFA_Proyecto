using LFA_Proyecto.Arbol;
using LFA_Proyecto.Help;
using LFA_Proyecto.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LFA_Proyecto
{
    public partial class Form1 : Form
    {
        #region Valores
        int Max = int.MaxValue;
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
            var contArchivo = string.Empty;
            var rutaArchivo = string.Empty;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            using (OpenFileDialog actuArchivo = new OpenFileDialog())
            {
                actuArchivo.InitialDirectory = @"~/LFA_Proyecto/PRUEBAS";
                actuArchivo.Filter = "txt files (*.txt)|*.txt";
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;
                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {

                    RebootList();
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
                        var reservado = string.Empty;
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
                                var listaSETS = new List<string>();
                                for (int z = 0; z < Datos.Instance.listaSets.Count; z++)//Para obtener y comparar los SETs existentes
                                {
                                    var spliter = Datos.Instance.listaSets.ElementAt(z).Split('=');
                                    listaSETS.Add(spliter[0].Replace("\t", "").Replace(" ", ""));
                                }
                                for (int i = 0; i < Datos.Instance.listaToken.Count(); i++)//Agregar al GridView y comprobar sintaxis
                                {
                                    try
                                    {
                                        #region Comprobacion Sintaxis
                                        var Delimitador = Datos.Instance.listaToken.ElementAt(i).Trim(Delimitadores);
                                        if (Delimitador == "")
                                        {
                                            Datos.Instance.listaToken.RemoveAt(i);
                                        }
                                        var myText = Datos.Instance.listaToken.ElementAt(i);
                                        var textoAux = myText;
                                        myText = Regex.Replace(myText, @"\s+", "");
                                        textoAux = Regex.Replace(myText, @"\s+", "");
                                        if (!textoAux.Trim(Delimitadores).Contains("TOKEN"))//Error de escritura
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene TOKEN o se encunetra mal escrito.");
                                            return;
                                        }
                                        if (textoAux.All(char.IsDigit))//No contiene dígito----NO FUNCIONA, REVISAR!
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene numeración válida.");
                                            return;
                                        }
                                        if (!textoAux.Contains("="))//Error de igualación
                                        {
                                            MessageBox.Show("TOKEN en la linea " + i + " no contiene =.");
                                            return;
                                        }
                                        else//Enviar split de igual para comprobar Sintaxis
                                        {
                                            string[] Linea = textoAux.Split('=');
                                            SpliterTOKEN(Linea, sender, e);
                                        }
                                        //Agregar la comprobación de expresión regular
                                        this.miDato.Rows.Add(i, textoAux, "TOKENS");
                                        #endregion
                                        if (Datos.Instance.SimbolosTerminales.Count() == 0)
                                        {
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(0, "("));
                                        }
                                        var txtAgregado = myText.Split(new char[] { '=' }, 2);
                                        var numero = Regex.Match(txtAgregado[0], @"\d+").Value;
                                        int numAgregado = Int32.Parse(numero);
                                        #region Ingresar a Lista
                                        if (txtAgregado[1].StartsWith("'") && txtAgregado[1].EndsWith("'"))
                                        {
                                            //Start_End(numAgregado, txtAgregado[1], listaSETS);
                                            txtAgregado[1] = txtAgregado[1].TrimStart();
                                            var tamCiclo = txtAgregado[1].Length;
                                            bool SETonTOKEN = false;
                                            foreach (var item in listaSETS)
                                            {
                                                if (txtAgregado[1].Contains(item))
                                                {
                                                    SETonTOKEN = true;
                                                    break;
                                                }
                                            }
                                            if (txtAgregado[1].Length <= 3 && !SETonTOKEN)//TOKENs Individuales
                                            {
                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txtAgregado[1]));
                                            }
                                            else if (!SETonTOKEN)
                                            {
                                                txtAgregado[1] = txtAgregado[1].Replace("''", ".");
                                                txtAgregado[1] = txtAgregado[1].TrimStart('\'').TrimEnd('\'');
                                                var listaAuxiliar = new List<string>();
                                                for (int k = 0; k < txtAgregado[1].Length; k++)
                                                {
                                                    listaAuxiliar.Add(txtAgregado[1].Substring(k, 1));
                                                }
                                                int primero = 0;
                                                int comillas = 0;
                                                foreach (var item in listaAuxiliar)
                                                {
                                                    if (item != "'")
                                                    {
                                                        comillas = 0;
                                                        if (primero == 0)
                                                        {
                                                            string final = item;
                                                            final = "'" + final + "'";
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, final));
                                                            primero++;
                                                        }
                                                        else
                                                        {
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, item));
                                                            primero--;
                                                        }
                                                    }
                                                    else if (item == "'")//Al llegar a 3 significa que contiene comilla como TOKEN
                                                    {
                                                        comillas++;
                                                    }
                                                    if (comillas == 3)
                                                    {
                                                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, item));
                                                        comillas = 0;//Reiniciar conteo
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var listaAuxiliar = new List<string>();
                                                int inicio = 0;
                                                string txt = string.Empty;
                                                string previo = string.Empty;
                                                txtAgregado[1] = txtAgregado[1].Trim();
                                                for (int k = 0; k < txtAgregado[1].Length; k++)
                                                {
                                                    string actual = txtAgregado[1].Substring(k, 1);
                                                    if (actual == "'")//Utilizado para indicar inicios y finales entre comillas
                                                    {
                                                        inicio++;
                                                    }
                                                    else//Y guardar lo que está enmedio
                                                    {
                                                        txt = txt + txtAgregado[1].Substring(k, 1);
                                                    }
                                                    if (inicio == 2 || Utilities.Ter.Contains(actual))
                                                    {
                                                        if (Utilities.Ter.Contains(actual))
                                                        {
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, actual));
                                                            txt = string.Empty;
                                                            inicio = 0;
                                                        }
                                                        else
                                                        {
                                                            txt = "'" + txt + "'";
                                                            if (txt == "''")
                                                            {
                                                                txt = txt.Replace("''", "'''");
                                                            }
                                                            string next = txtAgregado[1].Substring(k + 1, 1);
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txt));
                                                            if (!Utilities.Car.Contains(next) || next == "'")//Sii es distinto a simbolos no concatenables o 3er comilla
                                                            {
                                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                            }
                                                            //Concatenacion
                                                            txt = string.Empty;
                                                            inicio = 0;
                                                        }
                                                    }
                                                    else if (txt.Length > 4)//Se comprueba que no pertenezca al área e SETs
                                                    {
                                                        bool isHere = false;
                                                        foreach (var item in listaSETS)
                                                        {
                                                            if (txt == item)
                                                            {
                                                                isHere = true;
                                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txt));
                                                                string next = txtAgregado[1].Substring(k + 1, 1);
                                                                if (Utilities.Car.Contains(next))
                                                                {
                                                                    Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                                }
                                                                txt = string.Empty;
                                                                inicio = 0;
                                                                break;
                                                            }
                                                        }
                                                        if (!isHere && actual == "'")
                                                        {
                                                            MessageBox.Show(txt + "\nNo se encuentra declarado en área SET");
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                            if (Datos.Instance.SimbolosTerminales.Last().StringData == ".")//Sii se buscaba concatenar mas se elimina
                                            {
                                                Datos.Instance.SimbolosTerminales.RemoveAt(Datos.Instance.SimbolosTerminales.Count - 1);
                                            }
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "|"));
                                        }
                                        else
                                        {
                                            var actual = txtAgregado[1];
                                            bool isHere = false;
                                            bool tokens = false;
                                            bool reservada = false;
                                            var token = string.Empty;
                                            for (int o = 0; o < txtAgregado[1].Length; o++)
                                            {
                                                if (actual.StartsWith("{") || reservada)
                                                {
                                                    reservada = true;
                                                    if (token.EndsWith("}") && token.Length > 1)
                                                    {
                                                        reservada = false;
                                                        var next = actual.Substring(0, 1);
                                                        if (!Utilities.Car.Contains(next))
                                                        {
                                                            Datos.Instance.misReservas.Add(token);
                                                            token = string.Empty;
                                                            reservada = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        var next = actual.Substring(0, 1);
                                                        if (next != "{" || next != "}")//No se agregan por la lectura de lineas
                                                        {
                                                            token += actual.Substring(0, 1);
                                                        }
                                                        actual = actual.Remove(0, 1);
                                                    }
                                                }
                                                else if (actual.StartsWith("'") || tokens)
                                                {
                                                    tokens = true;
                                                    if (token.EndsWith("'") && token.Length > 1)
                                                    {
                                                        tokens = false;
                                                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, token));
                                                        var next = actual.Substring(0, 1);
                                                        if (!Utilities.Car.Contains(next))
                                                        {
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                            token = string.Empty;
                                                            tokens = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        token += actual.Substring(0, 1);
                                                        actual = actual.Remove(0, 1);
                                                    }
                                                }
                                                if (!tokens && !reservada)
                                                {
                                                    foreach (var item in listaSETS)
                                                    {
                                                        if (actual.StartsWith(item))
                                                        {
                                                            isHere = true;
                                                            while (actual.StartsWith(item))
                                                            {
                                                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, item));
                                                                actual = actual.Remove(0, item.Length);
                                                                if (actual.Length == 0)
                                                                {
                                                                    break;
                                                                }
                                                                var siguiente = actual.Substring(0, 1);
                                                                if (!Utilities.Op.Contains(siguiente) && siguiente != ")")
                                                                {
                                                                    Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                                }
                                                            }
                                                        }
                                                        if (actual.Length == 0)
                                                        {
                                                            break;
                                                        }
                                                        var next = actual.Substring(0, 1);
                                                        if (Utilities.otros.Contains(next))
                                                        {
                                                            isHere = true;
                                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, next));
                                                            actual = actual.Remove(0, next.Length);
                                                        }
                                                    }
                                                }
                                                if (actual.Length == 3)
                                                {
                                                    if (actual.StartsWith("'") && actual.EndsWith("'"))
                                                    {
                                                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, actual));
                                                        actual = string.Empty;
                                                    }
                                                }
                                                if (actual.Length == 0)
                                                {
                                                    if (token.StartsWith("'"))
                                                    {
                                                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                                        token = string.Empty;
                                                        tokens = false;
                                                    }
                                                    else if (token.StartsWith("{"))
                                                    {
                                                        Datos.Instance.misReservas.Add(token);
                                                        token = string.Empty;
                                                        tokens = false;
                                                    }
                                                    break;
                                                }
                                                if (!isHere)
                                                {
                                                    MessageBox.Show(txtAgregado[1] + "\nNo se encuentra declarado en área SET");
                                                    return;
                                                }
                                            }
                                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "|"));
                                        }
                                        #endregion
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        i = i - 2;
                                    }
                                }
                                if (Datos.Instance.SimbolosTerminales.Last().StringData == "|")
                                {
                                    Datos.Instance.SimbolosTerminales.RemoveAt(Datos.Instance.SimbolosTerminales.Count - 1);
                                }
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(Max + 1, ")"));//Fin de SimbolosTerminales
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(Max + 1, "."));//Fin de SimbolosTerminales
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(Max + 1, "#"));//Fin de SimbolosTerminales
                            }
                            #endregion
                            #region All ACTIONS Sintaxis //COMPLETO
                            if (lecturaAux.ToString().Replace("( |\t)", "").Replace(" ", "") == "ACTIONS")
                            {
                                foreach (var item in Datos.Instance.misReservas)
                                {
                                    lecturaAux = reader.ReadLine();//Siguiente linea
                                    var reserva = item.TrimStart('{').TrimEnd('}');
                                    if (lecturaAux == "")
                                    {
                                        while (lecturaAux == "")//Omitir todos los saltos de linea
                                        {
                                            lecturaAux = reader.ReadLine();
                                        }
                                    }
                                    lecturaAux = lecturaAux.Trim();
                                    if (lecturaAux.ToString().Trim() == reserva)//Comprobar que le siga RESERVADAS()
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
                                                var numero = Regex.Match(lecturaAux, @"\d+").Value;
                                                int numAgregado = Int32.Parse(numero);
                                                var txtAgregar = lecturaAux.Split('=');
                                                Datos.Instance.SimbolosAction.Add(new Datos.AllActions(numAgregado, reserva, txtAgregar[1].Trim()));
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
                                            MessageBox.Show(item + " debe iniciar en -{-");
                                            RebootList();
                                            return;
                                        }
                                    }
                                    else//Sintaxis incorrecto entre ACTIONS & RESERVADAS()
                                    {
                                        MessageBox.Show(item + " se utiliza en TOKEN\nsin embargo no se encuentra delcarado en ACTIONS");
                                        return;
                                    }
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
                    sw.Stop();
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
            Generar.Enabled = true;
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
            Datos.Instance.DiccTrans.Clear();
            Datos.Instance.DicFollow.Clear();
            Datos.Instance.PilaS.Clear();
            Datos.Instance.PilaT.Clear();
            Datos.Instance.misReservas.Clear();
            miDato.Rows.Clear();
            FollowData.Rows.Clear();
            FLFN_Data.Rows.Clear();
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
                        Final = Encoding.ASCII.GetBytes(Puntos[1]);
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
        /// <summary>
        /// Guarda TOKENs que empiezan y terminan con comillas
        /// </summary>
        /// <param name="numAgregado"></param>
        /// <param name="Actual"></param>
        /// <param name="listaSETS"></param>
        private void Start_End(int numAgregado, string Actual, List<string> listaSETS)
        {
            Actual = Actual.TrimStart();
            var tamCiclo = Actual.Length;
            bool SETonTOKEN = false;
            foreach (var item in listaSETS)
            {
                if (Actual.Contains(item))
                {
                    SETonTOKEN = true;
                    break;
                }
            }
            if (Actual.Length <= 3 && !SETonTOKEN)//TOKENs Individuales
            {
                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, Actual));
            }
            else if (!SETonTOKEN)
            {
                Actual = Actual.Replace("''", ".");
                Actual = Actual.TrimStart('\'').TrimEnd('\'');
                var listaAuxiliar = new List<string>();
                for (int k = 0; k < Actual.Length; k++)
                {
                    listaAuxiliar.Add(Actual.Substring(k, 1));
                }
                int primero = 0;
                int comillas = 0;
                foreach (var item in listaAuxiliar)
                {
                    if (item != "'")
                    {
                        comillas = 0;
                        if (primero == 0)
                        {
                            string final = item;
                            final = "'" + final + "'";
                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, final));
                            primero++;
                        }
                        else
                        {
                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, item));
                            primero--;
                        }
                    }
                    else if (item == "'")//Al llegar a 3 significa que contiene comilla como TOKEN
                    {
                        comillas++;
                    }
                    if (comillas == 3)
                    {
                        Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, item));
                        comillas = 0;//Reiniciar conteo
                    }
                }
            }
            else
            {
                var listaAuxiliar = new List<string>();
                int inicio = 0;
                string txt = string.Empty;
                string previo = string.Empty;
                Actual = Actual.Trim();
                for (int k = 0; k < Actual.Length; k++)
                {
                    string actual = Actual.Substring(k, 1);
                    if (actual == "'")//Utilizado para indicar inicios y finales entre comillas
                    {
                        inicio++;
                    }
                    else//Y guardar lo que está enmedio
                    {
                        txt = txt + Actual.Substring(k, 1);
                    }
                    if (inicio == 2 || Utilities.Ter.Contains(actual))
                    {
                        if (Utilities.Ter.Contains(actual))
                        {
                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, actual));
                            txt = string.Empty;
                            inicio = 0;
                        }
                        else
                        {
                            txt = "'" + txt + "'";
                            if (txt == "''")
                            {
                                txt = txt.Replace("''", "'''");
                            }
                            string next = Actual.Substring(k + 1, 1);
                            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txt));
                            if (!Utilities.Car.Contains(next) || next == "'")//Sii es distinto a simbolos no concatenables o 3er comilla
                            {
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                            }
                            //Concatenacion
                            txt = string.Empty;
                            inicio = 0;
                        }
                    }
                    else if (txt.Length > 4)//Se comprueba que no pertenezca al área e SETs
                    {
                        bool isHere = false;
                        foreach (var item in listaSETS)
                        {
                            if (txt == item)
                            {
                                isHere = true;
                                Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, txt));
                                string next = Actual.Substring(k + 1, 1);
                                if (Utilities.Car.Contains(next))
                                {
                                    Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "."));
                                }
                                txt = string.Empty;
                                inicio = 0;
                                break;
                            }
                        }
                        if (!isHere)
                        {
                            MessageBox.Show(txt + "\nNo se encuentra declarado en área SET");
                            return;
                        }
                    }
                }
            }
            if (Datos.Instance.SimbolosTerminales.Last().StringData == ".")//Sii se buscaba concatenar mas se elimina
            {
                Datos.Instance.SimbolosTerminales.RemoveAt(Datos.Instance.SimbolosTerminales.Count - 1);
            }
            Datos.Instance.SimbolosTerminales.Add(new Datos.AllData(numAgregado, "|"));
        }
        private void Generar_Click(object sender, EventArgs e)//Generar ArbolExpresines & First,Last,Follow
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            TextBoxER.Clear();
            foreach (var item in Datos.Instance.SimbolosTerminales)
            {
                TextBoxER.Text = TextBoxER.Text + item.StringData;
            }
            ER_ET ArbolExpresiones = new ER_ET();
            ArbolExpresiones.Tree(Datos.Instance.SimbolosTerminales);
            var Transiciones = new Transiciones();
            Transiciones.GenerarTransiciones();
            ArbolB _tree = new ArbolB();
            _tree = Datos.Instance.PilaS.Pop();
            GenerarPostOrden(_tree);
            Datos.Instance.PilaS.Push(_tree);
            MostrarFollow();
            MostrarTrans();
            sw.Stop();
            txtTime.Text = "Tiempo de ejeccion en creación del arbol y ER: " + sw.Elapsed.ToString("hh\\:mm\\:ss\\.fff");
            DrawTree drawMyTree = new DrawTree();
            drawMyTree.Show();
            Exportar.Enabled = true;
            FLFN_Data.Visible = true;
            EstadoData.Visible = true;
            FollowData.Visible = true;
        }
        private void GenerarPostOrden(ArbolB tree)//Imprime First, Last, Nullers
        {
            if (tree != null)
            {
                GenerarPostOrden(tree.HijoIzquierdo);
                GenerarPostOrden(tree.HijoDerecho);
                var First = string.Empty;
                if (tree.First.Count > 0)
                {
                    for (int i = 0; i < tree.First.Count; i++)
                    {
                        First = First + tree.First[i].ToString() + ",";
                    }
                    First = First.Remove(First.Length - 1);
                }

                var Last = string.Empty;
                if (tree.Last.Count > 0)
                {
                    for (int i = 0; i < tree.Last.Count; i++)
                    {
                        Last = Last + tree.Last[i].ToString() + ",";
                    }
                    Last = Last.Remove(Last.Length - 1);
                }
                this.FLFN_Data.Rows.Add(tree.Dato, First, Last, tree.Nuller);

            }
        }
        private void MostrarFollow()
        {
            foreach (var item in Datos.Instance.DicFollow)
            {
                var follow = string.Empty;
                foreach (var values in item.Value)
                {
                    follow = follow + values.ToString() + ",";
                }
                if (follow.Length > 0)
                {
                    follow = follow.Remove(follow.Length - 1);
                }
                this.FollowData.Rows.Add(item.Key, follow);
            }
        }
        private void MostrarTrans()
        {
            var Tabla = new DataTable();
            var primerelemnto = Datos.Instance.DiccionarioTransiciones.First();
            var diccestados = Datos.Instance.DiccionarioTransiciones;
            Tabla.Columns.Add("Estados");
            foreach (var item in primerelemnto.Value)
            {
                Tabla.Columns.Add($"{item.Key}");
            }
            var columnas = Tabla.Columns.Count;
            var ContadorColumnas = 0;
            Tabla.Rows.Add();
            var ContadorFilas = 0;
            foreach (var item in diccestados)
            {
                Tabla.Rows.Add();
                Tabla.Rows[ContadorFilas][ContadorColumnas] = string.Join(",", item.Key);
                ContadorColumnas++;
                foreach (var item2 in item.Value)
                {
                    if (item2.Value.Count != 0)
                    {
                        Tabla.Rows[ContadorFilas][ContadorColumnas] = string.Join(",", item2.Value);
                        ContadorColumnas++;
                    }
                    else
                    {
                        Tabla.Rows[ContadorFilas][ContadorColumnas] = "----";
                        ContadorColumnas++;
                    }
                }
                ContadorFilas++;
                ContadorColumnas = 0;
            }
            this.EstadoData.DataSource = Tabla;
        }
        private void Exportar_Click(object sender, EventArgs e)
        {
            var switchtipo = string.Empty;
            var casetotal = string.Empty;
            List<Datos.AllData> FormandoTokens = new List<Datos.AllData>(); List<string> ListaS = new List<string>();
            List<string> FormandoActions = new List<string>();
            List<string> listaLetras = new List<string>();
            List<string> listaDigito = new List<string>();
            List<string> listaCHR = new List<string>();
            var token = string.Empty;
            foreach (var item in Datos.Instance.listaToken)
            {
                var toadd = item.Split(new char[] { '=' }, 2);
                var numero = Regex.Match(toadd[0], @"\d+").Value;
                int numAgregado = Int32.Parse(numero);
                toadd[1] = toadd[1].Trim();
                if (toadd[1].StartsWith("'") && toadd[1].EndsWith("'") && toadd[1].Length <= 9)
                {
                    toadd[1] = toadd[1].Replace("''", string.Empty);
                    FormandoTokens.Add(new Datos.AllData(numAgregado, toadd[1]));
                }
            }
            foreach (var item in Datos.Instance.listaSets)
            {
                var toAdd = item.Split('=');
                toAdd[0] = toAdd[0].Trim();
                ListaS.Add(toAdd[0]);
                #region test
                string[] Mas = toAdd[1].Split('+');
                foreach (var set in Mas)
                {
                    var tipo = set.Trim().Split(new string[] { ".." }, StringSplitOptions.None);
                    if (tipo.Length > 1)
                    {
                        byte[] start;
                        byte[] end;
                        int rank;
                        start = Encoding.ASCII.GetBytes(tipo[0]);
                        var st = start.Min();
                        end = Encoding.ASCII.GetBytes(tipo[1]);
                        var nd = end.Max();
                        rank = nd - start[1] + 1;
                        AlfabetoMayuscula = Enumerable.Range(start[1], rank).Select(x => (char)x).ToArray();
                        if (toAdd[0] == "LETRA")
                        {
                            foreach (var letra in AlfabetoMayuscula)
                            {
                                if (!listaLetras.Contains(letra.ToString()))
                                {
                                    listaLetras.Add(letra.ToString());
                                }
                            }
                        }
                        else if (toAdd[0] == "DIGITO")
                        {
                            foreach (var letra in AlfabetoMayuscula)
                            {
                                if (!listaDigito.Contains(letra.ToString()))
                                {
                                    listaDigito.Add(letra.ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            foreach (var item in Datos.Instance.SimbolosAction)
            {
                var valor = item.StringData;
                valor = valor.TrimStart('\'').TrimEnd('\'');
                casetotal += "case \"" + valor + "\":\n" +
                    "Console.WriteLine(item + \" - ACTION: " + item.IntegerData + " - Tipo: " + item.typeAction + "\");\nbreak;\n";
            }
            foreach (var item in ListaS)
            {
                if (item.Trim() == "LETRA")
                {
                    foreach (var letra in listaLetras)
                    {
                        casetotal += "case \"" + letra + "\":\n" +
                         "Console.WriteLine(item +\" - SET: " + item + "\");\nbreak;\n";
                    }
                }
                else if (item == "DIGITO")
                {
                    foreach (var letra in listaDigito)
                    {
                        casetotal += "case \"" + letra + "\":\n" +
                         "Console.WriteLine(item +\" - SET: " + item + "\");\nbreak;\n";
                    }
                }
            }
            foreach (var item in FormandoTokens)
            {
                if (!ListaS.Contains(item.StringData))
                {
                    var addtoken = item.StringData;
                    if (addtoken == "'''")
                    {
                        addtoken = addtoken.Replace("'''", "'");
                    }
                    else
                    {
                        addtoken = addtoken.TrimStart('\'').TrimEnd('\'');
                    }
                    var comillas = Convert.ToChar(34);
                    if (addtoken == "\"")
                    {
                        var barra = Convert.ToChar(92);
                        casetotal += "case " + comillas + barra + addtoken + comillas + ":\n" +
                         "Console.WriteLine(item +\" - TOKEN: " + item.IntegerData + "\");\nbreak;\n";
                    }
                    else if (addtoken != "#")
                    {
                        casetotal += "case " + comillas + addtoken + comillas + ":\n" +
                         "Console.WriteLine(item +\" - TOKEN: " + item.IntegerData + "\");\nbreak;\n";
                    }
                }
            }
            var final = "\ndefault:" +
                  "Console.WriteLine(\"ERROR # - item - no se encuentra asignado\");\n" +
                  "break;\n}\n}" +
                  "Console.ReadKey();\n}\n}\n}";
            foreach (var error in Datos.Instance.listaError)
            {
                var numero = Regex.Match(error, @"\d+").Value;
                int numAgregado = Int32.Parse(numero);
                final = "\ndefault:" +
                  "Console.WriteLine(item + \" - ERROR " + numAgregado + " no se encuentra asignado\");\n" +
                  "break;\n}\n}" +
                  "Console.ReadKey();\n}\n}\n}";
            }
            using (SaveFileDialog actuArchivo = new SaveFileDialog())
            {
                actuArchivo.FilterIndex = 2;
                actuArchivo.RestoreDirectory = true;
                actuArchivo.DefaultExt = "cs";
                if (actuArchivo.ShowDialog() == DialogResult.OK)
                {
                    var tipo_Namespace = Path.GetFileNameWithoutExtension(actuArchivo.FileName);
                    using (StreamWriter escritor = File.CreateText(actuArchivo.FileName))
                    {
                        var inicio = "using System;" +
                        "\nnamespace " + tipo_Namespace+ " \n{\nclass Program\n{\nstatic void Main(string[] args)" +
                        "\n{\nConsole.WriteLine(" +
                        "\"Ingrese texto a comprobar:\");\n" +
                          "var txtresultante = Console.ReadLine();\n" +
                         "var spliter = txtresultante.Split(' ');\n" +
                         "foreach (var item in spliter){" +
                        "\nswitch(item){\n";

                        escritor.Write(inicio);
                        escritor.Write(casetotal);
                        escritor.Write(final);
                    }
                    MessageBox.Show("Archivo creado en:\n" + actuArchivo.FileName, "Lenguajes Formales & Autómatas", MessageBoxButtons.OK);
                }
            }
        }
    }
}
