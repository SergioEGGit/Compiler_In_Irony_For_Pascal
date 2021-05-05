// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{
    class PrimitiveDeclaration : AbstractInstruccion
    {
        
        // Atributos
        
        // Identifiers 
        private readonly String Identifiers;

        // Tipo 
        private readonly String Type;

        // Tipo De Declaracion 
        private readonly String DecType;

        // Valor
        private readonly AbstractExpression Value;

        // Token Linea
        private readonly int TokenLine;

        // Token Columna
        private readonly int TokenColumn;
        
        // Constructor
        public PrimitiveDeclaration(String Identifiers, String Type, AbstractExpression Value, String DecType, int TokenLine, int TokenColumn) {

            // Inicializar Valores 
            this.Identifiers = Identifiers;
            this.Type = Type;
            this.Value = Value;
            this.DecType = DecType;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Si Las Constantes Tiene Un Valor 
            if (this.DecType == "Const" && this.Value == null)
            {

                // Agregar Errores Constantes 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Una Constante Siempre Debe De Ser Inicializada Con Un Valor", this.TokenLine, this.TokenColumn));

            }
            else if (this.DecType == "Const" && this.Value != null)
            {

                // Variable Auxiliar 
                bool AuxiliaryReturn;

                // Objeto 
                ObjectReturn Value = this.Value.Execute(Env);

                // Agregar Variable 
                AuxiliaryReturn = Env.AddVariable(Identifiers, Value.Type.ToString(), Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                // Si Existe Error 
                AddError(AuxiliaryReturn, Identifiers);

            }
            else if(this.DecType == "Var") {

                // Arreglo De Identificadores 
                String[] Identifiers = this.Identifiers.Split(',');

                // Verificar Si Se Asigna A Un Solo ID
                if (this.Value != null && Identifiers.Length > 1)
                {

                    // Agregar Error Variables 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "Unicamente Se Puede Realizar Una Asignacion A Un Unico Identificador", this.TokenLine, this.TokenColumn));

                }
                else 
                {

                    // Recorrer Todos Los Identificadores
                    foreach (String Identifier in Identifiers)
                    {

                        if (this.Value == null && this.Identifiers.Length > 0)
                        {

                            // Variable Auxiliar 
                            bool AuxiliaryReturn;

                            // Objeto 
                            ObjectReturn Value = new ObjectReturn("", ""); ;

                            if (this.Type == "integer")
                            {

                                // Crear Valor 
                                Value.Type = "integer";
                                Value.Value = 0;

                                // Verificar Si La Variable Existe O NO 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn, Identifier);

                            }
                            else if (this.Type == "string")
                            {

                                // Crear Valor 
                                Value.Type = "string";
                                Value.Value = "";

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn, Identifier);

                            }
                            else if (this.Type == "boolean")
                            {

                                // Crear Valor 
                                Value.Type = "boolean";
                                Value.Value = false;

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn, Identifier);

                            }
                            else if (this.Type == "real")
                            {

                                // Crear Valor 
                                Value.Type = "real";
                                Value.Value = 0.0;

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn, Identifier);

                            }
                            else
                            {

                                //Tipos distintos

                            }

                        }
                        else
                        {

                            // Objeto 
                            ObjectReturn Value = this.Value.Execute(Env);

                            // Verififcar Que no Sea Nulo 
                            if (Value != null)
                            {

                                // Verificar Que Sean Del Mismo Tipo
                                if(Value.Type.ToString().Equals("integer") && this.Type.ToString().Equals("real")) {

                                    // Variable Auxiliar 
                                    bool AuxiliaryReturn;

                                    // Convertir Value
                                    Value.Value = Decimal.Parse(Value.Value.ToString());

                                    // Convertir Typo 
                                    Value.Type = "real";

                                    // Verificar Si La Variable Existe O No 
                                    AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                    // Si Existe Error 
                                    AddError(AuxiliaryReturn, Identifier);

                                }
                                else if (Value.Type.ToString().Equals(this.Type.ToString()))
                                {

                                    // Variable Auxiliar 
                                    bool AuxiliaryReturn;

                                    // Verificar Si La Variable Existe O No 
                                    AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn);

                                    // Si Existe Error 
                                    AddError(AuxiliaryReturn, Identifier);

                                }
                                else
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Valor Asignado (" + Value.Type.ToString() + ") No Coinicide con El Tipo De Variable (" + this.Type.ToString() + ")", this.TokenLine, this.TokenColumn));

                                }

                            }

                        }
                    }

                }                

            }
            
            // Retornar Null
            return null;
        }

        // Método Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Obtener Identificadores
            String[] IdentifierList = this.Identifiers.Split(',');

            // Agregar Identacion
            VariablesMethods.TranslateString += VariablesMethods.Ident();

            // Contador Auxiliar
            int AuxiliaryCounter = 1;

            // Recorrer Ids
            foreach(String Identifier in IdentifierList) 
            {

                // Verificar Si Es El Ultimo
                if(AuxiliaryCounter == IdentifierList.Length)
                {

                    // Agregar Sin Coma 
                    VariablesMethods.TranslateString += Identifier;

                }
                else 
                {

                    // Agregar Traduccion 
                    VariablesMethods.TranslateString += Identifier + ", ";

                }

                AuxiliaryCounter += 1;

            }

            // Verificar Si Es Variable O Constante
            if(this.DecType.Equals("Var")) 
            {

                // Agregar Tipo y Resto Traducción
                VariablesMethods.TranslateString += " : " + this.Type.ToString();

            }

            // Verificar Si Hay Un Valor
            if(this.Value != null) 
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += " = ";

                // Ejecutar Metodo Traducir 
                this.Value.Translate(Env);

            }

            // Agregar Final 
            VariablesMethods.TranslateString += "; \n";

            // Retonar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Obtener Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Variables Auxiliares 
            String CommentAuxiliary;
            String InsAuxiliary;

            // Verificar Si EStoy En Global
            if(Env.EnviromentName.Equals("Env_Global"))
            {

                // Agregar Valores 
                CommentAuxiliary = "Uno Global";
                InsAuxiliary = "Dos Global";
                Instance_1.AddIdent();

            }
            else
            {

                // Agregar Valores 
                CommentAuxiliary = "Uno";
                InsAuxiliary = "Dos";

            }

            // Verificar Si EStoy En Global 

            // Arreglo De Identificadores 
            String[] Identifiers = this.Identifiers.Split(',');            

            // Agregar Comentarios 
            Instance_1.AddCommentOneLine("Declaración De Variables", CommentAuxiliary);

            // Recorrer Todos Los Identificadores
            foreach(String Identifier in Identifiers)
            {

                // Añadir Comentario 
                Instance_1.AddCommentOneLine("Variable O Constante: " + Identifier, CommentAuxiliary);

                // Verificar Si Son Valores Por Defecto
                if(this.Value == null && this.Identifiers.Length > 0)
                {

                    // Crear Temporal 
                    String Temporary = Instance_1.CreateTemporary();

                    // Limpiar Temporal 
                    Instance_1.DeleteTemporary(Temporary); 

                    // Verificar Tipo
                    if (this.Type == "integer")
                    {

                        SymbolTable ActualVar = null;

                        // Verificar Si Es Global 
                        if (Env.EnviromentName.Equals("Env_Global"))
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(ActualVar.Value.ToString(), "0", InsAuxiliary);

                        }
                        else
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            // Obtener Posicioin Del Stack Para Guardar La VAriable
                            Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.Value.ToString(), InsAuxiliary);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, "0", InsAuxiliary);

                        }

                    }
                    else if (this.Type == "string")
                    {

                        SymbolTable ActualVar = null;

                        // Verificar Si Es Global 
                        if (Env.EnviromentName.Equals("Env_Global"))
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            // Crear Temporal 
                            String TemporaryHeap = Instance_1.CreateTemporary();

                            // Eliminar TEmporal 
                            Instance_1.DeleteTemporary(TemporaryHeap);

                            // Agregar String A Heap 
                            Instance_1.AddOneExpression(TemporaryHeap, "HP", InsAuxiliary);

                            // Agregar Valor A Heap 
                            Instance_1.AddValueToHeap("HP", "-1", InsAuxiliary);

                            // Mover Heap 
                            Instance_1.MovePointerHeap(InsAuxiliary);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(ActualVar.GetValue().ToString(), TemporaryHeap, InsAuxiliary);

                        }
                        else
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            // Obtener Posicioin Del Stack Para Guardar La VAriable
                            Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.Value.ToString(), InsAuxiliary);

                            // Crear Temporal 
                            String TemporaryHeap = Instance_1.CreateTemporary();

                            // Eliminar TEmporal 
                            Instance_1.DeleteTemporary(TemporaryHeap);

                            // Agregar String A Heap 
                            Instance_1.AddOneExpression(TemporaryHeap, "HP", InsAuxiliary);

                            // Agregar Valor A Heap 
                            Instance_1.AddValueToHeap("HP", "-1", InsAuxiliary);

                            // Mover Heap 
                            Instance_1.MovePointerHeap(InsAuxiliary);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, TemporaryHeap, InsAuxiliary);

                        }
                        
                    }
                    else if (this.Type == "boolean")
                    {

                        SymbolTable ActualVar = null;

                        // Verificar Si Es Global 
                        if (Env.EnviromentName.Equals("Env_Global"))
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            // Crear Label 
                            String ActualLabel = Instance_1.CreateLabel();
                            String TrueLabel = Instance_1.CreateLabel();
                            String FalseLabel = Instance_1.CreateLabel();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Agregar Valor Bool A Stack", CommentAuxiliary);

                            // Agregar Salto No Condicional
                            Instance_1.AddNonConditionalJump(FalseLabel, InsAuxiliary);

                            // Agregar Label             
                            Instance_1.AddLabel(TrueLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(ActualVar.GetValue().ToString(), "1", InsAuxiliary);

                            // Añadir Salto 
                            Instance_1.AddNonConditionalJump(ActualLabel, InsAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Agregar Label 
                            Instance_1.AddLabel(FalseLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(ActualVar.GetValue().ToString(), "0", InsAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Añadir Label 
                            Instance_1.AddLabel(ActualLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Añadir Comentario
                            Instance_1.AddCommentOneLine("Fin Declarción Variable Bool\n", CommentAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                        }
                        else
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            // Obtener Posicioin Del Stack Para Guardar La VAriable
                            Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.Value.ToString(), InsAuxiliary);

                            // Crear Label 
                            String ActualLabel = Instance_1.CreateLabel();
                            String TrueLabel = Instance_1.CreateLabel();
                            String FalseLabel = Instance_1.CreateLabel();

                            // Agregar Comentario 
                            Instance_1.AddCommentOneLine("Agregar Valor Bool A Stack", CommentAuxiliary);

                            // Agregar Salto No Condicional
                            Instance_1.AddNonConditionalJump(FalseLabel, InsAuxiliary);

                            // Agregar Label             
                            Instance_1.AddLabel(TrueLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, "1", InsAuxiliary);

                            // Añadir Salto 
                            Instance_1.AddNonConditionalJump(ActualLabel, InsAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Agregar Label 
                            Instance_1.AddLabel(FalseLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, "0", InsAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                            // Añadir Label 
                            Instance_1.AddLabel(ActualLabel, InsAuxiliary);

                            // Añadir Identacion 
                            Instance_1.AddIdent();

                            // Añadir Comentario
                            Instance_1.AddCommentOneLine("Fin Declarción Variable Bool\n", CommentAuxiliary);

                            // Eliminar Identacion 
                            Instance_1.DeleteIdent();

                        }                        

                    }
                    else if (this.Type == "real")
                    {

                        SymbolTable ActualVar = null;

                        // Verificar Si Es Global 
                        if (Env.EnviromentName.Equals("Env_Global"))
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(ActualVar.Value.ToString(), "0.0", InsAuxiliary);

                        }
                        else
                        {

                            // Verificar Si La Variable Existe O NO 
                            ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            // Obtener Posicioin Del Stack Para Guardar La VAriable
                            Instance_1.AddTwoExpression(Temporary, "SP", "+", ActualVar.Value.ToString(), InsAuxiliary);

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, "0.0", InsAuxiliary);

                        }

                    }
                    else
                    {

                        //Tipos distintos

                    }

                }
                else
                {

                    // Crear Temporal 
                    String Temporary = Instance_1.CreateTemporary();

                    // Limpiar Temporal 
                    Instance_1.DeleteTemporary(Temporary); 

                    SymbolTable ActualVar = null;

                    // Setear Soy Global
                    if (Env.EnviromentName.Equals("Env_Global"))
                    {

                        // Setear Si Es Global
                        this.Value.IsGlobal = true;

                    }
                    else
                    {

                        // Setear No Global
                        this.Value.IsGlobal = false;
                    
                    }

                    // Verificar Si ES Global 
                    if(Env.EnviromentName.Equals("Env_Global"))
                    {

                        // Obtener Posicioin Del Stack Para Guardar La VAriable
                        Instance_1.AddOneExpression(Temporary, (Env.EnviromentSize).ToString(), InsAuxiliary);

                    }
                    else
                    {

                        // Obtener Posicioin Del Stack Para Guardar La VAriable
                        Instance_1.AddTwoExpression(Temporary, "SP", "+", (Env.EnviromentSize).ToString(), InsAuxiliary);

                    }

                    // Objeto 
                    ObjectReturn Value = this.Value.Compilate(Env);

                    // Verififcar Que no Sea Nulo 
                    if (Value != null)
                    {

                        if (Env.EnviromentName.Equals("Env_Global"))
                        {

                            // Verificar Si Es Constante
                            if (this.DecType.Equals("Const"))
                            {

                                // Verificar Si La Variable Existe O No 
                                ActualVar = Env.AddVariableStack(Identifier, Value.Type.ToString(), this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            }
                            else
                            {

                                // Verificar Si La Variable Existe O No 
                                ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, true);

                            }

                        }
                        else 
                        {

                            // Verificar Si Es Constnate 
                            if (this.DecType.Equals("Const"))
                            {

                                // Verificar Si La Variable Existe O No 
                                ActualVar = Env.AddVariableStack(Identifier, Value.Type.ToString(), this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            }
                            else
                            {

                                // Verificar Si La Variable Existe O No 
                                ActualVar = Env.AddVariableStack(Identifier, this.Type, this.DecType, Env.EnviromentName, this.TokenLine, this.TokenColumn, false);

                            }

                        }

                        // Verificar Que Sean Del Mismo Tipo
                        if (Value.Type.ToString().Equals("integer") && this.Type.ToString().Equals("real"))
                        {

                            // Agregar A Stack 
                            Instance_1.AddValueToStack(Temporary, Value.GetValue(), InsAuxiliary);

                        }
                        else
                        {

                            if(Value.Type.Equals("boolean")) 
                            {

                                // Crear Label 
                                String ActualLabel = Instance_1.CreateLabel();

                                // Agregar Comentario 
                                Instance_1.AddCommentOneLine("Agregar Valor Bool A Stack", CommentAuxiliary);

                                // Agregar Label             
                                Instance_1.AddLabel(Value.BoolTrue, InsAuxiliary);

                                // Añadir Identacion 
                                Instance_1.AddIdent();

                                // Agregar A Stack 
                                Instance_1.AddValueToStack(Temporary, "1", InsAuxiliary);

                                // Añadir Salto 
                                Instance_1.AddNonConditionalJump(ActualLabel, InsAuxiliary);

                                // Eliminar Identacion 
                                Instance_1.DeleteIdent();

                                // Agregar Label 
                                Instance_1.AddLabel(Value.BoolFalse, InsAuxiliary);

                                // Añadir Identacion 
                                Instance_1.AddIdent();

                                // Agregar A Stack 
                                Instance_1.AddValueToStack(Temporary, "0", InsAuxiliary);

                                // Eliminar Identacion 
                                Instance_1.DeleteIdent();

                                // Añadir Label 
                                Instance_1.AddLabel(ActualLabel, InsAuxiliary);

                                // Añadir Identacion 
                                Instance_1.AddIdent();

                                // Añadir Comentario
                                Instance_1.AddCommentOneLine("Fin Declarción Variable Bool\n", CommentAuxiliary);

                                // Eliminar Identacion 
                                Instance_1.DeleteIdent();

                            }
                            else 
                            {

                                // Agregar A Stack 
                                Instance_1.AddValueToStack(Temporary, Value.GetValue(), InsAuxiliary);

                            }

                        }

                    }

                }
            }

            // Verificar Si EStoy En Global
            if(Env.EnviromentName.Equals("Env_Global"))
            {

                // Agregar Valores 
                Instance_1.DeleteIdent();

            }

            // Retornar Null
            return null;

        }

        // Indicar Error
        private void AddError(bool IsError, String Identifier) {
            
            // Verificar Si Hay Error
            if (IsError == false) {

                // Agregar Error
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semático", "La Variable O Constante (" + Identifier + ") Ya Existe En El Ambito", this.TokenLine, this.TokenColumn));
            
            }

        }

    }

}