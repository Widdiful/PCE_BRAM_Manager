namespace BRAM_Manager
{
    partial class TransferTool
    {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferTool));
			this.SourceDirectoryText = new System.Windows.Forms.TextBox();
			this.DestinationDirectoryText = new System.Windows.Forms.TextBox();
			this.GameDirectoryText = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.GameDirectoryLabel = new System.Windows.Forms.Label();
			this.BrowseSourceDirectory = new System.Windows.Forms.Button();
			this.BrowseDestinationDirectory = new System.Windows.Forms.Button();
			this.TransferButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.Tooltip1 = new System.Windows.Forms.ToolTip(this.components);
			this.BrowseGameDirectory = new System.Windows.Forms.Button();
			this.SourceTypeComboBox = new System.Windows.Forms.ComboBox();
			this.DestinationTypeComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// SourceDirectoryText
			// 
			this.SourceDirectoryText.Location = new System.Drawing.Point(124, 12);
			this.SourceDirectoryText.Name = "SourceDirectoryText";
			this.SourceDirectoryText.Size = new System.Drawing.Size(274, 20);
			this.SourceDirectoryText.TabIndex = 0;
			this.Tooltip1.SetToolTip(this.SourceDirectoryText, "Folder where all your save files are stored.\r\nWill be \"bup\" for SSDS3, \"gamedata\"" +
        " for Everdrive\r\nand \"sav\" for Mednafen.");
			// 
			// DestinationDirectoryText
			// 
			this.DestinationDirectoryText.Location = new System.Drawing.Point(124, 38);
			this.DestinationDirectoryText.Name = "DestinationDirectoryText";
			this.DestinationDirectoryText.Size = new System.Drawing.Size(274, 20);
			this.DestinationDirectoryText.TabIndex = 2;
			this.Tooltip1.SetToolTip(this.DestinationDirectoryText, "Where you want to copy the save files");
			// 
			// GameDirectoryText
			// 
			this.GameDirectoryText.Enabled = false;
			this.GameDirectoryText.Location = new System.Drawing.Point(124, 64);
			this.GameDirectoryText.Name = "GameDirectoryText";
			this.GameDirectoryText.Size = new System.Drawing.Size(274, 20);
			this.GameDirectoryText.TabIndex = 4;
			this.Tooltip1.SetToolTip(this.GameDirectoryText, "When transferring to Everdrive, this directory will be used to determine the game" +
        "\'s file extension which the Everdrive needs to load the data.");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Source Directory";
			this.Tooltip1.SetToolTip(this.label1, "Folder where all your save files are stored.\r\nWill be \"bup\" for SSDS3, \"gamedata\"" +
        " for Everdrive\r\nand \"sav\" for Mednafen.");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Destination Directory";
			this.Tooltip1.SetToolTip(this.label2, "Where you want to copy the save files");
			// 
			// GameDirectoryLabel
			// 
			this.GameDirectoryLabel.AutoSize = true;
			this.GameDirectoryLabel.Location = new System.Drawing.Point(13, 67);
			this.GameDirectoryLabel.Name = "GameDirectoryLabel";
			this.GameDirectoryLabel.Size = new System.Drawing.Size(80, 13);
			this.GameDirectoryLabel.TabIndex = 5;
			this.GameDirectoryLabel.Text = "Game Directory";
			this.Tooltip1.SetToolTip(this.GameDirectoryLabel, "When transferring to Everdrive, this directory will be used to determine the game" +
        "\'s file extension which the Everdrive needs to load the data.");
			// 
			// BrowseSourceDirectory
			// 
			this.BrowseSourceDirectory.Location = new System.Drawing.Point(404, 10);
			this.BrowseSourceDirectory.Name = "BrowseSourceDirectory";
			this.BrowseSourceDirectory.Size = new System.Drawing.Size(26, 23);
			this.BrowseSourceDirectory.TabIndex = 1;
			this.BrowseSourceDirectory.Text = "...";
			this.Tooltip1.SetToolTip(this.BrowseSourceDirectory, "Browse...");
			this.BrowseSourceDirectory.UseVisualStyleBackColor = true;
			this.BrowseSourceDirectory.Click += new System.EventHandler(this.BrowseSourceDirectory_Click);
			// 
			// BrowseDestinationDirectory
			// 
			this.BrowseDestinationDirectory.Location = new System.Drawing.Point(404, 36);
			this.BrowseDestinationDirectory.Name = "BrowseDestinationDirectory";
			this.BrowseDestinationDirectory.Size = new System.Drawing.Size(26, 23);
			this.BrowseDestinationDirectory.TabIndex = 3;
			this.BrowseDestinationDirectory.Text = "...";
			this.Tooltip1.SetToolTip(this.BrowseDestinationDirectory, "Browse...");
			this.BrowseDestinationDirectory.UseVisualStyleBackColor = true;
			this.BrowseDestinationDirectory.Click += new System.EventHandler(this.BrowseDestinationDirectory_Click);
			// 
			// TransferButton
			// 
			this.TransferButton.Location = new System.Drawing.Point(404, 88);
			this.TransferButton.Name = "TransferButton";
			this.TransferButton.Size = new System.Drawing.Size(119, 23);
			this.TransferButton.TabIndex = 6;
			this.TransferButton.Text = "Transfer";
			this.Tooltip1.SetToolTip(this.TransferButton, "Begin the transfer!");
			this.TransferButton.UseVisualStyleBackColor = true;
			this.TransferButton.Click += new System.EventHandler(this.TransferButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(400, 52);
			this.label4.TabIndex = 9;
			this.label4.Text = resources.GetString("label4.Text");
			// 
			// BrowseGameDirectory
			// 
			this.BrowseGameDirectory.Location = new System.Drawing.Point(404, 61);
			this.BrowseGameDirectory.Name = "BrowseGameDirectory";
			this.BrowseGameDirectory.Size = new System.Drawing.Size(26, 23);
			this.BrowseGameDirectory.TabIndex = 5;
			this.BrowseGameDirectory.Text = "...";
			this.Tooltip1.SetToolTip(this.BrowseGameDirectory, "Browse...");
			this.BrowseGameDirectory.UseVisualStyleBackColor = true;
			this.BrowseGameDirectory.Click += new System.EventHandler(this.BrowseGameDirectory_Click);
			// 
			// SourceTypeComboBox
			// 
			this.SourceTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SourceTypeComboBox.FormattingEnabled = true;
			this.SourceTypeComboBox.Location = new System.Drawing.Point(437, 11);
			this.SourceTypeComboBox.Name = "SourceTypeComboBox";
			this.SourceTypeComboBox.Size = new System.Drawing.Size(90, 21);
			this.SourceTypeComboBox.TabIndex = 10;
			this.Tooltip1.SetToolTip(this.SourceTypeComboBox, "What format are the source save files in?");
			this.SourceTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.SourceTypeComboBox_SelectedIndexChanged);
			// 
			// DestinationTypeComboBox
			// 
			this.DestinationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DestinationTypeComboBox.FormattingEnabled = true;
			this.DestinationTypeComboBox.Location = new System.Drawing.Point(437, 37);
			this.DestinationTypeComboBox.Name = "DestinationTypeComboBox";
			this.DestinationTypeComboBox.Size = new System.Drawing.Size(90, 21);
			this.DestinationTypeComboBox.TabIndex = 11;
			this.Tooltip1.SetToolTip(this.DestinationTypeComboBox, "What format do you want to transfer to?");
			this.DestinationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.DestinationTypeComboBox_SelectedIndexChanged);
			// 
			// TransferTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(535, 145);
			this.Controls.Add(this.DestinationTypeComboBox);
			this.Controls.Add(this.SourceTypeComboBox);
			this.Controls.Add(this.TransferButton);
			this.Controls.Add(this.BrowseGameDirectory);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.BrowseDestinationDirectory);
			this.Controls.Add(this.BrowseSourceDirectory);
			this.Controls.Add(this.GameDirectoryLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.GameDirectoryText);
			this.Controls.Add(this.DestinationDirectoryText);
			this.Controls.Add(this.SourceDirectoryText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TransferTool";
			this.ShowInTaskbar = false;
			this.Text = "Transfer Tool";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SourceDirectoryText;
        private System.Windows.Forms.TextBox DestinationDirectoryText;
        private System.Windows.Forms.TextBox GameDirectoryText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label GameDirectoryLabel;
        private System.Windows.Forms.Button BrowseSourceDirectory;
        private System.Windows.Forms.Button BrowseDestinationDirectory;
        private System.Windows.Forms.Button TransferButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip Tooltip1;
        private System.Windows.Forms.Button BrowseGameDirectory;
        private System.Windows.Forms.ComboBox SourceTypeComboBox;
        private System.Windows.Forms.ComboBox DestinationTypeComboBox;
    }
}