// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2
{
    
    // Clase Principal Form
    public partial class Form1 : Form
    {
        // Instancia Clase Irony_Analyze
        readonly Irony_Resources.Irony_Parser ParserTranslate = new Irony_Resources.Irony_Parser();

        // Instancia Clase Optimizar
        readonly Optimization.Optimizer OptimizeMethod = new Optimization.Optimizer();

        // Constructor Inicial 
        public Form1()
        {
            
            // Inicializar Componentes 
            InitializeComponent();

        }

        // Acción Click Botón Analizar 
        private void ButtonCompilate_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Obtener Instnacia 
            ThreeAddressCode Instancia_1 = ThreeAddressCode.GetInstance;

            // Resetera Codigo 
            Instancia_1.ResetIntermediateCode();

            // Agregar Comentario
            Instancia_1.AddCommentOneLine("Funciones Nativas", "Uno");

            // Agregar Print
            Instancia_1.AddNativePrintString();

            // Agregar Concat
            Instancia_1.AddNativeConcatString();

            // Agregar Compare 
            Instancia_1.AddNativeCompareString();

            // Analizar Texto Compilar
            ParserTranslate.AnalyzeCompilate(EntranceString);

            // Limpiar Consola 
            TextConsole.Text = "";

            // Agregar Encabezado 
            String Translate = Instancia_1.CreateHeader();

            // Obtener codigo 
            Translate += Instancia_1.GetIntermediateCode();

            // Agregar Ejecucion
            TextConsole.Text = Translate;

        }

        // Acción Click Botón Ejecutar
        private void ButtonOptimize_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Array De Lineas 
            String[] LineSplit = EntranceString.Split("\n");

            // Lista
            List<Optimization.CodeLine> LineArray = new List<Optimization.CodeLine>();

            // Vaciar Lista 
            VariablesMethods.OptimizedList = new LinkedList<Optimization.OptimizationTable>();

            // Contador Reporte 
            VariablesMethods.AuxiliaryCounterRep += 1;

            // Contador Auxiliary 
            VariablesMethods.AuxiliaryCounterOp = 0;

            // Recorrer Array Auxiliar 
            for (int Counter = 0; Counter < LineSplit.Length; Counter++) 
            {

                // Agregar A Array Lineas 
                LineArray.Add(new Optimization.CodeLine(Counter, LineSplit[Counter]));
            
            }

            // Optimizar Texto
            LineArray = OptimizeMethod.OptimizerMethod(LineArray);

            // Limpiar Consola 
            TextConsole.Text = "";

            // Obtener String Linas 
            String OutputConsole = "";

            // Recorrer Array 
            foreach(Optimization.CodeLine Line in LineArray) 
            {

                // Agregar A Salida 
                OutputConsole += Line.TextLine + "\n";
            
            }

            // Agregar Ejecucion
            TextConsole.Text = OutputConsole;           

            // Abrir Reporte 
            VariablesMethods.ReportOptimizeTable();

        }

        // Acción Click Botón Ejecutar
        private void ButtonRule2_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Array De Lineas 
            String[] LineSplit = EntranceString.Split("\n");

            // Lista
            List<Optimization.CodeLine> LineArray = new List<Optimization.CodeLine>();

            // Vaciar Lista 
            VariablesMethods.OptimizedList = new LinkedList<Optimization.OptimizationTable>();

            // Contador Reporte 
            VariablesMethods.AuxiliaryCounterRep += 1;

            // Contador Auxiliary 
            VariablesMethods.AuxiliaryCounterOp = 0;

            // Recorrer Array Auxiliar 
            for (int Counter = 0; Counter < LineSplit.Length; Counter++)
            {

                // Agregar A Array Lineas 
                LineArray.Add(new Optimization.CodeLine(Counter, LineSplit[Counter]));

            }

            // Optimizar Texto
            LineArray = OptimizeMethod.OptimizerMethodRule2(LineArray);

            // Limpiar Consola 
            TextConsole.Text = "";

            // Obtener String Linas 
            String OutputConsole = "";

            // Recorrer Array 
            foreach (Optimization.CodeLine Line in LineArray)
            {

                // Agregar A Salida 
                OutputConsole += Line.TextLine + "\n";

            }

            // Agregar Ejecucion
            TextConsole.Text = OutputConsole;

            // Abrir Reporte 
            VariablesMethods.ReportOptimizeTable();

        }

        // Acción Click Botón Reportes 
        private void ButtonReports_Click(object sender, EventArgs e)
        {

            // Llamar A Metodo Report
            ParserTranslate.GenerateReports();

        }
    
    }

}