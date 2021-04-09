// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2
{
    
    // Clase Principal Form
    public partial class Form1 : Form
    {
        // Instancia Clase Irony_Analyze
        readonly Irony_Resources.Irony_Parser ParserTranslate = new Irony_Resources.Irony_Parser();

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

            // Analizar Texto Compilar
            ParserTranslate.AnalyzeCompilate(EntranceString);

            // Analizar Texto Ejeuctar
            ParserTranslate.AnalyzeExecute(EntranceString);

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
        private void ButtonExecute_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Analizar Texto 
            ParserTranslate.AnalyzeExecute(EntranceString);

            // Limpiar Consola 
            TextConsole.Text = "";

            // Agregar Ejecucion
            TextConsole.Text = VariablesMethods.ExecuteString;

        }

        // Acción Click Botón Reportes 
        private void ButtonReports_Click(object sender, EventArgs e)
        {

            // Llamar A Metodo Report
            ParserTranslate.GenerateReports();

        }
    
    }

}