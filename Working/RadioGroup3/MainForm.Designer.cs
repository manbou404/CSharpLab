﻿namespace JFactory.CsSrcLib.Forms
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
            this.attachButton = new System.Windows.Forms.Button();
            this.detachButton = new System.Windows.Forms.Button();
            this.propertyGridWithControlSelector1 = new JFactory.CsSrcLib.Forms.PropertyGridWithControlSelector();
            this.SuspendLayout();
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 482);
            this.Controls.Add(this.propertyGridWithControlSelector1);
            this.Controls.Add(this.detachButton);
            this.Controls.Add(this.attachButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button attachButton;
        private System.Windows.Forms.Button detachButton;
        private PropertyGridWithControlSelector propertyGridWithControlSelector1;
    }
}

