# Proyecto Lenguajes Formales & Autómatas
Creación de un proyecto en lenguaje C# Windows Forms, que consta de la lectura de un archivo con extensión .txt *excusivamente*

## Fases
* Fase I - Análisis Léxico de la gramática
* Fase II - Análisis Sintáctico de la gramática
* Fase II - Generador de Scanner

### Requisitos
Programado en [Visual Studio 2019](https://visualstudio.microsoft.com/es/) 

* Desarrollo de ASP.NET  y web
* Desarrollo de Escritorio de .Net
* Desarrollo multiplataforma de .Net Core

### Como usar
El archivo .txt utilizado para pruebas se encuentra en la carpeta *PRUEBAS*, tiene el formato correcto para la buena ejecición.
Una vez ejecutado el programa, click en el botón "Examinar" el cual abrira por defecto la ubicación de la carpeta PRUEBAS, dentro se encuentra
un archivo llamado *Gramática*, importar dicho archivo y se ejecutará la lectura.
Posteriormente se hace click en el botón "Generar" que generará toda la Expresión Regular a partir de los TOKENs del archivo.
Se creará el árbol de Expresión con dichos TOKENs y de esta forma se obtienen FIRST, LAST, FOLLOW  
* Formato Ejemplo:  
* SETS
  * LETRA   = 'A'..'Z'+'a'..'z'
  * Add others...
* TOKENS
  * TOKEN 1= LETRA LETRA *
  * TOKEN 4 = '='
  * TOKEN 5  = '<''>'
  * TOKEN 15 = 'M''O''D'
* ACTIONS  
* RESERVADAS()
  * 18 = 'PROGRAM'
  * -Número- = 'Palabra_Reservada'
* ERROR = 54
* ERROR = #  
<p>
Todo TOKEN que se encuentre entre comillas individuales(') se considera TOKEN, de no estar encerrado en comillas se considera del área de SETS, por lo que se debe de definir previamente
<p>

## Versionamiento
Usé [Github](https://github.com/) como almacenamiento de versiones. Para ver, porfavor visita los [commits](https://github.com/AlfaRojo/LFA_Proyecto/commits/master)

## Autor
Kevin Andrés Ortiz Ramírez, Guatemala
