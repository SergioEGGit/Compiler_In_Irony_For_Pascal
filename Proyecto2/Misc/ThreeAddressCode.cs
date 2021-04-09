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
            TemporaryCounter = 0;
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
        public void AddLineToIntermediateCode(String LineCode)
        {

            // Agregar Codigo A Variable 
            IntermediateCode += Ident + LineCode + "\n";

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
            TemporaryCounter = 0;
            LabelCounter = 0;
            IntermediateCode = "";
            TemporaryArray = new Dictionary<String, String>();

        }

        // Agregar Cabecera 
        public String CreateHeader() 
        {

            // Auxiliar Header 
            String AuxiliaryString = "// Librerias Y Declaraciones\n\n" +
                                     "#include <stdio.h> \n\n" +
                                     "float Heap[100000]; \n" +
                                     "float Stack[100000]; \n\n" +
                                     "float SP; \n" +
                                     "float HP; \n\n";

            // Verificar Si Hay Temporales 
            if(TemporaryCounter > 0) 
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
            String AuxiliaryString = "L" + LabelCounter.ToString() + ":";

            // Crear Temporal 
            return AuxiliaryString;

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
        public String CreateNonConditionalJump(String Label) 
        {

            // String Auxiliar 
            String AuxiliaryString = "goto " + Label + ";";

            // Retornar Goto 
            return AuxiliaryString;
        
        }

        // Crear If 
        public String CreateConditionalJump(String LeftExp, String Operator, String RightExp, String Label)
        {

            // String Auxiliar 
            String AuxiliaryString = "if(" + LeftExp + " " + Operator  + " " + RightExp + ") goto " + Label + ";";

            // Retornar Goto 
            return AuxiliaryString;

        }

        // Una Expression 
        public void AddOneExpression(String AssingExp, String Exp) 
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + Exp + ";";

            // Agregar A Codigo
            AddLineToIntermediateCode(AuxiliaryString);

        }

        // Una Operacion
        public void AddTwoExpression(String AssingExp, String LeftExp, String Operator, String RightExp)
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + LeftExp + " " + Operator + " " + RightExp + ";";

            // Agregar Linea De Codigo 
            AddLineToIntermediateCode(AuxiliaryString);

        }

        // Bloque Main Comienzo
        public void AddFuncBegin(String Identifier) 
        {

            // Crear Linea 
            String Begin = "void " + Identifier + "() { \n";

            // Agregar Linea A codigo
            AddLineToIntermediateCode(Begin);

        }

        // Bloque Main Final
        public void AddFuncEnd()
        {

            // Crear Linea 
            String End = "\n    return;\n\n" +
                         "}\n";

            // Agregar Linea A codigo
            AddLineToIntermediateCode(End);

        }

        // Agregar Identacion
        public void AddIdent() 
        {

            // Agregar Identacion
            Ident = "    ";
        
        }

        // Quitar Identacion
        public void DeleteIdent()
        {

            // Agregar Identacion
            Ident = "";

        }

        // Agergar Printf
        public void AddPrintf(String Format, String Value) 
        {

            // Crear PrintF
            String LineCode = "printf(\"%" + Format + "\", " + Value + ");";

            // Añadir Linea De Codigo
            AddLineToIntermediateCode(LineCode);
         
        }

        // Añadir Comentario Unilinea 
        public void AddCommentOneLine(String Text) 
        {

            // Crear String Con Comentario 
            String LineCode = "\n" + Ident + "// " + Text;

            // Agregar Linea A Codigo
            AddLineToIntermediateCode(LineCode);
        
        }

    }

}