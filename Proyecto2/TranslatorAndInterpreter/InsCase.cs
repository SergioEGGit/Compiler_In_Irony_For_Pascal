// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsCase : AbstractInstruccion
    {

        // Atributos

        // Expression 
        public readonly AbstractExpression Expression_;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> CaseList = new LinkedList<AbstractInstruccion>();

        // Token Line 
        public readonly int TokenLine;

        // Token Column 
        public readonly int TokenColumn;

        // Constructor 
        public InsCase(AbstractExpression Expression_, LinkedList<AbstractInstruccion> CaseList, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.Expression_ = Expression_;
            this.CaseList = CaseList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Bool Auxiliar
            bool TypeFlag = false;

            // Nuevo Entorno 
            EnviromentTable CaseEnv = new EnviromentTable(Env, "Env_Case");

            // Verificar Si ESta Nullo
            if (this.CaseList != null) 
            {

                // Verificar Los Tipos De Datos 
                foreach (Cases AuxCase in this.CaseList)
                {

                    // Verificar Si No Esta Nullo
                    if (AuxCase.Expression_ != null && this.Expression_ != null)
                    {

                        // Ejectuar 
                        ObjectReturn CaseExp = AuxCase.Expression_.Execute(CaseEnv);
                        ObjectReturn SwitchExp = this.Expression_.Execute(CaseEnv);

                        // Verificar Tipo 
                        if(!SwitchExp.Type.Equals(CaseExp.Type))
                        {

                            TypeFlag = true;

                        }

                    }

                }
                
                // Marcar Error 
                if (TypeFlag == true)
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Expresion En El Case No Es Compatible Con La Expression De Uno O Mas Casos", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }
                else
                {

                    // Recorrer Cases 
                    foreach (Cases AuxCase in this.CaseList)
                    {

                        // Verificar Que No Esa Nullo
                        if (AuxCase != null)
                        {

                            // Verificar Si No Esta Nullo
                            if(AuxCase.Expression_ != null && this.Expression_ != null)
                            {

                                // Ejectuar 
                                ObjectReturn CaseExp = AuxCase.Expression_.Execute(CaseEnv);
                                ObjectReturn SwitchExp = this.Expression_.Execute(CaseEnv);
                               
                                // VErificar Que No SEan Nullos
                                if (CaseExp != null && SwitchExp != null)
                                {
                                   
                                    // Verificar Si La Condicion Es Igual
                                    if(CaseExp.Value.ToString().Equals(SwitchExp.Value.ToString()))
                                    {

                                        // Ejecutar Instrucciones 
                                        ObjectReturn ObjectTransfer = (ObjectReturn) AuxCase.Execute(CaseEnv);

                                        // Verificar SI ESta DIferente De Nullo
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
                                            else if (ObjectTransfer.Option.Equals("return"))
                                            {

                                                // Verificar Si Hay Ciclos 
                                                bool Flag = CaseEnv.SearchFuncs();

                                                // Verificar 
                                                if(Flag)
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

                                        break;

                                    }

                                }

                            }
                            else if (AuxCase.Expression_ == null && this.Expression_ != null)
                            {

                                ObjectReturn SwitchExp = this.Expression_.Execute(CaseEnv);

                                // VErificar Que No SEan Nullos
                                if (SwitchExp != null)
                                {

                                    //Ejecutar Instrucciones 
                                    AuxCase.Execute(CaseEnv);

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

            // Crear Entorno 
            EnviromentTable CaseEnv = new EnviromentTable(Env, "Case_Env");

            // Verificar SI No Es Nullo
            if(this.Expression_ != null) 
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "case ";

                // Ejcutar Traduccion
                this.Expression_.Translate(CaseEnv);

                // Agregar Traduccion
                VariablesMethods.TranslateString +=  " of\n";

                // Agregar A Pila 
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verifidcar Si ESta Nullo
                if(this.CaseList != null) 
                {

                    // Traducir Cases
                    foreach (AbstractInstruccion Instruccion in this.CaseList)
                    {

                        // Verifidar Si Esta Nullo
                        if (Instruccion != null)
                        {

                            // Agregar Traducion
                            Instruccion.Translate(CaseEnv);

                        }


                    }

                }

                // Pop A Pila 
                VariablesMethods.AuxiliaryPile.Pop();

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            }

            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Contador Auxiliar 
            int AuxiliaryCounter = 1;

            // Auxiliares 
            String CommentAuxiliary = "Uno";
            String InsAuxiliary = "Dos";

            // Nuevo Entorno 
            EnviromentTable CaseEnv = new EnviromentTable(Env, "Env_Case");

            // Crear Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Agrear Comentario 
            Instance_1.AddCommentOneLine("Comienzo Instrucción Switch Case", CommentAuxiliary);

            // Crear Etiquetas 
            String LabelCaseInicio = Instance_1.CreateLabel();
            String LabelCaseFinal = Instance_1.CreateLabel();

            // Agregar A Entorno 
            CaseEnv.ContinueLabel = LabelCaseInicio;
            CaseEnv.BreakLabel = LabelCaseFinal;

            // Agregar Etiqueta De Inicio 
            Instance_1.AddLabel(LabelCaseInicio, InsAuxiliary);

            // Agregar Identacion 
            Instance_1.AddIdent();

            // Verificar Si ESta Nullo
            if(this.CaseList != null)
            {
                // Recorrer Cases 
                foreach (Cases AuxCase in this.CaseList)
                {

                    // Verificar Que No Esa Nullo
                    if (AuxCase != null)
                    {

                        // Verificar Si No Esta Nullo
                        if(AuxCase.Expression_ != null && this.Expression_ != null)
                        {

                            // Crear Etiquetas 
                            String LabelInicioCase = Instance_1.CreateLabel();
                            String LabelFinalCase = Instance_1.CreateLabel();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Case No." + AuxiliaryCounter + "\n", CommentAuxiliary);

                            // Obtener Expresiones
                            ObjectReturn SwitchExp = this.Expression_.Compilate(Env);
                            ObjectReturn CaseExp = AuxCase.Expression_.Compilate(CaseEnv);

                            // Verificar Tipo
                            if(SwitchExp.Type.Equals("integer") || SwitchExp.Type.Equals("real"))
                            {

                                // Agregar Comentario
                                Instance_1.AddCommentOneLine("Validación Expression Relacional", CommentAuxiliary);

                                // Agregar If De Salto 
                                Instance_1.AddConditionalJump(SwitchExp.GetValue(), "==", CaseExp.GetValue(), LabelInicioCase, InsAuxiliary);

                                // Agregar Identacion
                                Instance_1.AddIdent();

                                // Agregar Salto No Condicional
                                Instance_1.AddNonConditionalJump(LabelFinalCase, InsAuxiliary);

                                // Quitar Identacion
                                Instance_1.DeleteIdent();

                            }
                            else if (SwitchExp.Type.Equals("string"))
                            {

                                // Agregar Comentario
                                Instance_1.AddCommentOneLine("Obtener Posicion Inicial De Los Strings", CommentAuxiliary);

                                // Obtener Strings 
                                Instance_1.AddOneExpression("T1", SwitchExp.GetValue().ToString(), InsAuxiliary);
                                Instance_1.AddOneExpression("T2", CaseExp.GetValue().ToString(), InsAuxiliary);

                                // Agregar Comentario 
                                Instance_1.AddCommentOneLine("Llamada A Función", CommentAuxiliary);

                                // Agregar Llamada A Metodo 
                                Instance_1.AddFunctionCall("compare_string", InsAuxiliary);

                                // Agregar Comentario
                                Instance_1.AddCommentOneLine("Validación Expression Relacional", CommentAuxiliary);

                                // Agregar If De Salto 
                                Instance_1.AddConditionalJump("T4", "==", "1", LabelInicioCase, InsAuxiliary);

                                // Agregar Identacion
                                Instance_1.AddIdent();

                                // Agregar Salto No Condicional
                                Instance_1.AddNonConditionalJump(LabelFinalCase, InsAuxiliary);

                                // Quitar Identacion
                                Instance_1.DeleteIdent();

                            }

                            // Agregar Etiqueta 
                            Instance_1.AddLabel(LabelInicioCase, InsAuxiliary);

                            // Agregar Identacion 
                            Instance_1.AddIdent();
                            
                            // Ejectuar 
                            // AuxCase.Expression_.Compilate(CaseEnv);

                            // Ejecutar Instrucciones 
                            AuxCase.Compilate(CaseEnv);

                            // Agregar Comentario
                            Instance_1.AddCommentOneLine("Instrucción Break", CommentAuxiliary);

                            // Agregar Break 
                            Instance_1.AddNonConditionalJump(LabelCaseFinal, InsAuxiliary);
                            
                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Agregar Label 
                            Instance_1.AddLabel(LabelFinalCase, InsAuxiliary);

                            // Agregar Identacion 
                            Instance_1.AddIdent();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Fin Case No." + AuxiliaryCounter + "\n", CommentAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Aumentar Contador 
                            AuxiliaryCounter += 1;

                        }
                        else if (AuxCase.Expression_ == null && this.Expression_ != null)
                        {

                            // Crear Etiquetas 
                            String LabelInicioDefault = Instance_1.CreateLabel();
                            String LabelFinalDefault = Instance_1.CreateLabel();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Default", CommentAuxiliary);

                            // Agregar Etiqueta 
                            Instance_1.AddLabel(LabelInicioDefault, InsAuxiliary);

                            // Agregar Identacion 
                            Instance_1.AddIdent();

                            // Ejecutar Instrucciones 
                            AuxCase.Compilate(CaseEnv);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Agregar Label 
                            Instance_1.AddLabel(LabelFinalDefault, InsAuxiliary);

                            // Agregar Identacion 
                            Instance_1.AddIdent();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Fin Default \n", CommentAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                        }

                    }

                }

            }

            // Quitar Identacion 
            Instance_1.DeleteIdent();

            // Agregar Etiqueta De Inicio 
            Instance_1.AddLabel(LabelCaseFinal, InsAuxiliary);

            // Añadir Identacion 
            Instance_1.AddIdent();

            // Agregar Comentario 
            Instance_1.AddCommentOneLine("Fin Instrucción Switch Case\n", CommentAuxiliary);

            // Eliminar Identacion 
            Instance_1.DeleteIdent();

            // Retornar 
            return null;

        }

    }

}