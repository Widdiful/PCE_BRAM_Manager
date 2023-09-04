using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// This library contains functions related to the user interface.
internal class GuiFuncLib
{
	public static string savedInitialDirectory = "c:\\";
	public static string fileDialogFilter = 
		"All BRAM files (*.bup;*.brm;*.sav)|*.bup;*.brm;*.sav|" +
		"BUP files (SSDS3) (*.bup)|*.bup|" +
		"BRM files (Everdrive) (*.brm)|*.brm|" +
		"SAV files (Emulator) (*.sav)|*.sav|" +
		"All files (*.*)|*.*";

	public static string OpenFileBrowser()
	{
		return OpenFileBrowser(savedInitialDirectory);
	}

	// Opens a file browser and returns the path to the selected file
	public static string OpenFileBrowser(string initialDirectory)
	{
		using (OpenFileDialog openFileDialog = new OpenFileDialog())
		{
			openFileDialog.InitialDirectory = initialDirectory;
			openFileDialog.Filter = fileDialogFilter;
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				savedInitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
				BRAM_Manager.Properties.Settings.Default.savedInitialDirectory = savedInitialDirectory;
				BRAM_Manager.Properties.Settings.Default.Save();
				return openFileDialog.FileName;
			}
		}

		return string.Empty;
	}

	// Opens a file browser and returns the paths to the selected files
	public static string[] OpenMultiFileBrowser(string initialDirectory)
	{
		using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
			openFileDialog.InitialDirectory = initialDirectory;
			openFileDialog.Filter = fileDialogFilter;
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;
			openFileDialog.Multiselect = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				savedInitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
				BRAM_Manager.Properties.Settings.Default.savedInitialDirectory = savedInitialDirectory;
				BRAM_Manager.Properties.Settings.Default.Save();
				return openFileDialog.FileNames;
			}
		}

		return new string[0];
	}

	public static string OpenFolderBrowser(string initialDirectory) {
		using (CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog()) {
			openFolderDialog.InitialDirectory = initialDirectory;
			openFolderDialog.IsFolderPicker = true;

			if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				return openFolderDialog.FileName;
			}
		}

		return string.Empty;
	}

	public static IEnumerable<string> OpenMultiFolderBrowser(string initialDirectory) {
		using (CommonOpenFileDialog openFolderDialog = new CommonOpenFileDialog()) {
			openFolderDialog.InitialDirectory = initialDirectory;
			openFolderDialog.IsFolderPicker = true;
			openFolderDialog.Multiselect = true;

			if (openFolderDialog.ShowDialog() == CommonFileDialogResult.Ok) {
				return openFolderDialog.FileNames;
			}
		}

		return Enumerable.Empty<string>();
	}

	// Opens a file browser and returns a path to the new file
	public static string SaveFileBrowser(string defaultDirectory = "", string defaultFileName = "")
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();

		if (defaultDirectory.Length > 0)
			saveFileDialog.InitialDirectory = defaultDirectory;
		else
			saveFileDialog.InitialDirectory = savedInitialDirectory;
		if (defaultFileName.Length > 0)
			saveFileDialog.FileName = defaultFileName;
		saveFileDialog.Filter = fileDialogFilter;
		saveFileDialog.FilterIndex = 1;
		saveFileDialog.RestoreDirectory = true;

		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			savedInitialDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
			BRAM_Manager.Properties.Settings.Default.savedInitialDirectory = savedInitialDirectory;
			BRAM_Manager.Properties.Settings.Default.Save();
			return saveFileDialog.FileName;
		}

		return string.Empty;
	}

	public static bool WarningPopup(string msg) {
		if (BRAM_Manager.Properties.Settings.Default.disablePopups)
			return false;

		return MessageBox.Show(msg, "", MessageBoxButtons.YesNo) == DialogResult.No;
	}
}
