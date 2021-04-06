// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;
using System.Windows.Forms;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsFor : AbstractInstruccion
    {

        // Atributos

        // Identifier
        public readonly String Identifier;

        // Expresion 1
        public readonly AbstractExpression Expression_;

        // Expresion 2
        public readonly AbstractExpression Expression__;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Token Line
        public readonly int TokenLine;

        // Token COlumn 
        public readonly int TokenColumn;

        // Type For 
        public readonly String TypeFor;

        // Constructor 
        public InsFor(String Identifier, AbstractExpression Expression_, AbstractExpression Expression__, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn, String TypeFor)
        {

            // Inicializar Valores 
            this.Identifier = Identifier;
            this.Expression_ = Expression_;
            this.Expression__ = Expression__;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
            this.TypeFor = TypeFor;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Crear Nuevo Entorno
            EnviromentTable ForEnv = new EnviromentTable(Env, "Env_For");

            // Verificar La Expression 1
            ObjectReturn AsgExp = this.Expression_.Execute(ForEnv);

            // Verificar La Expression 2
            ObjectReturn LmExp = this.Expression__.Execute(ForEnv);

            // Simbolo
            SymbolTable ActualVar = ForEnv.GetVariable(this.Identifier);

            // Value Auxiliar 
            ObjectReturn ActualValue = new ObjectReturn("", "");

            // Buscar Variable 
            if (ActualVar != null)
            {

                // Verificar Si Ambas Condiciones Son Integers
                if(AsgExp.Type.Equals("integer") && LmExp.Type.Equals("integer") && ActualVar.Type.Equals("integer"))
                {
                    
                    // Verificar TIpo De Foor 
                    if (this.TypeFor.Equals("to"))
                    {

                        // For 
                        for (int Count = int.Parse(AsgExp.Value.ToString()); Count <= int.Parse(LmExp.Value.ToString()); Count++)
                        {

                            // Setear Valores 
                            ActualValue.Type = "integer";
                            ActualValue.Value = Count;

                            // Agregar A Tabla De Simbolos 
                            ActualVar.Value = ActualValue;

                            // Setear Nueva Variable 
                            ForEnv.SetVariable(this.Identifier, ActualVar);

                            // Verificar Si La Lista De Instrucciones ESta Nulla
                            if (this.InstruccionsList != null)
                            {

                                // Recorrer Lista 
                                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                                {

                                    // Verificar Si NO Es Null
                                    if (Instruccion != null)
                                    {

                                        // Obtener Objeto
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(ForEnv);

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

                                                // Continuar 
                                                break;

                                            }
                                            else
                                            {

                                                // Verificar Si Hay Ciclos 
                                                bool Flag = ForEnv.SearchFuncs();

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

                        }

                    }
                    else
                    {

                        // For 
                        for (int Count = int.Parse(AsgExp.Value.ToString()); Count >= int.Parse(LmExp.Value.ToString()); Count--)
                        {

                            // Setear Valores 
                            ActualValue.Type = "integer";
                            ActualValue.Value = Count;

                            // Agregar A Tabla De Simbolos 
                            ActualVar.Value = ActualValue;

                            // Setear Nueva Variable 
                            ForEnv.SetVariable(this.Identifier, ActualVar);

                            // Verificar Si La Lista De Instrucciones ESta Nulla
                            if (this.InstruccionsList != null)
                            {

                                // Recorrer Lista 
                                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                                {

                                    // Verificar Si NO Es Null
                                    if (Instruccion != null)
                                    {

                                        // Obtener Objeto
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(ForEnv);

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

                                                // Continuar 
                                                break;

                                            }
                                            else
                                            {

                                                // Verificar Si Hay Ciclos 
                                                bool Flag = ForEnv.SearchFuncs();

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

                        }

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable De Asignacion, Comienzo Y Limite De Un For Tienen Que Ser De Tipo Integer", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }
                
            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable Indica En El Ciclo For No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

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
            EnviromentTable ForEnv = new EnviromentTable(Env, "Env_For");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "for " + this.Identifier + " := ";
                       
            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(ForEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += " to ";

            // Obtener Traduccion De Expressiones 
            this.Expression__.Translate(ForEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += " do";

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "begin\n";

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            // Recorrer Instrucciones 
            if (this.InstruccionsList != null)
            {

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Verificar Si Esta Vaica 
                    if(Instruccion != null) 
                    {

                        // Traudcir Instruccion
                        Instruccion.Translate(ForEnv);
                
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

            // Agregar A Traduccion
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            // Retornar 
            return null;

        }

    }

}