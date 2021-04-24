// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal 
    class VariablesDeclaration : AbstractInstruccion
    {

        // Atributos 

        // LIsta De Variables 
        private readonly LinkedList<AbstractInstruccion> VarList;

        // Constructor 
        public VariablesDeclaration(LinkedList<AbstractInstruccion> VarList) 
        {

            // Inicializar Valores 
            this.VarList = VarList;
        
        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Si Esta Nullo
            if (this.VarList != null)
            {

                // Ejectuar Traduccion
                foreach(AbstractInstruccion Var in this.VarList)
                {

                    // Verifiar Si Es Nullo
                    if (Var != null)
                    {

                        // Agregar ha Traduccion
                        Var.Execute(Env);

                    }

                }

            }

            // Retornar Null 
            return null;

        }

        // Método Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Verificar Si Esta Nullo
            if(this.VarList != null) 
            {

                // Agregar ha Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "var \n";

                // Ejectuar Traduccion
                foreach (AbstractInstruccion Var in this.VarList) 
                {

                    // Verifiar Si Es Nullo
                    if (Var != null) 
                    {

                        // Agregar ha Traduccion
                        Var.Translate(Env);

                    }
                
                }

            }
            else 
            {

                // Agregar ha Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "var \n";

            }

            // Retornar Null
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Verificar Si Esta Nullo
            if (this.VarList != null)
            {

                // Ejectuar Traduccion
                foreach (AbstractInstruccion Var in this.VarList)
                {

                    // Verifiar Si Es Nullo
                    if (Var != null)
                    {

                        // Agregar ha Traduccion
                        Var.Compilate(Env);

                    }

                }

            }

            // Retornar Null 
            return null;

        }

    }

}