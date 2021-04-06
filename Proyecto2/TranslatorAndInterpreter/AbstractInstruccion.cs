// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
   
    // Clase Abastracta     
    abstract class AbstractInstruccion
    {

        // Creación De Metodos Abstractos 
        
        // Método Traducir 
        public abstract object Translate(EnviromentTable Env);

        // Método Ejecutar 
        public abstract object Execute(EnviromentTable Env);

    }
    
}