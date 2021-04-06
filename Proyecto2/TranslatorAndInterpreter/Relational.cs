// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class Relational : AbstractExpression
    {

        // Atributos

        // Expression Izquierda
        private readonly AbstractExpression LeftValue;

        // Expresssion Derecha 
        private readonly AbstractExpression RightValue;

        // Tipo De Operacion
        private readonly String RelationalType;

        // TOken Line 
        private readonly int TokenLine;

        // Token Column 
        private readonly int TokenColumn;

        // Constructor 
        public Relational(AbstractExpression LeftValue, AbstractExpression RightValue, String RelationalType, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.LeftValue = LeftValue;
            this.RightValue = RightValue;
            this.RelationalType = RelationalType;
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
            if (this.RelationalType.Equals("LessSame"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) <= int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) <= Decimal.Parse(Right.Value.ToString()))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite <= Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }
                
            }
            else if(this.RelationalType.Equals("GreaterSame"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) >= int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) >= Decimal.Parse(Right.Value.ToString()))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite >= Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.RelationalType.Equals("Less"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) < int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) < Decimal.Parse(Right.Value.ToString()))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite < Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.RelationalType.Equals("Greater"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) > int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) > Decimal.Parse(Right.Value.ToString()))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite > Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.RelationalType.Equals("Equal"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) == int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) == Decimal.Parse(Right.Value.ToString()))
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
                else if (Type == "string")
                {

                    // Verificar Si Es True O False 
                    if (Left.Value.ToString() == Right.Value.ToString())
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
                else if (Type == "boolean")
                {

                    // Verificar Si Es True O False 
                    if (bool.TryParse(Left.Value.ToString(), out bool Value) == bool.TryParse(Right.Value.ToString(), out bool Value_))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite = Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.RelationalType.Equals("Differ"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Es True O False 
                    if (int.Parse(Left.Value.ToString()) != int.Parse(Right.Value.ToString()))
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
                else if (Type == "real")
                {

                    // Verificar Si Es True O False 
                    if (Decimal.Parse(Left.Value.ToString()) != Decimal.Parse(Right.Value.ToString()))
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
                else if (Type == "string")
                {

                    // Verificar Si Es True O False 
                    if (Left.Value.ToString() != Right.Value.ToString())
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
                else if (Type == "boolean")
                {

                    // Verificar Si Es True O False 
                    if (bool.TryParse(Left.Value.ToString(), out bool Value) != bool.TryParse(Right.Value.ToString(), out bool Value_))
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
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite <> Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }

            // Retorno
            return AuxiliaryReturn;

        }

        public override ObjectReturn Translate(EnviromentTable Env)
        {
            
            // Verificar Operacion
            if (this.RelationalType.Equals("LessSame"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " <= ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.RelationalType.Equals("GreaterSame"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " >= ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.RelationalType.Equals("Less"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " < ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.RelationalType.Equals("Greater"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " > ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.RelationalType.Equals("Equal"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " = ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.RelationalType.Equals("Differ"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " <> ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }

            // Retornar 
            return null;

        } 

    }

}