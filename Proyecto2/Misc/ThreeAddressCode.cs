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

        // Constructor 
        private ThreeAddressCode() 
        {

            // Inicializar Valores
            TemporaryCounter = 4;
            LabelCounter = 0;
            IntermediateCode = "";
            TemporaryArray = new Dictionary<String, String>();
            Ident = "";

        }

        // Obtener Instancia
        public static ThreeAddressCode GetInstance
        {

            // Método Get
            get 
            {

                // Verificar Si Existe La Instancia
                if(ActualInstance == null) 
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

            // Verificar Tipo
            if (Type.Equals("Dos"))
            {

                // Agregar Codigo A Variable 
                IntermediateCode += Ident + LineCode + "\n\n";

            }
            else if(Type.Equals("Uno"))
            {

                // Agregar Codigo A Variable 
                IntermediateCode += Ident + LineCode + "\n";

            }
            else if (Type.Equals("Sin"))
            {

                // Agregar Codigo A Variable 
                IntermediateCode += Ident + LineCode;

            }

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
        public void AddLabel(String Label) 
        {

            // Linea De codigo 
            String LineCode = Label + ":";

            // Agregar Linea 
            AddLineToIntermediateCode(LineCode, "Dos");
        
        }

        // Obtener Array De Temporales 
        public Dictionary<String, String> GetTemporaryArray()
        {

            // Retornar Temporal 
            return TemporaryArray;

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
        public void AddNonConditionalJump(String Label) 
        {

            // String Auxiliar 
            String AuxiliaryString = "goto " + Label + ";";

            // Agregar Linea De Codigo
            AddLineToIntermediateCode(AuxiliaryString, "Dos");
        
        }

        // Crear If 
        public void AddConditionalJump(String LeftExp, String Operator, String RightExp, String Label)
        {

            // String Auxiliar 
            String AuxiliaryString = "if(" + LeftExp + " " + Operator  + " " + RightExp + ") goto " + Label + ";";

            // Agregar Linea A Codigo
            AddLineToIntermediateCode(AuxiliaryString, "Dos");

        }

        // Una Expression 
        public void AddOneExpression(String AssingExp, String Exp) 
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + Exp + ";";

            // Agregar A Codigo
            AddLineToIntermediateCode(AuxiliaryString, "Dos");

        }

        // Una Operacion
        public void AddTwoExpression(String AssingExp, String LeftExp, String Operator, String RightExp)
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + LeftExp + " " + Operator + " " + RightExp + ";";

            // Agregar Linea De Codigo 
            AddLineToIntermediateCode(AuxiliaryString, "Dos");

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
        public void AddCommentOneLine(String Text) 
        {

            // Crear String Con Comentario 
            String LineCode = "// " + Text;

            // Agregar Linea A Codigo
            AddLineToIntermediateCode(LineCode, "Uno");
        
        }

        // Añadir Llamada A Función 
        public void AddFunctionCall(String Identifier) 
        {

            // Linea De Codigo 
            String LineCode = Identifier + "();";

            // Agregar Linea De Codigo 
            AddLineToIntermediateCode(LineCode, "Dos");
        
        }

        // Heap 

        // Agregar Valor A Heap 
        public void AddValueToHeap(String Index, String Value) 
        {

            // String Con Instruccion
            String LineCode = "Heap[(int) " + Index + "] = " + Value + ";";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, "Dos");

        }

        // Obtener Valor Del Heap
        public void GetValueOfHeap(String Index, String AssigExp)
        {

            // String Con Instruccion
            String LineCode = AssigExp + " = Heap[(int) " + Index + "];";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, "Dos");

        }

        // Avanzar Puntero Heap
        public void MovePointerHeap()
        {

            // String Con Instruccion
            String LineCode = "HP = HP + 1;";

            // Agregar Linea A Heap 
            AddLineToIntermediateCode(LineCode, "Dos");

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
            AddCommentOneLine("Ciclo For Para Imprimir String");

            // Agregar Label
            AddLabel(ActualLabel_1);

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T1", "T2");

            // Agregar If 
            AddConditionalJump("T2", "==", "-1", ActualLabel_2);

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddPrintf("c", "(char) T2");

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1);

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label 2
            AddLabel(ActualLabel_2);

            // Agregar Identacion
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método");

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
            AddCommentOneLine("Almacenar Inicio String Retorno");

            // Agregar Retorno 
            AddOneExpression("T4", "HP");

            // Agregar Comentario 
            AddCommentOneLine("Recuperar Y Almacenar Nuevo String");

            // Agregar Label
            AddLabel(ActualLabel_1);

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T1", "T3");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_2);

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "T3");

            // Mover Puntero 
            MovePointerHeap();

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1);

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_2);

            // Agregar Identacion
            AddIdent();

            // Obtener Valor De Heap
            GetValueOfHeap("T2", "T3");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_3);

            // Agregar Identacion
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "T3");

            // Mover Puntero 
            MovePointerHeap();

            // Aumentar Posicion 
            AddTwoExpression("T2", "T2", "+", "1");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_2);

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_3);

            // Añadir Identacion 
            AddIdent();

            // Añadir Print
            AddValueToHeap("HP", "-1");

            // Mover Puntero 
            MovePointerHeap();

            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método");

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

            // Añadir Label 
            AddLabel(ActualLabel_1);

            // Agregar Identacion 
            AddIdent();

            // Agregar Comentario
            AddCommentOneLine("Obtener Caracteres A Comparar");

            // Obtener Valor Del Heap
            GetValueOfHeap("T1", "T3");
            GetValueOfHeap("T2", "T4");

            // Agregar Comentario 
            AddCommentOneLine("Verificar Si Son Iguales");

            // Agregar Salto Condicional 
            AddConditionalJump("T3", "==", "T4", ActualLabel_2);

            // Agregar Identacion 
            AddIdent();

            // Agregar Comentario 
            AddCommentOneLine("El 0 Indica Que Los Strings No Son Iguales");

            // Agregar Estado 
            AddOneExpression("T4", "0");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_3);

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_2);

            // Agregar Identacion
            AddIdent();

            // Agregar Estado 
            AddOneExpression("T4", "1");

            // Agregar If 
            AddConditionalJump("T3", "==", "-1", ActualLabel_3);

            // Agregar Identacion
            AddIdent();

            // Aumentar Posicion 
            AddTwoExpression("T1", "T1", "+", "1");

            // Aumentar Posicion 
            AddTwoExpression("T2", "T2", "+", "1");

            // Agregar Salto No Condicional
            AddNonConditionalJump(ActualLabel_1);

            // Quitar Identacion
            DeleteIdent();
            DeleteIdent();

            // Agregar Label
            AddLabel(ActualLabel_3);

            // Añadir Identacion 
            AddIdent();
            
            // Agregar Comentario 
            AddCommentOneLine("Fin Del Método");

            // Sin Identacion 
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