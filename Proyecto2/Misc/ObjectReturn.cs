// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto2.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    
    // Clase Principal 
    class ObjectReturn
    {
        
        // Valor Expression 
        public object Value;

        // Tipo Expression
        public object Type;

        // Opcional
        public String Option;

        // End
        public String End;

        // Constructor 
        public ObjectReturn(object Value, object Type) {

            // Inicializar Valroes 
            this.Value = Value;
            this.Type = Type;
            this.Option = "";
            this.End = "";

        }

        // Obtener Valor 
        public String GetValue() 
        {

            // Obtener Instancia
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Eliminar Temporal 
            Instance_1.DeleteTemporary(this.Value.ToString());

            // Retornar Valor 
            return this.Value.ToString();
        
        }

    }

}