﻿namespace V2RayGCon.Views
{
    partial class FormQRCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQRCode));
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxServList = new System.Windows.Forms.ComboBox();
            this.cboxLinkType = new System.Windows.Forms.ComboBox();
            this.btnSavePic = new System.Windows.Forms.Button();
            this.tboxLink = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // picQRCode
            // 
            resources.ApplyResources(this.picQRCode, "picQRCode");
            this.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboxServList
            // 
            this.cboxServList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxServList.FormattingEnabled = true;
            this.cboxServList.Items.AddRange(new object[] {
            resources.GetString("cboxServList.Items"),
            resources.GetString("cboxServList.Items1"),
            resources.GetString("cboxServList.Items2")});
            resources.ApplyResources(this.cboxServList, "cboxServList");
            this.cboxServList.Name = "cboxServList";
            this.cboxServList.SelectedIndexChanged += new System.EventHandler(this.cboxServList_SelectedIndexChanged);
            // 
            // cboxLinkType
            // 
            this.cboxLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxLinkType.FormattingEnabled = true;
            this.cboxLinkType.Items.AddRange(new object[] {
            resources.GetString("cboxLinkType.Items"),
            resources.GetString("cboxLinkType.Items1")});
            resources.ApplyResources(this.cboxLinkType, "cboxLinkType");
            this.cboxLinkType.Name = "cboxLinkType";
            this.cboxLinkType.SelectedIndexChanged += new System.EventHandler(this.cboxLinkType_SelectedIndexChanged);
            // 
            // btnSavePic
            // 
            resources.ApplyResources(this.btnSavePic, "btnSavePic");
            this.btnSavePic.Name = "btnSavePic";
            this.btnSavePic.UseVisualStyleBackColor = true;
            this.btnSavePic.Click += new System.EventHandler(this.btnSavePic_Click);
            // 
            // tboxLink
            // 
            resources.ApplyResources(this.tboxLink, "tboxLink");
            this.tboxLink.Name = "tboxLink";
            this.tboxLink.TextChanged += new System.EventHandler(this.tboxLink_TextChanged);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // FormQRCode
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.tboxLink);
            this.Controls.Add(this.btnSavePic);
            this.Controls.Add(this.cboxLinkType);
            this.Controls.Add(this.cboxServList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picQRCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormQRCode";
            this.Shown += new System.EventHandler(this.FormQRCode_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxServList;
        private System.Windows.Forms.ComboBox cboxLinkType;
        private System.Windows.Forms.Button btnSavePic;
        private System.Windows.Forms.TextBox tboxLink;
        private System.Windows.Forms.Button btnCopy;
    }
}