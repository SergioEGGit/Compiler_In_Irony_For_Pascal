
namespace Proyecto2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextEntrance = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TextConsole = new System.Windows.Forms.RichTextBox();
            this.ButtonCompilate = new System.Windows.Forms.Button();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.ButtonReports = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextEntrance
            // 
            this.TextEntrance.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextEntrance.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TextEntrance.Location = new System.Drawing.Point(12, 68);
            this.TextEntrance.Name = "TextEntrance";
            this.TextEntrance.Size = new System.Drawing.Size(1076, 335);
            this.TextEntrance.TabIndex = 0;
            this.TextEntrance.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(642, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Compi Pascal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.DeepPink;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Código De Entrada:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.DeepPink;
            this.label3.Location = new System.Drawing.Point(12, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 28);
            this.label3.TabIndex = 4;
            this.label3.Text = "Consola:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.DeepPink;
            this.label4.Location = new System.Drawing.Point(1466, 859);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(241, 28);
            this.label4.TabIndex = 8;
            this.label4.Text = "Seleccione Un Opción:";
            // 
            // TextConsole
            // 
            this.TextConsole.BackColor = System.Drawing.SystemColors.ControlText;
            this.TextConsole.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextConsole.ForeColor = System.Drawing.Color.Yellow;
            this.TextConsole.Location = new System.Drawing.Point(12, 437);
            this.TextConsole.Name = "TextConsole";
            this.TextConsole.ReadOnly = true;
            this.TextConsole.Size = new System.Drawing.Size(1335, 239);
            this.TextConsole.TabIndex = 9;
            this.TextConsole.Text = "> Consola";
            // 
            // ButtonCompilate
            // 
            this.ButtonCompilate.BackColor = System.Drawing.Color.Thistle;
            this.ButtonCompilate.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonCompilate.ForeColor = System.Drawing.Color.Blue;
            this.ButtonCompilate.Location = new System.Drawing.Point(1172, 112);
            this.ButtonCompilate.Name = "ButtonCompilate";
            this.ButtonCompilate.Size = new System.Drawing.Size(103, 36);
            this.ButtonCompilate.TabIndex = 10;
            this.ButtonCompilate.Text = "Compilar";
            this.ButtonCompilate.UseVisualStyleBackColor = false;
            this.ButtonCompilate.Click += new System.EventHandler(this.ButtonCompilate_Click);
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.BackColor = System.Drawing.Color.Thistle;
            this.ButtonExecute.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonExecute.ForeColor = System.Drawing.Color.Blue;
            this.ButtonExecute.Location = new System.Drawing.Point(1172, 166);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(103, 36);
            this.ButtonExecute.TabIndex = 11;
            this.ButtonExecute.Text = "Ejecutar";
            this.ButtonExecute.UseVisualStyleBackColor = false;
            this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // ButtonReports
            // 
            this.ButtonReports.BackColor = System.Drawing.Color.Thistle;
            this.ButtonReports.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonReports.ForeColor = System.Drawing.Color.Blue;
            this.ButtonReports.Location = new System.Drawing.Point(1172, 222);
            this.ButtonReports.Name = "ButtonReports";
            this.ButtonReports.Size = new System.Drawing.Size(103, 36);
            this.ButtonReports.TabIndex = 12;
            this.ButtonReports.Text = "Reportes";
            this.ButtonReports.UseVisualStyleBackColor = false;
            this.ButtonReports.Click += new System.EventHandler(this.ButtonReports_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.DeepPink;
            this.label5.Location = new System.Drawing.Point(1094, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(253, 28);
            this.label5.TabIndex = 13;
            this.label5.Text = "Seleccione Una Opcion:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1370, 696);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ButtonReports);
            this.Controls.Add(this.ButtonExecute);
            this.Controls.Add(this.ButtonCompilate);
            this.Controls.Add(this.TextConsole);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextEntrance);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Proyecto 1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox TextEntrance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox TextConsole;
        private System.Windows.Forms.Button ButtonCompilate;
        private System.Windows.Forms.Button ButtonExecute;
        private System.Windows.Forms.Button ButtonReports;
        private System.Windows.Forms.Label label5;
    }
}

