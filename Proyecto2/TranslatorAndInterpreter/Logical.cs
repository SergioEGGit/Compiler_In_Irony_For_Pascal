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

        // Método Compilar
        public override ObjectReturn Compilate(EnviromentTable Env)
        {

            // Varibles 
            ObjectReturn Left = null;
            ObjectReturn Right = null;

            // Auxiliar
            ObjectReturn AuxiliaryReturn = null;

            // Crear Auxiliares 
            String InsAuxiliary;

            // Verificar Si EStoy En Global
            if (this.IsGlobal)
            {

                // Agregar Valores 
                InsAuxiliary = "Dos Global";


            }
            else
            {

                // Agregar Valores 
                InsAuxiliary = "Dos";

            }

            // Verificar Operacion
            if (this.LogicalType.Equals("And"))
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

                // Crear Etiqueta Auxiliar 
                String AuxLabel = Instance_1.CreateLabel();

                // Agregar A Izquierda Verdadera
                this.LeftValue.BoolTrue = AuxLabel;

                // Agregar A Derecha Verdadera
                this.RightValue.BoolTrue = this.BoolTrue;

                // Agregar A Izquierda Falsa 
                this.LeftValue.BoolFalse = this.BoolFalse;

                // Agregar A Derecha Falsa
                this.RightValue.BoolFalse = this.BoolFalse;

                // Verificar Si EStoy En Global
                if (this.IsGlobal)
                {

                    // Agregar Valores 
                    this.LeftValue.IsGlobal = true;
                    this.RightValue.IsGlobal = true;


                }
                else
                {

                    // Agregar Valores 
                    this.LeftValue.IsGlobal = false;
                    this.RightValue.IsGlobal = false;

                }

                // Verificar Si No Esta Nullo
                if (this.LeftValue != null) 
                {

                    // Compilar Left 
                    Left = this.LeftValue.Compilate(Env);
                    
                }

                // Agregar Label 
                Instance_1.AddLabel(this.LeftValue.BoolTrue, InsAuxiliary);

                // Agregar Identacion
                Instance_1.AddIdent();

                // Verificar Si No Esta Nullo
                if (this.RightValue != null)
                {

                    // Compilar Right 
                    Right = this.RightValue.Compilate(Env);

                }

                // Quitar Identacion
                Instance_1.DeleteIdent();

                // Verificar Tipos 
                if (Left.Type.Equals("boolean") && Left != null && Right.Type.Equals("boolean") && Right != null) 
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.RightValue.BoolFalse

                    };

                }
                                
            }
            else if (this.LogicalType.Equals("Or"))
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

                // Crear Etiqueta Auxiliar 
                String AuxLabel = Instance_1.CreateLabel();

                // Agregar A Izquierda Verdadera
                this.LeftValue.BoolTrue = this.BoolTrue;

                // Agregar A Derecha Verdadera
                this.RightValue.BoolTrue = this.BoolTrue;

                // Agregar A Izquierda Falsa 
                this.LeftValue.BoolFalse = AuxLabel;

                // Agregar A Derecha Falsa
                this.RightValue.BoolFalse = this.BoolFalse;

                // Verificar Si EStoy En Global
                if (this.IsGlobal)
                {

                    // Agregar Valores 
                    this.LeftValue.IsGlobal = true;
                    this.RightValue.IsGlobal = true;


                }
                else
                {

                    // Agregar Valores 
                    this.LeftValue.IsGlobal = false;
                    this.RightValue.IsGlobal = false;

                }

                // Verificar Si No Esta Nullo
                if (this.LeftValue != null)
                {

                    // Compilar Left 
                    Left = this.LeftValue.Compilate(Env);

                }

                // Agregar Label 
                Instance_1.AddLabel(this.LeftValue.BoolFalse, InsAuxiliary);

                // Agregar Identacion
                Instance_1.AddIdent();

                // Verificar Si No Esta Nullo
                if (this.RightValue != null)
                {

                    // Compilar Right 
                    Right = this.RightValue.Compilate(Env);

                }

                // Quitar Identacion
                Instance_1.DeleteIdent();

                // Verificar Tipos 
                if (Left.Type.Equals("boolean") && Left != null && Right.Type.Equals("boolean") && Right != null)
                {

                    // Obtener
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.RightValue.BoolFalse

                    };

                }

            }
            else if (this.LogicalType.Equals("Not"))
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

                // Agregar A Izquierda Verdadera
                this.RightValue.BoolTrue = this.BoolFalse;

                // Agregar A Derecha Verdadera
                this.RightValue.BoolFalse = this.BoolTrue;

                // Verificar Si EStoy En Global
                if (this.IsGlobal)
                {

                    // Agregar Valores 
                    this.RightValue.IsGlobal = true;


                }
                else
                {

                    // Agregar Valores 
                    this.RightValue.IsGlobal = false;

                }

                // Verificar Si No Esta Nullo
                if (this.RightValue != null)
                {

                    // Compilar Left 
                    Right = this.RightValue.Compilate(Env);

                }

                // Verificar Tipos 
                if (Right.Type.Equals("boolean") && Right != null)
                {

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
