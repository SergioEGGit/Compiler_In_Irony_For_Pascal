// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
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

        // Constructor 
        public PrimitiveValue(object Value, String StringType) {

            // Inicicalizar Valores  
            this.Value = Value;
            this.StringType = StringType;
        
        }

        // Métodod Ejecutar 
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Objecto A Retornar
            ObjectReturn AuxiliaryReturn;
            
            // Verificar Que Tipo De Valor Primtivo ES 
            if(int.TryParse(this.Value.ToString(), out int AuxiliaryValueI))
            {

                // Agreagr A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueI, "integer");

            }
            else if(Decimal.TryParse(this.Value.ToString(), out Decimal AuxiliaryValueD))
            {

                // Agregar A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueD, "real");

            }
            else if(this.Value.ToString() == "true")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(true, "boolean");

            }
            else if(this.Value.ToString() == "false")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(false, "boolean");

            }
            else
            {

                // Verificar Tipo
                if(this.StringType.Equals("Identifier")) 
                {

                    // Buscar Variable 
                    SymbolTable ActualVar = Env.GetVariable(this.Value.ToString());

                    // Obtener Variable 
                    if(ActualVar != null) 
                    {

                        ObjectReturn ActualValue = (ObjectReturn) ActualVar.Value;

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
            ObjectReturn AuxiliaryReturn;

            // Verificar Que Tipo De Valor Primtivo ES 
            if(int.TryParse(this.Value.ToString(), out int AuxiliaryValueI))
            {

                // Agreagr A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueI, "integer");

            }
            else if(Decimal.TryParse(this.Value.ToString(), out Decimal AuxiliaryValueD))
            {

                // Agregar A Objecto Valor 
                AuxiliaryReturn = new ObjectReturn(AuxiliaryValueD, "real");

            }
            else if (this.Value.ToString() == "true")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(1, "boolean");

            }
            else if (this.Value.ToString() == "false")
            {

                // Agregar A Objecto Valor
                AuxiliaryReturn = new ObjectReturn(0, "boolean");

            }
            else
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

                    // Obtener Instancia 
                    ThreeAddressCode Instancia_1 = ThreeAddressCode.GetInstance;

                    // Crear Temporal 
                    String ActualTemporary = Instancia_1.CreateTemporary();

                    // Obtener Punter 
                    Instancia_1.CreateOneExpression();

                    // Agregar A Objecto Valor
                    AuxiliaryReturn = new ObjectReturn(this.Value.ToString(), "string");

                }

            }

            // Retornar 
            return AuxiliaryReturn;

        }

    }

}