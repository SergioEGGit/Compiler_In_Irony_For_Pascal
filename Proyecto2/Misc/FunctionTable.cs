// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System.Collections.Generic;
using System;
using Proyecto2.TranslatorAndInterpreter;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{

    // Clase Principal
    class FunctionTable
    {

        // Atributos 

        // Tipo Funcion 
        public readonly String TypeFunc;

        // Tipo Returno 
        public readonly String ReturnType;

        // Identificaro 
        public readonly String Identifier;

        // LIsta De Parametros 
        public readonly LinkedList<ObjectReturn> ParamsList;

        // Lista De Declaraciones 
        public readonly LinkedList<AbstractInstruccion> DeclarationsList;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Env
        public readonly String Env;

        // Linea 
        public readonly int Line;

        // Columan
        public readonly int Column;

        // Entornor Padre 
        public readonly EnviromentTable Parent;

        // Numero De Parametros 
        public int ParamsCount;

        // Constructor  
        public FunctionTable(String TypeFunc, String Identifier, String ReturnType, LinkedList<ObjectReturn> ParamsList, LinkedList<AbstractInstruccion> DeclarationsList, LinkedList<AbstractInstruccion> InstruccionsList, String Env, int Line, int Column, EnviromentTable Parent)
        {

            // Inicializar Valores
            this.TypeFunc = TypeFunc;
            this.Identifier = Identifier;
            this.ReturnType = ReturnType;
            this.ParamsList = ParamsList;
            this.DeclarationsList = DeclarationsList;
            this.InstruccionsList = InstruccionsList;
            this.Env = Env; 
            this.Line = Line;
            this.Column = Column;
            this.Parent = Parent;
            this.ParamsCount = 0;

        }

    }

}