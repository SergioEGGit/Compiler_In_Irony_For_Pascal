// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{
    
    // Clase Error 
    class ErrorTable
    {

        // Atributos 

        // Contador De Errores 
        public int ErrorCounter;

        // Indica El Tipo De Error (Lexico, Sintactico Y Semantico)
        public String ErrorType;

        // Descripcion De Los Errores 
        public String ErrorDescripcion;

        // Fila Donde Esta El Error
        public int ErrorLine;

        // Columna Donde Esta El Error
        public int ErrorColumn;

        // Constructor 
        public ErrorTable(int ErrorCounter, String ErrorType, String ErrorDescripcion, int ErrorLine, int ErrorColumn) {

            // Inicializar Valores 
            this.ErrorCounter = ErrorCounter;
            this.ErrorType = ErrorType;
            this.ErrorDescripcion = ErrorDescripcion;
            this.ErrorLine = ErrorLine;
            this.ErrorColumn = ErrorColumn;
        
        }

    }

}