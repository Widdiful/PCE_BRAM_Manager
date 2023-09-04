using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BRAM_Manager {
	public partial class BRAM_Merge : Form {
		MainForm mainForm;
		BRAM mergeBRAM;
		List<BRAMEntry> SavesToMerge = new List<BRAMEntry>();
		int freeSpace = 0;
		int hoveredIndex = -1;

		public BRAM_Merge() {
			InitializeComponent();
		}

		public void InitialiseForm(MainForm main, string[] paths) {
			mainForm = main;

			// initialise new BRAM
			mergeBRAM = new BRAM();

			// Find all save data and add it to a list
			MergeAddress.Text = "";
			MergeList.Items.Clear();
			foreach (string path in paths) {
				// Provide useful file name
				string fileName = Path.GetFileName(path);
				if (fileName == "bram_exp.brm") {
					fileName = Path.GetFileName(Path.GetDirectoryName(path));
				}

				// Read data from path
				BRAM newBRAM = DataFuncLib.ReadFile(path);
				if (newBRAM.loaded) {
					foreach (BRAMEntry save in newBRAM.saves) {
						SavesToMerge.Add(save);

						// Adding a tab to make it look nice, \t is too big
						string tab = String.Concat(Enumerable.Repeat(" ", IntNumOfDigits(ByteFuncLib.maxBRAMSize) - IntNumOfDigits(save.length)));

						MergeList.Items.Add(String.Format("{0} ({1}B) {2}- {3}", save.name, save.length, tab, fileName));
					}
					MergeAddress.Text += String.Format("\"{0}\" ", path);
				}
			}

			MergeFreeSpace.Text = String.Format("Free: {0}B", mergeBRAM.freeSpace);
		}

		public void InitialiseForm(MainForm main, IEnumerable<string> paths) {
			// Find BRAM files in folders
			List<string> files = new List<string>();
			foreach (string path in paths) {
				foreach (string file in Directory.EnumerateFiles(path, "*.*")) {
					if (DataFuncLib.IsValidBRAMFile(file)) {
						files.Add(file);
					}
				}
			}

			// Regular initialisation
			InitialiseForm(main, files.ToArray());
		}

		public int IntNumOfDigits(int inInt) {
			return (int)Math.Floor(Math.Log10(inInt) + 1);
		}

		private void MergeList_SelectedIndexChanged(object sender, EventArgs e) {
			// Calculate how much free space would be left after adding all selected files
			freeSpace = mergeBRAM.freeSpace;

			foreach (int item in MergeList.SelectedIndices) {
				freeSpace -= SavesToMerge[item].length;
			}

			MergeFreeSpace.Text = String.Format("Free: {0}B", freeSpace);
		}

		private void MergeButton_Click(object sender, EventArgs e) {
			if (freeSpace < 0) {
				MessageBox.Show("Not enough space! Please select fewer saves.");
				return;
			}
			if (freeSpace >= mergeBRAM.freeSpace) {
				MessageBox.Show("No items selected.");
				return;
			}

			string path = GuiFuncLib.SaveFileBrowser();
			if (path == "") {
				return;
			}

			// Add saved files to BRAM and save it
			foreach (int item in MergeList.SelectedIndices) {
				mergeBRAM.saves.Add(SavesToMerge[item]);
			}

			DataFuncLib.SaveFile(ref mergeBRAM, path);
			mainForm.QuickOpen(path);

			Close();
		}

		// Show the full name of the hovered item
		private void MergeList_MouseMove(object sender, MouseEventArgs e) {
			int newHoverIndex = MergeList.IndexFromPoint(e.Location);
			if (newHoverIndex != hoveredIndex) {
				hoveredIndex = newHoverIndex;
				if (hoveredIndex >= 0) {
					toolTip1.SetToolTip(MergeList, MergeList.Items[hoveredIndex].ToString());
				}
			}
		}

		private void MergeList_MouseLeave(object sender, EventArgs e) {
			hoveredIndex = -1;
		}
	}
}
