namespace BRAM_Manager
{
	partial class About
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.VersionNumLabel = new System.Windows.Forms.Label();
			this.pictureBoxWithInterpolation1 = new BRAM_Manager.PictureBoxWithInterpolation();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxWithInterpolation1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(296, 26);
			this.label1.TabIndex = 0;
			this.label1.Text = "PC Engine BRAM Manager is free and open source software.\r\nCreated by wid";
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(12, 65);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(40, 13);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "GitHub";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// VersionNumLabel
			// 
			this.VersionNumLabel.AutoSize = true;
			this.VersionNumLabel.Location = new System.Drawing.Point(12, 52);
			this.VersionNumLabel.Name = "VersionNumLabel";
			this.VersionNumLabel.Size = new System.Drawing.Size(54, 13);
			this.VersionNumLabel.TabIndex = 3;
			this.VersionNumLabel.Text = "Version: v";
			// 
			// pictureBoxWithInterpolation1
			// 
			this.pictureBoxWithInterpolation1.Image = global::BRAM_Manager.Properties.Resources.icon;
			this.pictureBoxWithInterpolation1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			this.pictureBoxWithInterpolation1.Location = new System.Drawing.Point(337, 9);
			this.pictureBoxWithInterpolation1.Name = "pictureBoxWithInterpolation1";
			this.pictureBoxWithInterpolation1.Size = new System.Drawing.Size(69, 69);
			this.pictureBoxWithInterpolation1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxWithInterpolation1.TabIndex = 4;
			this.pictureBoxWithInterpolation1.TabStop = false;
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(58, 65);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(29, 13);
			this.linkLabel2.TabIndex = 5;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Help";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// About
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 87);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.pictureBoxWithInterpolation1);
			this.Controls.Add(this.VersionNumLabel);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.Text = "About";
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxWithInterpolation1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label VersionNumLabel;
		private PictureBoxWithInterpolation pictureBoxWithInterpolation1;
		private System.Windows.Forms.LinkLabel linkLabel2;
	}
}