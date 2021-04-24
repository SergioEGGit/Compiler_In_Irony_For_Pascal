// ------------------------------------------ Librerias E Imports --------------------------------------------------
using Proyecto2.Misc;
using System;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Abastracta     
    abstract class AbstractExpression
    {

        // Atributos 
        
        // Es Bool Etiqueta True 
        public String BoolTrue = "";

        // Es Bool Etiqueta False 
        public String BoolFalse = "";

        // Es Global 
        public bool IsGlobal = false;

        // Creación De Metodos Abstractos 

        // Método Traducir 
        public abstract ObjectReturn Translate(EnviromentTable Env); 

        // Método Ejecutar 
        public abstract ObjectReturn Execute(EnviromentTable Env);

        // Método Compilar
        public abstract ObjectReturn Compilate(EnviromentTable Env);

    }
}