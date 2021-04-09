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
            throw new NotImplementedException();
        }

    }

}