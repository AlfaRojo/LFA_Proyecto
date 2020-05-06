
namespace LFA_Proyecto.Modelos
{
    /// <summary>
    /// Utilizados para comprobar Simbolos Terminales
    /// </summary>
    public class Utilities
    {
        public static string[] Nullers = { "?", "*" };
        public static string[] Car = { "*", "?", "+", "(", ")", ".", "|", "'", "\"" };
        public static string[] Ter = { "*", "?", "+", ".", "|", "(", ")" };
        public static string[] Op = { "*", "?", "+", ".", "|" };
    }
}
