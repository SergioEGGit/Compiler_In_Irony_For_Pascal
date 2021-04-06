// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class Cases : AbstractInstruccion
    {

        // Atributos 

        // Expression
        public readonly AbstractExpression Expression_;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Token LIne 
        public readonly int TokenLine;

        // Token COlumn 
        public readonly int TokenColumn;

        // Constructor
        public Cases(AbstractExpression Expression_, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn) 
        {

            // Inicializar Valores 
            this.Expression_ = Expression_;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        public override object Execute(EnviromentTable Env)
        {

            // Verificar SI ES Nullo
            if(this.InstruccionsList != null)
            {

                // Recorrer Lista De Instrucciones
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Verificar Si Es Nullo
                    if (Instruccion != null)
                    {

                        // Ejecutar Instruccion
                        ObjectReturn ObjectTransfer = (ObjectReturn) Instruccion.Execute(Env);

                        // Verificar SI ESta Nullo
                        if (ObjectTransfer != null)
                        {

                            // Verificar Si ES Break
                            if (ObjectTransfer.Option.Equals("break"))
                            {

                                // Retrun Null
                                return null;

                            }
                            else if (ObjectTransfer.Option.Equals("continue"))
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Continue Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }
                            else
                            {

                                // Verificar Si Hay Ciclos 
                                bool Flag = Env.SearchFuncs();

                                // Verificar 
                                if (Flag)
                                {

                                    // Retornar Valor 
                                    return ObjectTransfer;

                                }
                                else
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Exit Tiene Que Aparecer Dentro De Una Funcion", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;


                                }

                            }

                        }


                    }

                }

            }

            // Retonar 
            return null;

        }

        public override object Translate(EnviromentTable Env)
        {

            // Verificar Si Esta Null
            if (this.Expression_ != null)
            {

                // Agregar Instruccion 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident();

                // Ejectuar Trauddcion
                this.Expression_.Translate(Env);

                // Agregar Traduccion
                VariablesMethods.TranslateString += ":\n";

                // Agregaar Traduccion
                VariablesMethods.TranslateString += VariablesMethods.Ident() + "begin\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verficiar Si Es Nullo
                if (this.InstruccionsList != null) 
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        // Verificar Si Esta Nullo
                        if (Instruccion != null)
                        {

                            // Traducir 
                            Instruccion.Translate(Env);

                        }

                    }

                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Agregaar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            }
            else 
            {

                // Agregar Instruccion 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "else\n";

                // Agregaar Traduccion
                VariablesMethods.TranslateString += VariablesMethods.Ident() + "begin\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verificar Si ESt a Nullo
                if(this.InstruccionsList != null) 
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        // Verificar Si Esta Nullo
                        if (Instruccion != null)
                        {

                            // Traducir 
                            Instruccion.Translate(Env);

                        }

                    }

                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Agregaar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            }

            // Retornar
            return null;

        }

    }

}