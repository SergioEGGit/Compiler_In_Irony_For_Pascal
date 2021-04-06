// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Irony.Parsing;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.Irony_Resources
{

    // Clase Irony Common Methods
    class Irony_CommonMethods
    {

        // Variables 

        // Cadena Que Contiene El Codigo De Graphviz
        private static String GraphicString;

        // Contador De Nodos De La Grafica
        private static int GraphicCounter;

        // Métodos 

        // Método Que Recorre El Ast Para Generar El Codigo De Graphviz
        private static void RunTreeAST(ParseTreeNode RootTreeNode, String ParnetNode) {
            
            // Ciclo Para Recorrer El Arbol 
            foreach(ParseTreeNode CurrentTreeNode in RootTreeNode.ChildNodes) {

                // Agregar Nombre A Nodo Hijo 
                String ActualChildNode = "Node_" + GraphicCounter.ToString();
                
                // Crear Nuevo Nodo Graphviz Con Datos Del Nodo Hijo 
                GraphicString += ActualChildNode + "[label = \"" + CurrentTreeNode.ToString() +  "\"];\n";

                // Crear Enlance Con Nodo Padre 
                GraphicString += ParnetNode + "->" + ActualChildNode + "; \n";

                // Avanzar Contador Nodos 
                GraphicCounter += 1;

                // Llamada Recursiva Al Metodo 
                RunTreeAST(CurrentTreeNode, ActualChildNode);

            }
        
        }

        // Método Que Genera El Codigo De Graphviz 
        public static String GenerateGraphicString(ParseTreeNode RootTreeNode) {

            // Estructura Grafica 
            GraphicString = "digraph Arbol_Analisis_Sintactico { \n" +
                            "node[color = firebrick1] \n" +
                            "Node_0[label = \"" + RootTreeNode.ToString() + "\"]; \n";

            // Inicializar Contador De Nodos
            GraphicCounter = 1;

            // Llamada A Metodo RunTreeAST
            RunTreeAST(RootTreeNode, "Node_0");

            // Fin De La Grafica 
            GraphicString += "}";

            // Retornar Valor 
            return GraphicString;

        }

    }

}