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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BRAM_Merge));
			this.MergeList = new System.Windows.Forms.ListBox();
			this.MergeFreeSpace = new System.Windows.Forms.Label();
			this.MergeButton = new System.Windows.Forms.Button();
			this.MergeAddress = new System.Windows.Forms.TextBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// MergeList
			// 
			this.MergeList.Dock = System.Windows.Forms.DockStyle.Top;
			this.MergeList.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MergeList.FormattingEnabled = true;
			this.MergeList.ItemHeight = 15;
			this.MergeList.Location = new System.Drawing.Point(0, 0);
			this.MergeList.Name = "MergeList";
			this.MergeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.MergeList.Size = new System.Drawing.Size(206, 199);
			this.MergeList.TabIndex = 0;
			this.MergeList.SelectedIndexChanged += new System.EventHandler(this.MergeList_SelectedIndexChanged);
			this.MergeList.MouseLeave += new System.EventHandler(this.MergeList_MouseLeave);
			this.MergeList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MergeList_MouseMove);
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
			this.toolTip1.SetToolTip(this.MergeButton, "Merge the data and save the file");
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
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(999, 307);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(222, 307);
			this.Name = "BRAM_Merge";
			this.ShowInTaskbar = false;
			this.Text = "Merge";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MergeList;
        private System.Windows.Forms.Label MergeFreeSpace;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.TextBox MergeAddress;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}