// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class MainBlock : AbstractInstruccion
    {

        // Atributos 

        // Lista De Instrucciones
        private readonly LinkedList<AbstractInstruccion> IntruccionsList = new LinkedList<AbstractInstruccion>();

        // TOken LIne 
        private readonly int TokenLine;

        // Token Column
        private readonly int TokenColumn;

        // Constructor 
        public MainBlock(LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn) 
        {

            // Inicializar Valores 
            this.IntruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {
            // Nuevo Ambiente 
            EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

            // Verificar Si No Esta Nullo
            if (this.IntruccionsList != null) 
            {

                // Recorrer Lista De Instrucciones 
                foreach(AbstractInstruccion Instruccion in this.IntruccionsList) 
                {

                    // Verificar Si Esta Nullo
                    if (Instruccion != null) 
                    {

                        // Ejecutar Instrucciones 
                        ObjectReturn ObjectTransfer = (ObjectReturn) Instruccion.Execute(MainEnv);

                        // VErificar Si ETSa Nullo 
                        if (ObjectTransfer != null) 
                        {

                            // Verificar TIpo
                            if (ObjectTransfer.Option.Equals("break") || ObjectTransfer.Option.Equals("continue"))
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Break O Continue Deben De Aparecer Dentro De Un CIclo", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }
                            else
                            {

                                // Verificar SI Es Llamada A Funcion 
                                if(!ObjectTransfer.End.Equals("FunctionCall")) 
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Exit Deben De Aparecer Dentro De Una Funcion", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                }

                            }

                        }

                    }
                
                }
            
            }
           
            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\nbegin\n";

            // Nuevo Ambiente 
            EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            if(this.IntruccionsList != null) 
            {

                // Recorrer Lista 
                foreach (AbstractInstruccion Instruccion in this.IntruccionsList)
                {

                    // Verificar SI No Es Nullo
                    if (Instruccion != null) 
                    {

                        // Traducir 
                        Instruccion.Translate(MainEnv);

                    }

                }

            }
            else
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n \n";

            }

            // Pop A Pila 
            VariablesMethods.AuxiliaryPile.Pop();

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\nend.\n";
            
            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Nuevo Ambiente 
            EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

            // Instancia Codigo TA
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Verificar Si No Esta Nullo
            if (this.IntruccionsList != null)
            {

                // Agregar Comentario 
                Instance_1.AddCommentOneLine("Método Main");

                // Agregar Inicio Bloque
                Instance_1.AddFuncBegin("main");

                // Agregar Identacion
                Instance_1.AddIdent();

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.IntruccionsList)
                {

                    // Verificar Si Esta Nullo
                    if (Instruccion != null)
                    {

                        Instruccion.Compilate(MainEnv);
                                                
                    }

                }

                // Quitar Identacion
                Instance_1.DeleteIdent();

                // Agregar Final Bloque
                Instance_1.AddFuncEnd();

            }

            // Retornar 
            return null;

        }

    }

}