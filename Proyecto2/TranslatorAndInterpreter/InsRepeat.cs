// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsRepeat : AbstractInstruccion
    {

        // Atributos 

        // Expression 
        public readonly AbstractExpression Expression_;

        // Lista De Intrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Token Line
        public readonly int TokenLine;

        // Token Column
        public readonly int TokenColumn;

        // Constructor 
        public InsRepeat(AbstractExpression Expression_, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.Expression_ = Expression_;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Crear Nuevo Entorno
            EnviromentTable RepeatEnv = new EnviromentTable(Env, "Env_Repeat");

            // Verificar La Expression
            ObjectReturn RepeatExp = this.Expression_.Execute(RepeatEnv);

            // Verificar Si Hay Error Semantico 
            if (RepeatExp.Type.Equals("boolean"))
            {

                do
                {

                    // Verificar Si Hay Instrucciones 
                    if (this.InstruccionsList != null)
                    {

                        // Recorrer Lista De Instrucciones 
                        foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                        {

                            // Verificar Si ESta Nullo
                            if (Instruccion != null) 
                            {

                                // Ejecutar Instruccion
                                ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(RepeatEnv);

                                // Verificar SI ESta Nullo
                                if (ObjectTransfer != null)
                                {

                                    // Verificar Si ES Break
                                    if (ObjectTransfer.Option.Equals("break"))
                                    {

                                        // Retrun Null
                                        return null;

                                    }
                                    else if(ObjectTransfer.Option.Equals("continue"))
                                    {

                                        // Continuar 
                                        break;

                                    }
                                    else 
                                    {

                                        // Verificar Si Hay Ciclos 
                                        bool Flag = RepeatEnv.SearchFuncs();

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

                    // Ejecutar Expression
                    RepeatExp = this.Expression_.Execute(RepeatEnv);

                } while (!bool.Parse(RepeatExp.Value.ToString()));

            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expresion A Cumplir De Un Repeat Tiene Que Ser De Tipo Boolean", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Crear Nuevo Entorno 
            EnviromentTable RepeatEnv = new EnviromentTable(Env, "Env_Repeat");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "repeat\n";

            // Agregar Traduccion 
            VariablesMethods.TranslateString += VariablesMethods.Ident() + "begin\n";

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            // Recorrer Instrucciones 
            if (this.InstruccionsList != null)
            {

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Verificar Si ESta Nullo
                    if (Instruccion != null) 
                    {

                        // Ejecutar Instruccion 
                        Instruccion.Translate(RepeatEnv);

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
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            // Agregar A Traduccion
            VariablesMethods.TranslateString += VariablesMethods.Ident() + "until ";

            // Verificar Si ES Nullo 
            if (Expression_ != null) 
            {

                // Obtener Traduccion De Expressiones 
                this.Expression_.Translate(RepeatEnv);

            }

            // Agregar Traduccion
            VariablesMethods.TranslateString += ";\n";

            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {
            throw new NotImplementedException();
        }

    }

}