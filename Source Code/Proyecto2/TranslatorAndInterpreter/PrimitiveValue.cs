// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    
    // Clase Principal 
    class PrimitiveValue : AbstractExpression
    {

        // Atributos

        // Type 
        private readonly String StringType;

        // Valor 
        private readonly object Value;

        // Es String 
        public bool IsString;

        // Constructor 
        public PrimitiveValue(object Value, String StringType) {

            // Inicicalizar Valores  
            this.Value = Value;
            this.StringType = StringType;
            this.IsString = false;
        
        }

        // Métodod Ejecutar 
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Objecto A Retornar
            ObjectReturn AuxiliaryReturn = null;

            if(IsString)
            {

                // Verificar Tipo
                if (this.StringType.Equals("Identifier"))
                {

                    // Buscar Variable 
                    SymbolTable ActualVar = Env.GetVariable(this.Value.ToString());

                    // Obtener Variable 
                    if (ActualVar != null)
                    {

                        ObjectReturn ActualValue = (ObjectReturn)ActualVar.Value;

                        // Retornar Objecto 
                        AuxiliaryReturn = new ObjectReturn(ActualValue.Value, ActualVar.Type);

                    }
                    else
                    {

                        // Retornar Objecto 
                        AuxiliaryReturn = null;

                    }

                }
                else
                {

                    // Agregar A Objecto Valor
                    AuxiliaryReturn = new ObjectReturn(this.Value.ToString(), "string");

                }

            }
            else
            {

                // Verificar Que Tipo De Valor Primtivo ES 
                if (int.TryParse(this.Value.ToString(), out int AuxiliaryValueI))
                {

                    // Agreagr A Objecto Valor 
                    AuxiliaryReturn = new ObjectReturn(AuxiliaryValueI, "integer");

                }
                else if (Decimal.TryParse(this.Value.ToString(), out Decimal AuxiliaryValueD))
                {

                    // Agregar A Objecto Valor 
                    AuxiliaryReturn = new ObjectReturn(AuxiliaryValueD, "real");

                }
                else if (this.Value.ToString() == "true")
                {

                    // Agregar A Objecto Valor
                    AuxiliaryReturn = new ObjectReturn(true, "boolean");

                }
                else if (this.Value.ToString() == "false")
                {

                    // Agregar A Objecto Valor
                    AuxiliaryReturn = new ObjectReturn(false, "boolean");

                }

            }

            // Retornar 
            return AuxiliaryReturn;

        }

        // Método Traducir
        public override ObjectReturn Translate(EnviromentTable Env)
        {

            // Verificar Si Es Integer, Real O Boolean
            if (int.TryParse(this.Value.ToString(), out int Value) || Decimal.TryParse(this.Value.ToString(), out Decimal Value_) || this.Value.ToString().ToLower().Equals("true") || this.Value.ToString().ToLower().Equals("false") || this.StringType.Equals("Identifier"))
            {

                // Agregar Traduccion 
                VariablesMethods.TranslateString += this.Value.ToString();

            }
            else 
            {

                // Agregar Traduccion 
                VariablesMethods.TranslateString += "'" + this.Value.ToString() + "'";

            }

            // Retornar Null
            return null;

        }

        // Método Compilar 
        public override ObjectReturn Compilate(EnviromentTable Env) 
        {

            // Objecto A Retornar
            ObjectReturn AuxiliaryReturn = null;

            // Agregar Valroes 
            String CommentAuxiliary;
            String InsAuxiliary;

            // Obtener Instancia 
            ThreeAddressCode Instancia_1 = ThreeAddressCode.GetInstance;

            // Verificar Si Es Global 
            if (this.IsGlobal)
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

            if(IsString)
            {
          
                // Verificar Tipo
                if (this.StringType.Equals("Identifier"))
                {
                    
                    // String Crear Temporal 
                    String Temporary = Instancia_1.CreateTemporary();
                    String TemporaryAuxiliary = Instancia_1.CreateTemporary();

                    // Eliminar Temporal 
                    Instancia_1.DeleteTemporary(TemporaryAuxiliary);

                    // Buscar Variable 
                    SymbolTable ActualVar = Env.GetVariableStack(this.Value.ToString());
                    
                    // Obtener Variable 
                    if (ActualVar != null)
                    {
                       
                        // Agregar Comentario 
                        Instancia_1.AddCommentOneLine("Obtener Valor De Variable", CommentAuxiliary);

                        if (ActualVar.IsGlobalVar)
                        {

                            // Agrebar Mover Puntero
                            Instancia_1.AddOneExpression(TemporaryAuxiliary, ActualVar.GetValue(), InsAuxiliary);

                        }
                        else 
                        {

                            // Verificar Si Es Anidada 
                            if(this.Anidate)
                            {
                          
                                // Crear Temporal 
                                String Temporary_1 = Instancia_1.CreateTemporary();

                                // Limpiar Temporal 
                                Instancia_1.DeleteTemporary(Temporary_1);

                                // Auxiliary Size 
                                int EnvChange = Env.GetPositionVar(this.Value.ToString());
                      
                                // Obtener Valor Restar
                                Instancia_1.AddTwoExpression(Temporary_1, "SP", "-", EnvChange.ToString(), "Dos");

                                // Añadir Expression 
                                Instancia_1.AddTwoExpression(TemporaryAuxiliary, Temporary_1, "+", ActualVar.GetValue(), "Dos");

                            }
                            else
                            {

                                // Añadir Expression 
                                Instancia_1.AddTwoExpression(TemporaryAuxiliary, "SP", "+", ActualVar.GetValue(), "Dos");

                            }

                        }

                        // Obtener Valor Del Stack 
                        Instancia_1.GetValueOfStack(TemporaryAuxiliary, Temporary, InsAuxiliary);
            
                        // Verificar Si Es Boolean 
                        if (ActualVar.Type.Equals("boolean"))
                        {

                            // Crear Etiquetas 
                            if (this.BoolTrue.Equals(""))
                            {

                                // Setear Valor 
                                this.BoolTrue = Instancia_1.CreateLabel();
                            
                            }

                            // Crear Etiquetas 
                            if (this.BoolFalse.Equals(""))
                            {

                                // Setear Valor 
                                this.BoolFalse = Instancia_1.CreateLabel();

                            }

                            // Agregar Salto Condicional
                            Instancia_1.AddConditionalJump(Temporary, "==", "1", this.BoolTrue, InsAuxiliary);

                            // Agregar IDentacion 
                            Instancia_1.AddIdent();

                            // Agregar Salot No Condicional 
                            Instancia_1.AddNonConditionalJump(this.BoolFalse, InsAuxiliary);

                            // ELiminar Identacion 
                            Instancia_1.DeleteIdent();

                            // Retornar Objecto 
                            AuxiliaryReturn = new ObjectReturn("", ActualVar.Type)
                            {

                                BoolTrue = this.BoolTrue,
                                BoolFalse = this.BoolFalse

                            };

                        }
                        else
                        {

                            AuxiliaryReturn = new ObjectReturn(Temporary, ActualVar.Type)
                            {

                                Temporary = true
                            
                            };    
                        
                        }                        

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Variable Indicada " + this.Value.ToString() + " No Existe En El Contexto Actual", 0, 0));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                        // Retornar Objecto 
                        AuxiliaryReturn = null;

                    }

                }
                else
                {

                    // Crear Temporal 
                    String ActualTemporary = Instancia_1.CreateTemporary();

                    // Agregar Comentario
                    Instancia_1.AddCommentOneLine("Almacenar String En El Heap (Print)", CommentAuxiliary);

                    // Obtener Puntero Heap 
                    Instancia_1.AddOneExpression(ActualTemporary, "HP", InsAuxiliary);

                    // Variable Ascii
                    int Ascii;

                    // Insertar Valores Al Heap 
                    foreach (Char Letter in this.Value.ToString())
                    {

                        // Valor Ascii
                        Ascii = (int)Letter;

                        // Agregar Valor A Heap
                        Instancia_1.AddValueToHeap("HP", Ascii.ToString(), InsAuxiliary);

                        // Mover Puntero 
                        Instancia_1.MovePointerHeap(InsAuxiliary);

                    }

                    // Agregar Valor A Heap
                    Instancia_1.AddValueToHeap("HP", "-1", InsAuxiliary);

                    // Mover Puntero 
                    Instancia_1.MovePointerHeap(InsAuxiliary);

                    // Agregar A Objecto Valor
                    AuxiliaryReturn = new ObjectReturn(ActualTemporary, "string")
                    {

                        Temporary = true

                    };

                }

            }
            else
            {

                // Verificar Tipo
                if (int.TryParse(this.Value.ToString(), out int AuxiliaryValueI))
                {

                    // Agreagr A Objecto Valor 
                    AuxiliaryReturn = new ObjectReturn(AuxiliaryValueI, "integer");

                }
                else if (Decimal.TryParse(this.Value.ToString(), out Decimal AuxiliaryValueD))
                {

                    // Agregar A Objecto Valor 
                    AuxiliaryReturn = new ObjectReturn(AuxiliaryValueD, "real");

                }
                else if (bool.TryParse(this.Value.ToString(), out bool AuxiliaryValueB))
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
                    Instance_1.AddCommentOneLine("Salto Condición Bool", CommentAuxiliary);

                    // Verificar Valor 
                    if (AuxiliaryValueB)
                    {

                        // Agregar Goto 
                        Instance_1.AddNonConditionalJump(this.BoolTrue, InsAuxiliary);

                    }
                    else
                    {

                        // Agregar Goto 
                        Instance_1.AddNonConditionalJump(this.BoolFalse, InsAuxiliary);

                    }

                    // Pendiente
                    AuxiliaryReturn = new ObjectReturn("", "boolean")
                    {

                        BoolTrue = this.BoolTrue,
                        BoolFalse = this.BoolFalse

                    };

                }

            }

            // Retornar 
            return AuxiliaryReturn;

        }

    }

}