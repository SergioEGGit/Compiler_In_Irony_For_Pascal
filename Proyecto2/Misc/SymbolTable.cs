// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{
    
    // Clase Simbolo
    class SymbolTable
    {

        // Atributos 

        // Identificador De Simbolo
        public String Identifier;

        // Tipo De Simbolo
        public String Type;

        // Valor Asociado Al Simbolo
        public object Value;
        
        // Tipo De Declaracion
        public String DecType;

        // Ambito 
        public String Env;

        // Fila 
        public int Line;

        // Columna 
        public int Column;

        // Constructor 
         public SymbolTable(String Identifier, String Type, object Value, String DecType, String Env, int Line, int Column) {

            // Incializar Valores
            this.Identifier = Identifier;
            this.Type = Type;
            this.Value = Value;
            this.DecType = DecType;
            this.Env = Env;
            this.Line = Line;
            this.Column = Column;
         
         }

    }

}