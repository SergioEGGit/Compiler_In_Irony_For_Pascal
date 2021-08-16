// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Optimization
{

    // Clase Optimization
    class OptimizationTable
    {

        // Atributos 

        // Contador Codigo Optimizado 
        public int AuxiliaryCounter;

        // Tipo De Optimización 
        public String OptimizeType;

        // Regla De Optimizacion 
        public String OptimizationRule;

        // Codigo Eliminado 
        public String CodeRemoved;

        // Codigo Agregado
        public String CodeAdded;

        // Line
        public int Line;

        // Constructor 
        public OptimizationTable(int AuxiliaryCounter, String OptimizedType, String OptimizationRule, String CodeRemoved, String CodeAdded, int Line) 
        {

            // Iniciar Valors 
            this.AuxiliaryCounter = AuxiliaryCounter;
            this.OptimizeType = OptimizedType;
            this.OptimizationRule = OptimizationRule;
            this.CodeRemoved = CodeRemoved;
            this.CodeAdded = CodeAdded;
            this.Line = Line;
        
        }

    }

}