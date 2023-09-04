using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

/* TODO
 * Renaming BRAM entries
 * Japanese language support
 */

namespace BRAM_Manager {

	// Primary struct for all BRAM information
	public struct BRAM
	{
		public BRAM() {
			byte[] newData = new byte[ByteFuncLib.maxBRAMSize];
			ByteFuncLib.WriteBytes(ByteFuncLib.newFileBytes, newData, 0, ByteFuncLib.newFileBytes.Length);
			data = newData;

			byte[] newHeader = new byte[ByteFuncLib.headerSize];
			ByteFuncLib.WriteBytes(ByteFuncLib.newFileBytes, newHeader, 0, ByteFuncLib.newFileBytes.Length);
			header = newHeader;

			saves = new List<BRAMEntry>();
			nextSlot = ByteFuncLib.headerSize;
			freeSpace = ByteFuncLib.maxBRAMSize - ByteFuncLib.headerSize;
			loaded = true;
		}

		public byte[] data;				// Entire 2048-byte set of data for the BRAM
		public byte[] header;			// BRAM header
		public List<BRAMEntry> saves;	// Each save file, including save header
		public int nextSlot;			// The next available slot that can be saved in
		public int freeSpace;			// How much free space is left in the BRAM in bytes
		public bool loaded;				// Whether or not the file has been loaded
		public bool edited;				// Weather or not the file has been edited
	}

	// Data required for a BRAM entry
	public struct BRAMEntry
	{
		public byte[] data;				// Entire set of data for the save data
		public string name;				// Name of the save file
		public int length;				// Total length of the save file
		public int startsAt;			// Which byte in the BRAM does this file start at
	}

	public partial class MainForm : Form {
		public BRAM leftBRAM, rightBRAM;

		public MainForm() {
			InitializeComponent();

			leftBRAM = new BRAM();
			rightBRAM = new BRAM();

			disableWarningsToolStripMenuItem.Checked = Properties.Settings.Default.disablePopups;
			GuiFuncLib.savedInitialDirectory = Properties.Settings.Default.savedInitialDirectory;
		}

		//--------------------------------------------------
		// Main functions
		//--------------------------------------------------

		// Custom data handling ----------------------------

		// Opens a file to a free BRAM slot
		public void QuickOpen(string path) {
			if (!leftBRAM.loaded) {
				DataFuncLib.OpenAndLoadFile(path, ref leftBRAM, ref LeftList, ref LeftAddress, ref LeftFreeSpace);
			}
			else if (!rightBRAM.loaded) {
				DataFuncLib.OpenAndLoadFile(path, ref rightBRAM, ref RightList, ref RightAddress, ref RightFreeSpace);
			}
		}

		// Copies a save file from one BRAM to another
		public void CopyEntry(ref BRAM sourceBRAM, ref BRAM targetBRAM, ref ListBox targetList, int index) {
			DataFuncLib.CopyEntry(ref sourceBRAM, ref targetBRAM, ref targetList, index);

			LeftEdited.Visible = leftBRAM.edited;
			RightEdited.Visible = rightBRAM.edited;

			LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
			RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);
		}

