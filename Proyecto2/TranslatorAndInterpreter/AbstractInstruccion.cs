// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using Proyecto2.Misc;
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
   
    // Clase Abastracta     
    abstract class AbstractInstruccion
    {

        // Bool Auxiliary 
        public String BoolAux = "";

        // Creación De Metodos Abstractos 

        // Método Traducir 
        public abstract object Translate(EnviromentTable Env);

        // Método Ejecutar 
        public abstract object Execute(EnviromentTable Env);

        // Método Compilar 
        public abstract object Compilate(EnviromentTable Env);

    }
    
}