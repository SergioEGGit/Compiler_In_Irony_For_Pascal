// ------------------------------------------ Librerias E Imports --------------------------------------------------
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Abastracta     
    abstract class AbstractExpression
    {

        // Creación De Metodos Abstractos 

        // Método Traducir 
        public abstract ObjectReturn Translate(EnviromentTable Env); 

        // Método Ejecutar 
        public abstract ObjectReturn Execute(EnviromentTable Env);  
        
        // Método Compilar
        //public abstract ObjectR

    }
}