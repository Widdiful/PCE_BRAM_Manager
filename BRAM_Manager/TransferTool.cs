using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BRAM_Manager
{
	public partial class TransferTool : Form
	{
		FolderFormat sourceFormat = FolderFormat.None;
		FolderFormat destinationFormat = FolderFormat.None;
		IEnumerable<string> gameDirectoryEnumerator = Enumerable.Empty<string>();

		enum FolderFormat{
			None,
			SSDS3,
			Everdrive,
			Emulator
		}

		struct FileData {
			public string filePath;
			public string gameName;
		}

		public TransferTool() {
			InitializeComponent();

			SourceTypeComboBox.DataSource = Enum.GetValues(typeof(FolderFormat));
			DestinationTypeComboBox.DataSource = Enum.GetValues(typeof(FolderFormat));
		}

		// Main functions ----------------------------------

		private FolderFormat CalculateFolderFormat(string directory) {
			// bup folder means SSDS3
			if (directory.EndsWith("\\bup")) {
				return FolderFormat.SSDS3;
			}

			// gamedata folder means Everdrive
			if (directory.EndsWith("\\gamedata")) {
				return FolderFormat.Everdrive;
			}

			if (directory.EndsWith("\\sav")) {
				return FolderFormat.Emulator;
			}

			return FolderFormat.None;
		}

		private void StartTransfer() {
			// Find the appropriate files
			List<FileData> files = new List<FileData>();
			switch (sourceFormat) {
				case FolderFormat.SSDS3:
					files = GetSSDS3Files(SourceDirectoryText.Text);
					break;
				case FolderFormat.Everdrive:
					files = GetEverdriveFiles(SourceDirectoryText.Text);
					break;
				case FolderFormat.Emulator:
					files = GetEmulatorFiles(SourceDirectoryText.Text);
					break;
				default:
					break;
			}

			// Transfer them to the appropriate format
			switch (destinationFormat) {
				case FolderFormat.SSDS3:
					TransferToSSDS3(files, DestinationDirectoryText.Text);
					break;
				case FolderFormat.Everdrive:
					TransferToEverdrive(files, DestinationDirectoryText.Text);
					break;
				case FolderFormat.Emulator:
					TransferToEmulator(files, DestinationDirectoryText.Text);
					break;
				default:
					break;
			}

			MessageBox.Show("Done!");
		}

		private List<FileData> GetSSDS3Files(string directory) {
			List<FileData> result = new List<FileData>();

			// Get all .bup files in the directory
			foreach (string file in Directory.EnumerateFiles(directory)) {
				if (file.EndsWith(".bup") && DataFuncLib.IsValidBRAMFile(file)) {
					FileData newData;
					newData.filePath = file;
					newData.gameName = Path.GetFileNameWithoutExtension(file);

					result.Add(newData);
				}
			}

			return result;
		}

		private List<FileData> GetEverdriveFiles(string directory) {
			List<FileData> result = new List<FileData>();

			// Get all .bup files in the directory
			foreach (string dir in Directory.EnumerateDirectories(directory)) {
				foreach (string file in Directory.EnumerateFiles(dir)) {
					if (file.EndsWith(".brm") && DataFuncLib.IsValidBRAMFile(file)) {
						FileData newData;
						newData.filePath = file;
						newData.gameName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file));
						result.Add(newData);
					}
				}
			}

			return result;
		}

		private List<FileData> GetEmulatorFiles(string directory) {
			List<FileData> result = new List<FileData>();

			// Get all PCE .sav files in the directory
			foreach (string file in Directory.EnumerateFiles(directory, "*.*", SearchOption.AllDirectories)) {
				if (DataFuncLib.IsValidBRAMFile(file)) {
					FileData newData;
					newData.filePath = file;
					newData.gameName = Path.GetFileNameWithoutExtension(file).Split('.')[0];
					result.Add(newData);
				}
			}

			return result;
		}

		private void TransferToSSDS3(List<FileData> files, string directory) {
			// Transfer each file to SSDS3 format
			foreach (FileData file in files) {
				TransferToSSDS3(file, DestinationDirectoryText.Text);
			}
		}

		private void TransferToSSDS3(FileData file, string directory) {
			// Copy the file
			string destinationFileName = directory + "\\" + file.gameName + ".bup";
			File.Copy(file.filePath, destinationFileName, true);
		}

		private void TransferToEverdrive(List<FileData> files, string directory) {
			// Create enumerator to find game file extensions
			if (GameDirectoryText.Text != "") {
				gameDirectoryEnumerator = Directory.EnumerateFiles(GameDirectoryText.Text, "*.*", SearchOption.AllDirectories);
				gameDirectoryEnumerator.OrderBy(filename => filename);
			}

			// Transfer each file to Everdrive format
			foreach (FileData file in files) {
				TransferToEverdrive(file, DestinationDirectoryText.Text);
			}
		}

		private void TransferToEverdrive(FileData file, string directory) {
			string[] validExtensions = new[] { ".pce", ".cue", ".iso", ".img" };
			string fileExtension = "";

			// Find game extension
			// This is a bit slow but games may not be stored alphabetically, so it's safer to do a full check
			if (gameDirectoryEnumerator.Count() > 0) {
				foreach (string game in gameDirectoryEnumerator) {
					if (Path.GetFileNameWithoutExtension(game) == file.gameName) {
						string gameExtension = Path.GetExtension(game);

						if (validExtensions.Contains(gameExtension)) {
							fileExtension = gameExtension;
							break;
						}
					}
				}
			}
			else {
				// Assume cue as that's most likely
				fileExtension = ".cue";
			}

			// Only transfer games with a discovered file extension
			if (fileExtension != "") {
				string folderName = directory + "\\" + file.gameName + fileExtension;
				string destinationFileName = folderName + "\\bram_exp.brm";

				// Create the directory and copy the file
				Directory.CreateDirectory(folderName);
				File.Copy(file.filePath, destinationFileName, true);
			}
		}

		private void TransferToEmulator(List<FileData> files, string directory) {
			// Transfer each file to emulator format
			foreach (FileData file in files) {
				TransferToEmulator(file, DestinationDirectoryText.Text);
			}
		}

		private void TransferToEmulator(FileData file, string directory) {
			// Copy the file
			string destinationFileName = directory + "\\" + file.gameName + ".sav";
			File.Copy(file.filePath, destinationFileName, true);
		}

		// GUI functions ----------------------------------

		private void BrowseSourceDirectory_Click(object sender, EventArgs e) {
			string folderPath;
			folderPath = GuiFuncLib.OpenFolderBrowser(GuiFuncLib.savedInitialDirectory);
			if (folderPath.Length == 0) {
				return;
			}

			sourceFormat = CalculateFolderFormat(folderPath);
			SourceTypeComboBox.SelectedIndex = (int)sourceFormat;
			SourceDirectoryText.Text = folderPath;
		}

		private void BrowseDestinationDirectory_Click(object sender, EventArgs e) {
			string folderPath = GuiFuncLib.OpenFolderBrowser(GuiFuncLib.savedInitialDirectory);
			if (folderPath.Length == 0) {
				return;
			}

			destinationFormat = CalculateFolderFormat(folderPath);
			DestinationTypeComboBox.SelectedIndex = (int)destinationFormat;
			DestinationDirectoryText.Text = folderPath;
		}

		private void BrowseGameDirectory_Click(object sender, EventArgs e) {
			GameDirectoryText.Text = GuiFuncLib.OpenFolderBrowser(GuiFuncLib.savedInitialDirectory);
		}

		private void TransferButton_Click(object sender, EventArgs e) {
			if (sourceFormat == FolderFormat.None) {
				MessageBox.Show("No source folder format selected");
				return;
			}

			if (destinationFormat == FolderFormat.None) {
				MessageBox.Show("No destination folder format selected");
				return;
			}

			if (SourceDirectoryText.Text.Length == 0) {
				MessageBox.Show("No source directory selected");
				return;
			}

			if (DestinationDirectoryText.Text.Length == 0) {
				MessageBox.Show("No destination directory selected");
				return;
			}

			if (GameDirectoryText.Text.Length == 0 && destinationFormat == FolderFormat.Everdrive) {
				if (GuiFuncLib.WarningPopup("Attempting a transfer to Everdrive without specifying a game directory, all files will be assumed .cue! This means you will have to manually edit the folders to get non-cue games to work. Are you sure?")) {
					return;
				}
			}

			if (GuiFuncLib.WarningPopup("This process will overwrite files in the destination directory, are you sure?")) {
				return;
			}

			StartTransfer();
		}

		private void SourceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			FolderFormat outVal;
			Enum.TryParse<FolderFormat>(SourceTypeComboBox.SelectedValue.ToString(), out outVal);
			sourceFormat = outVal;
		}

		private void DestinationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			FolderFormat outVal;
			Enum.TryParse<FolderFormat>(DestinationTypeComboBox.SelectedValue.ToString(), out outVal);
			destinationFormat = outVal;

			GameDirectoryText.Enabled = BrowseGameDirectory.Enabled = outVal == FolderFormat.Everdrive;
		}
	}
}
