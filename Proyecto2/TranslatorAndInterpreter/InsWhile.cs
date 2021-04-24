// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;
using System.Windows.Forms;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsWhile : AbstractInstruccion
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
        public InsWhile(AbstractExpression Expression_, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn) 
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
            EnviromentTable WhileEnv = new EnviromentTable(Env, "Env_While");

            // Verificar La Expression
            ObjectReturn WhileExp = this.Expression_.Execute(WhileEnv);

            if(WhileExp != null) 
            {

                // Verificar Si Hay Error Semantico 
                if (WhileExp.Type.Equals("boolean"))
                {

                    while (bool.Parse(WhileExp.Value.ToString()))
                    {

                        // Verificar Si Hay Instrucciones 
                        if (this.InstruccionsList != null)
                        {

                            // Recorrer Lista De Instrucciones 
                            foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                            {

                                // Verificar Si Es Nullo
                                if (Instruccion != null)
                                {

                                    // Ejecutar Instruccion
                                    ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(WhileEnv);

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
                                            bool Flag = WhileEnv.SearchFuncs();

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

                        WhileExp = this.Expression_.Execute(WhileEnv);

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expresion A Cumplie De Un While Tiene Que Ser De Tipo Boolean", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expresion A Cumplie De Un While Tiene Que Ser De Tipo Boolean", this.TokenLine, this.TokenColumn));

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
            EnviromentTable WhileEnv = new EnviromentTable(Env, "Env_While");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "while ";

            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(WhileEnv);

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

                    // verificar Si Es Nullo
                    if (Instruccion != null) 
                    {

                        // Ejecutar Instruccion 
                        Instruccion.Translate(WhileEnv);

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
            EnviromentTable WhileEnv = new EnviromentTable(Env, "Env_While");

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Auxiliares 
            String CommentAuxiliary = "Uno";
            String InsAuxiliary = "Dos";

            //  Agregar Comentario 
            Instance_1.AddCommentOneLine("Comienzo Instrucción While", CommentAuxiliary);

            // Crear Label 
            String LabelWhile = Instance_1.CreateLabel();

            // Añadir Label 
            Instance_1.AddLabel(LabelWhile, InsAuxiliary);

            // Agregar Identacion 
            Instance_1.AddIdent();
            
            // Verificar La Expression
            ObjectReturn WhileExp = this.Expression_.Compilate(WhileEnv);

            // Verificar Si No Es Nullo
            if(WhileExp != null)
            {

                // Verificar Si Hay Error Semantico 
                if(WhileExp.Type.Equals("boolean"))
                {

                    // Agregar Etiquetas De Break Y Continue 
                    WhileEnv.BreakLabel = WhileExp.BoolFalse;
                    WhileEnv.ContinueLabel = LabelWhile;

                    // Agergar Etiqueta True 
                    Instance_1.AddLabel(WhileExp.BoolTrue, InsAuxiliary);

                    // Añadir Identacion 
                    Instance_1.AddIdent();
                    
                    // Verificar Si Hay Instrucciones 
                    if (this.InstruccionsList != null)
                    {

                        // Recorrer Lista De Instrucciones 
                        foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                        {

                            // Verificar Si Es Nullo
                            if (Instruccion != null)
                            {

                                // Ejecutar Instruccion
                                Instruccion.Compilate(WhileEnv);

                            }

                        }

                    }

                    // Agregar Salto De Retorno Del While 
                    Instance_1.AddNonConditionalJump(LabelWhile, InsAuxiliary);

                    // Eliminar Identación
                    Instance_1.DeleteIdent();
                    Instance_1.DeleteIdent();

                    // Agregar Label 
                    Instance_1.AddLabel(WhileExp.BoolFalse, InsAuxiliary);

                    // Agregar Identación
                    Instance_1.AddIdent();

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Fin Instrucción While\n", CommentAuxiliary);

                    // Eliminar Identación
                    Instance_1.DeleteIdent();

                }

            }

            // Retornar 
            return null;

        }

    }

}