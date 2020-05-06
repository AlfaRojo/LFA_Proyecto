using System;
namespace LFA
{
class Program
{
static void Main(string[] args)
{
Console.WriteLine("Ingrese texto a comprobar:");
var txtresultante = Console.ReadLine();
var spliter = txtresultante.Split(' ');
foreach (var item in spliter){
switch(item){
case "PROGRAM":
Console.WriteLine(item + " - ACTION: 18");
break;
case "INCLUDE":
Console.WriteLine(item + " - ACTION: 19");
break;
case "CONST":
Console.WriteLine(item + " - ACTION: 20");
break;
case "TYPE":
Console.WriteLine(item + " - ACTION: 21");
break;
case "VAR":
Console.WriteLine(item + " - ACTION: 22");
break;
case "RECORD":
Console.WriteLine(item + " - ACTION: 23");
break;
case "ARRAY":
Console.WriteLine(item + " - ACTION: 24");
break;
case "OF":
Console.WriteLine(item + " - ACTION: 25");
break;
case "PROCEDURE":
Console.WriteLine(item + " - ACTION: 26");
break;
case "FUNCTION":
Console.WriteLine(item + " - ACTION: 27");
break;
case "IF":
Console.WriteLine(item + " - ACTION: 28");
break;
case "THEN":
Console.WriteLine(item + " - ACTION: 29");
break;
case "ELSE":
Console.WriteLine(item + " - ACTION: 30");
break;
case "FOR":
Console.WriteLine(item + " - ACTION: 31");
break;
case "TO":
Console.WriteLine(item + " - ACTION: 32");
break;
case "WHILE":
Console.WriteLine(item + " - ACTION: 33");
break;
case "DO":
Console.WriteLine(item + " - ACTION: 34");
break;
case "EXIT":
Console.WriteLine(item + " - ACTION: 35");
break;
case "END":
Console.WriteLine(item + " - ACTION: 36");
break;
case "CASE":
Console.WriteLine(item + " - ACTION: 37");
break;
case "BREAK":
Console.WriteLine(item + " - ACTION: 38");
break;
case "DOWNTO":
Console.WriteLine(item + " - ACTION: 39");
break;
case "A":
Console.WriteLine(item +" - SET: LETRA");
break;
case "B":
Console.WriteLine(item +" - SET: LETRA");
break;
case "C":
Console.WriteLine(item +" - SET: LETRA");
break;
case "D":
Console.WriteLine(item +" - SET: LETRA");
break;
case "E":
Console.WriteLine(item +" - SET: LETRA");
break;
case "F":
Console.WriteLine(item +" - SET: LETRA");
break;
case "G":
Console.WriteLine(item +" - SET: LETRA");
break;
case "H":
Console.WriteLine(item +" - SET: LETRA");
break;
case "I":
Console.WriteLine(item +" - SET: LETRA");
break;
case "J":
Console.WriteLine(item +" - SET: LETRA");
break;
case "K":
Console.WriteLine(item +" - SET: LETRA");
break;
case "L":
Console.WriteLine(item +" - SET: LETRA");
break;
case "M":
Console.WriteLine(item +" - SET: LETRA");
break;
case "N":
Console.WriteLine(item +" - SET: LETRA");
break;
case "O":
Console.WriteLine(item +" - SET: LETRA");
break;
case "P":
Console.WriteLine(item +" - SET: LETRA");
break;
case "Q":
Console.WriteLine(item +" - SET: LETRA");
break;
case "R":
Console.WriteLine(item +" - SET: LETRA");
break;
case "S":
Console.WriteLine(item +" - SET: LETRA");
break;
case "T":
Console.WriteLine(item +" - SET: LETRA");
break;
case "U":
Console.WriteLine(item +" - SET: LETRA");
break;
case "V":
Console.WriteLine(item +" - SET: LETRA");
break;
case "W":
Console.WriteLine(item +" - SET: LETRA");
break;
case "X":
Console.WriteLine(item +" - SET: LETRA");
break;
case "Y":
Console.WriteLine(item +" - SET: LETRA");
break;
case "Z":
Console.WriteLine(item +" - SET: LETRA");
break;
case "a":
Console.WriteLine(item +" - SET: LETRA");
break;
case "b":
Console.WriteLine(item +" - SET: LETRA");
break;
case "c":
Console.WriteLine(item +" - SET: LETRA");
break;
case "d":
Console.WriteLine(item +" - SET: LETRA");
break;
case "e":
Console.WriteLine(item +" - SET: LETRA");
break;
case "f":
Console.WriteLine(item +" - SET: LETRA");
break;
case "g":
Console.WriteLine(item +" - SET: LETRA");
break;
case "h":
Console.WriteLine(item +" - SET: LETRA");
break;
case "i":
Console.WriteLine(item +" - SET: LETRA");
break;
case "j":
Console.WriteLine(item +" - SET: LETRA");
break;
case "k":
Console.WriteLine(item +" - SET: LETRA");
break;
case "l":
Console.WriteLine(item +" - SET: LETRA");
break;
case "m":
Console.WriteLine(item +" - SET: LETRA");
break;
case "n":
Console.WriteLine(item +" - SET: LETRA");
break;
case "o":
Console.WriteLine(item +" - SET: LETRA");
break;
case "p":
Console.WriteLine(item +" - SET: LETRA");
break;
case "q":
Console.WriteLine(item +" - SET: LETRA");
break;
case "r":
Console.WriteLine(item +" - SET: LETRA");
break;
case "s":
Console.WriteLine(item +" - SET: LETRA");
break;
case "t":
Console.WriteLine(item +" - SET: LETRA");
break;
case "u":
Console.WriteLine(item +" - SET: LETRA");
break;
case "v":
Console.WriteLine(item +" - SET: LETRA");
break;
case "w":
Console.WriteLine(item +" - SET: LETRA");
break;
case "x":
Console.WriteLine(item +" - SET: LETRA");
break;
case "y":
Console.WriteLine(item +" - SET: LETRA");
break;
case "z":
Console.WriteLine(item +" - SET: LETRA");
break;
case "0":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "1":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "2":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "3":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "4":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "5":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "6":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "7":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "8":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "9":
Console.WriteLine(item +" - SET: DIGITO");
break;
case "\"":
Console.WriteLine(item +" - TOKEN: 2");
break;
case "\"":
Console.WriteLine(item +" - TOKEN: 2");
break;
case "'":
Console.WriteLine(item +" - TOKEN: 2");
break;
case "'":
Console.WriteLine(item +" - TOKEN: 2");
break;
case "=":
Console.WriteLine(item +" - TOKEN: 4");
break;
case "<":
Console.WriteLine(item +" - TOKEN: 5");
break;
case ">":
Console.WriteLine(item +" - TOKEN: 5");
break;
case "<":
Console.WriteLine(item +" - TOKEN: 6");
break;
case ">":
Console.WriteLine(item +" - TOKEN: 7");
break;
case ">":
Console.WriteLine(item +" - TOKEN: 8");
break;
case "=":
Console.WriteLine(item +" - TOKEN: 8");
break;
case "<":
Console.WriteLine(item +" - TOKEN: 9");
break;
case "=":
Console.WriteLine(item +" - TOKEN: 9");
break;
case "+":
Console.WriteLine(item +" - TOKEN: 10");
break;
case "-":
Console.WriteLine(item +" - TOKEN: 11");
break;
case "O":
Console.WriteLine(item +" - TOKEN: 12");
break;
case "R":
Console.WriteLine(item +" - TOKEN: 12");
break;
case "*":
Console.WriteLine(item +" - TOKEN: 13");
break;
case "A":
Console.WriteLine(item +" - TOKEN: 14");
break;
case "N":
Console.WriteLine(item +" - TOKEN: 14");
break;
case "D":
Console.WriteLine(item +" - TOKEN: 14");
break;
case "M":
Console.WriteLine(item +" - TOKEN: 15");
break;
case "O":
Console.WriteLine(item +" - TOKEN: 15");
break;
case "D":
Console.WriteLine(item +" - TOKEN: 15");
break;
case "D":
Console.WriteLine(item +" - TOKEN: 16");
break;
case "I":
Console.WriteLine(item +" - TOKEN: 16");
break;
case "V":
Console.WriteLine(item +" - TOKEN: 16");
break;
case "N":
Console.WriteLine(item +" - TOKEN: 17");
break;
case "O":
Console.WriteLine(item +" - TOKEN: 17");
break;
case "T":
Console.WriteLine(item +" - TOKEN: 17");
break;
case "(":
Console.WriteLine(item +" - TOKEN: 40");
break;
case "*":
Console.WriteLine(item +" - TOKEN: 40");
break;
case "*":
Console.WriteLine(item +" - TOKEN: 41");
break;
case ")":
Console.WriteLine(item +" - TOKEN: 41");
break;
case ";":
Console.WriteLine(item +" - TOKEN: 42");
break;
case ".":
Console.WriteLine(item +" - TOKEN: 43");
break;
case "{":
Console.WriteLine(item +" - TOKEN: 44");
break;
case "}":
Console.WriteLine(item +" - TOKEN: 45");
break;
case "(":
Console.WriteLine(item +" - TOKEN: 46");
break;
case ")":
Console.WriteLine(item +" - TOKEN: 47");
break;
case "[":
Console.WriteLine(item +" - TOKEN: 48");
break;
case "]":
Console.WriteLine(item +" - TOKEN: 49");
break;
case ".":
Console.WriteLine(item +" - TOKEN: 50");
break;
case ".":
Console.WriteLine(item +" - TOKEN: 50");
break;
case ":":
Console.WriteLine(item +" - TOKEN: 51");
break;
case ",":
Console.WriteLine(item +" - TOKEN: 52");
break;
case ":":
Console.WriteLine(item +" - TOKEN: 53");
break;
case "=":
Console.WriteLine(item +" - TOKEN: 53");
break;

default:Console.WriteLine("ERROR 5 - item - no se encuentra asignado");
break;
}
}Console.ReadKey();
}
}
}