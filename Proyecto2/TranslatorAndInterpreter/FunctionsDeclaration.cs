// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        private String Identifier;

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

        // Esta Precompilador
        private bool IsCompile;

        // Constructor  
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
            bool Flag = Env.AddFunction(this.TypeFunc, this.Identifier, this.ReturnType, this.ParamsList, this.DeclarationsList, this.InstruccionsList, Env.EnviromentName, this.TokenLine, this.TokenColumn, Env);

            // Verificar SI Se Agrego 
            if(!Flag) 
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion (" + this.Identifier + ") Ya Existe En El Contexto Actual O Existe Una Variable/Constante Con El Mismo Nombre", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }

            // Crear Entorno 
            EnviromentTable Func_Env = new EnviromentTable(Env, "Env_Func_" + this.Identifier);

            // Setear Enviroment 
            Func_Env.SetFunctionEnv("");
            
            // Verificar Parametros 
            if (this.ParamsList != null)
            {

                // Tipo Auxiliar 
                ObjectReturn ValueParam = null;

                // Obtener Parametros 
                foreach (ObjectReturn Param in this.ParamsList)
                {

                    // Split No1 
                    String[] Split1 = Param.Value.ToString().Split(',');
                   
                    // Recorer Split 
                    foreach (String Var in Split1)
                    {
              
                        // Verificar Por Referencias 
                        String[] Split2 = Var.Split(' ');
                   
                        // Verificar Si Hay Dos 
                        if (Split2.Length == 2)
                        {

                            // Verificar Tipo 
                            if (Param.Type.ToString().Equals("integer"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("0", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("real"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("0.0", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("boolean"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("false", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("string"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("", Param.Type.ToString());

                            }

                            // Agregar Varialbe 
                            bool Flag_1 = Func_Env.AddVariable(Split2[1], Param.Type.ToString(), ValueParam, "Param_Ref", Func_Env.EnviromentName, this.TokenLine, this.TokenColumn);
                         
                            // Verificar Si Existe 
                            if (!Flag_1)
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }

                        }
                        else
                        {

                            // Verificar Tipo 
                            if (Param.Type.ToString().Equals("integer"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("0", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("real"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("0.0", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("boolean"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("false", Param.Type.ToString());

                            }
                            else if (Param.Type.ToString().Equals("string"))
                            {

                                // Asignar Tipo
                                ValueParam = new ObjectReturn("", Param.Type.ToString());

                            }

                            // Agregar Varialbe 
                            bool Flag_1 = Func_Env.AddVariable(Split2[0], Param.Type.ToString(), ValueParam, "Param", Func_Env.EnviromentName, this.TokenLine, this.TokenColumn);
                          
                            // Verificar Si Existe 
                            if (!Flag_1)
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                                // Terminar 
                                break;

                            }

                        }

                    }

                }

            }

            // Verificar Declaracioens 
            if(this.DeclarationsList != null)
            {

                // Compilar Declaraciones
                foreach (AbstractInstruccion Declaration in this.DeclarationsList)
                {

                    // Compilar 
                    Declaration.Execute(Func_Env);

                }

            }

            // Verifivcar Instrucciones 
            if (this.InstruccionsList != null) 
            {

                // Compilar Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Compilar 
                    Instruccion.Execute(Func_Env);

                }

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

            // Verficar Si ESta Compilador 
            if (!IsCompile) 
            {

                // Ya ESta Comiplador
                this.IsCompile = true;

                // Verificar Parametros 
                LinkedList<String> ArrayAuxiliary = new LinkedList<String>();

                // Verificar Si Hay Parametros 
                if (this.ParamsList != null) 
                {

                    // Recorrer Lista De Parametros 
                    foreach (ObjectReturn Param in this.ParamsList)
                    {

                        // Split No1 
                        String[] Split1 = Param.Value.ToString().Split(',');

                        // Recorer Split 
                        foreach (String Var in Split1)
                        {

                            // Agregar A Array 
                            if (ArrayAuxiliary.Contains(Var))
                            {

                                // Agregar Error 
                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parámetro " + Param.Value.ToString() + " Ya Existe En La Función", this.TokenLine, this.TokenColumn));

                                // Aumentar Contador
                                VariablesMethods.AuxiliaryCounter += 1;

                            }

                            // Arregar A Array
                            ArrayAuxiliary.AddLast(Var);

                        }


                    }

                }

                // Verificar Si Esta Anidada 
                if(this.Anidate) 
                {

                    this.Identifier += "_" + Env.EnviromentName;
                
                }

                // Verificar Que No Exista La Funcion
                bool Flag = Env.AddFunctionStack(this.TypeFunc, this.Identifier, this.ReturnType, this.ParamsList, this.DeclarationsList, this.InstruccionsList, Env.EnviromentName, this.TokenLine, this.TokenColumn, Env, ArrayAuxiliary.Count);

                if (!Flag) 
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Función " + this.Identifier + " Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }

            // Verificar Que No Exista La Funcion
            FunctionTable ActualFunc = Env.GetFunctionStack(this.Identifier);

            // Verificar SI Se Agrego 
            if (ActualFunc != null)
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Entorno 
                EnviromentTable Func_Env = new EnviromentTable(Env, "Env_Func_" + this.Identifier)
                {

                    ActualFunction = ActualFunc

                };

                // Crear Etiqueta Retorno 
                String ReturnLabel = Instance_1.CreateLabel();

                // Almacenar Arreglo De Temporales 
                Dictionary<String, String> TemporaryArray = Instance_1.GetTemporaryArray();

                // Agregar Env 
                Func_Env.SetFunctionEnv(ReturnLabel);

                // Verificar Parametros 
                if (ActualFunc.ParamsList != null)
                {
                    
                    // Obtener Parametros 
                    foreach (ObjectReturn Param in ActualFunc.ParamsList)
                    {

                        // Split No1 
                        String[] Split1 = Param.Value.ToString().Split(',');

                        // Recorer Split 
                        foreach (String Var in Split1)
                        {

                            // Verificar Por Referencias 
                            String[] Split2 = Var.Split(' ');

                            // Verificar Si Hay Dos 
                            if (Split2.Length == 2)
                            {

                                // Agregar Varialbe 
                                SymbolTable IsVar = Func_Env.AddVariableStack(Split2[1], Param.Type.ToString(), "Param_Ref", Func_Env.EnviromentName, this.TokenLine, this.TokenColumn, false);
                             
                                // Verificar Si Existe 
                                if (IsVar == null)
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro " + Split2[1] + " Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                }

                            }
                            else
                            {
                                
                                // Agregar Varialbe 
                                SymbolTable IsVar = Func_Env.AddVariableStack(Split2[0], Param.Type.ToString(), "Param", Func_Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                                // Verificar Si Existe 
                                if (IsVar == null)
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro " + Split2[0] + " Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                    // Terminar 
                                    break;

                                }

                            }

                        }

                    }                    

                }

                // Auxiliary 
                List<AbstractInstruccion> AnidateFunctions = new List<AbstractInstruccion>();

                // Verificar Si Es Diferente De Null
                if (this.DeclarationsList != null) 
                {

                    // Compilar Declaraciones
                    foreach (AbstractInstruccion Declaration in this.DeclarationsList)
                    {

                        // Verificar Si Son Funciones 
                        if (typeof(FunctionsDeclaration).IsInstanceOfType(Declaration))
                        {

                            // Compilar 
                            AnidateFunctions.Add(Declaration);

                        }

                    }

                }

                // Verificar Si Esta Null
                if(AnidateFunctions != null) 
                {

                    // REcorrer Funciones Anidadas
                    foreach (AbstractInstruccion Function in AnidateFunctions)
                    {

                        // Marcar Como Anidada
                        Function.Anidate = true;
                        
                        // Compilar Funcion 
                        Function.Compilate(Func_Env);

                    }

                }

                // Limpiar Temporales 
                Instance_1.ResetTemporaryArray();

                // Agregar Inicio Funcion 
                Instance_1.AddFuncBegin(this.Identifier);

                // Agregar Identacion 
                Instance_1.AddIdent();

                // Verificar Lista De Delcaraciones 
                if(this.DeclarationsList != null) 
                {

                    // Compilar Declaraciones
                    foreach (AbstractInstruccion Declaration in this.DeclarationsList)
                    {

                        // Verificar Si Son Funciones 
                        if(!typeof(FunctionsDeclaration).IsInstanceOfType(Declaration))
                        {

                            // Compilar 
                            Declaration.Compilate(Func_Env);

                        }

                    }

                }

                // Verificar Lista De Instrucciones 
                if(this.InstruccionsList != null)
                {

                    // Compilar Instrucciones 
                    foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                    {

                        
                        // Compilar 
                        Instruccion.Compilate(Func_Env);

                    }

                }

                // Añadir Comentario 
                Instance_1.AddCommentOneLine("Fin De Función", "Uno");

                // Add Label Return 
                Instance_1.AddLabel(ReturnLabel, "Dos");

                // Añadir Identacion 
                Instance_1.AddIdent();

                // Añadir Comentario 
                Instance_1.AddCommentOneLine("Fin Funcion\n", "Dos");

                // Eliminar Identacion 
                Instance_1.DeleteIdent();
                Instance_1.DeleteIdent();

                // Fin Funcion 
                Instance_1.AddFuncEnd();

                // Setear Array TEmporales 
                Instance_1.SetTemporaryArray(TemporaryArray);                

            }

            // Retornar 
            return null;

        }

    }

}