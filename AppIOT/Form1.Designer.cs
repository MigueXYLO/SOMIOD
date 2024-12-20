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
            this.btnSetUrl = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxUrl = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBoxNomeSensor
            // 
            this.txtBoxNomeSensor.Location = new System.Drawing.Point(198, 109);
            this.txtBoxNomeSensor.Name = "txtBoxNomeSensor";
            this.txtBoxNomeSensor.Size = new System.Drawing.Size(202, 22);
            this.txtBoxNomeSensor.TabIndex = 0;
            this.txtBoxNomeSensor.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nome Luz:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nome App:";
            // 
            // txtBoxAppName
            // 
            this.txtBoxAppName.Location = new System.Drawing.Point(12, 109);
            this.txtBoxAppName.Name = "txtBoxAppName";
            this.txtBoxAppName.Size = new System.Drawing.Size(180, 22);
            this.txtBoxAppName.TabIndex = 5;
            // 
            // btnCreateApp
            // 
            this.btnCreateApp.Enabled = false;
            this.btnCreateApp.Location = new System.Drawing.Point(12, 137);
            this.btnCreateApp.Name = "btnCreateApp";
            this.btnCreateApp.Size = new System.Drawing.Size(180, 32);
            this.btnCreateApp.TabIndex = 7;
            this.btnCreateApp.Text = "Criar App";
            this.btnCreateApp.UseVisualStyleBackColor = true;
            this.btnCreateApp.Click += new System.EventHandler(this.btnCreateApp_Click);
            // 
            // btnCreateContainer
            // 
            this.btnCreateContainer.Enabled = false;
            this.btnCreateContainer.Location = new System.Drawing.Point(198, 137);
            this.btnCreateContainer.Name = "btnCreateContainer";
            this.btnCreateContainer.Size = new System.Drawing.Size(202, 32);
            this.btnCreateContainer.TabIndex = 8;
            this.btnCreateContainer.Text = "Criar Luz";
            this.btnCreateContainer.UseVisualStyleBackColor = true;
            this.btnCreateContainer.Click += new System.EventHandler(this.btnCreateContainer_Click);
            // 
            // txtBoxMosquitto
            // 
            this.txtBoxMosquitto.Location = new System.Drawing.Point(12, 197);
            this.txtBoxMosquitto.Multiline = true;
            this.txtBoxMosquitto.Name = "txtBoxMosquitto";
            this.txtBoxMosquitto.ReadOnly = true;
            this.txtBoxMosquitto.Size = new System.Drawing.Size(388, 159);
            this.txtBoxMosquitto.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ReceivedValues";
            // 
            // btnSetUrl
            // 
            this.btnSetUrl.Location = new System.Drawing.Point(12, 52);
            this.btnSetUrl.Name = "btnSetUrl";
            this.btnSetUrl.Size = new System.Drawing.Size(388, 32);
            this.btnSetUrl.TabIndex = 13;
            this.btnSetUrl.Text = "Definir Url Base";
            this.btnSetUrl.UseVisualStyleBackColor = true;
            this.btnSetUrl.Click += new System.EventHandler(this.btnSetUrl_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Url Base:";
            // 
            // txtBoxUrl
            // 
            this.txtBoxUrl.Location = new System.Drawing.Point(12, 24);
            this.txtBoxUrl.Name = "txtBoxUrl";
            this.txtBoxUrl.Size = new System.Drawing.Size(388, 22);
            this.txtBoxUrl.TabIndex = 11;
            this.txtBoxUrl.Text = "http://localhost:44392/api/somiod/";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 365);
            this.Controls.Add(this.btnSetUrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBoxUrl);
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
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button btnSetUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxUrl;
    }
}

