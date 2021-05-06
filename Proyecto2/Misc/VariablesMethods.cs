// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Misc
{
   
    // Clase Variables 
    static class VariablesMethods
    {

        // Variables Globales 

        // Lista Que Contiene Los Errores 
        public static LinkedList<ErrorTable> ErrorList = new LinkedList<ErrorTable>();

        // Lista Que Contiene Las Clases Del Analizador(Traducción)
        public static LinkedList<TranslatorAndInterpreter.AbstractInstruccion> TranslateList = new LinkedList<TranslatorAndInterpreter.AbstractInstruccion>();

        // Lista De Ambientes
        public static LinkedList<Misc.EnviromentTable> EnviromentList = new LinkedList<EnviromentTable>();

        // String Que Contiene La Traduccion 
        public static String TranslateString = "";

        // String Que Contiene La Ejecucion
        public static String ExecuteString = "";

        // Contador De Errores 
        public static int AuxiliaryCounter = 0;

        // Contador De Errores 
        public static int AuxiliaryCounterRep = 1;

        // Arreglo Auxiliar Identacion
        public static Stack AuxiliaryPile = new Stack();

        // Calcular Identacion 
        public static String Ident() 
        {

            // String Auxiliar 
            String Identation = "";

            // Verificar Tamaño Pila 
            for (int Count = 0; Count < AuxiliaryPile.Count; Count++) 
            {

                // Agregar Espacio 
                Identation += "    ";
            
            }

            // Retornar 
            return Identation;
        
        }
        
        // Método Reporte Tabla De Simbolos
        public static void ReportSymbolTable(LinkedList<EnviromentTable> EnvTable, String EnvName) 
        {
            
            // Cadena Html 
            String HtmlString =  "<html> \n" +
                                 "<head> <title> Reporte Tabla De Simbolos </title> </head>\n" +
                                 "<body style=\"background-color:#FFE4B5; \">\n" +
                                 "  <center>\n" +
                                 "      <font size=\"5\" face=\"Times New Roman\">\n" +
                                 "      <table class=\"default\" width=\"100%\" style=\"font-size: 20px\">\n" +
                                 "          <h1> Reporte Tabla De Simbolos </h1>" +
                                 "          <tbody style=\"background: rgba(128, 255, 0, 0.3);\">\n" +
                                 "              <tr>\n" +
                                 "                  <th> Nombre </th>\n" +
                                 "                  <th> Tipo </th>\n" +
                                 "                  <th> Position </th>\n" + 
                                 "                  <th> Tipo Declaracion </th>\n" +
                                 "                  <th> Entorno </th>\n" +
                                 "                  <th> Numero De Parametros </th> \n" +
                                 "                  <th> Linea </th>\n" +
                                 "                  <th> Columna </th>\n" +
                                 "              </tr>\n" +
                                 "          </tbody>\n";

            // Bandera Auxiliar 
            int Auxiliary = 0;

            // Verificar Que La Lista No Este Nulla
            if(EnvTable != null) 
            {

                // Recorrer Lista 
                foreach(EnviromentTable Env in EnvTable) 
                {

                    // Recorrer Simbolos
                    foreach(KeyValuePair<string, SymbolTable> Valor in Env.PrimitiveVariablesStack) 
                    {

                        if(Auxiliary == 0) 
                        {

                            // Agregar Fila
                            HtmlString += "          <tbody style=\"background: #00BFFF;\">\n" +
                                          "             <tr>\n" +
                                          "                 <td><center>" + Valor.Value.Identifier.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Type.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Position.ToString() + "</center></td>\n" +     
                                          "                 <td><center>" + Valor.Value.DecType.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Env.ToString() + "</center></td>\n" +
                                          "                 <td><center> - </center></td> \n" +        
                                          "                 <td><center>" + Valor.Value.Line.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Column.ToString() + "</center></td>\n" +
                                          "             </tr>\n" +
                                          "          </tbody>\n";

                            // Mover Contador 
                            Auxiliary = 1;

                        }
                        else
                        {

                            // Agregar Fila
                            HtmlString += "          <tbody style=\"background: #90EE90;\">\n" +
                                          "             <tr>\n" +
                                          "                 <td><center>" + Valor.Value.Identifier.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Type.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Position.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.DecType.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Env.ToString() + "</center></td>\n" +
                                          "                 <td><center> - </center></td> \n" +
                                          "                 <td><center>" + Valor.Value.Line.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Column.ToString() + "</center></td>\n" +
                                          "             </tr>\n" +
                                          "          </tbody>\n";

                            // Mover Contador 
                            Auxiliary = 0;

                        }
                    
                    }

                    // Recorrer Funciones
                    foreach(KeyValuePair<string, FunctionTable> Valor in Env.FunctionsStack)
                    {

                        if (Auxiliary == 0)
                        {

                            // Agregar Fila
                            HtmlString += "          <tbody style=\"background: #00BFFF;\">\n" +
                                          "             <tr>\n" +
                                          "                 <td><center>" + Valor.Value.Identifier + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.ReturnType + "</center></td>\n" +
                                          "                 <td><center> - </center></td>\n" +
                                          "                 <td><center>" + Valor.Value.TypeFunc + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Env.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.ParamsCount + "</center></td> \n" +
                                          "                 <td><center>" + Valor.Value.Line.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Column.ToString() + "</center></td>\n" +
                                          "             </tr>\n" +
                                          "          </tbody>\n";

                            // Mover Contador 
                            Auxiliary = 1;

                        }
                        else
                        {

                            // Agregar Fila
                            HtmlString += "          <tbody style=\"background: #90EE90;\">\n" +
                                          "             <tr>\n" +
                                          "                 <td><center>" + Valor.Value.Identifier.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.ReturnType.ToString() + "</center></td>\n" +
                                          "                 <td><center> - </center></td>\n" +
                                          "                 <td><center>" + Valor.Value.TypeFunc.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Env.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.ParamsCount + "</center></td> \n" +
                                          "                 <td><center>" + Valor.Value.Line.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Valor.Value.Column.ToString() + "</center></td>\n" +
                                          "             </tr>\n" +
                                          "          </tbody>\n";

                            // Mover Contador 
                            Auxiliary = 0;

                        }

                    }

                }    
            
            }

            // Agregar Resto Tabla
            HtmlString += "      </table>\n" +
                          "  </center>\n" +
                          "</body>\n" +
                          "</html>";

            // Escribir Archivo
            // Try Catch Para Evitar Errores 
            try
            {

                // Crear FStream Archivo 
                FileStream SimpleFileStream = File.Create("C:\\compiladores2\\ReporteTablaDeSimbolos_"+ EnvName + AuxiliaryCounterRep.ToString() + ".html");

                // Crear Arreglo De Bytes Para Escribir La Cadena 
                byte[] ArchiveData = new UTF8Encoding(true).GetBytes(HtmlString);

                // Escribir Arreglo En Archivo 
                SimpleFileStream.Write(ArchiveData, 0, ArchiveData.Length);

                // Cerrar Archivo 
                SimpleFileStream.Close();

                // Ejecutar Comando 
                VariablesMethods.ExecuteCommand("C:\\compiladores2\\ReporteTablaDeSimbolos_" + EnvName + AuxiliaryCounterRep.ToString() + ".html");

                // Aumentar Contador 
                AuxiliaryCounterRep += 1;

            }
            catch (Exception)
            {

                // Mostrar Mensaje De Error 
                MessageBox.Show("Error Al Generar El Reporte!");

            }

        }

        // Método Reporte Tabla De Errores 
        public static void ReportErrorTable(LinkedList<ErrorTable> ErrTable) 
        {

            // Cadena Html 
            String HtmlString = "<html> \n" +
                                 "<head> <title> Reporte Tabla De Errores </title> </head>\n" +
                                 "<body style=\"background-color:#FFE4B5; \">\n" +
                                 "  <center>\n" +
                                 "      <font size=\"5\" face=\"Times New Roman\">\n" +
                                 "      <table class=\"default\" width=\"100%\" style=\"font-size: 20px\">\n" +
                                 "          <h1> Reporte Tabla De Errores </h1>" +
                                 "          <tbody style=\"background: rgba(128, 255, 0, 0.3);\">\n" +
                                 "              <tr>\n" +
                                 "                  <th> No. </th>\n" +
                                 "                  <th> Tipo </th>\n" +
                                 "                  <th> Descripcion </th>\n" +
                                 "                  <th> Linea </th>\n" +
                                 "                  <th> Columna </th>\n" +
                                 "              </tr>\n" +
                                 "          </tbody>\n";

            // Bandera Auxiliar 
            int Auxiliary = 0;

            // Verificar Que La Lista No Este Nulla
            if (ErrTable != null)
            {

                // Recorrer Lista 
                foreach (ErrorTable Err in ErrTable)
                {

                    if (Auxiliary == 0)
                    {

                        // Agregar Fila
                        HtmlString += "          <tbody style=\"background: #00BFFF;\">\n" +
                                      "             <tr>\n" +
                                      "                 <td><center>" + Err.ErrorCounter.ToString() + "</center></td>\n" +
                                      "                 <td><center>" + Err.ErrorType.ToString() + "</center></td>\n" +
                                      "                 <td><center>" + Err.ErrorDescripcion.ToString() + "</center></td>\n" +
                                      "                 <td><center>" + Err.ErrorLine.ToString() + "</center></td>\n" +
                                      "                 <td><center>" + Err.ErrorColumn.ToString() + "</center></td>\n" +
                                      "             </tr>\n" +
                                      "          </tbody>\n";

                         // Mover Contador 
                         Auxiliary = 1;

                    }
                    else
                    {

                            // Agregar Fila
                            HtmlString += "          <tbody style=\"background: #90EE90;\">\n" +
                                          "            <tr>\n" +
                                          "                 <td><center>" + Err.ErrorCounter.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Err.ErrorType.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Err.ErrorDescripcion.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Err.ErrorLine.ToString() + "</center></td>\n" +
                                          "                 <td><center>" + Err.ErrorColumn.ToString() + "</center></td>\n" +
                                          "             </tr>\n" +
                                          "          </tbody>\n";

                        // Mover Contador 
                        Auxiliary = 0;

                    }

                }

            }

            // Agregar Resto Tabla
            HtmlString += "      </table>\n" +
                          "  </center>\n" +
                          "</body>\n" +
                          "</html>";

            // Escribir Archivo
            // Try Catch Para Evitar Errores 
            try
            {

                // Crear FStream Archivo 
                FileStream SimpleFileStream = File.Create("C:\\compiladores2\\ReporteTablaDeErrores.html");

                // Crear Arreglo De Bytes Para Escribir La Cadena 
                byte[] ArchiveData = new UTF8Encoding(true).GetBytes(HtmlString);

                // Escribir Arreglo En Archivo 
                SimpleFileStream.Write(ArchiveData, 0, ArchiveData.Length);

                // Cerrar Archivo 
                SimpleFileStream.Close();

                // Ejecutar Comando 
                VariablesMethods.ExecuteCommand("C:\\compiladores2\\ReporteTablaDeErrores.html");

            }
            catch (Exception)
            {

                // Mostrar Mensaje De Error 
                MessageBox.Show("Error Al Generar El Reporte!");

            }

        }

        // Método Para Ejecutar Comando En Cmd 
        public static void ExecuteCommand(String StringCommand)
        {

            // Iniciar Proceso CMD Con El Comando Indicado
            ProcessStartInfo InfoProcess = new ProcessStartInfo("cmd", "/c " + StringCommand)
            {

                // Escirbir La Salida Del Comando En Un Stream 
                RedirectStandardOutput = true,
                UseShellExecute = false,

                // Evitar Que El Proceso Inicie La Consola 
                CreateNoWindow = true

            };

            // Crear El Proceso  
            Process SimpleProcess = new Process
            {

                // Agregar Informacion E Iniciar El Proceso
                StartInfo = InfoProcess

            };

            // Inciar El Proceso 
            SimpleProcess.Start();

            // Recibiar En Un String El Resultado 
            // String Result = SimpleProcess.StandardOutput.ReadToEnd();

        }

    }

}