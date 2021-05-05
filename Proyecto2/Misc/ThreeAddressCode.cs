// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{

    // Clase Principal 
    class ThreeAddressCode
    {

        // Instancia 
        private static ThreeAddressCode ActualInstance = null;

        // Temporal Contador 
        private int TemporaryCounter;

        // Label Contador 
        private int LabelCounter;

        // String De Codigo 
        private String IntermediateCode;

        // Array De Temporales
        private Dictionary<String, String> TemporaryArray;

        // Identacion
        private String Ident;

        // Codigo De Declaraciones Globales 
        private String GlobalDeclarations;

        // Constructor 
        private ThreeAddressCode()
        {

            // Inicializar Valores
            TemporaryCounter = 4;
            LabelCounter = 0;
            IntermediateCode = "";
            TemporaryArray = new Dictionary<String, String>();
            Ident = "";
            GlobalDeclarations = "";

        }

        // Obtener Instancia
        public static ThreeAddressCode GetInstance
        {

            // Método Get
            get
            {

                // Verificar Si Existe La Instancia
                if (ActualInstance == null)
                {

                    // Nueva Instancia
                    ActualInstance = new ThreeAddressCode();

                }

                // Retornar 
                return ActualInstance;

            }

        }

        // Agregar Codigo 
        public void AddLineToIntermediateCode(String LineCode, String Type)
        {

            // Split 
            String[] Auxiliary = Type.Split(" ");

            if (Auxiliary.Length > 1)
            {

                // Verificar Tipo
                if (Auxiliary[0].Equals("Dos"))
                {

                    // Agregar Codigo A Variable 
                    GlobalDeclarations += Ident + LineCode + "\n\n";

                }
                else if (Auxiliary[0].Equals("Uno"))
                {

                    // Agregar Codigo A Variable 
                    GlobalDeclarations += Ident + LineCode + "\n";

                }
                else
                {

                    // Agregar Codigo A Variable 
                    GlobalDeclarations += Ident + LineCode;

                }

            }
            else
            {

                // Verificar Tipo
                if (Auxiliary[0].Equals("Dos"))
                {

                    // Agregar Codigo A Variable 
                    IntermediateCode += Ident + LineCode + "\n\n";

                }
                else if (Auxiliary[0].Equals("Uno"))
                {

                    // Agregar Codigo A Variable 
                    IntermediateCode += Ident + LineCode + "\n";

                }
                else
                {

                    // Agregar Codigo A Variable 
                    IntermediateCode += Ident + LineCode;

                }

            }

        }

        // Agregar Varibles Globales 
        public void AddGlobalVariables()
        {

            // Agregar Codigo 
            AddLineToIntermediateCode(GlobalDeclarations, "Sin");

        }

        // Size Global Variables 
        public int SizeGlobalDeclarations()
        {

            // Obtener Tamaño
            return this.GlobalDeclarations.Length;

        }

        // Obtener Codigo
        public String GetIntermediateCode()
        {

            // Retornar Codigo 
            return IntermediateCode;

        }

        // Limpiar Codigo
        public void ResetIntermediateCode()
        {

            // Inicializar Valores
            TemporaryCounter = 4;
            LabelCounter = 0;
            IntermediateCode = "";
            TemporaryArray = new Dictionary<String, String>();
            Ident = "";
            GlobalDeclarations = "";

        }

        // Agregar Cabecera 
        public String CreateHeader()
        {

            // Auxiliar Header 
            String AuxiliaryString = "// Librerias Y Declaraciones\n" +
                                     "#include <stdio.h> \n\n" +
                                     "float Heap[100000]; \n" +
                                     "float Stack[100000]; \n\n" +
                                     "float SP; \n" +
                                     "float HP; \n\n";

            // Verificar Si Hay Temporales 
            if (TemporaryCounter > 0)
            {

                // Agregar Tipo
                AuxiliaryString += "float ";

                // Recorrer Arreglo De Temporales
                for (int Counter = 1; Counter <= TemporaryCounter; Counter++)
                {

                    // Verificar Si Es El Final 
                    if (Counter == TemporaryCounter)
                    {

                        // Agregar Final 
                        AuxiliaryString += "T" + Counter + ";\n\n";

                    }
                    else
                    {

                        // Agregar Con Coma
                        AuxiliaryString += "T" + Counter + ", ";

                    }

                }

            }

            // Retornar 
            return AuxiliaryString;

        }

        // Crear Un Temporal 
        public String CreateTemporary()
        {

            // Aumentar Contador 
            TemporaryCounter += 1;

            // String Auxiliar 
            String AuxiliaryString = "T" + TemporaryCounter.ToString();

            // Agregar A Diccionario 
            TemporaryArray.Add(AuxiliaryString, AuxiliaryString);

            // Crear Temporal 
            return AuxiliaryString;

        }

        // Crear Un Label 
        public String CreateLabel()
        {

            // Aumentar Contador 
            LabelCounter += 1;

            // String Auxiliar 
            String AuxiliaryString = "L" + LabelCounter.ToString();

            // Crear Temporal 
            return AuxiliaryString;

        }

        // Agregar Label 
        public void AddLabel(String Label, String IdentType)
        {

            // Linea De codigo 
            String LineCode = Label + ":";

            // Agregar Linea 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Obtener Array De Temporales 
        public Dictionary<String, String> GetTemporaryArray()
        {

            // Retornar Temporal 
            return TemporaryArray;

        }

        // Agregar Temporal 
        public void AddTemporaryToArray(String Temporary)
        {

            // Verificar Si Existe Temporal 
            if (!TemporaryArray.ContainsKey(Temporary))
            {

                // Agregar A Array 
                TemporaryArray.Add(Temporary, Temporary);
            
            }
        
        }

        // Reemplazar Array De Temporales 
        public void SetTemporaryArray(Dictionary<String, String> ActualArray) 
        {

            // Setear Array De Temporales
            TemporaryArray = ActualArray;
        
        }

        // Limpiar Array De Temporales 
        public void ResetTemporaryArray() 
        {

            // LImpiar Arreglo
            TemporaryArray.Clear();
        
        }

        // Eliminar El Temporal
        public void DeleteTemporary(String Temporary) 
        {

            // Verificar Si Existe El Temporal 
            if(TemporaryArray.ContainsKey(Temporary)) 
            {

                // Eliminar El Temporal
                TemporaryArray.Remove(Temporary);

            }
        
        }

        // Crear Goto 
        public void AddNonConditionalJump(String Label, String IdentType) 
        {

            // String Auxiliar 
            String AuxiliaryString = "goto " + Label + ";";

            // Agregar Linea De Codigo
            AddLineToIntermediateCode(AuxiliaryString, IdentType);
        
        }

        // Crear If 
        public void AddConditionalJump(String LeftExp, String Operator, String RightExp, String Label, String IdentType)
        {

            // String Auxiliar 
            String AuxiliaryString = "if(" + LeftExp + " " + Operator  + " " + RightExp + ") goto " + Label + ";";

            // Agregar Linea A Codigo
            AddLineToIntermediateCode(AuxiliaryString, IdentType);

        }

        // Una Expression 
        public void AddOneExpression(String AssingExp, String Exp, String IdentType) 
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + Exp + ";";

            // Agregar A Codigo
            AddLineToIntermediateCode(AuxiliaryString, IdentType);

        }

        // Una Operacion
        public void AddTwoExpression(String AssingExp, String LeftExp, String Operator, String RightExp, String IdentType)
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + LeftExp + " " + Operator + " " + RightExp + ";";

            // Agregar Linea De Codigo 
            AddLineToIntermediateCode(AuxiliaryString, IdentType);

        }

        // Bloque Main Comienzo
        public void AddFuncBegin(String Identifier) 
        {

            // Crear Linea 
            String Begin = "void " + Identifier + "() {";

            // Agregar Linea A codigo
            AddLineToIntermediateCode(Begin, "Dos");

        }

        // Bloque Main Final
        public void AddFuncEnd()
        {

            // Crear Linea 
            String End = "\n    return;\n\n" +
                         "}\n";

            // Agregar Linea A codigo
            AddLineToIntermediateCode(End, "Dos");

        }

        // Agregar Identacion
        public void AddIdent() 
        {

            // Agregar Identacion
            Ident += "    ";
        
        }

        // Quitar Identacion
        public void DeleteIdent()
        {

            // Verificar Si Es Mayor Que 4
            if (Ident.Length >= 4) 
            {

                // Agregar Identacion
                Ident = Ident[0..^4];

            }

        }

        // Agregar Printf
        public void AddPrintf(String Format, String Value) 
        {

            // Crear PrintF
            String LineCode = "printf(\"%" + Format + "\", " + Value + ");";

            // Añadir Linea De Codigo
            AddLineToIntermediateCode(LineCode, "Dos");
         
        }

        // Añadir Comentario Unilinea 
        public void AddCommentOneLine(String Text, String IdentType) 
        {

            // Crear String Con Comentario 
            String LineCode = "// " + Text;

            // Agregar Linea A Codigo
            AddLineToIntermediateCode(LineCode, IdentType);
        
        }

        // Añadir Llamada A Función 
        public void AddFunctionCall(String Identifier, String IdentType) 
        {

            // Linea De Codigo 
            String LineCode = Identifier + "();";

            // Agregar Linea De Codigo 
            AddLineToIntermediateCode(LineCode, IdentType);
        
        }

        // Heap 

        // Agregar Valor A Heap 
        public void AddValueToHeap(String Index, String Value, String IdentType) 
        {

            // String Con Instruccion
            String LineCode = "Heap[(int) " + Index + "] = " + Value + ";";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Obtener Valor Del Heap
        public void GetValueOfHeap(String Index, String AssigExp, String IdentType)
        {

            // String Con Instruccion
            String LineCode = AssigExp + " = Heap[(int) " + Index + "];";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Avanzar Puntero Heap
        public void MovePointerHeap(String IdentType)
        {

            // String Con Instruccion
            String LineCode = "HP = HP + 1;";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Heap 

        // Agregar Valor A Stack 
        public void AddValueToStack(String Index, String Value, String IdentType)
        {

            // String Con Instruccion
            String LineCode = "Stack[(int) " + Index + "] = " + Value + ";";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Obtener Valor Del Stack
        public void GetValueOfStack(String Index, String AssigExp, String IdentType)
        {

            // String Con Instruccion
            String LineCode = AssigExp + " = Stack[(int) " + Index + "];";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, IdentType);

        }

        // Avanzar Next Env Stack
        public void MoveNextEnv(String SizeEnv)
        {

            // String Con Instruccion
            String LineCode = "SP = SP + " + SizeEnv + ";";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, "Dos");

        }

        // Avanzar Ant Env
        public void MoveAntEnv(String SizeEnv)
        {

            // String Con Instruccion
            String LineCode = "SP = SP - " + SizeEnv + ";";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, "Dos");

        }

        // Recursivas 

        // Guardar Temporales 
        public int SaveTemporaryAux(EnviromentTable ActualEnv) 
        {

            // Entorno Func O Global 
            EnviromentTable Func_Global_Env = ActualEnv;

            // Recorrer Env 
            while(Func_Global_Env != null) 
            {

                // Verificar Si ES Global O Func
                if(Func_Global_Env.EnviromentName.Contains("Env_Global") || Func_Global_Env.EnviromentName.Contains("Env_Func_")) 
                {

                    // Parar
                    break;
                
                }

                // Anvanzar Ambientes 
                Func_Global_Env = Func_Global_Env.ParentEnviroment;
            
            }

            // Verificar Si Hay Temporales En La Lista 
            if (this.TemporaryArray.Count > 0) 
            {

                // Crear Un Nuevo Temporal 
                String TemporaryActual = CreateTemporary();

                // Limipiar Temporal 
                DeleteTemporary(TemporaryActual);

                // Tamaño Auxiliar 
                int SizeAuxilary = 0;

                // Agregar Comentario 
                AddCommentOneLine("Comienzo Guardado De Temporales", "Uno");

                // Añadir Expression Cambio De Entorno Simulado
                AddTwoExpression(TemporaryActual, "SP", "+", Func_Global_Env.EnviromentSize.ToString(), "Dos");

                // Recorrer Lista De Temporales 
                foreach(KeyValuePair<String, String> Temporary in this.TemporaryArray) 
                {

                    // Aumentar Size Auxiliar 
                    SizeAuxilary += 1;

                    // Agregar A Stack 
                    AddValueToStack(TemporaryActual, Temporary.Value, "Dos");

                    // Verificar Si Estoy Por Terminar 
                    if(this.TemporaryArray.Count != SizeAuxilary)
                    {

                        // Agregar Expression 
                        AddTwoExpression(TemporaryActual, TemporaryActual, "+", "1", "Dos");
                    
                    }                                 
                
                }

                // Agregar Comentario 
                AddCommentOneLine("Fin Guardado De Temporales", "Dos");
                
            }

            // Puntero 
            int Pointer = ActualEnv.EnviromentSize;

            // Cambiar Size Del Ambiente 
            ActualEnv.EnviromentSize = Pointer + this.TemporaryArray.Count;

            // Retornar 
            return Pointer;

        }

        // Recuperar Temporales 
        public void GetTemporaryAux(EnviromentTable ActualEnv, int Position)
        {

            // Entorno Func O Global 
            EnviromentTable Func_Global_Env = ActualEnv;

            // Recorrer Env 
            while (Func_Global_Env != null)
            {

                // Verificar Si ES Global O Func
                if (Func_Global_Env.EnviromentName.Contains("Env_Global") || Func_Global_Env.EnviromentName.Contains("Env_Func_"))
                {

                    // Parar
                    break;

                }

                // Anvanzar Ambientes 
                Func_Global_Env = Func_Global_Env.ParentEnviroment;

            }

            // Verificar Si Hay Temporales En La Lista 
            if (this.TemporaryArray.Count > 0)
            {

                // Crear Un Nuevo Temporal 
                String TemporaryActual = CreateTemporary();

                // Limipiar Temporal 
                DeleteTemporary(TemporaryActual);

                // Tamaño Auxiliar 
                int SizeAuxilary = 0;

                // Agregar Comentario 
                AddCommentOneLine("Comienzo Recuperación De Temporales", "Uno");

                // Obtener Posicion 
                AddTwoExpression(TemporaryActual, "SP", "+", Position.ToString(), "Dos");

                // Recorrer Lista De Temporales 
                foreach (KeyValuePair<String, String> Temporary in this.TemporaryArray)
                {

                    // Aumentar Size Auxiliar 
                    SizeAuxilary += 1;

                    // Agregar A Stack 
                    GetValueOfStack(TemporaryActual, Temporary.Value, "Dos");

                    // Verificar Si Estoy Por Terminar 
                    if (this.TemporaryArray.Count != SizeAuxilary)
                    {

                        // Agregar Expression 
                        AddTwoExpression(TemporaryActual, TemporaryActual, "+", "1", "Dos");

                    }



                }

                // Agregar Comentario 
                AddCommentOneLine("Fin Recuperación De Temporales", "Dos");

                // Setear Size 
                Func_Global_Env.EnviromentSize = Position;

            }
        }

        // Nativas 

        // Imprimir String 
        public void AddNativePrintString()
        {

            // Crear Label 
            String ActualLabel_1 = CreateLabel();

            // Crear Label 
            String ActualLabel_2 = CreateLabel();

            // Agregar Inicio Funcion
            AddFuncBegin("print_string");

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario
            AddCommentOneLine("Ciclo For Para Imprimir String", "Uno");

            // Agregar Label
            AddLabel(ActualLabel_1, "Dos");

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T1", "T2", "Dos");

            // Agregar If 
            AddConditionalJump("T2", "==", "-1", ActualLabel_2, "Dos");

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddPrintf("c", "(char) T2");

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1", "Dos");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1, "Dos");

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label 2
            AddLabel(ActualLabel_2, "Dos");

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método", "Dos");

            // Sin Identacion 
            DeleteIdent();
            DeleteIdent();

            // Fin Del Metodo 
            AddFuncEnd();

        }

        // Concatenar String
        public void AddNativeConcatString()
        {

            // Crear Label 
            String ActualLabel_1 = CreateLabel();

            // Crear Label 
            String ActualLabel_2 = CreateLabel();

            // Crear Label 
            String ActualLabel_3 = CreateLabel();

            // Agregar Inicio Funcion
            AddFuncBegin("concat_string");

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario
            AddCommentOneLine("Almacenar Inicio String Retorno", "Uno");

            // Agregar Retorno 
            AddOneExpression("T4", "HP", "Dos");

            // Agregar Comentario 
            AddCommentOneLine("Recuperar Y Almacenar Nuevo String", "Uno");

            // Agregar Label
            AddLabel(ActualLabel_1, "Dos");

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T1", "T3", "Dos");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_2, "Dos");

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "T3", "Dos");

            // Mover Puntero 
            MovePointerHeap("Dos");

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1", "Dos");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1, "Dos");

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_2, "Dos");

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T2", "T3", "Dos");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_3, "Dos");

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "T3", "Dos");

            // Mover Puntero 
            MovePointerHeap("Dos");

            // Aumentar Posicion 
            AddTwoExpression("T2", "T2", "+", "1", "Dos");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_2, "Dos");

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_3, "Dos");

            // Añadir Identacion 
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "-1", "Dos");

            // Mover Puntero 
            MovePointerHeap("Dos");

            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método", "Uno");

            // Sin Identacion 
            DeleteIdent();
            DeleteIdent();

            // Fin Del Metodo 
            AddFuncEnd();

        }

        // Comparar String 
        public void AddNativeCompareString() 
        {

            // Crear Label 
            String ActualLabel_1 = CreateLabel();

            // Crear Label 
            String ActualLabel_2 = CreateLabel();

            // Crear Label 
            String ActualLabel_3 = CreateLabel();

            // Agregar Inicio Funcion
            AddFuncBegin("compare_string");

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("Recorrido De Ambas Cadenas", "Uno");

            // Añadir Label 
            AddLabel(ActualLabel_1, "Dos");

            // Agregar Identacion 
            AddIdent();

            // Agregar Comentario
            AddCommentOneLine("Obtener Caracteres A Comparar", "Uno");

            // Obtener Valor Del Heap
            GetValueOfHeap("T1", "T3", "Dos");
            GetValueOfHeap("T2", "T4", "Dos");

            // Agregar Comentario 
            AddCommentOneLine("Verificar Si Son Iguales", "Uno");

            // Agregar Salto Condicional 
            AddConditionalJump("T3", "==", "T4", ActualLabel_2, "Dos");

            // Agregar Identacion 
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("El 0 Indica Que Los Strings No Son Iguales", "Uno");

            // Agregar Estado 
            AddOneExpression("T4", "0", "Dos");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_3, "Dos");

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_2, "Dos");

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("El 1 Indica Que Los Strings Son Iguales", "Uno");

            // Agregar Estado 
            AddOneExpression("T4", "1", "Dos");

            // Agregar Comentario 
            AddCommentOneLine("Verificar Si Es Fin De Cadena", "Uno");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_3, "Dos");

            // Agregar Identacion
            AddIdent();

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1", "Dos");

            // Aumentar Posicion 
            AddTwoExpression("T2", "T2", "+", "1", "Dos");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1, "Dos");

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_3, "Dos");

            // Añadir Identacion 
            AddIdent();
            
            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método", "Uno");

            // Sin Identacion 
            DeleteIdent();
            DeleteIdent();

            // Fin Del Metodo 
            AddFuncEnd();

        }

        // Imprimir Bool
        public void PrintBool(bool Value) 
        {

            // Verificar Valor 
            if(Value)
            {

                // Agregar Print
                AddPrintf("c", ((int)'t').ToString());
                AddPrintf("c", ((int)'r').ToString());
                AddPrintf("c", ((int)'u').ToString());
                AddPrintf("c", ((int)'e').ToString());

            }
            else
            {

                // Agregar Print
                AddPrintf("c", ((int)'f').ToString());
                AddPrintf("c", ((int)'a').ToString());
                AddPrintf("c", ((int)'l').ToString());
                AddPrintf("c", ((int)'s').ToString());
                AddPrintf("c", ((int)'e').ToString());

            }
        
        }

    }

}