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

        // Método Compilar
        public override ObjectReturn Compilate(EnviromentTable Env)
        {

            // Varibles 
            ObjectReturn Left = null;
            ObjectReturn Right = null;

            // Verificar Si No EStan Nullos 
            if (LeftValue != null)
            {

                // Ejecutar
                Left = this.LeftValue.Compilate(Env);

            }
            if (RightValue != null)
            {

                // Verificar Si No Es Boolean
                if(!Left.Type.Equals("boolean"))
                {

                    // Ejecutar 
                    Right = this.RightValue.Compilate(Env);

                }

            }

            // Tipo De Dato 
            String Type = "boolean";

            // Verificar Si No Es Nulo
            if(Left != null && Right != null)
            {

                // Obtener Tipo Operacion
                Type = DominantType.TypeTableValue(Left.Type.ToString(), Right.Type.ToString());

            }

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Verificar Operacion
            if(this.RelationalType.Equals("LessSame"))
            {

                // Verificar Tipo
                if(Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), "<=", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean") { 
                    
                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse
                    
                    };

                }                

            }
            else if (this.RelationalType.Equals("GreaterSame"))
            {

                // Verificar Tipo
                if (Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), ">=", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }
            else if (this.RelationalType.Equals("Less"))
            {

                // Verificar Tipo
                if (Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), "<", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }
            else if (this.RelationalType.Equals("Greater"))
            {

                // Verificar Tipo
                if (Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), ">", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }
            else if (this.RelationalType.Equals("Equal"))
            {
                
                // Verificar Tipo
                if(Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), "==", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }
                else if (Type == "boolean")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Crear Labels 
                    String BoolTrue = Instance_1.CreateLabel();
                    String BoolFalse = Instance_1.CreateLabel();
     
                    // Añadir Label 
                    Instance_1.AddLabel(Left.BoolTrue);

                    // Agregar Labels False And True 
                    this.RightValue.BoolTrue = BoolTrue;
                    this.RightValue.BoolFalse = BoolFalse;

                    // Agregar Identacion 
                    Instance_1.AddIdent();

                    // Compilar Right Value 
                    this.RightValue.Compilate(Env);

                    // Eliminar Identacion 
                    Instance_1.DeleteIdent();

                    // Añadir Label False 
                    Instance_1.AddLabel(Left.BoolFalse);

                    // Agregar Labels False And True 
                    this.RightValue.BoolTrue = BoolFalse;
                    this.RightValue.BoolFalse = BoolTrue;

                    // Agregar Identacion 
                    Instance_1.AddIdent();

                    // Compilar Right Value 
                    ObjectReturn Auxiliary = this.RightValue.Compilate(Env);

                    // Eliminar Identacion 
                    Instance_1.DeleteIdent();                    

                    // Verificar Si Es Boolean
                    if(Auxiliary.Type.Equals("boolean")) 
                    {

                        
                        // Retornar Objeto
                        AuxiliaryReturn = new ObjectReturn("", "boolean")
                        {

                            BoolTrue = BoolTrue,
                            BoolFalse = BoolFalse

                        };

                    }


                }
                else if (Type == "string")
                {


                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Obtener Posicion Inicial De Los Strings");

                    // Obtener Strings 
                    Instance_1.AddOneExpression("T1", Left.Value.ToString());
                    Instance_1.AddOneExpression("T2", Right.Value.ToString());

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Llamada A Función");

                    // Agregar Llamada A Metodo 
                    Instance_1.AddFunctionCall("compare_string");

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump("T4", "==", "1", this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }
            else if (this.RelationalType.Equals("Differ"))
            {

                // Verificar Tipo
                if (Type == "integer" || Type == "real")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump(Left.GetValue(), "!=", Right.GetValue(), this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }
                else if (Type == "boolean")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Crear Labels 
                    String BoolTrue = Instance_1.CreateLabel();
                    String BoolFalse = Instance_1.CreateLabel();

                    // Añadir Label 
                    Instance_1.AddLabel(Left.BoolTrue);

                    // Agregar Labels False And True 
                    this.RightValue.BoolTrue = BoolFalse;
                    this.RightValue.BoolFalse = BoolTrue;

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Compilar Right Value 
                    this.RightValue.Compilate(Env);

                    // Eliminar Identacion
                    Instance_1.DeleteIdent();

                    // Añadir Label False 
                    Instance_1.AddLabel(Left.BoolFalse);

                    // Agregar Labels False And True 
                    this.RightValue.BoolTrue = BoolTrue;
                    this.RightValue.BoolFalse = BoolFalse;

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Compilar Right Value 
                    ObjectReturn Auxiliary = this.RightValue.Compilate(Env);

                    // Eliminar Identacion
                    Instance_1.DeleteIdent();

                    // Verificar Si Es Boolean
                    if (Auxiliary.Type.Equals("boolean"))
                    {

                        AuxiliaryReturn = new ObjectReturn("", "boolean")
                        {

                            BoolTrue = BoolTrue,
                            BoolFalse = BoolFalse

                        };

                    }


                }
                else if (Type == "string")
                {

                    // Obtener Instancia 
                    ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

                    // Agregar Labels 
                    if (this.BoolTrue.Equals(""))
                    {

                        // Crear Label 
                        this.BoolTrue = Instance_1.CreateLabel();

                    }
                    if (this.BoolFalse.Equals(""))
                    {

                        // Crear Label 
                        this.BoolFalse = Instance_1.CreateLabel();

                    }

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Obtener Posicion Inicial De Los Strings");

                    // Obtener Strings 
                    Instance_1.AddOneExpression("T1", Left.Value.ToString());
                    Instance_1.AddOneExpression("T2", Right.Value.ToString());

                    // Agregar Comentario 
                    Instance_1.AddCommentOneLine("Llamada A Función");

                    // Agregar Llamada A Metodo 
                    Instance_1.AddFunctionCall("compare_string");

                    // Agregar Comentario
                    Instance_1.AddCommentOneLine("Validación Expression Relacional");

                    // Agregar If De Salto 
                    Instance_1.AddConditionalJump("T4", "!=", "1", this.BoolTrue);

                    // Agregar Identacion
                    Instance_1.AddIdent();

                    // Agregar Salto No Condicional
                    Instance_1.AddNonConditionalJump(this.BoolFalse);

                    // Quitar Identacion
                    Instance_1.DeleteIdent();

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }

            // Retorno
            return AuxiliaryReturn;

        }

    }

}