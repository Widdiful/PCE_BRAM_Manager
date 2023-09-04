namespace BRAM_Manager {
    partial class MainForm {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mergeMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mergeFromFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.transferToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.disableWarningsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LeftList = new System.Windows.Forms.ListBox();
			this.RightList = new System.Windows.Forms.ListBox();
			this.LeftAddress = new System.Windows.Forms.TextBox();
			this.RightAddress = new System.Windows.Forms.TextBox();
			this.LeftBrowse = new System.Windows.Forms.Button();
			this.RightBrowse = new System.Windows.Forms.Button();
			this.CopyButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.DownButton = new System.Windows.Forms.Button();
			this.UpButton = new System.Windows.Forms.Button();
			this.LeftSave = new System.Windows.Forms.Button();
			this.RightSave = new System.Windows.Forms.Button();
			this.LeftFreeSpace = new System.Windows.Forms.Label();
			this.RightFreeSpace = new System.Windows.Forms.Label();
			this.LeftEdited = new System.Windows.Forms.Label();
			this.RightEdited = new System.Windows.Forms.Label();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(370, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.mergeMultipleToolStripMenuItem,
            this.mergeFromFoldersToolStripMenuItem,
            this.transferToolToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// clearAllToolStripMenuItem
			// 
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.clearAllToolStripMenuItem.Text = "Unload Both BRAMs";
			this.clearAllToolStripMenuItem.ToolTipText = "Unload the files in both BRAM slots, does not delete data in the files themselves" +
    "";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
			// 
			// mergeMultipleToolStripMenuItem
			// 
			this.mergeMultipleToolStripMenuItem.Name = "mergeMultipleToolStripMenuItem";
			this.mergeMultipleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.mergeMultipleToolStripMenuItem.Text = "Merge Multiple...";
			this.mergeMultipleToolStripMenuItem.ToolTipText = "Merge multiple BRAM files together to create a new one";
			this.mergeMultipleToolStripMenuItem.Click += new System.EventHandler(this.mergeMultipleToolStripMenuItem_Click);
			// 
			// mergeFromFoldersToolStripMenuItem
			// 
			this.mergeFromFoldersToolStripMenuItem.Name = "mergeFromFoldersToolStripMenuItem";
			this.mergeFromFoldersToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.mergeFromFoldersToolStripMenuItem.Text = "Merge From Folders...";
			this.mergeFromFoldersToolStripMenuItem.ToolTipText = "Merges multiple BRAM files within selected folders";
			this.mergeFromFoldersToolStripMenuItem.Click += new System.EventHandler(this.mergeFromFoldersToolStripMenuItem_Click);
			// 
			// transferToolToolStripMenuItem
			// 
			this.transferToolToolStripMenuItem.Name = "transferToolToolStripMenuItem";
			this.transferToolToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.transferToolToolStripMenuItem.Text = "Transfer Tool...";
			this.transferToolToolStripMenuItem.ToolTipText = "Bulk transfer saves between two formats (SSDS3, Everdrive, Emulator...)";
			this.transferToolToolStripMenuItem.Click += new System.EventHandler(this.transferToolToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableWarningsToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// disableWarningsToolStripMenuItem
			// 
			this.disableWarningsToolStripMenuItem.CheckOnClick = true;
			this.disableWarningsToolStripMenuItem.Name = "disableWarningsToolStripMenuItem";
			this.disableWarningsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.disableWarningsToolStripMenuItem.Text = "Disable Warnings";
			this.disableWarningsToolStripMenuItem.ToolTipText = "When ticked, no \"are you sure?\" warnings will pop up.\r\nOnly enable if you know wh" +
    "at you\'re doing";
			this.disableWarningsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.disableWarningsToolStripMenuItem_CheckedChanged);
			// 
			// LeftList
			// 
			this.LeftList.AllowDrop = true;
			this.LeftList.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LeftList.FormattingEnabled = true;
			this.LeftList.ItemHeight = 14;
			this.LeftList.Location = new System.Drawing.Point(12, 27);
			this.LeftList.Name = "LeftList";
			this.LeftList.Size = new System.Drawing.Size(130, 186);
			this.LeftList.TabIndex = 1;
			this.LeftList.SelectedIndexChanged += new System.EventHandler(this.LeftList_SelectedIndexChanged);
			this.LeftList.DragDrop += new System.Windows.Forms.DragEventHandler(this.LeftList_DragDrop);
			this.LeftList.DragEnter += new System.Windows.Forms.DragEventHandler(this.LeftList_DragEnter);
			// 
			// RightList
			// 
			this.RightList.AllowDrop = true;
			this.RightList.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.RightList.FormattingEnabled = true;
			this.RightList.ItemHeight = 14;
			this.RightList.Location = new System.Drawing.Point(229, 27);
			this.RightList.Name = "RightList";
			this.RightList.Size = new System.Drawing.Size(129, 186);
			this.RightList.TabIndex = 2;
			this.RightList.SelectedIndexChanged += new System.EventHandler(this.RightList_SelectedIndexChanged);
			this.RightList.DragDrop += new System.Windows.Forms.DragEventHandler(this.RightList_DragDrop);
			this.RightList.DragEnter += new System.Windows.Forms.DragEventHandler(this.RightList_DragEnter);
			// 
			// LeftAddress
			// 
			this.LeftAddress.Location = new System.Drawing.Point(12, 232);
			this.LeftAddress.Name = "LeftAddress";
			this.LeftAddress.ReadOnly = true;
			this.LeftAddress.Size = new System.Drawing.Size(130, 20);
			this.LeftAddress.TabIndex = 3;
			// 
			// RightAddress
			// 
			this.RightAddress.Location = new System.Drawing.Point(229, 232);
			this.RightAddress.Name = "RightAddress";
			this.RightAddress.ReadOnly = true;
			this.RightAddress.Size = new System.Drawing.Size(129, 20);
			this.RightAddress.TabIndex = 4;
			// 
			// LeftBrowse
			// 
			this.LeftBrowse.Location = new System.Drawing.Point(86, 259);
			this.LeftBrowse.Name = "LeftBrowse";
			this.LeftBrowse.Size = new System.Drawing.Size(56, 23);
			this.LeftBrowse.TabIndex = 5;
			this.LeftBrowse.Text = "Browse...";
			this.toolTip1.SetToolTip(this.LeftBrowse, "Opens a file to the left BRAM slot");
			this.LeftBrowse.UseVisualStyleBackColor = true;
			this.LeftBrowse.Click += new System.EventHandler(this.LeftBrowse_Click);
			// 
			// RightBrowse
			// 
			this.RightBrowse.Location = new System.Drawing.Point(302, 258);
			this.RightBrowse.Name = "RightBrowse";
			this.RightBrowse.Size = new System.Drawing.Size(56, 23);
			this.RightBrowse.TabIndex = 6;
			this.RightBrowse.Text = "Browse...";
			this.toolTip1.SetToolTip(this.RightBrowse, "Opens a file to the right BRAM slot");
			this.RightBrowse.UseVisualStyleBackColor = true;
			this.RightBrowse.Click += new System.EventHandler(this.RightBrowse_Click);
			// 
			// CopyButton
			// 
			this.CopyButton.Location = new System.Drawing.Point(148, 161);
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(75, 23);
			this.CopyButton.TabIndex = 7;
			this.CopyButton.Text = "Copy";
			this.toolTip1.SetToolTip(this.CopyButton, "Copies the selected item to the other BRAM slot");
			this.CopyButton.UseVisualStyleBackColor = true;
			this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.Location = new System.Drawing.Point(148, 190);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 8;
			this.DeleteButton.Text = "Delete";
			this.toolTip1.SetToolTip(this.DeleteButton, "Deletes the selected item");
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// DownButton
			// 
			this.DownButton.Location = new System.Drawing.Point(148, 132);
			this.DownButton.Name = "DownButton";
			this.DownButton.Size = new System.Drawing.Size(75, 23);
			this.DownButton.TabIndex = 9;
			this.DownButton.Text = "Down";
			this.toolTip1.SetToolTip(this.DownButton, "Moves the selected item down");
			this.DownButton.UseVisualStyleBackColor = true;
			this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
			// 
			// UpButton
			// 
			this.UpButton.Location = new System.Drawing.Point(148, 103);
			this.UpButton.Name = "UpButton";
			this.UpButton.Size = new System.Drawing.Size(75, 23);
			this.UpButton.TabIndex = 10;
			this.UpButton.Text = "Up";
			this.toolTip1.SetToolTip(this.UpButton, "Moves the selected item up");
			this.UpButton.UseVisualStyleBackColor = true;
			this.UpButton.Click += new System.EventHandler(this.UpButton_Click);
			// 
			// LeftSave
			// 
			this.LeftSave.Location = new System.Drawing.Point(12, 259);
			this.LeftSave.Name = "LeftSave";
			this.LeftSave.Size = new System.Drawing.Size(56, 23);
			this.LeftSave.TabIndex = 11;
			this.LeftSave.Text = "Save...";
			this.toolTip1.SetToolTip(this.LeftSave, "Saves the left BRAM slot to a file");
			this.LeftSave.UseVisualStyleBackColor = true;
			this.LeftSave.Click += new System.EventHandler(this.LeftSave_Click);
			// 
			// RightSave
			// 
			this.RightSave.Location = new System.Drawing.Point(229, 258);
			this.RightSave.Name = "RightSave";
			this.RightSave.Size = new System.Drawing.Size(56, 23);
			this.RightSave.TabIndex = 12;
			this.RightSave.Text = "Save...";
			this.toolTip1.SetToolTip(this.RightSave, "Saves the right BRAM slot to a file");
			this.RightSave.UseVisualStyleBackColor = true;
			this.RightSave.Click += new System.EventHandler(this.RightSave_Click);
			// 
			// LeftFreeSpace
			// 
			this.LeftFreeSpace.AutoSize = true;
			this.LeftFreeSpace.Location = new System.Drawing.Point(9, 285);
			this.LeftFreeSpace.Name = "LeftFreeSpace";
			this.LeftFreeSpace.Size = new System.Drawing.Size(37, 13);
			this.LeftFreeSpace.TabIndex = 13;
			this.LeftFreeSpace.Text = "          ";
			// 
			// RightFreeSpace
			// 
			this.RightFreeSpace.AutoSize = true;
			this.RightFreeSpace.Location = new System.Drawing.Point(226, 285);
			this.RightFreeSpace.Name = "RightFreeSpace";
			this.RightFreeSpace.Size = new System.Drawing.Size(37, 13);
			this.RightFreeSpace.TabIndex = 14;
			this.RightFreeSpace.Text = "          ";
			// 
			// LeftEdited
			// 
			this.LeftEdited.AutoSize = true;
			this.LeftEdited.Location = new System.Drawing.Point(102, 285);
			this.LeftEdited.Name = "LeftEdited";
			this.LeftEdited.Size = new System.Drawing.Size(40, 13);
			this.LeftEdited.TabIndex = 15;
			this.LeftEdited.Text = "Edited ";
			this.LeftEdited.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.LeftEdited.Visible = false;
			// 
			// RightEdited
			// 
			this.RightEdited.AutoSize = true;
			this.RightEdited.Location = new System.Drawing.Point(318, 285);
			this.RightEdited.Name = "RightEdited";
			this.RightEdited.Size = new System.Drawing.Size(40, 13);
			this.RightEdited.TabIndex = 16;
			this.RightEdited.Text = "Edited ";
			this.RightEdited.TextAlign = System.Drawing.ContentAlignment.TopRight;
			this.RightEdited.Visible = false;
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.ToolTipText = "What is this program?";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 306);
			this.Controls.Add(this.RightEdited);
			this.Controls.Add(this.LeftEdited);
			this.Controls.Add(this.RightFreeSpace);
			this.Controls.Add(this.LeftFreeSpace);
			this.Controls.Add(this.RightSave);
			this.Controls.Add(this.LeftSave);
			this.Controls.Add(this.UpButton);
			this.Controls.Add(this.DownButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.CopyButton);
			this.Controls.Add(this.RightBrowse);
			this.Controls.Add(this.LeftBrowse);
			this.Controls.Add(this.RightAddress);
			this.Controls.Add(this.LeftAddress);
			this.Controls.Add(this.RightList);
			this.Controls.Add(this.LeftList);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "PCE BRAM Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button LeftBrowse;
        private System.Windows.Forms.Button RightBrowse;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button LeftSave;
        private System.Windows.Forms.Button RightSave;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mergeMultipleToolStripMenuItem;
        public System.Windows.Forms.ListBox LeftList;
        public System.Windows.Forms.ListBox RightList;
        public System.Windows.Forms.TextBox LeftAddress;
        public System.Windows.Forms.TextBox RightAddress;
        public System.Windows.Forms.Label LeftFreeSpace;
        public System.Windows.Forms.Label RightFreeSpace;
        public System.Windows.Forms.Label LeftEdited;
        public System.Windows.Forms.Label RightEdited;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableWarningsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transferToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mergeFromFoldersToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}

