// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsGraficarTS : AbstractInstruccion
    {

        // Atributos

        // Graficar Type 
        public readonly String GraphType;

        // Constructor
        public InsGraficarTS(String GraphType) 
        {

            // Iniciazliar Valores 
            this.GraphType = GraphType;
        
        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Ejecutar Tabla De Simbolos
            LinkedList<EnviromentTable> AuxiliaryList = Env.GraphSymbolTable();

            // Llamar A Metodo Graficar Tabla De Simbolos
            VariablesMethods.ReportSymbolTable(AuxiliaryList, Env.EnviromentName);

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Verificar Tipo Graph
            if (GraphType.Equals("2"))
            {

                // Agregar A Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "graficar_ts;\n";

            }
            else 
            {

                // Agregar A Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "graficar_ts();\n";

            }

            // Retornar Nullo
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Retornar 
            return null;

        }

    }

}