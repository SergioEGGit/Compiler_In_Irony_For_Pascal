// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class Arithmetic : AbstractExpression
    {

        // Atributos

        // Expression Izquierda
        private readonly AbstractExpression LeftValue;

        // Expresssion Derecha 
        private readonly AbstractExpression RightValue;

        // Tipo De Operacion
        private readonly String ArithmeticType;

        // TOken Line 
        private readonly int TokenLine;

        // Token Column 
        private readonly int TokenColumn;
         
        // Constructor 
        public Arithmetic(AbstractExpression LeftValue, AbstractExpression RightValue, String ArithmeticType, int TokenLine, int TokenColumn) {

            
            // Inicializar Valores 
            this.LeftValue = LeftValue;
            this.RightValue = RightValue;
            this.ArithmeticType = ArithmeticType;
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
            if(this.LeftValue != null)
            {

                // Ejecutar
                Left = this.LeftValue.Execute(Env);

            }
            if(this.RightValue != null) 
            {

                // Ejecutar 
                Right = this.RightValue.Execute(Env);

            }

            // Tipo De Dato 
            String Type = "";

            // Verificar Si No Es Nulo
            if(Left != null && Right != null) {

                // Obtener Tipo Operacion
                Type = DominantType.TypeTableValue(Left.Type.ToString(), Right.Type.ToString());

            }

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Verificar Operacion
            if (this.ArithmeticType.Equals("Sum"))
            {

                // Verificar Tipo
                if (Type == "string")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(Left.Value.ToString() + Right.Value.ToString(), Type);

                }
                else if (Type == "integer")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) + int.Parse(Right.Value.ToString()), Type);

                }
                else if (Type == "real")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(Decimal.Parse(Left.Value.ToString()) + Decimal.Parse(Right.Value.ToString()), Type);

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite La Suma Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }                    

                }

            }
            else if (this.ArithmeticType.Equals("Substraction"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) - int.Parse(Right.Value.ToString()), Type);

                }
                else if (Type == "real")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(Decimal.Parse(Left.Value.ToString()) - Decimal.Parse(Right.Value.ToString()), Type);

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite La Resta Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));
                   
                    }

                }

            } 
            else if(this.ArithmeticType.Equals("Multiplication")) 
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) * int.Parse(Right.Value.ToString()), Type);
                    
                }
                else if (Type == "real")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(Decimal.Parse(Left.Value.ToString()) * Decimal.Parse(Right.Value.ToString()), Type);

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite La Multiplicacion Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.ArithmeticType.Equals("Division"))
            {

                // Verificar Operacion
                if (Type == "integer")
                {

                    // Verificar Si Esta Divido Por Cero 
                    if (int.Parse(Right.Value.ToString()) != 0)
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(Decimal.Parse(Left.Value.ToString()) / Decimal.Parse(Right.Value.ToString()), "real");

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Error Al Dividir Sobre 0", this.TokenLine, this.TokenColumn));

                    }

                }
                else if (Type == "real")
                {

                    // Verificar Si Esta Divido Por Cero 
                    if (Decimal.Parse(Right.Value.ToString()) != 0)
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(Decimal.Parse(Left.Value.ToString()) / Decimal.Parse(Right.Value.ToString()), Type);

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Error Al Dividir Sobre 0", this.TokenLine, this.TokenColumn));

                    }

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite La Division Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }
            else if (this.ArithmeticType.Equals("Mod"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Verificar Si Esta Divido Por Cero 
                    if (int.Parse(Right.Value.ToString()) != 0)
                    {

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) % int.Parse(Right.Value.ToString()), Type);

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Error Al Realizar Mod Sobre 0", this.TokenLine, this.TokenColumn));

                    }                    

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite Mod Entre Los Tipos " + Left.Type.ToString() + " Y " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                } 

            }
            else if (this.ArithmeticType.Equals("Minus"))
            {

                // Verificar Tipo
                if (Type == "integer")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(-1 * int.Parse(Right.Value.ToString()), Type);

                }
                else if (Type == "real")
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(-1 * Decimal.Parse(Right.Value.ToString()), Type);

                }
                else
                {

                    // Verificar Si No Es Nulo
                    if (Left != null && Right != null)
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite Negativos En El Tipo " + Right.Type.ToString(), this.TokenLine, this.TokenColumn));

                    }

                }

            }

            // Retorno
            return AuxiliaryReturn;

        }

        public override ObjectReturn Translate(EnviromentTable Env)
        {

            // Verificar Operacion
            if (this.ArithmeticType.Equals("Sum"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " + ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.ArithmeticType.Equals("Substraction")) 
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " - ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);                

            }
            else if (this.ArithmeticType.Equals("Multiplication"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " * ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.ArithmeticType.Equals("Division"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " / ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.ArithmeticType.Equals("Mod"))
            {

                // Traducir Valor Izquierda
                this.LeftValue.Translate(Env);

                // Agregar Traduccion 
                VariablesMethods.TranslateString += " % ";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }
            else if (this.ArithmeticType.Equals("Minus"))
            {
    
                // Agregar Traduccion 
                VariablesMethods.TranslateString += " -";

                // Traducir Valor Derecha
                this.RightValue.Translate(Env);

            }

            // Retornar 
            return null;

        }
    
    }

}