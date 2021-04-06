// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{

    // Clase Principal 
    class ThreeAddressCode
    {

        // Crear Instancia Patron Singleton
        private static ThreeAddressCode ActualInstance;

        // Temporal Contador 
        public static int TemporalCounter;

        // Label Contador 
        public static int LabelCounter;

        // String De Codigo 
        public static String IntermediateCode;

        // Constructor Privado 
        private ThreeAddressCode() 
        {

            // Inicializar Valores
            TemporalCounter = 0;
            LabelCounter = 0;
            IntermediateCode = "";
        
        }

        // Acceder A Instancia 
        public static ThreeAddressCode GetInstance() 
        {

            // Verificar Si Ya Existe La Instancia 
            if(ActualInstance == null) 
            {

                // Crear Instancia 
                ActualInstance = new ThreeAddressCode();
            
            }

            // Retornar Instancia 
            return ActualInstance;
        
        }

        // Resetear Instancia
        public static void ResetInstance() 
        {

            // Crear Nueva Instancia 
            ActualInstance = new ThreeAddressCode();
        
        }

        // Crear Un Temporal 
        public static String CreateTemporal() 
        {

            // Aumentar Contador 
            TemporalCounter += TemporalCounter;

            // String Auxiliar 
            String AuxiliaryString = "T" + TemporalCounter.ToString();

            // Crear Temporal 
            return AuxiliaryString;
        
        }

        // Crear Un Label 
        public static String CreateLabel() 
        {

            // Aumentar Contador 
            LabelCounter += LabelCounter;

            // String Auxiliar 
            String AuxiliaryString = "L" + LabelCounter.ToString() + ":";

            // Crear Temporal 
            return AuxiliaryString;

        }

        // Crear Goto 
        public static String CreateNonConditionalJump(String Label) 
        {

            // String Auxiliar 
            String AuxiliaryString = "goto " + Label + ";";

            // Retornar Goto 
            return AuxiliaryString;
        
        }

        // Crear If 
        public static String CreateConditionalJump(String LeftExp, String Operator, String RightExp, String Label)
        {

            // String Auxiliar 
            String AuxiliaryString = "if(" + LeftExp + " " + Operator  + " " + RightExp + ") goto " + Label + ";";

            // Retornar Goto 
            return AuxiliaryString;

        }

        // Agregar Codigo 
        public static void AddLineToIntermediateCode(String LineCode) 
        {

            // Agregar Codigo A Variable 
            IntermediateCode += LineCode + "\n";
        
        }

        // Una Expression 
        public static String OneExpression(String AssingExp, String Exp) 
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + Exp + ";";

            // Retornar 
            return AuxiliaryString;
        
        }

        // Una Operacion
        public static String TwoExpression(String AssingExp, String LeftExp, String Operator, String RightExp)
        {

            // String Auxiliar 
            String AuxiliaryString = AssingExp + " = " + LeftExp + " " + Operator + " " + RightExp + ";";

            // Retornar 
            return AuxiliaryString;

        }

    }

}