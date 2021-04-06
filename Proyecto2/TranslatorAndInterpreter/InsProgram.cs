// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase InsProgram
    class InsProgram : AbstractInstruccion
    {

        // Atributos

        // Identificador
        public readonly String IdentifierInsProgram;

        // Linea
        public readonly int TokenLine;

        // Columna
        public readonly int TokenColumn;

        // Constructor 
        public InsProgram(String IdentifierInsProgram, int TokenLine, int TokenColumn) {

            // Inicializar Valores
            this.IdentifierInsProgram = IdentifierInsProgram;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }
        
        // Metodo Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Agregar Value 
            ObjectReturn Value = new ObjectReturn("-", "program");
            
            // Agregar Identificador A Tabla Simbolos
            Env.AddVariable(this.IdentifierInsProgram, "program", Value, "program", Env.EnviromentName, this.TokenLine, this.TokenColumn);

            // Retorno 
            return null;
        
        }

        // Metodo Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Traducción 
            VariablesMethods.TranslateString += VariablesMethods.Ident() + "program " + this.IdentifierInsProgram + ";\n";

            // Retorno 
            return null;

        }

    }

}