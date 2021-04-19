// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal 
    class Parenthesis : AbstractExpression
    {
        // Atributos

        // Expression Izquierda
        private readonly AbstractExpression Value;

        // Constructor 
        public Parenthesis(AbstractExpression Value)
        {

            // Inicializar Valores 
            this.Value = Value;

        }

        // Método Ejecutar
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Varibles 
            ObjectReturn Value_ = null;

            // Verificar Si No EStan Nullos 
            if(this.Value != null)
            {

                // Ejecutar
                Value_ = this.Value.Execute(Env);
            
            }

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Verificar 
            if (Value_ != null) 
            {

                // Obtener
                AuxiliaryReturn = new ObjectReturn(Value_.Value, Value_.Type);

            } 

            // Retorno
            return AuxiliaryReturn;

        }
                
        // Método Traducir
        public override ObjectReturn Translate(EnviromentTable Env)
        {
            
            // Agregar Traduccion 
            VariablesMethods.TranslateString += "(";
            
            // Traducir Valor 
            this.Value.Translate(Env);

            // Agregar Traduccion 
            VariablesMethods.TranslateString += ")";

            // Retornar 
            return null;

        }

        // Método Compilar
        public override ObjectReturn Compilate(EnviromentTable Env)
        {

            // Varibles 
            ObjectReturn Value_ = null;

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Verificar Si Etiqueta Estan Vacias 
            if (this.BoolTrue.Equals("")) 
            {

                // Agregar Etiqueta 
                this.BoolTrue = Instance_1.CreateLabel();
            
            }

            // Verificar Si Etiqueta Estan Vacias 
            if (this.BoolFalse.Equals(""))
            {

                // Agregar Etiqueta 
                this.BoolFalse = Instance_1.CreateLabel();

            }

            // Agregar Etiquetas 
            Value.BoolTrue = this.BoolTrue;
            Value.BoolFalse = this.BoolFalse;            

            // Verificar Si No EStan Nullos 
            if(this.Value != null)
            {

                // Ejecutar
                Value_ = this.Value.Compilate(Env);

            }

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Verificar 
            if (Value_ != null)
            {

                // Obtener
                AuxiliaryReturn = new ObjectReturn(Value_.Value, Value_.Type) { 
                
                    BoolTrue = Value.BoolTrue,
                    BoolFalse = Value.BoolFalse
                
                };

            }

            // Retorno
            return AuxiliaryReturn;

        }

    }

}