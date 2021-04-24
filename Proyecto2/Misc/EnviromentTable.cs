// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System.Collections.Generic;
using System;
using Proyecto2.TranslatorAndInterpreter;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{
    
    // Clase Enviroment     
    class EnviromentTable
    {

        // Atributos

        // Entorno Padre 
        public EnviromentTable ParentEnviroment;

        // Lista De Variables Primitivas
        public Dictionary<String, SymbolTable> PrimitiveVariables;

        // Lista De Funcions 
        public Dictionary<String, FunctionTable> Functions; 

        // Nombre Del Ambiente Actual
        public String EnviromentName;

        // Tamaño Del Ambiente 
        public int EnviromentSize;

        // Etiqueta Break 
        public String BreakLabel;

        // Etiqueta Continue 
        public String ContinueLabel;

        // Constructor 
        public EnviromentTable(EnviromentTable ParentEnviroment, String EnviromentName) 
        {

            // Inicializar Valores 
            this.ParentEnviroment = ParentEnviroment;
            this.PrimitiveVariables = new Dictionary<String, SymbolTable>();
            this.Functions = new Dictionary<String, FunctionTable>();
            this.EnviromentName = EnviromentName;
            this.EnviromentSize = 0;
            this.BreakLabel = "";
            this.ContinueLabel = "";

            // Agregar Entorno A Lista 
            VariablesMethods.EnviromentList.AddLast(this);
        
        }

        // Agregar Variable A Tabla De Simbolos
        public bool AddVariable(String Identifier, String Type, object Value, String DecType, String Env, int Line, int Column) {

            // Variables
            bool Variables = true;
            bool Functions = true;

            // Verificar si La Variable Existe En El Ambito
            if (this.PrimitiveVariables.ContainsKey(Identifier.ToLower()))
            {

                // Ya Existe 
                Variables = false;

            }

            // Verificar si La Funcion Existe En El Ambito
            if (this.Functions.ContainsKey(Identifier.ToLower()))
            {

                // Ya Existe 
                Functions = false;

            }

            // Verificar 
            if(Variables && Functions)
            {

                // Agregar Variable A Lista De Simbolos
                this.PrimitiveVariables.Add(Identifier.ToLower(), new SymbolTable(Identifier, Type, Value, DecType, Env, Line, Column));

                // Agregada Con Exito
                return true;
            
            }

            // return false
            return false;
            
        }

        // Agregar Variable A Tabla De Simbolos
        public SymbolTable AddVariableStack(String Identifier, String Type, String DecType, String Env, int Line, int Column)
        {

            // Variables
            bool Variables = true;
            bool Functions = true;

            // Verificar si La Variable Existe En El Ambito
            if (this.PrimitiveVariables.ContainsKey(Identifier.ToLower()))
            {

                // Ya Existe 
                Variables = false;

            }

            // Verificar si La Funcion Existe En El Ambito
            if (this.Functions.ContainsKey(Identifier.ToLower()))
            {

                // Ya Existe 
                Functions = false;

            }

            // Verificar 
            if (Variables && Functions)
            {

                // Obtener Posicion 
                int PositionStack = this.EnviromentSize;

                // Incremtnar Tamaña
                this.EnviromentSize += 1;

                // Crear Varaible 
                SymbolTable NewVariable = new SymbolTable(Identifier, Type, PositionStack, DecType, Env, Line, Column);

                // Agregar Variable A Lista De Simbolos
                this.PrimitiveVariables.Add(Identifier.ToLower(), NewVariable);

                // Retornar 
                return NewVariable;

            }

            // Retornar
            return null;

        }

        // Obtener Variable De Tabla De Simbolos
        public SymbolTable GetVariable(String VarName) 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Buscar Variable 
                if(ActualEnv.PrimitiveVariables.ContainsKey(VarName.ToLower())) 
                {

                    // Retornar Variable 
                    return ActualEnv.PrimitiveVariables[VarName.ToLower()];
                
                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }

            // Retornar Null
            return null;
        
        }

        // Setear Variable De Tabla De Simbolos
        public void SetVariable(String VarName, SymbolTable ActualVar) 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Buscar Variable 
                if(ActualEnv.PrimitiveVariables.ContainsKey(VarName.ToLower())) 
                {

                    // Agregar Variable 
                    ActualEnv.PrimitiveVariables[VarName.ToLower()] = ActualVar;
                
                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }
        
        }

        // Añadir Funcion
        public bool AddFunction(String FuncType, String Identifier, String ReturnType, LinkedList<ObjectReturn> ParamsList, LinkedList<AbstractInstruccion> DeclarationsList, LinkedList<AbstractInstruccion> InstruccionsList, String EnvName, int TokenLine, int TokenColumn, EnviromentTable Env) 
        {

            // Variables
            bool Variables = true;
            bool Functions = true;
            
            // Verificar si La Variable Existe En El Ambito
            if(this.PrimitiveVariables.ContainsKey(Identifier.ToLower()))
            {
               
                // Ya Existe 
                Variables = false;

            }

            // Verificar si La Funcion Existe En El Ambito
            if(this.Functions.ContainsKey(Identifier.ToLower()))
            {
               
                // Ya Existe 
                Functions = false;

            }

            // Verificar 
            if (Variables && Functions)
            {

                // Agregar Variable A Lista De Simbolos
                this.Functions.Add(Identifier.ToLower(), new FunctionTable(FuncType, Identifier, ReturnType, ParamsList, DeclarationsList, InstruccionsList, EnvName, TokenLine, TokenColumn, Env));

                // Agregada Con Exito
                return true;

            }

            // Retornar 
            return false;

        }

        // Obtener Variable De Tabla De Simbolos
        public FunctionTable GetFunction(String FuncName)
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while (ActualEnv != null)
            {

                // Buscar Variable 
                if (ActualEnv.Functions.ContainsKey(FuncName.ToLower()))
                {

                    // Retornar Variable 
                    return ActualEnv.Functions[FuncName.ToLower()];

                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;

            }

            // Retornar Null
            return null;

        }

        // Buscar Ciclos 
        public bool SearchCycles() 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while (ActualEnv != null)
            {

                // Verificar Si COntiene Nombre De Ciclos 
                if (ActualEnv.EnviromentName.Contains("While") || ActualEnv.EnviromentName.Contains("Repeat") || ActualEnv.EnviromentName.Contains("For")) 
                {

                    // Retornar 
                    return true;
                
                }


                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;

            }

            // Retornar Null
            return false;

        }

        // Buscar Etiqueta Break And Continue Cycles
        public String[] SearchBreakAndContinueCycles()
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Array Auxiliar
            String[] AuxiliaryArray = { "", "" };

            // Recorrer Entornos
            while (ActualEnv != null)
            {

                // Verificar Si COntiene Nombre De Ciclos 
                if (ActualEnv.EnviromentName.Contains("While") || ActualEnv.EnviromentName.Contains("Repeat") || ActualEnv.EnviromentName.Contains("For"))
                {

                    // Obtener Valores
                    AuxiliaryArray[0] = ActualEnv.BreakLabel;
                    AuxiliaryArray[1] = ActualEnv.ContinueLabel;

                    // Retornar 
                    return AuxiliaryArray;

                }


                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;

            }

            // Retornar Null
            return AuxiliaryArray;

        }

        // Buscar Ciclos 
        public bool SearchFuncs()
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while (ActualEnv != null)
            {

                // Verificar Si COntiene Nombre De Ciclos 
                if (ActualEnv.EnviromentName.Contains("Func"))
                {

                    // Retornar 
                    return true;

                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;

            }

            // Retornar Null
            return false;

        }

        // Graficar Tabla De Simbolos
        public LinkedList<EnviromentTable> GraphSymbolTable() 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Lista Auxiliar 
            LinkedList<EnviromentTable> AuxiliaryList = new LinkedList<EnviromentTable>();

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Agregar Entorno A Lista 
                AuxiliaryList.AddFirst(ActualEnv);

                // Recorrer Entorno
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }

            // Retorno Lista 
            return AuxiliaryList;
        
        }

    }

}