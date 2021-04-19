// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class InsElse : AbstractInstruccion
    {

        // Atributos 

        // Tipo Else 
        public readonly String ElseType;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList = new LinkedList<AbstractInstruccion>();

        // Instruccion If 
        public readonly AbstractInstruccion InsIf;

        // TOken LIne 
        public readonly int TokenLine;

        // Token COlumn
        public readonly int TokenColumn;

        // Constructor 
        public InsElse(String ElseType, LinkedList<AbstractInstruccion> InstruccionsList, AbstractInstruccion InsIf, int TokenLine, int TokenColumn) 
        {
        
            // Inicializar Valores 
            this.ElseType = ElseType;
            this.InstruccionsList = InstruccionsList;
            this.InsIf = InsIf;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        } 

        public override object Execute(EnviromentTable Env)
        {

            // Verificar Tipo Else 
            if (this.ElseType.Equals("ElseIf"))
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseIfEnv = new EnviromentTable(Env, "Env_ElseIf");

                // Verificar Si Instruccion No Es Nulla
                if(InsIf != null)
                {

                    // Ejecutar Else If 
                    InsIf.Execute(ElseIfEnv);

                }

            }
            else if (this.ElseType.Equals("Else")) 
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseEnv = new EnviromentTable(Env, "Env_Else");

                // Verificar Si Instruccion No Es Nulla
                if(InstruccionsList != null) 
                {

                    // Recorrer Lista 
                    foreach(AbstractInstruccion Instruccion in this.InstruccionsList) 
                    {

                        // Veriricar Si ESt aNullo
                        if (Instruccion != null) 
                        {

                            // Ejecutar 
                            ObjectReturn ObjectTransfer = (ObjectReturn) Instruccion.Execute(ElseEnv);

                            // Verificar Si Esta Nullo
                            if (ObjectTransfer != null) 
                            {

                                // Verificar TIpo 
                                if (ObjectTransfer.Option.Equals("return"))
                                {

                                    // Verificar Si Hay Ciclos 
                                    bool Flag = ElseEnv.SearchFuncs();

                                    // Verificar 
                                    if (Flag)
                                    {

                                        // Retornar Valor 
                                        return ObjectTransfer;

                                    }
                                    else
                                    {

                                        // Agregar Error 
                                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Return Tiene Que Aparecer Dentro De Una Funcion", this.TokenLine, this.TokenColumn));

                                        // Aumentar Contador
                                        VariablesMethods.AuxiliaryCounter += 1;


                                    }

                                }
                                else
                                {

                                    // Verificar Si Hay Ciclos 
                                    bool Flag = ElseEnv.SearchCycles();

                                    // Verificar 
                                    if (Flag)
                                    {

                                        // Retornar Valor 
                                        return ObjectTransfer;

                                    }
                                    else
                                    {

                                        // Agregar Error 
                                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Break O Continue Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                        // Aumentar Contador
                                        VariablesMethods.AuxiliaryCounter += 1;


                                    }

                                }

                            }

                        }
                    
                    }
                
                }

            }

            // Retonra 
            return null;

        }

        public override object Translate(EnviromentTable Env)
        {

            // Verificar Tipo Else 
            if(this.ElseType.Equals("ElseIf"))
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseIfEnv = new EnviromentTable(Env, "Env_ElseIf");

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "else ";

                // Verificar Si Instruccion No Es Nulla
                if (InsIf != null)
                {

                    // Ejecutar Else If 
                    InsIf.Translate(ElseIfEnv);

                }

            }
            else if(this.ElseType.Equals("Else"))
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseEnv = new EnviromentTable(Env, "Env_Else");

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "else \n";
                VariablesMethods.TranslateString += VariablesMethods.Ident() + "begin\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verificar Si Instruccion No Es Nulla
                if (InstruccionsList != null)
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        // Verificar Si Esta Nullo
                        if (Instruccion != null) 
                        {

                            // Ejecutar 
                            Instruccion.Translate(ElseEnv);

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

            }
            else 
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += ";\n";

            }

            // Retonra 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Verificar Tipo Else 
            if(this.ElseType.Equals("ElseIf"))
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseIfEnv = new EnviromentTable(Env, "Env_ElseIf");

                // Verificar Si Instruccion No Es Nulla
                if (InsIf != null)
                {

                    // Crear Etiqueta 
                    String TemporaryAux = Instance_1.CreateLabel();

                    // Agregar Salto 
                    Instance_1.AddNonConditionalJump(TemporaryAux);

                    // Agregar Label 
                    Instance_1.AddLabel(this.BoolAux);

                    // Agregar Identacion 
                    Instance_1.AddIdent();
                    
                    // AGregar Comentario 
                    Instance_1.AddCommentOneLine("Else If");

                    // Ejecutar Else If 
                    InsIf.Compilate(ElseIfEnv);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Añadir Label
                    Instance_1.AddLabel(TemporaryAux);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Fin Else If \n");

                    // Eliminar IDentacion
                    Instance_1.DeleteIdent();

                }

            }
            else if (this.ElseType.Equals("Else"))
            {

                // Crear Nuevo Entorno 
                EnviromentTable ElseEnv = new EnviromentTable(Env, "Env_Else");

                // Crear Etiqueta 
                String TemporaryAux = Instance_1.CreateLabel();

                // Agregar Salto 
                Instance_1.AddNonConditionalJump(TemporaryAux);

                // Agregar Label 
                Instance_1.AddLabel(this.BoolAux);

                // Agregar Identacion 
                Instance_1.AddIdent();

                // Verificar Si Instruccion No Es Nulla
                if (InstruccionsList != null)
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        // Veriricar Si ESt aNullo
                        if (Instruccion != null)
                        {

                            // Compilar Instruccion
                            Instruccion.Compilate(ElseEnv);
                                                        
                        }

                    }


                }

                // Eliminar Identacion 
                Instance_1.DeleteIdent();
                 
                // Añadir Label
                Instance_1.AddLabel(TemporaryAux);

                // Agregar Identacion
                Instance_1.AddIdent();

                // Agregar Comentario 
                Instance_1.AddCommentOneLine("Fin Else \n");

                // Eliminar IDentacion
                Instance_1.DeleteIdent();

            }
            else
            {

                // Agregar Salida 
                Instance_1.AddLabel(this.BoolAux);

                // Agregar Identacion
                Instance_1.AddIdent();

                // Agregar Comentario 
                Instance_1.AddCommentOneLine("Fin If \n");

                // Eliminar IDentacion
                Instance_1.DeleteIdent();

            }

            // Retonra 
            return null;

        }

    }

}