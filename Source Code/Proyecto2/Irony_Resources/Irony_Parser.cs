// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Irony.Parsing;
using Proyecto2.TranslatorAndInterpreter;
using Proyecto2.Misc;
using System.Collections;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto2.Irony_Resources
{

    // Clase Irony_Parser
    class Irony_Parser
    {

        // Analizador Sintactico 

        // Inicio 
        public void Begin(ParseTreeNode RootNode)
        {

            // Verficar Camino De La Produccion
            if (RootNode.ChildNodes.Count == 3)
            {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes[0]);

                // Lista De Delcaraciones 
                Declarations(RootNode.ChildNodes[1]);

                // Main Block
                MainBlock(RootNode.ChildNodes[2]);

            }
            else if (RootNode.ChildNodes.Count == 2)
            {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes[0]);

                // Lista De Delcaraciones 
                MainBlock(RootNode.ChildNodes[1]);

            }
            else
            {

                // No Se Hace Nada 
                // Archivo Vacio

            }

        }

        // Instruccion Program
        public void InsProgram(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Agregar Clase A La Lista 
                VariablesMethods.TranslateList.AddLast(new InsProgram(SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default"), ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1));

            }

        }

        // Lista De Declaraciones 
        public void Declarations(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Delcaraciones
                Declarations(ActualNode.ChildNodes[0]);

                // Declaracion
                Declaration(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Declaracion
                Declaration(ActualNode.ChildNodes[0]);

            }

        }

        // Delcaracion  
        public void Declaration(ParseTreeNode ActualNode)
        {

            // Verificar Nombre Del ActualNode 
            if (ActualNode.ChildNodes[0].ToString().Equals("VariablesDeclaration"))
            {

                // Delcaracions De Variables
                VariablesDeclaration(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("ConstantsDeclaration"))
            {

                // Declaraciones De Constantes
                ConstantsDeclaration(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsGraficarTS"))
            {

                // Agregar Instruccion A Lista 
                VariablesMethods.TranslateList.AddLast(InsGraficarTS(ActualNode.ChildNodes[0]));

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("Functions"))
            {

                // Agregar Instruccion A Lista 
                VariablesMethods.TranslateList.AddLast(Functions(ActualNode.ChildNodes[0]));

            }

        }

        // Declaracion De Variables 
        public void VariablesDeclaration(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Agregar Clase Declaracion De Variables 
                VariablesMethods.TranslateList.AddLast(new VariablesDeclaration(null));

                // Bloque Declaracion
                VariablesDeclarationBlock(ActualNode.ChildNodes[1]);

            }

        }

        // Bloque Declaracion
        public void VariablesDeclarationBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Bloque Declaracion
                VariablesDeclarationBlock(ActualNode.ChildNodes[0]);

                // LIsta De Declaraciones
                VariablesDeclarationList(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Lista De Declaraciones 
                VariablesDeclarationList(ActualNode.ChildNodes[0]);

            }

        }

        // Lista De Declaraciones
        public void VariablesDeclarationList(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Variables
                int TokenColumn = 0;
                int TokenLine = 0;

                // Obtener Identificadores 
                String Identifiers = DeclarationList(ActualNode.ChildNodes[0]);

                // Obtener Tipo
                String Type = SplitMethod(ActualNode.ChildNodes[2].ChildNodes[0].ToString(), "Default");

                // Expression
                AbstractExpression Expression = VariableAsignationDec(ActualNode.ChildNodes[3]);

                // Verificar Token 
                if (ActualNode.ChildNodes[0].ChildNodes.Count == 3)
                {

                    // Columna Token 
                    TokenColumn = ActualNode.ChildNodes[0].ChildNodes[2].Token.Location.Column + 1;

                    // Fila Token 
                    TokenLine = ActualNode.ChildNodes[2].ChildNodes[0].Token.Location.Line + 1;

                }
                else if (ActualNode.ChildNodes[0].ChildNodes.Count == 1)
                {

                    // Columna Token 
                    TokenColumn = ActualNode.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1;

                    // Fila Token 
                    TokenLine = ActualNode.ChildNodes[2].ChildNodes[0].Token.Location.Line + 1;

                }

                // Agregar A Lista De Clases 
                VariablesMethods.TranslateList.AddLast(new PrimitiveDeclaration(Identifiers, Type, Expression, "Var", TokenColumn, TokenLine));

            }

        }

        // Lista De Identificadores 
        public String DeclarationList(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Obtener Identificadores 
                return DeclarationList(ActualNode.ChildNodes[0]) + "," + SplitMethod(ActualNode.ChildNodes[2].ToString(), "Default");

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Identificador 
                return SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

            }

            // Retornar 
            return "";

        }

        // Asignacion En Declaracion
        public AbstractExpression VariableAsignationDec(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Retornar Expression
                return Expression(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Sin Asignacion
                return null;

            }

            // Retornar 
            return null;

        }

        // Declaracion De Constantes 
        public void ConstantsDeclaration(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Agregar Clase Declaracion De Variables 
                VariablesMethods.TranslateList.AddLast(new ConstantsDeclaration(null));

                // Bloque Declaracion
                ConstantsDeclarationBlock(ActualNode.ChildNodes[1]);

            }

        }

        // Bloque Declaracion
        public void ConstantsDeclarationBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Bloque Declaracion
                ConstantsDeclarationBlock(ActualNode.ChildNodes[0]);

                // LIsta De Declaraciones
                Constants(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Lista De Declaraciones 
                Constants(ActualNode.ChildNodes[0]);

            }

        }

        // Constantes
        public void Constants(ParseTreeNode ActualNode)
        {

            // Vefiicar Tamaño
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Variables 
                int TokenLine = ActualNode.ChildNodes[0].Token.Location.Line + 1;
                int TokenColumn = ActualNode.ChildNodes[0].Token.Location.Column + 1;

                // Obtener Identificadores 
                String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[2]);

                // Agregar A Lista De Clases 
                VariablesMethods.TranslateList.AddLast(new PrimitiveDeclaration(Identifier, "", Expression_, "Const", TokenColumn, TokenLine));

            }

        }

        // Main Block
        public void MainBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Agregar Instrucciones
                VariablesMethods.TranslateList.AddLast(new MainBlock(Instruccions(ActualNode.ChildNodes[1]), ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column));

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                // Agregar Bloque Sin Instrucciones
                VariablesMethods.TranslateList.AddLast(new MainBlock(null, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column));

            }

        }

        // Lista De Instrucciones
        public LinkedList<AbstractInstruccion> Instruccions(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Instrucciones
                LinkedList<AbstractInstruccion> AuxiliaryList = Instruccions(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(Instruccion(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<AbstractInstruccion> AuxiliaryList = new LinkedList<AbstractInstruccion>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(Instruccion(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar Null
            return null;

        }

        // Instruccion
        public AbstractInstruccion Instruccion(ParseTreeNode ActualNode)
        {

            // Verificar Instruccion
            if (ActualNode.ChildNodes[0].ToString().Equals("InsWrite"))
            {

                // Instruccion Write 
                return InsWrite(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsGraficarTS"))
            {

                // Instruccion GraficarTS
                return InsGraficarTS(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsIf"))
            {

                // Instruccion If
                return InsIf(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsWhile"))
            {

                // Instruccion While
                return InsWhile(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsRepeat"))
            {

                // Instruccion Repeat
                return InsRepeat(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsFor"))
            {

                // Instruccion For
                return InsFor(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("VariablesAsignation"))
            {

                // Instruccion For
                return VariablesAsignation(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("TransferSentences"))
            {

                // Instruccion For
                return TransferSentences(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsCase"))
            {

                // Instruccion For
                return InsCase(ActualNode.ChildNodes[0]);

            }

            // Retornar
            return null;

        }

        // Instruccion Write
        public AbstractInstruccion InsWrite(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 5 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("writeln"))
            {

                // Retornar Instruccion
                return new InsWrite("WriteLine", ParamsValueList(ActualNode.ChildNodes[2]), "", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 4 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("writeln"))
            {

                // Retornar Instruccion
                return new InsWrite("WriteLine", null, "2", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 2 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("writeln"))
            {

                // Retornar Instruccion
                return new InsWrite("WriteLine", null, "", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 5 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("write"))
            {

                // Retornar Instruccion
                return new InsWrite("Write", ParamsValueList(ActualNode.ChildNodes[2]), "", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 4 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("write"))
            {

                // Retornar Instruccion
                return new InsWrite("Write", null, "2", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 2 && SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("write"))
            {

                // Retornar Instruccion
                return new InsWrite("Write", null, "", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            // Retornar 
            return null;

        }

        // Lista De Valores Parametros
        public LinkedList<AbstractExpression> ParamsValueList(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Lista De Instrucciones
                LinkedList<AbstractExpression> AuxiliaryList = ParamsValueList(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(Expression(ActualNode.ChildNodes[2]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<AbstractExpression> AuxiliaryList = new LinkedList<AbstractExpression>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(Expression(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar 
            return null;

        }

        // Instruccion Graficar
        public AbstractInstruccion InsGraficarTS(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Retornar Instruccion
                return new InsGraficarTS("");

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {

                // Retornar Instruccion
                return new InsGraficarTS("2");

            }

            // Retornar Null
            return null;

        }

        // Instruccion If 
        public AbstractInstruccion InsIf(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 5)
            {

                // Obtener Expression 
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[1]);

                // Lista De Instruccioens 
                LinkedList<AbstractInstruccion> AuxiliaryList = IfBlock(ActualNode.ChildNodes[3]);

                // Else 
                AbstractInstruccion Else_ = InsElse(ActualNode.ChildNodes[4]);

                // Retornar Clase 
                return new InsIf(Expression_, AuxiliaryList, Else_, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            return null;

        }

        // Bloque If 
        public LinkedList<AbstractInstruccion> IfBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 3)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Instruccion Else 
        public AbstractInstruccion InsElse(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Retornar Instruccion
                return new InsElse("Else", IfBlock(ActualNode.ChildNodes[1]), null, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {

                // Retornar Instruccion
                return new InsElse("ElseIf", null, InsIf(ActualNode.ChildNodes[1]), ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Retornar Instruccion
                return new InsElse("If", null, null, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            // Retornar 
            return null;

        }

        // Instruccion While 
        public AbstractInstruccion InsWhile(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Obtener Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[1]);

                // Lista De Instrucciones 
                LinkedList<AbstractInstruccion> AuxiliaryList = WhileBlock(ActualNode.ChildNodes[3]);

                // Agregar Clase 
                return new InsWhile(Expression_, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);


            }

            // Retornar 
            return null;

        }

        // Bloque While
        public LinkedList<AbstractInstruccion> WhileBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Instruccion Repeat 
        public AbstractInstruccion InsRepeat(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 5)
            {

                // Obtener Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[3]);

                // Lista De Instrucciones 
                LinkedList<AbstractInstruccion> AuxiliaryList = RepeatBlock(ActualNode.ChildNodes[1]);

                // Agregar Clase 
                return new InsRepeat(Expression_, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            // Retornar 
            return null;

        }

        // Bloque Repeat
        public LinkedList<AbstractInstruccion> RepeatBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Instruccion For 
        public AbstractInstruccion InsFor(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 9)
            {

                // Obtener Identificador 
                String Identifier = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Obtener Primera Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[4]);

                // Obtener Segunda Expression
                AbstractExpression Expression__ = Expression(ActualNode.ChildNodes[6]);

                // Obtener Lista De Instruccions
                LinkedList<AbstractInstruccion> AuxiliaryList = ForBlock(ActualNode.ChildNodes[8]);

                // Retornar Instruccion
                return new InsFor(Identifier, Expression_, Expression__, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column, SplitMethod(ActualNode.ChildNodes[5].ToString(), "Default"));

            }

            // Retornar 
            return null;

        }

        // Bloque For
        public LinkedList<AbstractInstruccion> ForBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Asignacion De Varaibles 
        public AbstractInstruccion VariablesAsignation(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 5)
            {

                // Verificar Si Es Function Call
                if (SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default").Equals(":"))
                {

                    // Obtener String
                    String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                    // Expression
                    AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[3]);

                    // Retorno Instruccion 
                    return new VariablesAsignation(Identifier, Expression_, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

                }
                else
                {

                    // Obtener String
                    String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                    // Array De Expresoines 
                    LinkedList<AbstractExpression> AuxiliaryList = ParamsValueList(ActualNode.ChildNodes[2]);

                    // Retorno Instruccion 
                    return new FunctionCall(Identifier, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);


                }


            }
            else if(ActualNode.ChildNodes.Count == 4)
            {
               
                // Obtener String
                String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Retorno Instruccion 
                return new FunctionCall(Identifier, null, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);
            
            }
                
            // Retornar Null
            return null;

        }

        // Sentecias De transferecencia 
        public AbstractInstruccion TransferSentences(ParseTreeNode ActualNode)
        {

            // Verificar Tipo
            if (SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("break"))
            {

                // Retornar Instruccion
                return new TransFerSentences("Break", null);

            }
            else if (SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("continue"))
            {

                // Retornar Instruccion
                return new TransFerSentences("Continue", null);

            }
            else if (SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default").Equals("exit"))
            {

                // Retornar Instruccion
                return new TransFerSentences("Return", Expression(ActualNode.ChildNodes[2]));

            }

            // Retornar 
            return null;

        }

        // Instruccion Case 
        public AbstractInstruccion InsCase(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Obtener Expression 
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[1]);

                // Lista De Cases 
                LinkedList<AbstractInstruccion> CaseList = Cases(ActualNode.ChildNodes[3]);

                // Retornar Clase 
                return new InsCase(Expression_, CaseList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            // Retornar 
            return null;

        }

        // LIsta De Cases 
        public LinkedList<AbstractInstruccion> Cases(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Obtener Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[0]);

                // Lista De Instrucciones 
                LinkedList<AbstractInstruccion> AuxiliaryList = CaseBlock(ActualNode.ChildNodes[2]);

                // Crear Nueva Lista 
                LinkedList<AbstractInstruccion> CaseList = CaseElse(ActualNode.ChildNodes[3]);

                // Agregar Instruccion
                CaseList.AddFirst(new Cases(Expression_, AuxiliaryList, ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column));

                // Retornar Lista 
                return CaseList;

            }

            // Retornar
            return null;

        }

        // Case Else 
        public LinkedList<AbstractInstruccion> CaseElse(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Esle 
                LinkedList<AbstractInstruccion> ElseList = new LinkedList<AbstractInstruccion>();

                // Obtener Lista De Instrucioens 
                LinkedList<AbstractInstruccion> AuxiliaryList = CaseBlock(ActualNode.ChildNodes[1]);

                // Agregar A Lista 
                ElseList.AddLast(new Cases(null, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column));

                // Retornar Lista 
                return ElseList;

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {

                // Retornar Null
                LinkedList<AbstractInstruccion> CasesList = new LinkedList<AbstractInstruccion>();

                // Retornar
                return CasesList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Instrucciones 
                LinkedList<AbstractInstruccion> CasesList = Cases(ActualNode.ChildNodes[0]);

                // Retornar Lista 
                return CasesList;

            }

            // Retornar Null
            return null;

        }

        // Bloque De Instrucciones 
        public LinkedList<AbstractInstruccion> CaseBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Declaracion De Funciones 
        public AbstractInstruccion Functions(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 9)
            {

                // Obtener Identificador 
                String Identifier = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Type
                String Type = SplitMethod(ActualNode.ChildNodes[5].ChildNodes[0].ToString(), "Default");

                // Listado Declaraciones 
                LinkedList<AbstractInstruccion> DeclarationsListFunc = FunctionsDeclarations(ActualNode.ChildNodes[7]); ;

                // Listado De Instruccioens 
                LinkedList<AbstractInstruccion> InstruccionsList = FunctionsBlock(ActualNode.ChildNodes[8]);

                // Nueva Clase 
                return new FunctionsDeclaration("Function", Identifier, Type, null, DeclarationsListFunc, InstruccionsList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 7)
            {

                // Obtener Identificador 
                String Identifier = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Listado Declaraciones 
                LinkedList<AbstractInstruccion> DeclarationsListFunc = FunctionsDeclarations(ActualNode.ChildNodes[5]);

                // Listado De Instruccioens 
                LinkedList<AbstractInstruccion> InstruccionsList = FunctionsBlock(ActualNode.ChildNodes[6]);

                // Nueva Clase 
                return new FunctionsDeclaration("Procedure", Identifier, "void", null, DeclarationsListFunc, InstruccionsList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 10)
            {

                // Obtener Identificador 
                String Identifier = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Type
                String Type = SplitMethod(ActualNode.ChildNodes[6].ChildNodes[0].ToString(), "Default");

                // Listado De Parametros 
                LinkedList<ObjectReturn> ParamsList = ParamListDeclaration(ActualNode.ChildNodes[3]);

                // Listado Declaraciones 
                LinkedList<AbstractInstruccion> DeclarationsListFunc = FunctionsDeclarations(ActualNode.ChildNodes[8]);

                // Listado De Instruccioens 
                LinkedList<AbstractInstruccion> InstruccionsList = FunctionsBlock(ActualNode.ChildNodes[9]);

                // Nueva Clase 
                return new FunctionsDeclaration("Function", Identifier, Type, ParamsList, DeclarationsListFunc, InstruccionsList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else if (ActualNode.ChildNodes.Count == 8)
            {

                // Obtener Identificador 
                String Identifier = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Listado De Parametros 
                LinkedList<ObjectReturn> ParamsList = ParamListDeclaration(ActualNode.ChildNodes[3]);

                // Listado Declaraciones 
                LinkedList<AbstractInstruccion> DeclarationsListFunc = FunctionsDeclarations(ActualNode.ChildNodes[6]);

                // Listado De Instruccioens 
                LinkedList<AbstractInstruccion> InstruccionsList = FunctionsBlock(ActualNode.ChildNodes[7]);

                // Nueva Clase 
                return new FunctionsDeclaration("Procedure", Identifier, "void", ParamsList, DeclarationsListFunc, InstruccionsList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }

            // Retornar 
            return null;

        }

        // LIsta De Declaraciones 
        public LinkedList<AbstractInstruccion> FunctionsDeclarations(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 1)
            {

                // Declaraciones
                return DeclarationsFunc(ActualNode.ChildNodes[0]);

            }
            else
            {

                // Empty 
                return null;

            }

        }

        // Bloque Funcioens 
        public LinkedList<AbstractInstruccion> FunctionsBlock(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                return Instruccions(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 3)
            {

                return null;

            }

            // Retornar 
            return null;

        }

        // Declaracion De Parametros 
        public LinkedList<ObjectReturn> ParamListDeclaration(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Instrucciones
                LinkedList<ObjectReturn> AuxiliaryList = ParamListDeclaration(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(ParamDecList(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<ObjectReturn> AuxiliaryList = new LinkedList<ObjectReturn>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(ParamDecList(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar Null
            return null;

        }

        // Lista De Paramtros 
        public ObjectReturn ParamDecList(ParseTreeNode ActualNode)
        {

            // Verificar CAmino A Seguir 
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Obtener Strings 
                String Identifiers = ParamsDec(ActualNode.ChildNodes[0]);

                // Obtener Tipo
                String Type = SplitMethod(ActualNode.ChildNodes[2].ChildNodes[0].ToString(), "Default");

                // Obtener End 
                String End = ParamEnd(ActualNode.ChildNodes[3]);

                // Object Return
                ObjectReturn AuxiliaryObject = new ObjectReturn(Identifiers, Type)
                {

                    // Agregar End
                    End = End

                };

                // Retornar Objecto
                return AuxiliaryObject;

            }

            return null;

        }

        // Paremtros 
        public String ParamsDec(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Obtener Identificadores 
                return DeclarationList(ActualNode.ChildNodes[0]) + "," + SplitMethod(ActualNode.ChildNodes[2].ToString(), "Default");

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {
               
                // Identificador 
                return "var " + SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Identificador 
                return SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

            }

            // Retornar 
            return "";

        }

        // Param End
        public String ParamEnd(ParseTreeNode ActualNode)
        {

            // Verificar Camino A SEguir 
            if (ActualNode.ChildNodes.Count > 0)
            {

                // Retornar 
                return "End";

            }
            else
            {

                // Retornar 
                return "Empty";

            }

        }

        // Declaraciones Funciones 
        public LinkedList<AbstractInstruccion> DeclarationsFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Instrucciones
                LinkedList<AbstractInstruccion> AuxiliaryList = DeclarationsFunc(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(DeclarationFunc(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<AbstractInstruccion> AuxiliaryList = new LinkedList<AbstractInstruccion>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(DeclarationFunc(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar Null
            return null;

        }

        // Delcaracion  
        public AbstractInstruccion DeclarationFunc(ParseTreeNode ActualNode)
        {

            // Verificar Nombre Del ActualNode 
            if (ActualNode.ChildNodes[0].ToString().Equals("VariablesDeclaration"))
            {

                // Delcaracions De Variables
                return VariablesDeclarationFunc(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("ConstantsDeclaration"))
            {

                // Declaraciones De Constantes
                return ConstantsDeclarationFunc(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("InsGraficarTS"))
            {

                // Agregar Instruccion A Lista 
                return InsGraficarTS(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes[0].ToString().Equals("Functions"))
            {

                // Agregar Instruccion A Lista 
                return Functions(ActualNode.ChildNodes[0]);

            }

            // Retornar Null
            return null;

        }

        // Declaracion De Variables 
        public AbstractInstruccion VariablesDeclarationFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Agregar Clase Declaracion De Variables 
                return new VariablesDeclaration(VariablesDeclarationBlockFunc(ActualNode.ChildNodes[1]));

            }

            // REtornar 
            return null;

        }

        // Bloque Declaracion
        public LinkedList<AbstractInstruccion> VariablesDeclarationBlockFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Instrucciones
                LinkedList<AbstractInstruccion> AuxiliaryList = VariablesDeclarationBlockFunc(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(VariablesDeclarationListFunc(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<AbstractInstruccion> AuxiliaryList = new LinkedList<AbstractInstruccion>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(VariablesDeclarationListFunc(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar Null
            return null;

        }

        // Lista De Declaraciones
        public AbstractInstruccion VariablesDeclarationListFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Variables
                int TokenColumn = 0;
                int TokenLine = 0;

                // Obtener Identificadores 
                String Identifiers = DeclarationList(ActualNode.ChildNodes[0]);

                // Obtener Tipo
                String Type = SplitMethod(ActualNode.ChildNodes[2].ChildNodes[0].ToString(), "Default");

                // Expression
                AbstractExpression Expression = VariableAsignationDec(ActualNode.ChildNodes[3]);

                // Verificar Token 
                if (ActualNode.ChildNodes[0].ChildNodes.Count == 3)
                {

                    // Columna Token 
                    TokenColumn = ActualNode.ChildNodes[0].ChildNodes[2].Token.Location.Column + 1;

                    // Fila Token 
                    TokenLine = ActualNode.ChildNodes[2].ChildNodes[0].Token.Location.Line + 1;

                }
                else if (ActualNode.ChildNodes[0].ChildNodes.Count == 1)
                {

                    // Columna Token 
                    TokenColumn = ActualNode.ChildNodes[0].ChildNodes[0].Token.Location.Column + 1;

                    // Fila Token 
                    TokenLine = ActualNode.ChildNodes[2].ChildNodes[0].Token.Location.Line + 1;

                }

                // Agregar A Lista De Clases 
                return new PrimitiveDeclaration(Identifiers, Type, Expression, "Var", TokenColumn, TokenLine);

            }

            // Retornar 
            return null;

        }       

        // Declaracion De Constantes 
        public AbstractInstruccion ConstantsDeclarationFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Agregar Clase Declaracion De Variables 
                return new ConstantsDeclaration(ConstantsDeclarationBlockFunc(ActualNode.ChildNodes[1]));

            }

            // Retornar 
            return null;

        }

        // Bloque Declaracion
        public LinkedList<AbstractInstruccion> ConstantsDeclarationBlockFunc(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Instrucciones
                LinkedList<AbstractInstruccion> AuxiliaryList = ConstantsDeclarationBlockFunc(ActualNode.ChildNodes[0]);

                // Instruccion
                AuxiliaryList.AddLast(ConstantsFunc(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryList;

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Agregar De Lista 
                LinkedList<AbstractInstruccion> AuxiliaryList = new LinkedList<AbstractInstruccion>();

                // Agregar A Lista 
                AuxiliaryList.AddLast(ConstantsFunc(ActualNode.ChildNodes[0]));

                // Retornar 
                return AuxiliaryList;

            }

            // Retornar Null
            return null;

        }

        // Constantes
        public AbstractInstruccion ConstantsFunc(ParseTreeNode ActualNode)
        {

            // Vefiicar Tamaño
            if (ActualNode.ChildNodes.Count == 4)
            {

                // Variables 
                int TokenLine = ActualNode.ChildNodes[0].Token.Location.Line + 1;
                int TokenColumn = ActualNode.ChildNodes[0].Token.Location.Column + 1;

                // Obtener Identificadores 
                String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Expression
                AbstractExpression Expression_ = Expression(ActualNode.ChildNodes[2]);

                // Agregar A Lista De Clases 
                return new PrimitiveDeclaration(Identifier, "", Expression_, "Const", TokenColumn, TokenLine);

            }

            // Retornar 
            return null;

        }

        // Expresion
        public AbstractExpression Expression(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 1)
            {

                // Obtener Valor Primitivo 
                return VariablesValues(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {


                // Obtener Operador 
                String Operator = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn = null;
               
                // Switch 
                switch(Operator)
                {
                
                    case "-":

                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[1]), Expression(ActualNode.ChildNodes[1]), "Minus", ActualNode.ChildNodes[0].Token.Location.Line + 1, ActualNode.ChildNodes[0].Token.Location.Column + 1);

                    break;

                    case "not":

                        AuxiliaryReturn = new Logical(Expression(ActualNode.ChildNodes[1]), Expression(ActualNode.ChildNodes[1]), "Not", ActualNode.ChildNodes[0].Token.Location.Line + 1, ActualNode.ChildNodes[0].Token.Location.Column + 1);

                        break;


                }

                // Retornar 
                return AuxiliaryReturn;

            }
            else if (ActualNode.ChildNodes.Count == 3 && ActualNode.ChildNodes[1].ToString().Equals("Expression"))
            {

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn;

                // Agregar Clase 
                AuxiliaryReturn = new Parenthesis(Expression(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryReturn;

            }
            else
            {

                // Obtener Operador 
                String Operator = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn = null;

                // Switch 
                switch(Operator)
                {
                
                    case "+":
                        
                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Sum", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "-":

                        // Retornar
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Substraction", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "*":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Multiplication", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "/":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Division", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "mod":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Mod", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "<=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "LessSame", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case ">=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "GreaterSame", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "<":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Less", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case ">":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Greater", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Equal", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "<>":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Differ", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "and":

                        // Retornar 
                        AuxiliaryReturn = new Logical(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "And", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;

                    case "or":

                        // Retornar 
                        AuxiliaryReturn = new Logical(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Or", ActualNode.ChildNodes[1].Token.Location.Line + 1, ActualNode.ChildNodes[1].Token.Location.Column + 1);

                        break;
                        
                }

                // Retorno 
                return AuxiliaryReturn;

            }
 
        }

        // Valores Primitivos
        public AbstractExpression VariablesValues(ParseTreeNode ActualNode)
        {

            // Veriricar CAmino A SEguir 
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Obtener String
                String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Retorno Instruccion 
                return new FunctionCallExp(Identifier, null, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);


            }
            else if (ActualNode.ChildNodes.Count == 4)
            {

                // Obtener String
                String Identifier = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Array De Expresoines 
                LinkedList<AbstractExpression> AuxiliaryList = ParamsValueList(ActualNode.ChildNodes[2]);

                // Retorno Instruccion 
                return new FunctionCallExp(Identifier, AuxiliaryList, ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

            }
            else
            {

                // Veriricar Si Es Un Identificaodr 
                if (ActualNode.ChildNodes[0].ToString().Contains("Identifier"))
                {

                    // Retornar Valor Expression
                    return new PrimitiveValue(SplitMethod(ActualNode.ChildNodes[0].ToString(), "Value"), "Identifier")
                    {

                        IsString = ActualNode.ChildNodes[0].ToString().Contains("(Identifier)")

                    };

                }
                else
                {
                    
                    // Retornar Valor Expression
                    return new PrimitiveValue(SplitMethod(ActualNode.ChildNodes[0].ToString(), "Value"), "String") 
                    {

                        IsString = ActualNode.ChildNodes[0].ToString().Contains("(SimpleString)")  
                    
                    };

                }

            }

        }
        
        // Misc 

        // Método Analizar 
        public void AnalyzeCompilate(String AnalyzeString)
        {

            // Instancia Clase Gramatica 
            Irony_Grammar AnalyzeGrammar = new Irony_Grammar();

            // Instancia Clase Lenguaje
            LanguageData AnalyzeData = new LanguageData(AnalyzeGrammar);

            // Errores Que Se Enuentren En Mi Gramatica 
            foreach (var item in AnalyzeData.Errors)
            {

                // Mostrar Error En La Gramatica
                MessageBox.Show(item.Message.ToString());

            }

            // Instancia Clase Analizador 
            Parser AnalyzeParser = new Parser(AnalyzeData);

            // Instancia Arbol De Analisis Sintactico
            ParseTree AnalyzeTree = AnalyzeParser.Parse(AnalyzeString);

            // Instancia ActualNode Arbol De Analisis Sintactico
            ParseTreeNode RootTreeNode = AnalyzeTree.Root;

            // Inicializar Lista De Errores 
            VariablesMethods.ErrorList = new LinkedList<ErrorTable>();

            // Manejo De Errores 
            if (AnalyzeTree.ParserMessages.Count > 0)
            {

                // Contador Auxiliar 
                VariablesMethods.AuxiliaryCounter = 1;

                // Recorrer Mensajes De Error 
                foreach (var ItemError in AnalyzeTree.ParserMessages)
                {

                    // Errores Lexicos Y Sintacticos
                    if (ItemError.Message.Contains("Invalid character"))
                    {

                        // Obtener Error 
                        String CharacterError = ItemError.Message.Split(' ')[2];

                        // SplitMethodlitear Error 
                        CharacterError = CharacterError.Split('.')[0];

                        // Agregar Error Lexico A Lista
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Lexico", "El Caracter " + CharacterError + " No Es Permtdo Por El Lenguaje.", ItemError.Location.Line, ItemError.Location.Column));

                    }
                    else
                    {

                        // Agregar Error Sintactico A Lista 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Sintáctico", ItemError.Message, ItemError.Location.Line, ItemError.Location.Column));

                    }

                    // Sumar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }

            // Verificar Si La Raiz Esta Nula 
            if (RootTreeNode == null)
            {

                // Mostrar Mensage 
                MessageBox.Show("Existen Errores En El Analisis..!");

            }
            else
            {

                // Generar Arbol De Analisis Sintactico 
                GenerateReportAST(RootTreeNode);

                // Verificar Si El Archivo No Esta Vacio
                if (RootTreeNode.ChildNodes.Count > 0)
                {

                    //  Limpiar Contador 
                    VariablesMethods.AuxiliaryCounterRep = 1;

                    // Limpiar Pila 
                    VariablesMethods.AuxiliaryPile = new Stack();

                    // Limpiar Lista De Clases
                    VariablesMethods.TranslateList = new LinkedList<AbstractInstruccion>();

                    // Limpiar Variable Traduccion 
                    VariablesMethods.TranslateString = "";

                    // Limpiar Lista De Ambientes 
                    VariablesMethods.ExecuteString = "";

                    // Lista De Ambientes 
                    VariablesMethods.EnviromentList = new LinkedList<EnviromentTable>();

                    // Ejecutar Recorrido Del Arbol 
                    Begin(RootTreeNode);

                    // Crear Primer Ambiente (Global)
                    //EnviromentTable GlobalEnv_Execute = new EnviromentTable(null, "Env_Global");
                    EnviromentTable GlobalEnv_Compilate = new EnviromentTable(null, "Env_Global");

                    // Recorrer Lista De Traduccion
                    foreach (var ItemTranslate in VariablesMethods.TranslateList)
                    {

                        // Llamar A Método Traducir 
                        //ItemTranslate.Execute(GlobalEnv_Execute);
                        ItemTranslate.Compilate(GlobalEnv_Compilate);                        

                    }

                }

                // Mensaje De Exito 
                MessageBox.Show("Compilación Completada Con Exito!");

            }

        }

        // Método Analizar 
        public void AnalyzeExecute(String AnalyzeString)
        {

            // Instancia Clase Gramatica 
            Irony_Grammar AnalyzeGrammar = new Irony_Grammar();

            // Instancia Clase Lenguaje
            LanguageData AnalyzeData = new LanguageData(AnalyzeGrammar);

            // Errores Que Se Enuentren En Mi Gramatica 
            foreach (var item in AnalyzeData.Errors)
            {

                // Mostrar Error En La Gramatica
                MessageBox.Show(item.Message.ToString());

            }

            // Instancia Clase Analizador 
            Parser AnalyzeParser = new Parser(AnalyzeData);

            // Instancia Arbol De Analisis Sintactico
            ParseTree AnalyzeTree = AnalyzeParser.Parse(AnalyzeString);

            // Instancia ActualNode Arbol De Analisis Sintactico
            ParseTreeNode RootTreeNode = AnalyzeTree.Root;

            // Inicializar Lista De Errores 
            VariablesMethods.ErrorList = new LinkedList<ErrorTable>();

            // Manejo De Errores 
            if (AnalyzeTree.ParserMessages.Count > 0)
            {

                // Contador Auxiliar 
                VariablesMethods.AuxiliaryCounter = 1;

                // Recorrer Mensajes De Error 
                foreach (var ItemError in AnalyzeTree.ParserMessages)
                {

                    // Errores Lexicos Y Sintacticos
                    if (ItemError.Message.Contains("Invalid character"))
                    {

                        // Obtener Error 
                        String CharacterError = ItemError.Message.Split(' ')[2];

                        // SplitMethodlitear Error 
                        CharacterError = CharacterError.Split('.')[0];

                        // Agregar Error Lexico A Lista
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Lexico", "El Caracter " + CharacterError + " No Es Permtdo Por El Lenguaje.", ItemError.Location.Line, ItemError.Location.Column));

                    }
                    else
                    {

                        // Agregar Error Sintactico A Lista 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Sintáctico", ItemError.Message, ItemError.Location.Line, ItemError.Location.Column));

                    }

                    // Sumar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }

            // Verificar Si La Raiz Esta Nula 
            if (RootTreeNode == null)
            {

                // Mostrar Mensage 
                MessageBox.Show("Existen Errores En El Analisis..!");

            }
            else
            {

                // Generar Arbol De Analisis Sintactico 
                GenerateReportAST(RootTreeNode);

                // Verificar Si El Archivo No Esta Vacio
                if (RootTreeNode.ChildNodes.Count > 0)
                {

                    //  Limpiar Contador 
                    VariablesMethods.AuxiliaryCounterRep = 1;

                    // Limpiar Pila 
                    VariablesMethods.AuxiliaryPile = new Stack();

                    // Limpiar Lista De Clases
                    VariablesMethods.TranslateList = new LinkedList<AbstractInstruccion>();

                    // Limpiar Variable Traduccion 
                    VariablesMethods.TranslateString = "";

                    // Limpiar Lista De Ambientes 
                    VariablesMethods.ExecuteString = "";

                    // Lista De Ambientes 
                    VariablesMethods.EnviromentList = new LinkedList<EnviromentTable>();

                    // Ejecutar Recorrido Del Arbol 
                    Begin(RootTreeNode);

                    // Crear Primer Ambiente (Global)
                    EnviromentTable GlobalEnv = new EnviromentTable(null, "Env_Global");

                    // Recorrer Lista De Traduccion
                    foreach (var ItemTranslate in VariablesMethods.TranslateList)
                    {

                        // Llamar A Método Traducir 
                        ItemTranslate.Execute(GlobalEnv);

                    }

                }

            }

        }

        // Método Reportes 
        public void GenerateReports() 
        {
        
            // Verificar SI Hay Errores 
            if(VariablesMethods.ErrorList.Count > 0)  
            {

                // Generear Reporte De Errores 
                VariablesMethods.ReportErrorTable(VariablesMethods.ErrorList);

            }

            // Verificar Si Hay Simbolos 
            if (VariablesMethods.EnviromentList != null) 
            {

                // Generar Reporte De Symbolos 
                VariablesMethods.ReportSymbolTable(VariablesMethods.EnviromentList, "Env_Global");

                // Ejecutar Comando 
                VariablesMethods.ExecuteCommand("C:\\compiladores2\\ReporteAST.pdf");

            }
        
        }

        // Método Reporte Arbol AST 
        private void GenerateReportAST(ParseTreeNode RootTreeNode)
        {

            // Cadena Que Contiene El Codigo De Graphviz 
            String GraphicString = Irony_CommonMethods.GenerateGraphicString(RootTreeNode);

            // Cadena COn El Comando De Graphviz 
            String CommandString = "dot -Tpdf -o C:\\compiladores2\\ReporteAST.pdf C:\\compiladores2\\ReporteAST.txt";

            // Try Catch Para Evitar Errores 
            try
            {

                // Crear FStream Archivo 
                FileStream SimpleFileStream = File.Create("C:\\compiladores2\\ReporteAST.txt");

                // Crear Arreglo De Bytes Para Escribir La Cadena 
                byte[] ArchiveData = new UTF8Encoding(true).GetBytes(GraphicString);

                // Escribir Arreglo En Archivo 
                SimpleFileStream.Write(ArchiveData, 0, ArchiveData.Length);

                // Cerrar Archivo 
                SimpleFileStream.Close();

                // Ejecutar Comando 
                VariablesMethods.ExecuteCommand(CommandString);

            }
            catch (Exception)
            {

                // Mostrar Mensaje De Error 
                MessageBox.Show("Error Al Generar El Reporte!");

            }

        }

        // Método Que Realiza Un Split A ActualNode Del Arbol  Part_De_Interes (Keyword)
        private String SplitMethod(String SplitString, String Type)
        {

            // Array 
            String[] AuxiliaryArray = null;

            // Verificar Tipo
            if (Type.Equals("Value")) {

                // Array Auxiliar Para Guardar El Texto Spliteado
                AuxiliaryArray = SplitString.Split('(');

            } 
            else if(Type.Equals("Default")) 
            {

                // Array Auxiliar Para Guardar El Texto Spliteado
                AuxiliaryArray = SplitString.Split(' ');

            }

            // Verificar Si Hay Mas De Un Elemento En El Array 
            if(AuxiliaryArray.Length > 1)
            {

                // Verificar Si Es Valor
                if(Type.Equals("Value")) 
                {

                    // Recorrer Array
                    AuxiliaryArray[0] = AuxiliaryArray[0].TrimEnd();
                
                }
                
                // Retornar Primera Posicion
                return AuxiliaryArray[0];
            
            }

            // Retornar Cadena 
            return SplitString;

        }

    }

}