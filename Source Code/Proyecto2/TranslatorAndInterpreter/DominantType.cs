// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    
    // Clase Principal
    class DominantType
    {
        
        // Enumerable Tipos De Datos 
        enum DataType : int
        {

            Tstring = 0,
            Treal = 1,
            Tboolean =2,
            Tinteger = 3,
            Terror = 4,
        
        }

        // Tabla De Tips 
        private static DataType[,] TypeTable = new DataType[5, 5] 
        
                                                {       
                                                      /*   String   */  /*   Integer  */  /*   Boolean   */  /*     Real    */  /*    Error    */
                                   /*    String  */ { DataType.Tstring, DataType.Terror,  DataType.Terror,   DataType.Terror,   DataType.Terror},
                                   /*   Integer  */ { DataType.Terror,  DataType.Treal,   DataType.Terror,   DataType.Treal,    DataType.Terror},
                                   /*   Boolean  */ { DataType.Terror,  DataType.Terror,  DataType.Tboolean, DataType.Terror,   DataType.Terror},
                                   /*    Real    */ { DataType.Terror,  DataType.Treal,   DataType.Terror,   DataType.Tinteger, DataType.Terror},
                                   /*    Error   */ { DataType.Terror,  DataType.Terror,  DataType.Terror,   DataType.Terror,   DataType.Terror}

                                                };
        
        // Verificar Tipos Con La Tabla 
        public static String TypeTableValue(String LeftValue, String RightValue) {

            // Fila Y Columna
            int Row, Column;

            // Obtener Valor Enumerable
            Row = ReturnEnum(LeftValue);
            Column = ReturnEnum(RightValue);

            // Obtener Tipo 
            String AuxiliaryType = ReturnType(TypeTable[Row, Column].ToString());

            // Retornar 
            return AuxiliaryType;

        }

        // Retornar Value Enum
        private static int ReturnEnum(String Value) {

            // Verificar El Tipo
            if(Value == "string")
            {

                // Retornar Valor 
                return 0;

            }
            else if(Value == "real")
            {

                // Retornar Valor 
                return 1;

            }
            else if(Value == "boolean")
            {

                // Retornar Valor 
                return 2;

            }
            else if(Value == "integer")
            {

                // Retonar Valor 
                return 3;

            }
            else 
            {

                // Retornar Valor 
                return 4;

            }

        }

        // Retornar Tipo String
        private static String ReturnType(String Value) {

            // Verificar Si Es String
            if("Tstring" == Value) 
            {

                // Retornar Valor 
                return "string";
            
            }
            else if("Treal" == Value)  
            {

                // Retornar Valor 
                return "real";
            
            }
            else if("Tboolean" == Value)
            {

                // Retornar Valor 
                return "boolean";

            }
            else if ("Tinteger" == Value)
            {

                // Retornar Valor
                return "integer";

            }
            else 
            {

                // Retornar Valor 
                return "error";
            
            }

        }

    }

}