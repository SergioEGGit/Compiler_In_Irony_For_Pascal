// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------

namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class FunctionsDeclaration : AbstractInstruccion
    {

        // Atributos 

        // Tipo Funcion 
        private readonly String TypeFunc;

        // Tipo Returno 
        private readonly String ReturnType;

        // Identificaro 
        private readonly String Identifier;

        // LIsta De Parametros 
        private readonly LinkedList<ObjectReturn> ParamsList;

        // Lista De Declaraciones 
        private readonly LinkedList<AbstractInstruccion> DeclarationsList;

        // Lista De Instrucciones 
        private readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Linea 
        private readonly int TokenLine;

        // Columna 
        private readonly int TokenColumn;

        // Constructor , 
        public FunctionsDeclaration(String TypeFunc, String Identifier, String ReturnType, LinkedList<ObjectReturn> ParamsList, LinkedList<AbstractInstruccion> DeclarationsList, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn) 
        {

            // Inicializar Valores
            this.TypeFunc = TypeFunc;
            this.Identifier = Identifier;
            this.ReturnType = ReturnType;
            this.ParamsList = ParamsList;
            this.DeclarationsList = DeclarationsList;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Que No Exista La Funcion
            bool Flag = Env.AddFunction(this.TypeFunc, this.Identifier, this.ReturnType, ParamsList, DeclarationsList, InstruccionsList, Env.EnviromentName, this.TokenLine, this.TokenColumn, Env);

            // Verificar SI Se Agrego 
            if(!Flag) 
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion (" + this.Identifier + ") Ya Existe En El Contexto Actual O Existe Una Variable/Constante Con El Mismo Nombre", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Verifficar TIpo Funcion 
            if(this.TypeFunc.Equals("Function")) 
            {

                // Agregar A Traduccion 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "function " + this.Identifier + "(";

                // Verificar Si Esta Nullo
                if(this.ParamsList != null) 
                {

                    // REcorrer LIsta 
                    foreach(ObjectReturn Param in this.ParamsList) 
                    {

                        // Verificar Si No ES Nullo
                        if(Param != null) 
                        {

                            // Agregar Traduccion
                            VariablesMethods.TranslateString += Param.Value.ToString() + " : " + Param.Type;

                            // Verificar End 
                            if (Param.End.Equals("End")) 
                            {

                                // Agregar Traduccion
                                VariablesMethods.TranslateString += "; ";
                            
                            }
                        
                        }
                    
                    }
                
                }

                // Agregar Parentesis 
                VariablesMethods.TranslateString += ") : " + this.ReturnType + ";\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Nuevo Ambito 
                EnviromentTable FuncEnv = new EnviromentTable(Env, "Func_" + this.Identifier);

                // Verificar Si Hay Declaraciones
                if(this.DeclarationsList != null) 
                {

                    // Recorrer Lista 
                    foreach(AbstractInstruccion Declaration in this.DeclarationsList) 
                    {

                        // Verifica Si Es Nullo
                        if(Declaration != null) 
                        {

                            // Traducir 
                            Declaration.Translate(FuncEnv);     
                        
                        }
                        
                    
                    }
                
                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Bloque 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "begin\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verificar Lista De Instrucciones 
                if(this.InstruccionsList != null) 
                {

                    // Recorrer Lista 
                    foreach(AbstractInstruccion Instruccion in this.InstruccionsList) 
                    {

                        // Veriicar Si Es Nullo
                        if (Instruccion != null) 
                        {

                            // Traducir 
                            Instruccion.Translate(FuncEnv);
                        
                        }
                    
                    }
                
                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Bloque 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            }
            else if (this.TypeFunc.Equals("Procedure"))
            {

                // Agregar A Traduccion 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "procedure " + this.Identifier + "(";

                // Verificar Si Esta Nullo
                if (this.ParamsList != null)
                {

                    // REcorrer LIsta 
                    foreach (ObjectReturn Param in this.ParamsList)
                    {

                        // Verificar Si No ES Nullo
                        if (Param != null)
                        {

                            // Agregar Traduccion
                            VariablesMethods.TranslateString += Param.Value.ToString() + " : " + Param.Type;

                            // Verificar End 
                            if (Param.End.Equals("End"))
                            {

                                // Agregar Traduccion
                                VariablesMethods.TranslateString += "; ";

                            }

                        }

                    }

                }

                // Agregar Parentesis 
                VariablesMethods.TranslateString += ");\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Nuevo Ambito 
                EnviromentTable ProcEnv = new EnviromentTable(Env, "Proc_" + this.Identifier);

                // Verificar Si Hay Declaraciones
                if (this.DeclarationsList != null)
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Declaration in this.DeclarationsList)
                    {

                        // Verifica Si Es Nullo
                        if (Declaration != null)
                        {

                            // Traducir 
                            Declaration.Translate(ProcEnv);

                        }


                    }

                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Bloque 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "begin\n";

                // Agregar A Pila
                VariablesMethods.AuxiliaryPile.Push("_");

                // Verificar Lista De Instrucciones 
                if (this.InstruccionsList != null)
                {

                    // Recorrer Lista 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        // Veriicar Si Es Nullo
                        if (Instruccion != null)
                        {

                            // Traducir 
                            Instruccion.Translate(ProcEnv);

                        }

                    }

                }

                // Pop A Pila
                VariablesMethods.AuxiliaryPile.Pop();

                // Bloque 
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

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