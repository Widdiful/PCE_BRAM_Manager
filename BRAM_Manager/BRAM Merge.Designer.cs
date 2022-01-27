namespace BRAM_Manager {
    partial class BRAM_Merge {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BRAM_Merge));
            this.MergeList = new System.Windows.Forms.ListBox();
            this.MergeFreeSpace = new System.Windows.Forms.Label();
            this.MergeButton = new System.Windows.Forms.Button();
            this.MergeAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MergeList
            // 
            this.MergeList.FormattingEnabled = true;
            this.MergeList.Location = new System.Drawing.Point(12, 12);
            this.MergeList.Name = "MergeList";
            this.MergeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.MergeList.Size = new System.Drawing.Size(182, 199);
            this.MergeList.TabIndex = 0;
            this.MergeList.SelectedIndexChanged += new System.EventHandler(this.MergeList_SelectedIndexChanged);
            // 
            // MergeFreeSpace
            // 
            this.MergeFreeSpace.AutoSize = true;
            this.MergeFreeSpace.Location = new System.Drawing.Point(12, 246);
            this.MergeFreeSpace.Name = "MergeFreeSpace";
            this.MergeFreeSpace.Size = new System.Drawing.Size(37, 13);
            this.MergeFreeSpace.TabIndex = 14;
            this.MergeFreeSpace.Text = "          ";
            // 
            // MergeButton
            // 
            this.MergeButton.Location = new System.Drawing.Point(119, 241);
            this.MergeButton.Name = "MergeButton";
            this.MergeButton.Size = new System.Drawing.Size(75, 23);
            this.MergeButton.TabIndex = 15;
            this.MergeButton.Text = "Merge";
            this.MergeButton.UseVisualStyleBackColor = true;
            this.MergeButton.Click += new System.EventHandler(this.MergeButton_Click);
            // 
            // MergeAddress
            // 
            this.MergeAddress.Location = new System.Drawing.Point(12, 217);
            this.MergeAddress.Name = "MergeAddress";
            this.MergeAddress.ReadOnly = true;
            this.MergeAddress.Size = new System.Drawing.Size(182, 20);
            this.MergeAddress.TabIndex = 16;
            // 
            // BRAM_Merge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 268);
            this.Controls.Add(this.MergeAddress);
            this.Controls.Add(this.MergeButton);
            this.Controls.Add(this.MergeFreeSpace);
            this.Controls.Add(this.MergeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BRAM_Merge";
            this.Text = "Merge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MergeList;
        private System.Windows.Forms.Label MergeFreeSpace;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.TextBox MergeAddress;
    }
}