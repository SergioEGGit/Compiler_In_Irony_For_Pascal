// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using Proyecto2.Misc;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Optimization
{

    // Clase Optimizer 
    class Optimizer
    {

        // Método Optimizar 
        public List<CodeLine> OptimizerMethodRule2(List<CodeLine> Code) 
        {

            // Array Auxiliar 
            List<CodeLine> AuxiliaryArray = Code;

            // Optimizar 

            // Reglas De Optimizacion 

            // Eliminación De Código Muerto

            // Regla 1
            AuxiliaryArray = OptimizedRule1(AuxiliaryArray);

            // Regla 3
            AuxiliaryArray = OptimizedRule3(AuxiliaryArray);

            // Regla 4
            AuxiliaryArray = OptimizedRule4(AuxiliaryArray);

            // Regla 2
            AuxiliaryArray = OptimizedRule2(AuxiliaryArray);

            // Eliminación De Instrucciones Redundantes De Carga Y Almacenamiento 

            // Regla 5 
            AuxiliaryArray = OptimizedRule5(AuxiliaryArray);

            // Simplificación Algebraica Y Reducción Por Fuerza

            // Regla No.6
            AuxiliaryArray = OptimizedRule6(AuxiliaryArray);

            // Regla No.7
            AuxiliaryArray = OptimizedRule7(AuxiliaryArray);

            // Regla No.8
            AuxiliaryArray = OptimizedRule8(AuxiliaryArray);

            // Regla No.9
            AuxiliaryArray = OptimizedRule9(AuxiliaryArray);

            // Regla No.10
            AuxiliaryArray = OptimizedRule10(AuxiliaryArray);

            // Regla No.11
            AuxiliaryArray = OptimizedRule11(AuxiliaryArray);

            // Regla No.12
            AuxiliaryArray = OptimizedRule12(AuxiliaryArray);

            // Regla No.13
            AuxiliaryArray = OptimizedRule13(AuxiliaryArray);

            // Regla No.14
            AuxiliaryArray = OptimizedRule14(AuxiliaryArray);

            // Regla No.15
            AuxiliaryArray = OptimizedRule15(AuxiliaryArray);

            // Regla No.16
            AuxiliaryArray = OptimizedRule16(AuxiliaryArray);           

            // Retornar 
            return AuxiliaryArray;

        }

        public List<CodeLine> OptimizerMethod(List<CodeLine> Code)
        {

            // Array Auxiliar 
            List<CodeLine> AuxiliaryArray = Code;

            // Optimizar 

            // Reglas De Optimizacion 

            // Eliminación De Código Muerto

            // Regla 1
            AuxiliaryArray = OptimizedRule1(AuxiliaryArray);

            // Regla 3
            AuxiliaryArray = OptimizedRule3(AuxiliaryArray);

            // Regla 4
            AuxiliaryArray = OptimizedRule4(AuxiliaryArray);

            // Regla 2
            // AuxiliaryArray = OptimizedRule2(AuxiliaryArray);

            // Eliminación De Instrucciones Redundantes De Carga Y Almacenamiento 

            // Regla 5 
            AuxiliaryArray = OptimizedRule5(AuxiliaryArray);

            // Simplificación Algebraica Y Reducción Por Fuerza

            // Regla No.6
            AuxiliaryArray = OptimizedRule6(AuxiliaryArray);

            // Regla No.7
            AuxiliaryArray = OptimizedRule7(AuxiliaryArray);

            // Regla No.8
            AuxiliaryArray = OptimizedRule8(AuxiliaryArray);

            // Regla No.9
            AuxiliaryArray = OptimizedRule9(AuxiliaryArray);

            // Regla No.10
            AuxiliaryArray = OptimizedRule10(AuxiliaryArray);

            // Regla No.11
            AuxiliaryArray = OptimizedRule11(AuxiliaryArray);

            // Regla No.12
            AuxiliaryArray = OptimizedRule12(AuxiliaryArray);

            // Regla No.13
            AuxiliaryArray = OptimizedRule13(AuxiliaryArray);

            // Regla No.14
            AuxiliaryArray = OptimizedRule14(AuxiliaryArray);

            // Regla No.15
            AuxiliaryArray = OptimizedRule15(AuxiliaryArray);

            // Regla No.16
            AuxiliaryArray = OptimizedRule16(AuxiliaryArray);

            // Retornar 
            return AuxiliaryArray;

        }

        // Reglas De Optimizacion 

        // Eliminación De Codigo Muerto

        // Regla No.1
        public List<CodeLine> OptimizedRule1(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternRuleIf = @"\s*(if|IF)\s*\(\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*(==|!=|>=|<=|<|>)\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternRule1 = @"\s*(L|l)([0-9])+\s*:";
            String LabelRule = @"(L|l)([0-9])+";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex InstruccionIf = new Regex(PatternRuleIf);
            Regex Jump = new Regex(PatternRule1);
            Regex Label = new Regex(LabelRule);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if(Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine) && !InstruccionIf.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Posicion Instruccion Inicio 
                    int InitialPos = CounterLine + 1;

                    // SubInstrucciones 
                    List<CodeLine> SubInstructions = new List<CodeLine>();

                    // Label 
                    MatchCollection ActualLabel = Label.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Agreger Regex 
                    String PatternLabelActual = @"\s*" + ActualLabel[0].Value + @"\s*:";

                    // Regex 
                    Regex LabelActual = new Regex(PatternLabelActual);

                    // Buscar Otra 
                    for (int CounterAuxiliary = CounterLine + 1; CounterAuxiliary < AuxiliaryArray.Count; CounterAuxiliary++)
                    {

                        // Insertar Instrucciones 
                        SubInstructions.Add(AuxiliaryArray[CounterAuxiliary]);

                        // Verificar Si Hay Match
                        if(LabelActual.IsMatch(AuxiliaryArray[CounterAuxiliary].TextLine))
                        {

                            // Var Auxiliar
                            bool Flag = false;

                            // Verificar Si No Hay Instrucciones De Igualaciones O Etiquetas 
                            for (int InsCounter = 0; InsCounter < SubInstructions.Count - 1; InsCounter++)
                            {

                                // Verificar Si Hay Asignacion O Etiqueta 
                                if (Jump.IsMatch(SubInstructions[InsCounter].TextLine))
                                {

                                    // Agregar Flag 
                                    Flag = true;

                                    // Romper Ciclo 
                                    break;

                                }

                            }

                            // Verificar 
                            if (!Flag)
                            {

                                // String Codigo REmovido 
                                String RemovedCode = "";

                                // Arreglo De Caracters Trim
                                char[] CharTrim = { '\n', '\t', '\r', ' ' };
                                
                                // Recorrer Lista De Instrucciones A Eliminar 
                                for (int InsCounter = 0; InsCounter < SubInstructions.Count - 1; InsCounter++) 
                                {

                                    // Verficar 
                                    if(!AuxiliaryArray[InitialPos].TextLine.Trim(CharTrim).Equals("")) 
                                    {

                                        // Agregar A String Codigo Eliminado 
                                        RemovedCode += AuxiliaryArray[InitialPos].TextLine + "<br/>";

                                    }
                                    
                                    // Lista De Instruccioens 
                                    AuxiliaryArray.RemoveAt(InitialPos);
                                
                                }

                                // Comentario 
                                String CommentsPattern = @"\s*\/\/.*";
                                Regex Comments = new Regex(CommentsPattern);

                                // Verificar Si Codigo Removido Es Vacio 
                                if(!RemovedCode.Trim(CharTrim).Equals("") && !Comments.IsMatch(RemovedCode.Trim(CharTrim))) 
                                {

                                    // Agregar A Reporte 
                                    VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla/Bloques", "Regla No.1", RemovedCode, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                                }


                            }

                        }

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla No.2
        public List<CodeLine> OptimizedRule2(List<CodeLine> Code) {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*(if|IF)\s*\(\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*(==)\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternIF = @"\s*(if|IF)\s*\(\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*(==|!=|<=|>=|<|>)\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternGoto = @"\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternLabel = @"(L|l)([0-9])+";
            String PatternLabelDef = @"\s*(L|l)([0-9])+\s*:\s*";
            String PatternNumber = @"((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Goto = new Regex(PatternGoto);
            Regex Label = new Regex(PatternLabel);
            Regex LabelDef = new Regex(PatternLabelDef);
            Regex Numbers = new Regex(PatternNumber);
            Regex InstruccionIF = new Regex(PatternIF);

            // Auxiliares Enteros 
            int PosIf = 0;
            int PosGoto = 0;
            int PosEtiqueta = 0;
            int NumeroEtiquetas = 0;

            // Bandera 
            bool Flag = false;
            bool Delete = true;

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {
            
                // Bandera False 
                Flag = false;
                Delete = true;

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {
                    
                    // Matches 
                    MatchCollection LabelTrue = Label.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Agregar Posicion If 
                    PosIf = CounterLine;

                    // Recorrer Lista De Instrucciones 
                    for (int CounterGoto = CounterLine + 1; CounterGoto < AuxiliaryArray.Count; CounterGoto++)
                    {

                        // Verificar Si Existe Goto 
                        if (Goto.IsMatch(AuxiliaryArray[CounterGoto].TextLine) && !InstruccionIF.IsMatch(AuxiliaryArray[CounterGoto].TextLine))
                        {

                            // Matches 
                            MatchCollection LabelFalse = Label.Matches(AuxiliaryArray[CounterGoto].TextLine);

                            // Posicion Goto 
                            PosGoto = CounterGoto;

                            // Obtener Label True 
                            String PatternTrue = @"\s*" + LabelTrue[0].Value + @"\s*:";

                            // LableTrue Regex
                            Regex LabelTrueRegex = new Regex(PatternTrue);
                              
                            // Recorrer Lista De Instrucciones 
                            for (int CounterLabel = CounterGoto + 1; CounterLabel < AuxiliaryArray.Count; CounterLabel++)
                            {

                                
                                // Verificar Label 
                                if (LabelDef.IsMatch(AuxiliaryArray[CounterLabel].TextLine)) 
                                {

                                    // Verificar Numero De Etiquetas 
                                    NumeroEtiquetas += 1;
                                
                                }
                                
                                // Verificar Si Existe Goto 
                                if (LabelTrueRegex.IsMatch(AuxiliaryArray[CounterLabel].TextLine))
                                {

                                    // String Pattern
                                    String PatternGotoTrue = @"\s*goto\s*" + LabelTrue[0].Value + @"\s*;";

                                    // Regex 
                                    Regex GotoTrue = new Regex(PatternGotoTrue);

                                    // Verificar Si Agrego 
                                    if (NumeroEtiquetas == 1 && Delete) 
                                    {

                                        // Posicion 
                                        PosEtiqueta = CounterLabel;

                                        // Comienza Modificacion
                                        AuxiliaryArray[PosIf].TextLine = Regex.Replace(AuxiliaryArray[PosIf].TextLine, @"(==)", "!=");
                                        AuxiliaryArray[PosIf].TextLine = Regex.Replace(AuxiliaryArray[PosIf].TextLine, @"(" + LabelTrue[0].Value + ")", LabelFalse[0].Value);

                                        // String Eliminado 
                                        String RemovedCode = AuxiliaryArray[PosGoto].TextLine + "<br/>" + AuxiliaryArray[PosEtiqueta].TextLine;

                                        // Codigo Agregado 
                                        String AddedCode = AuxiliaryArray[PosIf].TextLine;

                                        // Agregar A Reporte 
                                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla/Bloques", "Regla No.2", RemovedCode, AddedCode, AuxiliaryArray[PosGoto].NoLine + 1));

                                        // ELiminar Goto 
                                        AuxiliaryArray.RemoveAt(PosGoto);

                                        // Eliminar Label 
                                        AuxiliaryArray.RemoveAt(PosEtiqueta - 1);

                                        // Bandera 
                                        Flag = true;
                                                                                
                                    }

                                    // Numero De Etiquetas 
                                    NumeroEtiquetas = 0;

                                    // Romper Ciclo
                                    break;                                    

                                }

                            }

                        }

                        // Verificar Si Hago Break 
                        if(Flag) 
                        {

                            // Break 
                            break;
                            
                        }

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla No.3
        public List<CodeLine> OptimizedRule3(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*(if|IF)\s*\(\s*(\-?([0-9]+))\s*(==)\s*(\-?([0-9]+))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternIF = @"\s*(if|IF)\s*\(\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*(==|!=|<=|>=|<|>)\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternGoto = @"\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternNumber = @"(\-?([0-9]+))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Goto = new Regex(PatternGoto);
            Regex InstruccionIF = new Regex(PatternIF);
            Regex Numbers = new Regex(PatternNumber);

            // Auxiliares Enteros 
            int PosIf = 0;
            int PosGoto = 0;

            // Bandera 
            bool Flag = false;

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Bandera False 
                Flag = false;

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Obtener Nuemros 
                    MatchCollection Exps = Numbers.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Agregar Posicion If 
                    PosIf = CounterLine;

                    // Verificar Si Son Iguales 
                    if(Exps[0].Value.Equals(Exps[1].Value))
                    {

                        // Recorrer Lista De Instrucciones 
                        for (int CounterGoto = CounterLine + 1; CounterGoto < AuxiliaryArray.Count; CounterGoto++)
                        {

                            // Verificar Si Existe Goto 
                            if (Goto.IsMatch(AuxiliaryArray[CounterGoto].TextLine) && !InstruccionIF.IsMatch(AuxiliaryArray[CounterGoto].TextLine))
                            {

                                // Posicion Goto 
                                PosGoto = CounterGoto;

                                // String Eliminado 
                                String RemovedCode = AuxiliaryArray[PosIf].TextLine + "<br/>" + AuxiliaryArray[PosGoto].TextLine;

                                // Codigo Agregado 
                                AuxiliaryArray[PosIf].TextLine = Regex.Replace(AuxiliaryArray[PosIf].TextLine, @"\s*(if|IF)\s*\(\s*(\-?([0-9]+))\s*(==)\s*(\-?([0-9]+))\s*\)*\s*", string.Empty);

                                // Agregar A Reporte 
                                VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla/Bloques", "Regla No.3", RemovedCode, AuxiliaryArray[PosIf].TextLine, AuxiliaryArray[PosIf].NoLine + 1));

                                // Eliminar If 
                                AuxiliaryArray.RemoveAt(PosGoto);

                                // Bandera 
                                Flag = true;

                                // Romper Ciclo
                                break;

                            }

                            // Verificar Si Hago Break 
                            if (Flag)
                            {

                                // Break 
                                break;

                            }

                        }

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla No.4
        public List<CodeLine> OptimizedRule4(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*(if|IF)\s*\(\s*(\-?([0-9]+))\s*(==)\s*(\-?([0-9]+))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternIF = @"\s*(if|IF)\s*\(\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*(==|!=|<=|>=|<|>)\s*((\-?([0-9]+))|(((T|t)([0-9])+|(HP|SP|sp|hp))))\s*\)\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternGoto = @"\s*goto\s*(L|l)([0-9])+\s*;";
            String PatternNumber = @"(\-?([0-9]+))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Goto = new Regex(PatternGoto);
            Regex InstruccionIF = new Regex(PatternIF);
            Regex Numbers = new Regex(PatternNumber);

            // Auxiliares Enteros 
            int PosIf = 0;
            int PosGoto = 0;

            // Bandera 
            bool Flag = false;

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Bandera False 
                Flag = false;

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Obtener Nuemros 
                    MatchCollection Exps = Numbers.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Agregar Posicion If 
                    PosIf = CounterLine;

                    // Verificar Si Son Iguales 
                    if(!Exps[0].Value.Equals(Exps[1].Value))
                    {

                        // Recorrer Lista De Instrucciones 
                        for (int CounterGoto = CounterLine + 1; CounterGoto < AuxiliaryArray.Count; CounterGoto++)
                        {

                            // Verificar Si Existe Goto 
                            if (Goto.IsMatch(AuxiliaryArray[CounterGoto].TextLine) && !InstruccionIF.IsMatch(AuxiliaryArray[CounterGoto].TextLine))
                            {

                                // Posicion Goto 
                                PosGoto = CounterGoto;

                                // String Eliminado 
                                String RemovedCode = AuxiliaryArray[PosIf].TextLine;

                                // Agregar A Reporte 
                                VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla/Bloques", "Regla No.4", RemovedCode, "-", AuxiliaryArray[PosIf].NoLine + 1));

                                // Eliminar If 
                                AuxiliaryArray.RemoveAt(PosIf);

                                // Bandera 
                                Flag = true;

                                // Romper Ciclo
                                break;                                

                            }

                            // Verificar Si Hago Break 
                            if (Flag)
                            {

                                // Break 
                                break;

                            }

                        }

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Eliminación De Instrucciones Redundantes De Carga Y Almacenamiento 

        // Regla 5 
        public List<CodeLine> OptimizedRule5(List<CodeLine> Code) 
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|sp|hp))\s*=\s*((T|t)([0-9])+|(HP|SP|sp|hp))\s*;";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|sp|hp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if(Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Matches 
                    MatchCollection TemporaryMatches_1 = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // SubInstrucciones 
                    List<CodeLine> SubInstructions = new List <CodeLine>();

                    // Buscar Otra 
                    for (int CounterAuxiliary = CounterLine + 1; CounterAuxiliary < AuxiliaryArray.Count; CounterAuxiliary++)
                    {

                        // Insertar Instrucciones 
                        SubInstructions.Add(AuxiliaryArray[CounterAuxiliary]);

                        // Verificar Si Hay Match
                        if(Instruccion.IsMatch(AuxiliaryArray[CounterAuxiliary].TextLine)) 
                        {

                            // Match 
                            MatchCollection InstruccionMatch_2 = Instruccion.Matches(AuxiliaryArray[CounterAuxiliary].TextLine);

                            // Matches 
                            MatchCollection TemporaryMatches_2 = Temporary.Matches(AuxiliaryArray[CounterAuxiliary].TextLine);

                            // Posicion Instuccion A Eliminar 
                            int RemovedPos = CounterAuxiliary;

                            // Verficiar Si Es COntrario 
                            if (TemporaryMatches_1[0].Value.Equals(TemporaryMatches_2[1].Value) && TemporaryMatches_1[1].Value.Equals(TemporaryMatches_2[0].Value))
                            {

                                // Patron Nuevo 
                                String PatternAsign = @"\s*" + TemporaryMatches_1[0] + @"\s*=";
                                String PatternLabel = @"\s*(L|l)([0-9])+\s*:";

                                // Regex 
                                Regex Asign = new Regex(PatternAsign);
                                Regex Label = new Regex(PatternLabel);

                                // Var Auxiliar
                                bool Flag = false;

                                // Verificar Si No Hay Instrucciones De Igualaciones O Etiquetas 
                                for (int InsCounter = 0; InsCounter < SubInstructions.Count - 1; InsCounter++) 
                                {

                                    // Verificar Si Hay Asignacion O Etiqueta 
                                    if(Asign.IsMatch(SubInstructions[InsCounter].TextLine) || Label.IsMatch(SubInstructions[InsCounter].TextLine)) 
                                    {

                                        // Agregar Flag 
                                        Flag = true;

                                        // Romper Ciclo 
                                        break;
                                    
                                    }
                                
                                }

                                // Verificar 
                                if(!Flag) 
                                {

                                    // Agregar A Reporte 
                                    VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla/Bloques", "Regla No.5", InstruccionMatch_2[0].Value, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                                    // Eliminar Codigo 
                                    AuxiliaryArray.RemoveAt(RemovedPos);
                                
                                }
                            
                            }

                        }                                                
                    
                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Simplificación Algebraica Y Reducción Por Fuerza

        // Regla 6 
        public List<CodeLine> OptimizedRule6(List<CodeLine> Code)
        {
            
            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;
            
            // Patter Rule 
            String PatternRule = @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\+\s*0\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*0\s*\+\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;)";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|sp|hp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.6", InstruccionMatch[0].Value, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                        // Remover Linea Arrar
                        AuxiliaryArray.RemoveAt(CounterLine);

                        // Regresar Anterior
                        CounterLine -= 1;

                    }

                }

            }
 
            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 7 
        public List<CodeLine> OptimizedRule7(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\-\s*0\s*;";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.7", InstruccionMatch[0].Value, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                        // Remover Linea Arrar
                        AuxiliaryArray.RemoveAt(CounterLine);

                        // Regresar Anterior
                        CounterLine -= 1;

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 8 
        public List<CodeLine> OptimizedRule8(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\*\s*1\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*1\s*\*\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;)";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.8", InstruccionMatch[0].Value, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                        // Remover Linea Arrar
                        AuxiliaryArray.RemoveAt(CounterLine);

                        // Regresar Anterior
                        CounterLine -= 1;

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 9 
        public List<CodeLine> OptimizedRule9(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\/\s*1\s*;";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.9", InstruccionMatch[0].Value, "-", AuxiliaryArray[CounterLine].NoLine + 1));

                        // Remover Linea Arrar
                        AuxiliaryArray.RemoveAt(CounterLine);

                        // Regresar Anterior
                        CounterLine -= 1;

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 10 
        public List<CodeLine> OptimizedRule10(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\+\s*0\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*0\s*\+\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;)";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (!TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Remover Linea Arrar
                        AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"((\s*\+\s*0\s*)|(\s*0\s*\+))", string.Empty);

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.10", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));
             
                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 11 
        public List<CodeLine> OptimizedRule11(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\-\s*0\s*;";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (!TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Remover Linea Arrar
                        AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"\s*\-\s*0\s*", string.Empty);

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.11", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 12
        public List<CodeLine> OptimizedRule12(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\*\s*1\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*1\s*\*\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;)";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (!TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Remover Linea Arrar
                        AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"((\s*\*\s*1\s*)|(\s*1\s*\*))", string.Empty);

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.12", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 13 
        public List<CodeLine> OptimizedRule13(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\/\s*1\s*;";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Verificar Si Es Son Iguales 
                    if (!TemporaryMatches[0].Value.Equals(TemporaryMatches[1].Value))
                    {

                        // Remover Linea Arrar
                        AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"\s*\/\s*1\s*", string.Empty);

                        // Agregar A Reporte 
                        VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.13", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                    }

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 14
        public List<CodeLine> OptimizedRule14(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\*\s*2\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*2\s*\*\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;)";
            String PatternTemporary = @"((T|t)([0-9])+|(HP|SP|hp|sp))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);
            Regex Temporary = new Regex(PatternTemporary);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Matches 
                    MatchCollection TemporaryMatches = Temporary.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Remover Linea Arrar
                    AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"((\s*" + TemporaryMatches[1] + @"\s*\*\s*2\s*)|(\s*2\s*\*\s*" + TemporaryMatches[1] + @"\s*))", " " + TemporaryMatches[1] + " + " + TemporaryMatches[1]);

                    // Agregar A Reporte 
                    VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.14", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 15 
        public List<CodeLine> OptimizedRule15(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"((\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\*\s*0\s*;)|(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*0\s*\*\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;))";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Remover Linea Arrar
                    AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"(\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*\*\s*0\s*)|(\s*0\s*\*\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*)", " 0");

                    // Agregar A Reporte 
                    VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.15", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

        // Regla 16
        public List<CodeLine> OptimizedRule16(List<CodeLine> Code)
        {

            // Array Auxiliar
            List<CodeLine> AuxiliaryArray = Code;

            // Patter Rule 
            String PatternRule = @"\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*=\s*0\s*\/\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*;";

            // Crear Regex
            Regex Instruccion = new Regex(PatternRule);

            // Recorrer Array De Lineas 
            for (int CounterLine = 0; CounterLine < AuxiliaryArray.Count; CounterLine++)
            {

                // Verificar Expression Regular
                if (Instruccion.IsMatch(AuxiliaryArray[CounterLine].TextLine))
                {

                    // Match 
                    MatchCollection InstruccionMatch = Instruccion.Matches(AuxiliaryArray[CounterLine].TextLine);

                    // Remover Linea Arrar
                    AuxiliaryArray[CounterLine].TextLine = Regex.Replace(AuxiliaryArray[CounterLine].TextLine, @"\s*0\s*\/\s*((T|t)([0-9])+|(HP|SP|hp|sp))\s*", " 0");

                    // Agregar A Reporte 
                    VariablesMethods.OptimizedList.AddLast(new OptimizationTable(VariablesMethods.AuxiliaryCounterOp += 1, "Mirilla", "Regla No.16", InstruccionMatch[0].Value, AuxiliaryArray[CounterLine].TextLine, AuxiliaryArray[CounterLine].NoLine + 1));

                }

            }

            // Retornar Array 
            return AuxiliaryArray;

        }

    }

}