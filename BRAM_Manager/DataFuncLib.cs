using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using BRAM_Manager;
using static System.Net.WebRequestMethods;

// This library contains functions related to data handling on the PC-side
internal class DataFuncLib
{
	public static int nextSlotStartByte = 6;
	public static int nextSlotLength = 2;

	// Reads a file and converts it to a BRAM data entry
	public static BRAM ReadFile(string file) {
		BRAM result = new BRAM();
		result.saves = new List<BRAMEntry>();

		// Allow loading from folder
		if (Directory.Exists(file)){
			foreach(string path in Directory.EnumerateFiles(file)) {
				if (IsValidBRAMFile(path)) {
					file = path;
					break;
				}
			}
		}

		string dataString;

		try {
			dataString = System.IO.File.ReadAllText(file);
		}
		catch (IOException) {
			MessageBox.Show("File is in use.");
			return result;
		}

		byte[] data = System.IO.File.ReadAllBytes(file);
		if (dataString.StartsWith("HUBM")) {
			result.data = data;
			result.header = ByteFuncLib.ReadBytes(data, 0, ByteFuncLib.headerSize);

			// bytes 7 and 8 are pointers to the next available memory slot
			byte[] nextSlotBytes = ByteFuncLib.ReadBytes(data, nextSlotStartByte, nextSlotStartByte + nextSlotLength);
			result.nextSlot = BitConverter.ToUInt16(nextSlotBytes, 0);
			result.nextSlot -= ByteFuncLib.memoryOffset; // remove the offset of 0x8000 to get the number we actually want
			result.freeSpace = ByteFuncLib.maxBRAMSize - result.nextSlot;

			int nextSaveIndex = ByteFuncLib.headerSize;
			int nextLength = BitConverter.ToInt16(data, nextSaveIndex);

			// loop through saves until reaching a file that is 0 bytes long (no file)
			while (nextLength != 0) {
				BRAMEntry newEntry = new BRAMEntry();

				newEntry.length = nextLength;
				newEntry.data = ByteFuncLib.ReadBytes(data, nextSaveIndex, nextSaveIndex + nextLength);
				newEntry.name = ByteFuncLib.DecodeBytes(ByteFuncLib.ReadBytes(data, nextSaveIndex + nextSlotStartByte, nextSaveIndex + ByteFuncLib.entryHeaderSize));
				newEntry.startsAt = nextSaveIndex;

				result.saves.Add(newEntry);
				nextSaveIndex += nextLength;
				nextLength = BitConverter.ToInt16(data, nextSaveIndex);
			}

			result.loaded = true;
		}
		else {
			MessageBox.Show("File is not a valid BRAM file.");
		}

		return result;
	}

	// Saves the BRAM file to a given path
	public static bool SaveFile(ref BRAM bram, string path) {
		if (path.Length > 0) {
			byte[] outputData = new byte[ByteFuncLib.maxBRAMSize];

			// create new data array
			ByteFuncLib.WriteBytes(bram.header, outputData, 0, ByteFuncLib.headerSize);
			int index = ByteFuncLib.headerSize;
			foreach (BRAMEntry entry in bram.saves) {
				ByteFuncLib.WriteBytes(entry.data, outputData, index, entry.length);
				index += entry.length;
			}

			// calculate new next slot pointer
			int nextSlot = index;
			byte[] nextSlotBytes = new byte[nextSlotLength];
			nextSlot += ByteFuncLib.memoryOffset;
			nextSlotBytes = BitConverter.GetBytes(nextSlot);
			ByteFuncLib.WriteBytes(nextSlotBytes, outputData, nextSlotStartByte, nextSlotLength);

			System.IO.File.WriteAllBytes(path, outputData);
			bram.edited = false;

			return true;
		}
		return false;
	}

	// Full file opening process, given a path
	public static bool OpenAndLoadFile(string path, ref BRAM bram, ref ListBox list, ref TextBox textBox, ref Label label) {
		BRAM newBRAM = ReadFile(path);

		if (newBRAM.loaded) {
			bram = newBRAM;

			list.Items.Clear();

			foreach (BRAMEntry entry in bram.saves) {
				list.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
			}

			textBox.Text = path;
			label.Text = String.Format("Free: {0}B", bram.freeSpace);

			return true;
		}

		return false;
	}

	public static bool IsValidBRAMFile(string path) {
		string dataString = System.IO.File.ReadAllText(path);
		return dataString.StartsWith("HUBM");
	}

	// Copies a save file from one BRAM to another
	public static void CopyEntry(ref BRAM sourceBRAM, ref BRAM targetBRAM, ref ListBox targetList, int index) {
		BRAMEntry entry = sourceBRAM.saves[index];
		if (targetBRAM.freeSpace < entry.length) {
			MessageBox.Show("Not enough space in destination BRAM.");
			return;
		}

		foreach (BRAMEntry otherEntry in targetBRAM.saves) {
			if (otherEntry.name == entry.name) {
				if (GuiFuncLib.WarningPopup("Filename already exists, which can cause issues. Are you sure you want to copy?")) {
					return;
				}
			}
		}

		targetBRAM.saves.Add(entry);
		targetList.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
		targetBRAM.freeSpace -= entry.length;
		targetBRAM.edited = true;
	}

	// Deletes a save file entry from a BRAM
	public static void DeleteEntry(ref BRAM bram, ref ListBox list, int index) {
		bram.freeSpace += bram.saves[index].length;
		bram.edited = true;

		bram.saves.RemoveAt(index);
		list.Items.RemoveAt(index);

		if (list.Items.Count > index) {
			list.SelectedIndex = index;
		}

		else {
			list.SelectedIndex = list.Items.Count - 1;
		}
	}
	public static void MoveEntry(ref BRAM bram, ref ListBox list, int index, int direction) {
		if (index + direction >= 0 && index + direction < list.Items.Count) {
			DataFuncLib.SwapListItems(bram.saves, index, index + direction);
			DataFuncLib.SwapListItems(list.Items, index, index + direction);
			list.SelectedIndex += direction;
			bram.edited = true;
		}
	}

	// Swaps item positions in a list
	public static void SwapListItems<T>(List<T> list, int indexA, int indexB)
	{
		T temp = list[indexA];
		list[indexA] = list[indexB];
		list[indexB] = temp;
	}

	// Swaps item positions in a list box
	public static void SwapListItems(ListBox.ObjectCollection list, int indexA, int indexB)
	{
		object temp = list[indexA];
		list[indexA] = list[indexB];
		list[indexB] = temp;
	}
}
