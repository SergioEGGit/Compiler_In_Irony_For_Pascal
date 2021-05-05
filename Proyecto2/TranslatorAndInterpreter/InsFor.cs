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

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Crear Nuevo Entorno
            EnviromentTable ForEnv = new EnviromentTable(Env, "Env_For");

            // Verificar La Expression 1
            ObjectReturn AsgExp = this.Expression_.Compilate(Env);

            // Verificar La Expression 2
            ObjectReturn LmExp = this.Expression__.Compilate(Env);

            // Simbolo
            SymbolTable ActualVar = ForEnv.GetVariableStack(this.Identifier);

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Crear Label 
            String InicioForLabel = Instance_1.CreateLabel();
            String FinalForLabel = Instance_1.CreateLabel();

            // Agregar A Entorno Las Etiquetas 
            ForEnv.ContinueLabel = InicioForLabel;
            ForEnv.BreakLabel = FinalForLabel;

            // Agregar Comentario
            Instance_1.AddCommentOneLine("Comienzo Instrucción For", "Uno");

            // Buscar Variable 
            if (ActualVar != null)
            {

                // Verificar TIpo De Foor 
                if (this.TypeFor.Equals("to"))
                {

                    // Crear Temporal 
                    String TemporaryActual = Instance_1.CreateTemporary();
                    String TemporarySum = Instance_1.CreateTemporary();
                    String TemporaryIndex = Instance_1.CreateTemporary();
                    String TemporaryAux = Instance_1.CreateTemporary();

                    // Eliminar Temporal
                    Instance_1.DeleteTemporary(TemporaryActual);
                    Instance_1.DeleteTemporary(TemporarySum);
                    Instance_1.DeleteTemporary(TemporaryIndex);
                    Instance_1.DeleteTemporary(TemporaryAux);

                    // Verificar Si Es Global 
                    if (ActualVar.IsGlobalVar)
                    {

                        // Inicializar Variable 
                        Instance_1.AddValueToStack(ActualVar.GetValue(), AsgExp.GetValue(), "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Inicializar Variable 
                        Instance_1.AddValueToStack(TemporaryAux, AsgExp.GetValue(), "Dos");

                    }                    

                    // Agregar Inicio For 
                    Instance_1.AddLabel(InicioForLabel, "Dos");

                    // Agregar Identacion 
                    Instance_1.AddIdent();

                    // Verificar Si Es Global
                    if (ActualVar.IsGlobalVar)
                    {

                        // Setear Valor A Temporal
                        Instance_1.AddOneExpression(TemporaryIndex, ActualVar.GetValue(), "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Setear Valor A Temporal
                        Instance_1.AddOneExpression(TemporaryIndex, TemporaryAux, "Dos");

                    }

                    // Obtener Valor Variable 
                    Instance_1.GetValueOfStack(TemporaryIndex, TemporaryActual, "Dos");

                    // Verificar Condicion 
                    Instance_1.AddConditionalJump(TemporaryActual, ">", LmExp.GetValue(), FinalForLabel, "Dos");

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
                                Instruccion.Compilate(ForEnv);
                                                                       

                            }

                        }

                    }

                    // Sumar Variable 
                    Instance_1.AddTwoExpression(TemporarySum, TemporaryActual, "+", "1", "Dos");

                    // Verificar Si Es Global 
                    if (ActualVar.IsGlobalVar)
                    {

                        // Agregar Nuevo Valor Variable
                        Instance_1.AddValueToStack(ActualVar.GetValue(), TemporarySum, "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Agregar Nuevo Valor Variable
                        Instance_1.AddValueToStack(TemporaryAux, TemporarySum, "Dos");

                    }

                    // Agregar Salto No Condicional 
                    Instance_1.AddNonConditionalJump(InicioForLabel, "Dos");

                    // Quitar Identacion 
                    Instance_1.DeleteIdent();

                    // Agregar Final For 
                    Instance_1.AddLabel(FinalForLabel, "Dos");

                    // Agregar IDentacion
                    Instance_1.AddIdent();

                    // AGregar Comentario 
                    Instance_1.AddCommentOneLine("Fin Instrucción For \n", "Uno");

                    // Eliminar Identacion
                    Instance_1.DeleteIdent();

                }
                else
                {

                    // Crear Temporal 
                    String TemporaryActual = Instance_1.CreateTemporary();
                    String TemporaryMinus = Instance_1.CreateTemporary();
                    String TemporaryIndex = Instance_1.CreateTemporary();
                    String TemporaryAux = Instance_1.CreateTemporary();

                    // Eliminar Temporal
                    Instance_1.DeleteTemporary(TemporaryActual);
                    Instance_1.DeleteTemporary(TemporaryMinus);
                    Instance_1.DeleteTemporary(TemporaryIndex);
                    Instance_1.DeleteTemporary(TemporaryAux);

                    // Verificar 
                    if (ActualVar.IsGlobalVar)
                    {

                        // Inicializar Variable 
                        Instance_1.AddValueToStack(ActualVar.GetValue(), AsgExp.GetValue(), "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Inicializar Variable 
                        Instance_1.AddValueToStack(TemporaryAux, AsgExp.GetValue(), "Dos");

                    }

                    // Agregar Inicio For 
                    Instance_1.AddLabel(InicioForLabel, "Dos");

                    // Agregar Identacion 
                    Instance_1.AddIdent();

                    // Verificar Global 
                    if (ActualVar.IsGlobalVar)
                    {

                        // Setear Valor A Temporal
                        Instance_1.AddOneExpression(TemporaryIndex, ActualVar.GetValue(), "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Setear Valor A Temporal
                        Instance_1.AddOneExpression(TemporaryIndex, TemporaryAux, "Dos");

                    }

                    // Obtener Valor Variable 
                    Instance_1.GetValueOfStack(TemporaryIndex, TemporaryActual, "Dos");

                    // Verificar Condicion 
                    Instance_1.AddConditionalJump(TemporaryActual, "<", LmExp.GetValue(), FinalForLabel, "Dos");

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
                                Instruccion.Compilate(ForEnv);


                            }

                        }

                    }

                    // Sumar Variable 
                    Instance_1.AddTwoExpression(TemporaryMinus, TemporaryActual, "-", "1", "Dos");

                    // Verificar Global
                    if (ActualVar.IsGlobalVar)
                    {

                        // Agregar Nuevo Valor Variable
                        Instance_1.AddValueToStack(ActualVar.GetValue(), TemporaryMinus, "Dos");

                    }
                    else 
                    {

                        // Agregar Temporal 
                        Instance_1.AddTwoExpression(TemporaryAux, "SP", "+", ActualVar.GetValue(), "Dos");

                        // Agregar Nuevo Valor Variable
                        Instance_1.AddValueToStack(TemporaryAux, TemporaryMinus, "Dos");

                    }

                    // Agregar Salto No Condicional 
                    Instance_1.AddNonConditionalJump(InicioForLabel, "Dos");

                    // Quitar Identacion 
                    Instance_1.DeleteIdent();

                    // Agregar Final For 
                    Instance_1.AddLabel(FinalForLabel, "Dos");

                    // Agregar IDentacion
                    Instance_1.AddIdent();

                    // AGregar Comentario 
                    Instance_1.AddCommentOneLine("Fin Instrucción For \n", "Uno");

                    // Eliminar Identacion
                    Instance_1.DeleteIdent();

                }

            }

            // Retornar
            return null;

        }

    }

}