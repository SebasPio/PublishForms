namespace PublishForms
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.Data = new System.Windows.Forms.DataGridView();
            this.SelectFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataModel = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.listBoxIds = new System.Windows.Forms.ListBox();
            this.DeleteControls = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteControls)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Seleccionar Modelo de Datos";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Data
            // 
            this.Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data.Location = new System.Drawing.Point(22, 152);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(298, 490);
            this.Data.TabIndex = 2;
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(326, 617);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(170, 25);
            this.SelectFile.TabIndex = 4;
            this.SelectFile.Text = "Eliminar Campos";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.SelectFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tabla de Reemplazo de Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tabla de Campos a Eliminar";
            // 
            // txtDataModel
            // 
            this.txtDataModel.AllowDrop = true;
            this.txtDataModel.Location = new System.Drawing.Point(22, 41);
            this.txtDataModel.Name = "txtDataModel";
            this.txtDataModel.Size = new System.Drawing.Size(1131, 20);
            this.txtDataModel.TabIndex = 7;
            this.txtDataModel.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtDataModel_DragDrop);
            this.txtDataModel.Leave += new System.EventHandler(this.GenerateList);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 67);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(198, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Seleccionar Carpeta de Salida";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFolder
            // 
            this.txtFolder.AllowDrop = true;
            this.txtFolder.Location = new System.Drawing.Point(21, 96);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(986, 20);
            this.txtFolder.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1013, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Cambiar Id";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // listBoxIds
            // 
            this.listBoxIds.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxIds.Cursor = System.Windows.Forms.Cursors.No;
            this.listBoxIds.FormattingEnabled = true;
            this.listBoxIds.Location = new System.Drawing.Point(502, 131);
            this.listBoxIds.Name = "listBoxIds";
            this.listBoxIds.Size = new System.Drawing.Size(651, 511);
            this.listBoxIds.TabIndex = 11;
            // 
            // DeleteControls
            // 
            this.DeleteControls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeleteControls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.DeleteControls.Location = new System.Drawing.Point(326, 184);
            this.DeleteControls.Name = "DeleteControls";
            this.DeleteControls.Size = new System.Drawing.Size(170, 427);
            this.DeleteControls.TabIndex = 12;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "EliminarControles";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(412, 152);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "Cargar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(324, 152);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(82, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "Guardar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 663);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.DeleteControls);
            this.Controls.Add(this.listBoxIds);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtDataModel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SelectFile);
            this.Controls.Add(this.Data);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeleteControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView Data;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDataModel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBoxIds;
        private System.Windows.Forms.DataGridView DeleteControls;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
    }
}

