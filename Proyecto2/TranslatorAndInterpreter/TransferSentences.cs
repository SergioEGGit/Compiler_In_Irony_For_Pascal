// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class TransFerSentences : AbstractInstruccion
    {

        // Tipo
        public readonly String TransType;

        // Expression
        public readonly AbstractExpression Expression_;

        // Constructor 
        public TransFerSentences(String TransType, AbstractExpression Expression_)
        {

            // Inicializar Valores 
            this.TransType = TransType;
            this.Expression_ = Expression_;

        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Objecto Retorno
            ObjectReturn TypeReturn = null;

            // Verificar Tipo
            if (this.TransType.Equals("Break"))
            {

                // Inicializar 
                TypeReturn = new ObjectReturn("", "")
                {

                    // Setear Opcional 
                    Option = "break"

                };

            }
            else if (this.TransType.Equals("Continue"))
            {

                // Inicializar 
                TypeReturn = new ObjectReturn("", "")
                {

                    // Setear Opcional 
                    Option = "continue"

                };

            }
            else if (this.TransType.Equals("Return"))
            {

                // Inicializar 
                TypeReturn = Expression_.Execute(Env);

                // Setear Opcional 
                TypeReturn.Option = "return";

            }

            // Retornar 
            return TypeReturn;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Verificar Tipo 
            if (this.TransType.Equals("Break"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "break;\n";

            }
            else if (this.TransType.Equals("Continue"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "continue;\n";

            }
            else if (this.TransType.Equals("Return"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "exit(";

                // Traducir 
                this.Expression_.Translate(Env);

                // Agregar Trauddcion
                VariablesMethods.TranslateString += ");\n";

            }

            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Buscar Etiquetas 
            String[] AuxiliaryArray = Env.SearchBreakAndContinueCycles();

            // Verificar Tipo De Sentencia 
            if (this.TransType.Equals("Break"))
            {

                // Veriricar Si ESta En Un Ciclo 
                if (!AuxiliaryArray[0].Equals(""))
                {

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Instrucción Break", "Uno");

                    // Agregar Salto 
                    Instance_1.AddNonConditionalJump(AuxiliaryArray[0], "Dos");

                }
                else 
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Break Debe Aparecer Dentro De Un Ciclo O Un Case", 0, 0));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }
            else if(this.TransType.Equals("Continue")) 
            {

                // Veriricar Si ESta En Un Ciclo 
                if (!AuxiliaryArray[1].Equals(""))
                {

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Instrucción Continue", "Uno");

                    // Agregar Salto 
                    Instance_1.AddNonConditionalJump(AuxiliaryArray[1], "Dos");

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Continue Debe Aparecer Dentro De Un Ciclo", 0, 0));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }
            else if(this.TransType.Equals("Return"))
            {

                // Objeto 
                ObjectReturn ValueExp = null;

                // Verificar Si Expression Es Nulla 
                if (Expression_ != null)
                {

                    // Compilar Expression 
                    ValueExp = this.Expression_.Compilate(Env);

                }

                // Obtener Funcion 
                EnviromentTable FunctionEnv = Env.SearchReturnFuncs();

                // Verificar Si Existe La Funcion 
                if (FunctionEnv != null)
                {

                    // Verificar Si Es Procedimiento 
                    if(FunctionEnv.ActualFunction.TypeFunc.Equals("procedure"))
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instrucción Return No Es Valida Dentro De Un Procedimiento Debe De Aparecer Dentro De Una Función", 0, 0));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }
                    else
                    {

                        // Verificar Expresssion
                        if (ValueExp != null)
                        {

                            // Verificar TIpo 
                            if (FunctionEnv.ActualFunction.ReturnType.Equals(ValueExp.Type))
                            {

                                // Verificar Tipo Boolean 
                                if (FunctionEnv.ActualFunction.ReturnType.Equals("boolean"))
                                {

                                    // Crear Temporal 
                                    String AuxiliaryLabel = Instance_1.CreateLabel();

                                    // Agregar Comentario 
                                    Instance_1.AddCommentOneLine("Return Tipo Boolean", "Uno");

                                    // Agregar Label 
                                    Instance_1.AddLabel(ValueExp.BoolTrue, "Dos");

                                    // Agregar Idetnacion 
                                    Instance_1.AddIdent();

                                    // Agregar a Stack 
                                    Instance_1.AddValueToStack("SP", "1", "Dos");

                                    // Agregar Non Conditional 
                                    Instance_1.AddNonConditionalJump(AuxiliaryLabel, "Dos");

                                    // Eliminar Identacion 
                                    Instance_1.DeleteIdent();

                                    // Agregar Lable False 
                                    Instance_1.AddLabel(ValueExp.BoolFalse, "Dos");

                                    // Agregar Idetnacion 
                                    Instance_1.AddIdent();

                                    // Agregar a Stack 
                                    Instance_1.AddValueToStack("SP", "0", "Dos");

                                    // Eliminar Identacion 
                                    Instance_1.DeleteIdent();

                                    // Agregar Etiqueta Final 
                                    Instance_1.AddLabel(AuxiliaryLabel, "Dos");

                                    // Agregar Idetnacion 
                                    Instance_1.AddIdent();

                                    // Agregar Comentario 
                                    Instance_1.AddCommentOneLine("Fin Retorno Boolean", "Uno");

                                    // Eliminar Identacion 
                                    Instance_1.DeleteIdent();

                                }
                                else 
                                {

                                    // Agregar Comentario 
                                    Instance_1.AddCommentOneLine("Return Tipo " + ValueExp.Type, "Uno");

                                    // Agregar Otro Valor A Stack 
                                    Instance_1.AddValueToStack("SP", ValueExp.GetValue(), "Dos");
                                
                                }

                                // Agregar Comentario
                                Instance_1.AddNonConditionalJump(FunctionEnv.ReturnLabel, "Dos");

                            }
                            else
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Retorno No Coincide Con El Tipo De La Función", 0, 0));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }

                        }
                        else
                        {

                            // Agregar Error 
                            VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instruccion Return Debe De Tener Una Expresion", 0, 0));

                            // Aumentar Contador
                            VariablesMethods.AuxiliaryCounter += 1;

                        }
                    
                    }

                }
                else 
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instrucción Return Debe Aparecer Dentro De Un Función", 0, 0));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }                

            }

            // Retornar 
            return null;

        }

    }

}