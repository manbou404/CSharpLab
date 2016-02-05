namespace JFactory.CsSrcLib.Win32
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
            this.openButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.open2Button = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.printTextBox = new System.Windows.Forms.TextBox();
            this.isShowButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 12);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(12, 41);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // open2Button
            // 
            this.open2Button.Location = new System.Drawing.Point(93, 12);
            this.open2Button.Name = "open2Button";
            this.open2Button.Size = new System.Drawing.Size(75, 23);
            this.open2Button.TabIndex = 2;
            this.open2Button.Text = "Open(Me)";
            this.open2Button.UseVisualStyleBackColor = true;
            // 
            // printButton
            // 
            this.printButton.Location = new System.Drawing.Point(12, 70);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 23);
            this.printButton.TabIndex = 3;
            this.printButton.Text = "WriteLine";
            this.printButton.UseVisualStyleBackColor = true;
            // 
            // printTextBox
            // 
            this.printTextBox.Location = new System.Drawing.Point(93, 72);
            this.printTextBox.Name = "printTextBox";
            this.printTextBox.Size = new System.Drawing.Size(100, 19);
            this.printTextBox.TabIndex = 4;
            this.printTextBox.Text = "はろーわーるど";
            // 
            // isShowButton
            // 
            this.isShowButton.Location = new System.Drawing.Point(12, 99);
            this.isShowButton.Name = "isShowButton";
            this.isShowButton.Size = new System.Drawing.Size(75, 23);
            this.isShowButton.TabIndex = 5;
            this.isShowButton.Text = "IsShow";
            this.isShowButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.isShowButton);
            this.Controls.Add(this.printTextBox);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.open2Button);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.openButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button open2Button;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.TextBox printTextBox;
        private System.Windows.Forms.Button isShowButton;
    }
}

