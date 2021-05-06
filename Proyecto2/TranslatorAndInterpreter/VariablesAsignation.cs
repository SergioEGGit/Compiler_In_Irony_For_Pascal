// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;
using System.Windows.Forms;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class VariablesAsignation : AbstractInstruccion
    {

        // Atributos

        // Identifier
        public readonly String Identifier;

        // Expresion 1
        public readonly AbstractExpression Expression_;

        // Token Line
        public readonly int TokenLine;

        // Token COlumn 
        public readonly int TokenColumn;

        // Constructor 
        public VariablesAsignation(String Identifier, AbstractExpression Expression_, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.Identifier = Identifier;
            this.Expression_ = Expression_;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Verificar La Expression 1
            ObjectReturn AsgExp = this.Expression_.Execute(Env);

            // Simbolo
            SymbolTable ActualVar = Env.GetVariable(this.Identifier);

            // Function 
            FunctionTable ActualFunc = Env.GetFunction(this.Identifier);

            // Value Auxiliar 
            ObjectReturn ActualValue = new ObjectReturn("", "");

            // Buscar Variable 
            if(AsgExp != null)
            {

                // Verificar Si Es Funcion 
                if (ActualFunc != null && ActualVar == null)
                {

                    // Obtener Valores 
                    ActualValue.Type = AsgExp.Type;
                    ActualValue.Value = AsgExp.Value;
                    ActualValue.Option = "return";

                    // Retornar Expression 
                    return ActualValue;

                }
                else if (ActualFunc == null && ActualVar != null)
                {

                    // Verificar Si Ambas Condiciones Son Integers
                    if (ActualVar.Type.Equals(AsgExp.Type))
                    {

                        // Obtener Valores 
                        ActualValue.Type = ActualVar.Type;
                        ActualValue.Value = AsgExp.Value;

                        // Agregar A Variable 
                        ActualVar.Value = ActualValue;

                        // Setear Variable 
                        Env.SetVariable(this.Identifier, ActualVar);

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Los Tipos De Dato No Coinciden", this.TokenLine, this.TokenColumn));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable O Funcion Indica No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expression Indicada Es Incorrecta", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }
            
            // Retornar
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + this.Identifier + " := ";

            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(Env);

            // Agregar Traduccion
            VariablesMethods.TranslateString += ";\n";

            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Verificar La Expression 1
            ObjectReturn AsgExp = this.Expression_.Compilate(Env);

            // Simbolo
            SymbolTable ActualVar = Env.GetVariableStack(this.Identifier);

            // Function 
            FunctionTable ActualFunc = Env.GetFunctionStack(this.Identifier);

            // Value Auxiliar 
            ObjectReturn ActualValue = new ObjectReturn("", "");

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Agregar Comentario 
            Instance_1.AddCommentOneLine("Asignación De Variables", "Uno");

            // Buscar Variable 
            if (AsgExp != null)
            {

                // Verificar Si Es Funcion 
                if (ActualFunc != null && ActualVar == null)
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
                        if (FunctionEnv.ActualFunction.TypeFunc.Equals("procedure"))
                        {

                            // Agregar Error 
                            VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instrucción Return No Es Valida Dentro De Un Procedimiento Debe De Aparecer Dentro De Una Función", this.TokenLine, this.TokenColumn));

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
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Retorno No Coincide Con El Tipo De La Función", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                }

                            }
                            else
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instruccion Return Debe De Tener Una Expresion", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }

                        }

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Instrucción Return Debe Aparecer Dentro De Un Función", this.TokenLine, this.TokenColumn));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }

                }
                else if (ActualFunc == null && ActualVar != null)
                {

                    // Verificar Si Ambas Condiciones Son Integers
                    if (ActualVar.Type.Equals(AsgExp.Type))
                    {

                        if (ActualVar.Type.Equals("boolean"))
                        {

                            // Crear Etiqueta 
                            String LabelAuxiliary = Instance_1.CreateLabel();

                            // Añadir Etiqueta Verdadera 
                            Instance_1.AddLabel(AsgExp.BoolTrue, "Dos");

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Crear Temporal 
                            String Temporary = Instance_1.CreateTemporary();

                            // Eliminar TEmporal 
                            Instance_1.DeleteTemporary(Temporary);

                            // Verificar Si Es Global 
                            if (ActualVar.IsGlobalVar)
                            {

                                // Añadir Expression 
                                Instance_1.AddOneExpression(Temporary, ActualVar.GetValue(), "Dos");

                            }
                            else
                            {

                                // Añadir Expression 
                                Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.GetValue(), "Dos");

                            }

                            // Añadir A STack 
                            Instance_1.AddValueToStack(Temporary, "1", "Dos");

                            // Añadir Goto 
                            Instance_1.AddNonConditionalJump(LabelAuxiliary, "Dos");

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Añadir Etiqueta Falsa 
                            Instance_1.AddLabel(AsgExp.BoolFalse, "Dos");

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Crear Temporal 
                            String Temporary_False = Instance_1.CreateTemporary();

                            // Eliminar TEmporal 
                            Instance_1.DeleteTemporary(Temporary_False);

                            // Añadir Expression 
                            Instance_1.AddTwoExpression(Temporary_False, "SP", "+", ActualVar.GetValue(), "Dos");

                            // Añadir A STack 
                            Instance_1.AddValueToStack(Temporary_False, "0", "Dos");

                            // Añadir Goto 
                            Instance_1.AddNonConditionalJump(LabelAuxiliary, "Dos");

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Agrego Etiqueta Auxiliar 
                            Instance_1.AddLabel(LabelAuxiliary, "Dos");

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Añadir Comentario 
                            Instance_1.AddCommentOneLine("Fin Asignación Expresión Boolean \n", "Uno");

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                        }
                        else
                        {

                            // Crear Temporal 
                            String Temporary = Instance_1.CreateTemporary();

                            // Eliminar TEmporal 
                            Instance_1.DeleteTemporary(Temporary);

                            // Verificar Si Es Global 
                            if (ActualVar.IsGlobalVar)
                            {

                                // Añadir Expression 
                                Instance_1.AddOneExpression(Temporary, ActualVar.GetValue(), "Dos");

                            }
                            else
                            {

                                // Añadir Expression 
                                Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.GetValue(), "Dos");

                            }

                            // Agreagr A Stack 
                            Instance_1.AddValueToStack(Temporary, AsgExp.GetValue(), "Dos");

                        }

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Error Al Asignar Expression Tipo " + AsgExp.Type + " A Variable De Tipo " + ActualVar.Type, this.TokenLine, this.TokenColumn));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }

                }
                else 
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable " + this.Identifier + " No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }

            // Retornar
            return null;

        }

    }

}