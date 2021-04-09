// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class InsIf : AbstractInstruccion
    {

        // Expression 
        public readonly AbstractExpression Expression_;

        // Lista De Instrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList = new LinkedList<AbstractInstruccion>();

        // Instruccion Else 
        public readonly AbstractInstruccion InsElse;

        // Token Line 
        public readonly int TokenLine;

        // Token Column 
        public readonly int TokenColumn;

        // Constructor 
        public InsIf(AbstractExpression Expression_, LinkedList<AbstractInstruccion> InstruccionsList, AbstractInstruccion InsElse, int TokenLine, int TokenColumn) 
        {

            // Inicializar Valores 
            this.Expression_ = Expression_;
            this.InstruccionsList = InstruccionsList;
            this.InsElse = InsElse;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Crear Nuevo Entorno 
            EnviromentTable IfEnv = new EnviromentTable(Env, "Env_If");

            // Verificar La Expression
            ObjectReturn ElseExp = this.Expression_.Execute(IfEnv);

            // Verificar Si Hay Error Semantico 
            if (ElseExp.Type.Equals("boolean"))
            {

                if(bool.Parse(ElseExp.Value.ToString()))
                {

                    // Verificar Si Hay Instrucciones 
                    if (this.InstruccionsList != null) 
                    {

                        // Recorrer Lista De Instrucciones 
                        foreach(AbstractInstruccion Instruccion in this.InstruccionsList)
                        {

                            // Verificar Si Es Nulloo
                            if (Instruccion != null) 
                            {

                                // Ejecutar Instruccion 
                                ObjectReturn ObjectTransfer = (ObjectReturn) Instruccion.Execute(IfEnv);
                                
                                // Verificar Si Se Retornar
                                if(ObjectTransfer != null) 
                                {

                                    // Verificar TIpo 
                                    if (ObjectTransfer.Option.Equals("return"))
                                    {

                                        // Verificar Si Hay Ciclos 
                                        bool Flag = IfEnv.SearchFuncs();

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
                                    else
                                    {

                                        // Verificar Si Hay Ciclos 
                                        bool Flag = IfEnv.SearchCycles();

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
                else 
                {

                    // Verificar Si ESta Nullo
                    if (InsElse != null)
                    { 

                        // Ejectar Instruccion Else 
                        this.InsElse.Execute(Env);

                    }
                
                }

            }
            else 
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expresion Dentro De Un If Tiene Que Ser De Tipo Boolean", this.TokenLine, this.TokenColumn));

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
            EnviromentTable IfEnv = new EnviromentTable(Env, "Env_If");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "if ";
            
            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(IfEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += " then";

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "begin\n";

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            // Recorrer Instrucciones 
            if(this.InstruccionsList != null) 
            {

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Verificar Si ESta Nulla
                    if(Instruccion != null) 
                    {

                        // Ejecutar Instruccion 
                        Instruccion.Translate(IfEnv);

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
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end";

            // Veridicar Si ESt a Nulla
            if (InsElse != null) 
            {

                // Metodo Traducir Else 
                this.InsElse.Translate(Env);

            }

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