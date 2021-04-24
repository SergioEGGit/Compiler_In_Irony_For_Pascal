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

                        // Verificar Si Ambos Son Integer 
                        if(Left.Type.Equals("integer") && Right.Type.Equals("integer")) 
                        {

                            // Obtener
                            AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) % int.Parse(Right.Value.ToString()), Type);

                        }
                        else
                        {

                            // Agregar Error 
                            VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite Mod Entre Integer Y Real", this.TokenLine, this.TokenColumn));


                        }

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Error Al Realizar Mod Sobre 0", this.TokenLine, this.TokenColumn));

                    }                    

                }
                else if (Type == "real")
                {

                    // Verificar Si Esta Divido Por Cero 
                    if(Decimal.Parse(Right.Value.ToString()) != 0)
                    {

                        // Verificar Si Ambos Son Integer 
                        if (Left.Type.Equals("integer") && Right.Type.Equals("integer"))
                        {

                            // Obtener
                            AuxiliaryReturn = new ObjectReturn(int.Parse(Left.Value.ToString()) % int.Parse(Right.Value.ToString()), Type);

                        }
                        else
                        {

                            // Agregar Error 
                            VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "No Se Permite Mod Entre Integer Y Real", this.TokenLine, this.TokenColumn));


                        }

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

        // Método Traducir
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

        // Método Compilar
        public override ObjectReturn Compilate(EnviromentTable Env)
        {

            // Crear Objeto Auxiliar 
            ObjectReturn AuxiliaryReturn = null;

            // Obtener Objetos Expresiones 
            ObjectReturn Left = null;
            ObjectReturn Right = null;

            // Verificar Si No EStan Nullos 
            if (this.LeftValue != null)
            {

                // Verificar Si Global 
                if (this.IsGlobal)
                {

                    // Agregar Expression 
                    this.LeftValue.IsGlobal = true;

                }
                else
                {

                    // Agregar Expression 
                    this.LeftValue.IsGlobal = false;

                }

                // Ejecutar
                Left = this.LeftValue.Compilate(Env);

            }
            if (this.RightValue != null)
            {

                // Verificar Si Global 
                if (this.IsGlobal)
                {

                    // Agregar Expression 
                    this.RightValue.IsGlobal = true;

                }
                else
                {

                    // Agregar Expression 
                    this.RightValue.IsGlobal = false;

                }

                // Ejecutar 
                Right = this.RightValue.Compilate(Env);

            }

            // Tipo De Dato 
            String Type = "";

            // Verificar Si No Es Nulo
            if (Left != null && Right != null)
            {

                // Obtener Tipo Operacion
                Type = DominantType.TypeTableValue(Left.Type.ToString(), Right.Type.ToString());

            }

            // Crear Auxiliares 
            String CommentAuxiliary;
            String InsAuxiliary;

            // Verificar Si EStoy En Global
            if(this.IsGlobal)
            {

                // Agregar Valores 
                CommentAuxiliary = "Uno Global";
                InsAuxiliary = "Dos Global";
                

            }
            else
            {

                // Agregar Valores 
                CommentAuxiliary = "Uno";
                InsAuxiliary = "Dos";

            }

            // Verificar Operacion
            if (this.ArithmeticType.Equals("Sum"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "+", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }
                else if (Type == "real")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "+", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }
                else if (Type == "string")
                {

                    // Crear Temporal
                    String ActualTemporary_1 = Instance_1.CreateTemporary();

                    // Eliminar Temporal 
                    // Instance_1.DeleteTemporary(ActualTemporary_1);

                    // Añadir Expression 1
                    Instance_1.AddOneExpression("T1", Left.GetValue(), InsAuxiliary);

                    // Añadir Expression 2
                    Instance_1.AddOneExpression("T2", Right.GetValue(), InsAuxiliary);

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Llamada Funcion Nativa (Concatenar String)", CommentAuxiliary);

                    // Agregar Llamada A Funcion 
                    Instance_1.AddFunctionCall("concat_string", InsAuxiliary);

                    // Obtener Valor De Nueva Cadena 
                    Instance_1.AddOneExpression(ActualTemporary_1, "T4", InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary_1, Type)
                    {

                        Temporary = true

                    };

                }

            } 
            else if (this.ArithmeticType.Equals("Substraction"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "-", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }
                else if (Type == "real")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "-", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }

            }
            else if (this.ArithmeticType.Equals("Multiplication"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "*", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }
                else if (Type == "real")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "*", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }

            }
            else if (this.ArithmeticType.Equals("Division"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "/", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, "real")
                    {

                        Temporary = true

                    };

                }
                else if (Type == "real")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "/", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }

            }
            else if (this.ArithmeticType.Equals("Mod"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Verificar Tipo Exp 
                    if (Left.Type.Equals("integer") && Right.Type.Equals("integer"))
                    {

                        // Añadir Expression
                        Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "%", Right.GetValue(), InsAuxiliary);

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                        {

                            Temporary = true

                        };

                    }

                }
                else if (Type == "real")
                {

                    // Verificar Tipo Exp 
                    if(Left.Type.Equals("integer") && Right.Type.Equals("integer")) 
                    {

                        // Añadir Expression
                        Instance_1.AddTwoExpression(ActualTemporary, Left.GetValue(), "%", Right.GetValue(), InsAuxiliary);

                        // Obtener
                        AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                        {

                            Temporary = true

                        };

                    }

                }

            }
            else if (this.ArithmeticType.Equals("Minus"))
            {

                // Obtener Instancia 
                ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                // Crear Temporal
                String ActualTemporary = Instance_1.CreateTemporary();

                // Eliminar Temporal 
                // Instance_1.DeleteTemporary(ActualTemporary);

                // Verificar Tipo 
                if (Type == "integer")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, "0", "-", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }
                else if (Type == "real")
                {

                    // Añadir Expression
                    Instance_1.AddTwoExpression(ActualTemporary, "0", "-", Right.GetValue(), InsAuxiliary);

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, Type)
                    {

                        Temporary = true

                    };

                }

            }

            // Retornar 
            return AuxiliaryReturn;

        }

    }

}