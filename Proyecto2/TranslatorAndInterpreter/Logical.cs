// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class Logical : AbstractExpression
    {

        // Atributos

        // Expression Izquierda
        private readonly AbstractExpression LeftValue;

        // Expresssion Derecha 
        private readonly AbstractExpression RightValue;

        // Tipo De Operacion
        private readonly String LogicalType;

        // TOken Line 
        private readonly int TokenLine;

        // Token Column 
        private readonly int TokenColumn;

        // Constructor 
        public Logical(AbstractExpression LeftValue, AbstractExpression RightValue, String LogicalType, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.LeftValue = LeftValue;
            this.RightValue = RightValue;
            this.LogicalType = LogicalType;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Varibles 
            ObjectReturn Left = null;
            ObjectReturn Right = null;

            // Verificar Si No EStan Nullos 
            if (LeftValue != null)
            {

                // Ejecutar
                Left = this.LeftValue.Execute(Env);

            }
            if (RightValue != null)
            {

                // Ejecutar 
                Right = this.RightValue.Execute(Env);

            }

            // Tipo De Dato 
            String Type = "";

            // Verificar Si No Es Nulo
            if (Left != null && Right != null)
            {

                // Obtener Tipo Operacion
                Type = DominantType.TypeTableValue(Left.Type.ToString(), Right.Type.ToString());

            }

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Verificar Operacion
            if (this.LogicalType.Equals("And"))
            {

                // Verificar Tipo
                if (Type == "boolean")
                {
                    
                    // Verificar Si Es True O False 
                    if (bool.Parse(Left.Value.ToString()) && bool.Parse(Right.Value.ToString()))
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(true, "boolean");

                    }
                    else
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(false, "boolean");

                    }

                }                
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite and Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.LogicalType.Equals("Or"))
            {

                // Verificar Tipo
                if (Type == "boolean")
                {

                    // Verificar Si Es True O False 
                    if (bool.Parse(Left.Value.ToString()) || bool.Parse(Right.Value.ToString()))
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(true, "boolean");

                    }
                    else
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(false, "boolean");

                    }

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite or Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.LogicalType.Equals("Not"))
            {

                // Verificar Tipo
                if (Type == "boolean")
                {

                    // Verificar Si Es True O False 
                    if (!bool.Parse(Right.Value.ToString()))
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(true, "boolean");

                    }
                    else
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(false, "boolean");

                    }

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite not Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }            

            // Retorno
            return AuxiliaryReturn;

        }

        public override ObjectReturn Translate(EnviromentTable Env)
        {

            // Verificar Operacion
            if (this.LogicalType.Equals("And"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " and ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.LogicalType.Equals("Or"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " or ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.LogicalType.Equals("Not"))
            {

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " not ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }            

            // Retornar 
            return null;

        }

    }
}
