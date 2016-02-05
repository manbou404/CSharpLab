namespace JFactory.CsSrcLib.Forms
{
    partial class MainForm
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.propertyGridWithControlSelector1 = new JFactory.CsSrcLib.Forms.PropertyGridWithControlSelector();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(82, 149);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // propertyGridWithControlSelector1
            // 
            this.propertyGridWithControlSelector1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridWithControlSelector1.Location = new System.Drawing.Point(341, 0);
            this.propertyGridWithControlSelector1.Name = "propertyGridWithControlSelector1";
            this.propertyGridWithControlSelector1.SelectedItem = null;
            this.propertyGridWithControlSelector1.Size = new System.Drawing.Size(324, 422);
            this.propertyGridWithControlSelector1.TabIndex = 4;
            this.propertyGridWithControlSelector1.TargetControl = this;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(113, 182);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(181, 20);
            this.comboBox1.TabIndex = 5;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(54, 255);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(224, 137);
            this.propertyGrid1.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 422);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.propertyGridWithControlSelector1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private PropertyGridWithControlSelector propertyGridWithControlSelector1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}