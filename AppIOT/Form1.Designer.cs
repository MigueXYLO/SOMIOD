namespace AppIOT
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
            this.txtBoxNomeSensor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxAppName = new System.Windows.Forms.TextBox();
            this.btnCreateApp = new System.Windows.Forms.Button();
            this.btnCreateContainer = new System.Windows.Forms.Button();
            this.txtBoxMosquitto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxNomeSensor
            // 
            this.txtBoxNomeSensor.Location = new System.Drawing.Point(117, 25);
            this.txtBoxNomeSensor.Name = "txtBoxNomeSensor";
            this.txtBoxNomeSensor.Size = new System.Drawing.Size(100, 22);
            this.txtBoxNomeSensor.TabIndex = 0;
            this.txtBoxNomeSensor.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome Luz:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nome App:";
            // 
            // txtBoxAppName
            // 
            this.txtBoxAppName.Location = new System.Drawing.Point(12, 25);
            this.txtBoxAppName.Name = "txtBoxAppName";
            this.txtBoxAppName.Size = new System.Drawing.Size(100, 22);
            this.txtBoxAppName.TabIndex = 5;
            // 
            // btnCreateApp
            // 
            this.btnCreateApp.Location = new System.Drawing.Point(12, 53);
            this.btnCreateApp.Name = "btnCreateApp";
            this.btnCreateApp.Size = new System.Drawing.Size(100, 32);
            this.btnCreateApp.TabIndex = 7;
            this.btnCreateApp.Text = "Criar App";
            this.btnCreateApp.UseVisualStyleBackColor = true;
            this.btnCreateApp.Click += new System.EventHandler(this.btnCreateApp_Click);
            // 
            // btnCreateContainer
            // 
            this.btnCreateContainer.Enabled = false;
            this.btnCreateContainer.Location = new System.Drawing.Point(117, 53);
            this.btnCreateContainer.Name = "btnCreateContainer";
            this.btnCreateContainer.Size = new System.Drawing.Size(100, 32);
            this.btnCreateContainer.TabIndex = 8;
            this.btnCreateContainer.Text = "Criar Luz";
            this.btnCreateContainer.UseVisualStyleBackColor = true;
            this.btnCreateContainer.Click += new System.EventHandler(this.btnCreateContainer_Click);
            // 
            // txtBoxMosquitto
            // 
            this.txtBoxMosquitto.Location = new System.Drawing.Point(12, 113);
            this.txtBoxMosquitto.Multiline = true;
            this.txtBoxMosquitto.Name = "txtBoxMosquitto";
            this.txtBoxMosquitto.ReadOnly = true;
            this.txtBoxMosquitto.Size = new System.Drawing.Size(205, 159);
            this.txtBoxMosquitto.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ReceivedValues";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxMosquitto);
            this.Controls.Add(this.btnCreateContainer);
            this.Controls.Add(this.btnCreateApp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxAppName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxNomeSensor);
            this.Name = "Form1";
            this.Text = "Luz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxNomeSensor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxAppName;
        private System.Windows.Forms.Button btnCreateApp;
        private System.Windows.Forms.Button btnCreateContainer;
        private System.Windows.Forms.TextBox txtBoxMosquitto;
        private System.Windows.Forms.Label label2;
    }
}

