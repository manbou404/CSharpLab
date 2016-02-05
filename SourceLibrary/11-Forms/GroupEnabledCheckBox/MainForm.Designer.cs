namespace JFactory.CsSrcLib.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.attachButton = new System.Windows.Forms.Button();
            this.detachButton = new System.Windows.Forms.Button();
            this.groupEnabledCheckBox1 = new JFactory.CsSrcLib.Forms.GroupEnabledCheckBox();
            this.propertyGridWithControlSelector1 = new JFactory.CsSrcLib.Forms.PropertyGridWithControlSelector();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(28, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 104);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "groupBoxの中身";
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(12, 12);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(75, 19);
            this.attachButton.TabIndex = 6;
            this.attachButton.Text = "Attach";
            this.attachButton.UseVisualStyleBackColor = true;
            // 
            // detachButton
            // 
            this.detachButton.Location = new System.Drawing.Point(93, 12);
            this.detachButton.Name = "detachButton";
            this.detachButton.Size = new System.Drawing.Size(75, 19);
            this.detachButton.TabIndex = 6;
            this.detachButton.Text = "Dettach";
            this.detachButton.UseVisualStyleBackColor = true;
            // 
            // groupEnabledCheckBox1
            // 
            this.groupEnabledCheckBox1.AutoSize = true;
            this.groupEnabledCheckBox1.GroupBox = null;
            this.groupEnabledCheckBox1.Location = new System.Drawing.Point(28, 94);
            this.groupEnabledCheckBox1.Name = "groupEnabledCheckBox1";
            this.groupEnabledCheckBox1.Size = new System.Drawing.Size(150, 16);
            this.groupEnabledCheckBox1.TabIndex = 2;
            this.groupEnabledCheckBox1.Text = "groupEnabledCheckBox1";
            this.groupEnabledCheckBox1.UseVisualStyleBackColor = true;
            // 
            // propertyGridWithControlSelector1
            // 
            this.propertyGridWithControlSelector1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridWithControlSelector1.Location = new System.Drawing.Point(235, 0);
            this.propertyGridWithControlSelector1.Name = "propertyGridWithControlSelector1";
            this.propertyGridWithControlSelector1.Size = new System.Drawing.Size(257, 261);
            this.propertyGridWithControlSelector1.TabIndex = 7;
            this.propertyGridWithControlSelector1.TargetControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 261);
            this.Controls.Add(this.propertyGridWithControlSelector1);
            this.Controls.Add(this.detachButton);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.groupEnabledCheckBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupEnabledCheckBox groupEnabledCheckBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Button detachButton;
        private PropertyGridWithControlSelector propertyGridWithControlSelector1;
    }
}

