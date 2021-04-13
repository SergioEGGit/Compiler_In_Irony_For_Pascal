// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class InsWrite : AbstractInstruccion
    {

        // Atributos 

        // Tipo De Write
        private readonly String WriteType;

        // Lista De Expressiones
        private readonly LinkedList<AbstractExpression> ExpressionList = new LinkedList<AbstractExpression>();

        // Tipo Traduccion
        private readonly String TranslateType;

        // Token Line 
        private readonly int TokenLine;

        // Token Column
        private readonly int TokenColumn;

        // Constructor 
        public InsWrite(String WriteType, LinkedList<AbstractExpression> ExpressionList, String TranslateType, int TokenLine, int TokenColumn) 
        {

            // Incializar Valores
            this.WriteType = WriteType;
            this.ExpressionList = ExpressionList;
            this.TranslateType = TranslateType;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Objecto Auxiliar 
            ObjectReturn AuxiliaryObject;

            // Verificar Tipo De Write 
            if(ExpressionList != null) 
            {

                // Recorrer Lista De Expressiones 
                foreach(AbstractExpression Expression in ExpressionList) 
                {

                    // Verificar Que No SEa Nullo
                    if(Expression != null) 
                    {

                        // Obtener Valor
                        AuxiliaryObject = Expression.Execute(Env);

                        // Verificar SI ES Diferetne De Nullo
                        if (AuxiliaryObject != null) 
                        {

                            // Obtener Valor Y Agregarlo A Consola
                            VariablesMethods.ExecuteString += AuxiliaryObject.Value.ToString();

                        }
                        else
                        {

                            // Agregar Error 
                            VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable Indicada No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                            // Aumentar Contador
                            VariablesMethods.AuxiliaryCounter += 1;

                        }

                    }
                
                }

                // Verificar Si Hay Que Agregar Salto De Linea
                if(this.WriteType.Equals("WriteLine")) 
                {

                    // Agregar Salto 
                    VariablesMethods.ExecuteString += "\n";
                
                }
            
            } 
            else
            {

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Salto 
                    VariablesMethods.ExecuteString += "\n";

                }

            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {
            
            // Objecto Auxiliar 
            ObjectReturn AuxiliaryObject;

            // Contador Auxiliar 
            int AuxiliaryCounter = 1;

            // Verificar Tipo De Write 
            if (ExpressionList != null)
            {

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Salto 
                    VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln(";

                }
                else 
                {

                    // Agregar Salto 
                    VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write(";

                }

                // Recorrer Lista De Expressiones 
                foreach (AbstractExpression Expression in ExpressionList)
                {

                    // Verificar Que no SEa Nullo
                    if(Expression != null) 
                    {

                        // Obtener Valor
                        AuxiliaryObject = Expression.Translate(Env);

                    }

                    if (AuxiliaryCounter < ExpressionList.Count) 
                    {

                        // Agregar Coma 
                        VariablesMethods.TranslateString += ", ";

                    }

                    // Aumentar Contador
                    AuxiliaryCounter += 1;

                }

                // Agregar Otro Parentesis
                VariablesMethods.TranslateString += ");\n";

            }
            else
            {

                // Verificar Si Es Write Line
                if(this.WriteType.Equals("WriteLine")) 
                {

                    // Verficar Tipo Traduccion
                    if (this.TranslateType.Equals("2"))
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln();\n";

                    }
                    else 
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "writeln;\n";

                    }

                }
                else
                {

                    // Verficar Tipo Traduccion
                    if (this.TranslateType.Equals("2"))
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write();\n";

                    }
                    else
                    {

                        // Agregar Traduccion
                        VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "write;\n";

                    }

                }

            }

            // Retornar 
            return null;
        
        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Objecto Auxiliar 
            ObjectReturn AuxiliaryObject;

            // Obtener Instnacia
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Verificar Tipo De Write 
            if(ExpressionList != null)
            {

                // Recorrer Lista De Expressiones 
                foreach(AbstractExpression Expression in ExpressionList)
                {

                    // Verificar Que No SEa Nullo
                    if(Expression != null)
                    {

                        // Obtener Valor
                        AuxiliaryObject = Expression.Compilate(Env);

                        // Añadir Comentario 
                        Instance_1.AddCommentOneLine("Método Print");

                        // Verificar SI ES Diferetne De Nullo
                        if (AuxiliaryObject != null)
                        {

                            // Verificar Tipo 
                            if (AuxiliaryObject.Type.Equals("integer"))
                            {

                                // Agregar Print 
                                Instance_1.AddPrintf("d", "(int) " + AuxiliaryObject.GetValue());

                            }
                            else if (AuxiliaryObject.Type.Equals("real"))
                            {

                                // Agregar Print 
                                Instance_1.AddPrintf("f", "(float) " + AuxiliaryObject.GetValue());

                            }
                            else if (AuxiliaryObject.Type.Equals("string")) 
                            {

                                // Agregar Expresion
                                Instance_1.AddOneExpression("T1", AuxiliaryObject.GetValue());

                                // Agregar Comentario 
                                Instance_1.AddCommentOneLine("Llamada Funcion Nativa (Imprimir String)");
                                
                                // Agregar Llamada A Funcion 
                                Instance_1.AddFunctionCall("print_string");
                            
                            }
                            else if(AuxiliaryObject.Type.Equals("boolean")) 
                            {

                                // Crear Label Salida 
                                String ExitLabel = Instance_1.CreateLabel();

                                // Agregar Lable True 
                                Instance_1.AddLabel(AuxiliaryObject.BoolTrue);

                                // Agregar Identacion 
                                Instance_1.AddIdent();

                                // Imprimir Bool 
                                Instance_1.PrintBool(true);

                                // Añadir Goto
                                Instance_1.AddNonConditionalJump(ExitLabel);

                                // Quitar Identacion
                                Instance_1.DeleteIdent();

                                // Agregar Lable True 
                                Instance_1.AddLabel(AuxiliaryObject.BoolFalse);

                                // Agregar Identacion 
                                Instance_1.AddIdent();

                                // Imprimir Bool 
                                Instance_1.PrintBool(false);

                                // Añadir Goto
                                Instance_1.AddNonConditionalJump(ExitLabel);

                                // Quitar Identacion
                                Instance_1.DeleteIdent();

                                // Agregar Etiquta Salida 
                                Instance_1.AddLabel(ExitLabel);

                                // Agregar Identacion 
                                Instance_1.AddIdent();

                                // Agregar Comentario 
                                Instance_1.AddCommentOneLine("Fin Imprimir Bool\n");

                                // Quitar Identacion 
                                Instance_1.DeleteIdent();

                            }

                        }

                    }

                }

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Print
                    Instance_1.AddPrintf("c", "(char) 10");

                }

            }
            else
            {

                // Verificar Si Hay Que Agregar Salto De Linea
                if (this.WriteType.Equals("WriteLine"))
                {

                    // Agregar Print
                    Instance_1.AddPrintf("c", "(char) 10");

                }

            }

            // Retornar 
            return null;

        }

    }

}