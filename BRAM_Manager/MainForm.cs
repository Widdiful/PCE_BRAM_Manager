using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BRAM_Manager {
    public partial class MainForm : Form {
        public string initialDirectory = "c:\\";
        public BRAM leftBRAM, rightBRAM;

        public struct BRAM {
            public byte[] data;
            public byte[] header;
            public List<BRAMEntry> saves;
            public int nextSlot;
            public int freeSpace;
            public bool loaded;
        }

        public struct BRAMEntry {
            public byte[] data;
            public string name;
            public int length;
            public int startsAt;
        }

        public MainForm() {
            InitializeComponent();
        }

        public string OpenFileBrowser() {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "BRAM files (*.bup)|*.bup|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    //Get the path of specified file
                    initialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    return openFileDialog.FileName;
                }
            }

            return string.Empty;
        }

        public byte[] ReadBytes(byte[] data, int from, int to) {
            byte[] result = new byte[to - from];

            int index = 0;
            for (int i = from; i < to; i++) {
                result[index] = data[i];
                index++;
            }

            return result;
        }

        public void WriteBytes(byte[] input, byte[] dest, int startLoc, int length) {
            int index = 0;
            for (int i = startLoc; i < startLoc + length; i++) {
                dest[i] = input[index];
                index++;
            }
        }

        public BRAM ReadFile(string file) {
            BRAM result = new BRAM();
            result.saves = new List<BRAMEntry>();

            string dataString = System.IO.File.ReadAllText(file);
            byte[] data = System.IO.File.ReadAllBytes(file);
            if (dataString.StartsWith("HUBM")) {
                int length = data.Length;
                result.data = data;
                result.header = ReadBytes(data, 0, 16);

                // bytes 7 and 8 are pointers to the next available memory slot
                byte[] nextSlotBytes = ReadBytes(data, 6, 8);
                result.nextSlot = BitConverter.ToUInt16(nextSlotBytes, 0);
                result.nextSlot -= 32768; // remove the offset of 0x8000 to get the number we actually want
                result.freeSpace = 2048 - result.nextSlot;

                int nextSaveIndex = 16;
                int nextLength = BitConverter.ToInt16(data, nextSaveIndex);

                // loop through saves until reaching a file that is 0 bytes long (no file)
                while (nextLength != 0) {
                    BRAMEntry newEntry = new BRAMEntry();

                    newEntry.length = nextLength;
                    newEntry.data = ReadBytes(data, nextSaveIndex, nextSaveIndex + nextLength);
                    newEntry.name = System.Text.Encoding.UTF8.GetString(ReadBytes(data, nextSaveIndex + 6, nextSaveIndex + 16));
                    newEntry.startsAt = nextSaveIndex;

                    result.saves.Add(newEntry);
                    nextSaveIndex += nextLength;
                    nextLength = BitConverter.ToInt16(data, nextSaveIndex);
                }

                result.loaded = true;
            }
            else {
                System.Windows.Forms.MessageBox.Show("File is not a valid BRAM file.");
            }

            return result;
        }

        public void SaveFile(BRAM bram, string path) {
            byte[] outputData = new byte[2048];

            // create new data array
            WriteBytes(bram.header, outputData, 0, 16);
            int index = 16;
            foreach(BRAMEntry entry in bram.saves) {
                WriteBytes(entry.data, outputData, index, entry.length);
                index += entry.length;
            }

            // calculate new next slot pointer
            int nextSlot = index;
            byte[] nextSlotBytes = new byte[2];
            nextSlot += 32768;
            nextSlotBytes = BitConverter.GetBytes(nextSlot);
            WriteBytes(nextSlotBytes, outputData, 6, 2);

            File.WriteAllBytes(path, outputData);
        }

        public void SwapListItems<T>(List<T> list, int indexA, int indexB) {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        public void SwapListItems(ListBox.ObjectCollection list, int indexA, int indexB) {
            object temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        private void LeftBrowse_Click(object sender, EventArgs e) {
            string path = OpenFileBrowser();
            if (path.Length == 0)
                return;
            LeftAddress.Text = path;

            leftBRAM = ReadFile(path);

            LeftList.Items.Clear();
            foreach(BRAMEntry entry in leftBRAM.saves) {
                LeftList.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
            }

            LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
        }

        private void LeftSave_Click(object sender, EventArgs e) {
            if (leftBRAM.loaded)
                SaveFile(leftBRAM, LeftAddress.Text);
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
                System.Windows.Forms.MessageBox.Show("Please select a file to delete.");
                return;
            }

            int index = selectedList.SelectedIndex;
            selectedBRAM.freeSpace += selectedBRAM.saves[index].length;
            selectedBRAM.saves.RemoveAt(index);
            selectedList.Items.RemoveAt(index);

            LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
            RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);

            if (selectedList.Items.Count > index) {
                selectedList.SelectedIndex = index;
            }
            else {
                selectedList.SelectedIndex = selectedList.Items.Count - 1;
            }
        }

        private void CopyButton_Click(object sender, EventArgs e) {
            if (!(leftBRAM.loaded && rightBRAM.loaded)) {
                System.Windows.Forms.MessageBox.Show("Please load two BRAM files.");
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
                System.Windows.Forms.MessageBox.Show("Please select a file to copy.");
                return;
            }

            BRAMEntry entry = selectedBRAM.saves[selectedList.SelectedIndex];
            if (otherBRAM.freeSpace < entry.length) {
                System.Windows.Forms.MessageBox.Show("Not enough space in destination BRAM.");
                return;
            }
            otherBRAM.saves.Add(entry);
            otherList.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
            otherBRAM.freeSpace -= entry.length;

            LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
            RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);
        }

        private void UpButton_Click(object sender, EventArgs e) {
            BRAM selectedBRAM = new BRAM();
            ListBox selectedList = new ListBox();
            if (LeftList.SelectedIndex >= 0) {
                selectedBRAM = leftBRAM;
                selectedList = LeftList;
            }
            else if (RightList.SelectedIndex >= 0) {
                selectedBRAM = rightBRAM;
                selectedList = RightList;
            }
            else {
                System.Windows.Forms.MessageBox.Show("Please select a file to move.");
                return;
            }

            int index = selectedList.SelectedIndex;
            if (index > 0) {
                SwapListItems(selectedBRAM.saves, index, index - 1);
                SwapListItems(selectedList.Items, index, index - 1);
                selectedList.SelectedIndex--;
            }
        }

        private void DownButton_Click(object sender, EventArgs e) {
            BRAM selectedBRAM = new BRAM();
            ListBox selectedList = new ListBox();
            if (LeftList.SelectedIndex >= 0) {
                selectedBRAM = leftBRAM;
                selectedList = LeftList;
            }
            else if (RightList.SelectedIndex >= 0) {
                selectedBRAM = rightBRAM;
                selectedList = RightList;
            }
            else {
                System.Windows.Forms.MessageBox.Show("Please select a file to move.");
                return;
            }

            int index = selectedList.SelectedIndex;
            if (index < selectedList.Items.Count - 1) {
                SwapListItems(selectedBRAM.saves, index, index + 1);
                SwapListItems(selectedList.Items, index, index + 1);
                selectedList.SelectedIndex++;
            }
        }

        private void LeftList_SelectedIndexChanged(object sender, EventArgs e) {
            if (LeftList.Focused)
                RightList.ClearSelected();
        }

        private void RightList_SelectedIndexChanged(object sender, EventArgs e) {
            if (RightList.Focused)
                LeftList.ClearSelected();
        }

        private void RightSave_Click(object sender, EventArgs e) {
            if (rightBRAM.loaded)
                SaveFile(rightBRAM, RightAddress.Text);
        }

        private void RightBrowse_Click(object sender, EventArgs e) {
            string path = OpenFileBrowser();
            if (path.Length == 0)
                return;
            RightAddress.Text = path;

            rightBRAM = ReadFile(path);

            RightList.Items.Clear();
            foreach (BRAMEntry entry in rightBRAM.saves) {
                RightList.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
            }

            RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);
        }
    }
}