		// Deletes a save file entry from a BRAM
		public void DeleteEntry(ref BRAM bram, ref ListBox list, int index) {
			DataFuncLib.DeleteEntry(ref bram, ref list, index);

			LeftEdited.Visible = leftBRAM.edited;
			RightEdited.Visible = rightBRAM.edited;

			if (leftBRAM.loaded)
				LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
			if (rightBRAM.loaded)
				RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);
		}

		public void MoveEntry(ref BRAM bram, ref ListBox list, int index, int direction) {
			DataFuncLib.MoveEntry(ref bram, ref list, index, direction);

			LeftEdited.Visible = leftBRAM.edited;
			RightEdited.Visible = rightBRAM.edited;
		}

		public void ClearAllBRAMs() {
			leftBRAM = new BRAM();
			rightBRAM = new BRAM();
			LeftList.Items.Clear();
			RightList.Items.Clear();
			LeftAddress.Text = "";
			RightAddress.Text = "";
			LeftFreeSpace.Text = "";
			RightFreeSpace.Text = "";
			LeftEdited.Visible = false;
			RightEdited.Visible = false;
		}

		//--------------------------------------------------
		// GUI Interactions
		//--------------------------------------------------

		// Main interface
		private void LeftBrowse_Click(object sender, EventArgs e) {
			string openPath = LeftAddress.Text.Length > 0 ? Path.GetDirectoryName(LeftAddress.Text) : GuiFuncLib.savedInitialDirectory;
			string path = GuiFuncLib.OpenFileBrowser(openPath);
			if (path.Length == 0)
				return;

			if (leftBRAM.edited) {
				if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to load?")) {
					return;
				}
			}

			if (DataFuncLib.OpenAndLoadFile(path, ref leftBRAM, ref LeftList, ref LeftAddress, ref LeftFreeSpace))
				LeftEdited.Visible = false;
		}

		private void RightBrowse_Click(object sender, EventArgs e) {
			string openPath = RightAddress.Text.Length > 0 ? Path.GetDirectoryName(RightAddress.Text) : GuiFuncLib.savedInitialDirectory;
			string path = GuiFuncLib.OpenFileBrowser(openPath);
			if (path.Length == 0)
				return;

			if (rightBRAM.edited) {
				if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to load?")) {
					return;
				}
			}

			if (DataFuncLib.OpenAndLoadFile(path, ref rightBRAM, ref RightList, ref RightAddress, ref RightFreeSpace))
				RightEdited.Visible = false;
		}

		private void LeftSave_Click(object sender, EventArgs e) {
			if (leftBRAM.loaded) {
				string directory = Directory.Exists(LeftAddress.Text) ? Path.GetDirectoryName(LeftAddress.Text) : GuiFuncLib.savedInitialDirectory;
				string fileName = Directory.Exists(LeftAddress.Text) ? Path.GetFileName(LeftAddress.Text) : "";

				string saveLocation = GuiFuncLib.SaveFileBrowser(directory, fileName);
				DataFuncLib.SaveFile(ref leftBRAM, saveLocation);
				LeftAddress.Text = saveLocation;
				LeftEdited.Visible = leftBRAM.edited;
			}
		}

		private void RightSave_Click(object sender, EventArgs e) {
			if (rightBRAM.loaded) {
				string directory = Directory.Exists(RightAddress.Text) ? Path.GetDirectoryName(RightAddress.Text) : GuiFuncLib.savedInitialDirectory;
				string fileName = Directory.Exists(RightAddress.Text) ? Path.GetFileName(RightAddress.Text) : "";

				string saveLocation = GuiFuncLib.SaveFileBrowser(directory, fileName);
				DataFuncLib.SaveFile(ref rightBRAM, saveLocation);
				RightAddress.Text = saveLocation;
				RightEdited.Visible = rightBRAM.edited;
			}
		}

		private void DeleteButton_Click(object sender, EventArgs e) {
			ref BRAM selectedBRAM = ref leftBRAM;
			ref ListBox selectedList = ref LeftList;
			if (LeftList.SelectedIndex >= 0) {
				selectedBRAM = ref leftBRAM;
				selectedList = ref LeftList;
			}
			else if (RightList.SelectedIndex >= 0) {
				selectedBRAM = ref rightBRAM;
				selectedList = ref RightList;
			}
			else {
				MessageBox.Show("Please select a file to delete.");
				return;
			}

			int index = selectedList.SelectedIndex;
			DeleteEntry(ref selectedBRAM, ref selectedList, index);
		}

		private void CopyButton_Click(object sender, EventArgs e) {
			if (!(leftBRAM.loaded && rightBRAM.loaded)) {
				MessageBox.Show("Please load two BRAM files.");
				return;
			}

			ref BRAM selectedBRAM = ref leftBRAM;
			ref BRAM otherBRAM = ref rightBRAM;
			ListBox selectedList = new ListBox();
			ListBox otherList = new ListBox();
			if (LeftList.SelectedIndex >= 0) {
				selectedList = LeftList;
				otherList = RightList;
			}
			else if (RightList.SelectedIndex >= 0) {
				selectedBRAM = ref rightBRAM;
				otherBRAM = ref leftBRAM;
				selectedList = RightList;
				otherList = LeftList;
			}
			else {
				MessageBox.Show("Please select a file to copy.");
				return;
			}

			CopyEntry(ref selectedBRAM, ref otherBRAM, ref otherList, selectedList.SelectedIndex);
		}

		private void UpButton_Click(object sender, EventArgs e) {
			ref BRAM selectedBRAM = ref leftBRAM;
			ref ListBox selectedList = ref LeftList;
			if (LeftList.SelectedIndex >= 0) {
				selectedBRAM = ref leftBRAM;
				selectedList = ref LeftList;
			}
			else if (RightList.SelectedIndex >= 0) {
				selectedBRAM = ref rightBRAM;
				selectedList = ref RightList;
			}
			else {
				MessageBox.Show("Please select a file to move.");
				return;
			}

			int index = selectedList.SelectedIndex;
			MoveEntry(ref selectedBRAM, ref selectedList, index, -1);
		}

		private void DownButton_Click(object sender, EventArgs e) {
			ref BRAM selectedBRAM = ref leftBRAM;
			ref ListBox selectedList = ref LeftList;
			if (LeftList.SelectedIndex >= 0) {
				selectedBRAM = ref leftBRAM;
				selectedList = ref LeftList;
			}
			else if (RightList.SelectedIndex >= 0) {
				selectedBRAM = ref rightBRAM;
				selectedList = ref RightList;
			}
			else {
				MessageBox.Show("Please select a file to move.");
				return;
			}

			int index = selectedList.SelectedIndex;
			MoveEntry(ref selectedBRAM, ref selectedList, index, 1);
		}

		// Toolbar --------------------

		// Create new file
		private void newBRAMToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!leftBRAM.loaded) {

			}
		}

		// Open merge tool
		private void mergeMultipleToolStripMenuItem_Click(object sender, EventArgs e) {
			string openPath = GuiFuncLib.savedInitialDirectory;
			string[] path = GuiFuncLib.OpenMultiFileBrowser(openPath);
			if (path.Length == 0)
				return;

			BRAM_Merge mergeForm = new BRAM_Merge();
			mergeForm.InitialiseForm(this, path);
			mergeForm.StartPosition = FormStartPosition.Manual;
			mergeForm.Left = this.Left;
			mergeForm.Top = this.Top;
			mergeForm.Show(this);
		}

		private void mergeFromFoldersToolStripMenuItem_Click(object sender, EventArgs e) {
			string openPath = GuiFuncLib.savedInitialDirectory;
			IEnumerable<string> paths = GuiFuncLib.OpenMultiFolderBrowser(openPath);
			if (paths.Count() == 0)
				return;

			BRAM_Merge mergeForm = new BRAM_Merge();
			mergeForm.InitialiseForm(this, paths);
			mergeForm.StartPosition = FormStartPosition.Manual;
			mergeForm.Left = this.Left;
			mergeForm.Top = this.Top;
			mergeForm.Show(this);
		}

		// Unload both BRAM slots
		private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) {
			if (leftBRAM.edited || rightBRAM.edited) {
				if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to clear?")) {
					return;
				}
			}

			ClearAllBRAMs();
		}

		// Toggle disable warnings option
		private void disableWarningsToolStripMenuItem_CheckedChanged(object sender, EventArgs e) {
			Properties.Settings.Default.disablePopups = disableWarningsToolStripMenuItem.Checked;
			Properties.Settings.Default.Save();
		}

		// Open the transfer tool
		private void transferToolToolStripMenuItem_Click(object sender, EventArgs e) {
			TransferTool transferForm = new TransferTool();
			transferForm.StartPosition = FormStartPosition.Manual;
			transferForm.Left = this.Left;
			transferForm.Top = this.Top;
			transferForm.Show(this);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			About aboutForm = new About();
			aboutForm.StartPosition = FormStartPosition.Manual;
			aboutForm.Left = this.Left;
			aboutForm.Top = this.Top;
			aboutForm.Show(this);
		}

		// Misc --------------------

		// Ensuring only one list has a selected item at a time
		private void LeftList_SelectedIndexChanged(object sender, EventArgs e) {
			if (LeftList.Focused)
				RightList.ClearSelected();
		}

		private void RightList_SelectedIndexChanged(object sender, EventArgs e) {
			if (RightList.Focused)
				LeftList.ClearSelected();
		}

		// Drag and drop ----------
		private void LeftList_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}

		private void LeftList_DragDrop(object sender, DragEventArgs e)
		{
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (s.Length > 0)
			{
				if (leftBRAM.edited) {
					if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to load?")) {
						return;
					}
				}

				if (DataFuncLib.OpenAndLoadFile(s[0], ref leftBRAM, ref LeftList, ref LeftAddress, ref LeftFreeSpace))
					LeftEdited.Visible = false;
			}
		}

		private void RightList_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;
		}

		private void RightList_DragDrop(object sender, DragEventArgs e)
		{
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (s.Length > 0)
			{
				if (rightBRAM.edited) {
					if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to load?")) {
						return;
					}
				}

				if (DataFuncLib.OpenAndLoadFile(s[0], ref rightBRAM, ref RightList, ref RightAddress, ref RightFreeSpace))
					RightEdited.Visible = false;
			}
		}

		// Confirm close if there are unsaved changes
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			if (leftBRAM.edited || rightBRAM.edited) {
				if (GuiFuncLib.WarningPopup("There are unsaved changes. Are you sure you want to quit?")){
					e.Cancel = true;
				}
			}
		}
	}
}
