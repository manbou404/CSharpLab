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
            this.attachButton = new System.Windows.Forms.Button();
            this.detachButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.propertyGridWithControlSelector1 = new JFactory.CsSrcLib.Forms.PropertyGridWithControlSelector();
            this.radioGroup1 = new JFactory.CsSrcLib.Forms.RadioGroup();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(46, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // attachButton
            // 
            this.attachButton.Location = new System.Drawing.Point(12, 12);
            this.attachButton.Name = "attachButton";
            this.attachButton.Size = new System.Drawing.Size(75, 23);
            this.attachButton.TabIndex = 4;
            this.attachButton.Text = "Attach";
            this.attachButton.UseVisualStyleBackColor = true;
            // 
            // detachButton
            // 
            this.detachButton.Location = new System.Drawing.Point(93, 12);
            this.detachButton.Name = "detachButton";
            this.detachButton.Size = new System.Drawing.Size(75, 23);
            this.detachButton.TabIndex = 5;
            this.detachButton.Text = "Detach";
            this.detachButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(46, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // propertyGridWithControlSelector1
            // 
            this.propertyGridWithControlSelector1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGridWithControlSelector1.Location = new System.Drawing.Point(321, 0);
            this.propertyGridWithControlSelector1.Name = "propertyGridWithControlSelector1";
            this.propertyGridWithControlSelector1.SelectedItem = null;
            this.propertyGridWithControlSelector1.Size = new System.Drawing.Size(246, 482);
            this.propertyGridWithControlSelector1.TabIndex = 6;
            this.propertyGridWithControlSelector1.TargetControl = null;
            // 
            // radioGroup1
            // 
            this.radioGroup1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.radioGroup1.Names = new string[0];
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 482);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.propertyGridWithControlSelector1);
            this.Controls.Add(this.detachButton);
            this.Controls.Add(this.attachButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Button detachButton;
        private PropertyGridWithControlSelector propertyGridWithControlSelector1;
        private FruitRadioGroup fruitRadioGroup1;
        private System.Windows.Forms.GroupBox groupBox2;
        private RadioGroup radioGroup1;
    }
}

