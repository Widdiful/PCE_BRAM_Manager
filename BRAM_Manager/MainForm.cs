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
        public string savedInitialDirectory = "c:\\";
        public BRAM leftBRAM, rightBRAM;

        public char[] characterMap = new char[] {
        ' ', '▁', '▂', '▃', '▄', '▅', '▆', '▇', '█', '♤', '♡', '◇', '♧', '○', '●', '/',
        '\\', '円', '年', '月', '日', '時', '分', '秒', '◢', '◣', '◥', '◤', '→', '←', '↑', '↓',
        ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
        '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '￥', ']', '^', '_',
        ' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', ':', '}', '~', '☓',
        ' ', '。', '「', '」', '、', '・', 'を', 'ぁ', '╭', '─', '╮', '│', '│', '╰', '─', '╯',
        'ー', 'あ', 'い', 'う', 'え', 'お', 'か', 'き', 'く', 'け', 'こ', 'さ', 'し', 'す', 'せ', 'そ',
        '　', '。', '「', '」', '、', '・', 'ヲ', 'ァ', '┬', '│', '┴', 'ォ', 'ャ', 'ュ', 'ョ', 'ッ',
        'ー', 'ア', 'イ', 'ウ', 'エ', 'オ', 'カ', 'キ', 'ク', 'ケ', 'コ', 'サ', 'シ', 'ス', 'セ', 'ソ',
        'タ', 'チ', 'ツ', 'テ', 'ト', 'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'マ',
        'ミ', 'ム', 'メ', 'モ', 'ヤ', 'ユ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ', 'ワ', 'ン', '゛', '゜',
        'た', 'ち', 'つ', 'て', 'と', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ひ', 'ふ', 'へ', 'ほ', 'ま',
        'み', 'む', 'め', 'も', 'や', 'ゆ', 'よ', 'ら', 'り', 'る', 'れ', 'ろ', 'わ', 'ん', '゛', '©'};

        public struct BRAM {
            public byte[] data;
            public byte[] header;
            public List<BRAMEntry> saves;
            public int nextSlot;
            public int freeSpace;
            public bool loaded;
            public bool edited;
        }

        public struct BRAMEntry {
            public byte[] data;
            public string name;
            public int length;
            public int startsAt;
        }

        public MainForm() {
            InitializeComponent();

            savedInitialDirectory = Properties.Settings.Default.savedInitialDirectory;
        }

        //--------------------------------------------------
        // Main functions
        //--------------------------------------------------

        public string OpenFileBrowser() {
            return OpenFileBrowser(savedInitialDirectory);
        }

        // Opens a file browser and returns the path to the selected file
        public string OpenFileBrowser(string initialDirectory) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "BRAM files (*.bup;*.sav)|*.bup;*.sav|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    savedInitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                    Properties.Settings.Default.savedInitialDirectory = savedInitialDirectory;
                    Properties.Settings.Default.Save();
                    return openFileDialog.FileName;
                }
            }

            return string.Empty;
        }

        // Opens a file browser and returns a path to the new file
        public string SaveFileBrowser(string defaultDirectory = "", string defaultFileName = "") {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (defaultDirectory.Length > 0)
                saveFileDialog.InitialDirectory = defaultDirectory;
            else
                saveFileDialog.InitialDirectory = savedInitialDirectory;
            if (defaultFileName.Length > 0)
                saveFileDialog.FileName = defaultFileName;
            saveFileDialog.Filter = "BRAM files (*.bup;*.sav)|*.bup;*.sav|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                savedInitialDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                Properties.Settings.Default.savedInitialDirectory = savedInitialDirectory;
                Properties.Settings.Default.Save();
                return saveFileDialog.FileName;
            }

            return string.Empty;
        }

        // Returns bytes from a byte array, given a range
        public byte[] ReadBytes(byte[] data, int from, int to) {
            byte[] result = new byte[to - from];

            int index = 0;
            for (int i = from; i < to; i++) {
                result[index] = data[i];
                index++;
            }

            return result;
        }

        public string DecodeBytes(byte[] data) {
            string result = string.Empty;

            foreach (byte Byte in data) {
                if (characterMap.Length > Byte) {
                    result += characterMap[Byte];
                }
            }

            return result;
        }

        // Writes bytes to a byte array, given a starting location and a length
        public void WriteBytes(byte[] input, byte[] dest, int startLoc, int length) {
            int index = 0;
            for (int i = startLoc; i < startLoc + length; i++) {
                dest[i] = input[index];
                index++;
            }
        }

        // Reads a file and converts it to a BRAM data entry
        public BRAM ReadFile(string file) {
            BRAM result = new BRAM();
            result.saves = new List<BRAMEntry>();

            string dataString;

            try {
                dataString = System.IO.File.ReadAllText(file);
            }
            catch (IOException)
            {
                MessageBox.Show("File is in use.");
                return result;
            }
            
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
                    //newEntry.name = System.Text.Encoding.UTF8.GetString(ReadBytes(data, nextSaveIndex + 6, nextSaveIndex + 16));
                    newEntry.name = DecodeBytes(ReadBytes(data, nextSaveIndex + 6, nextSaveIndex + 16));
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
        public bool SaveFile(ref BRAM bram, string path) {
            if (path.Length > 0) {
                byte[] outputData = new byte[2048];

                // create new data array
                WriteBytes(bram.header, outputData, 0, 16);
                int index = 16;
                foreach (BRAMEntry entry in bram.saves) {
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
                bram.edited = false;

                return true;
            }
            return false;
        }

        // Swaps item positions in a list
        public void SwapListItems<T>(List<T> list, int indexA, int indexB) {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        // Swaps item positions in a list box
        public void SwapListItems(ListBox.ObjectCollection list, int indexA, int indexB) {
            object temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }

        // Full file opening process, given a path
        public void OpenAndLoadFile(string path, ref BRAM bram, ref ListBox list, ref TextBox textBox, ref Label label) {
            BRAM newBRAM = ReadFile(path);

            if (newBRAM.loaded) {
                bram = newBRAM;

                list.Items.Clear();

                foreach (BRAMEntry entry in bram.saves) {
                    list.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
                }

                textBox.Text = path;
                label.Text = String.Format("Free: {0}B", bram.freeSpace);
            }
        }

        //--------------------------------------------------
        // GUI Interactions
        //--------------------------------------------------

        // Main interface

        private void LeftBrowse_Click(object sender, EventArgs e) {
            string openPath = LeftAddress.Text.Length > 0 ? Path.GetDirectoryName(LeftAddress.Text) : savedInitialDirectory;
            string path = OpenFileBrowser(openPath);
            if (path.Length == 0)
                return;

            OpenAndLoadFile(path, ref leftBRAM, ref LeftList, ref LeftAddress, ref LeftFreeSpace);
        }

        private void RightBrowse_Click(object sender, EventArgs e) {
            string openPath = RightAddress.Text.Length > 0 ? Path.GetDirectoryName(RightAddress.Text) : savedInitialDirectory;
            string path = OpenFileBrowser(openPath);
            if (path.Length == 0)
                return;

            OpenAndLoadFile(path, ref rightBRAM, ref RightList, ref RightAddress, ref RightFreeSpace);
        }

        private void LeftSave_Click(object sender, EventArgs e) {
            if (leftBRAM.loaded) {
                SaveFile(ref leftBRAM, SaveFileBrowser(Path.GetDirectoryName(LeftAddress.Text), Path.GetFileName(LeftAddress.Text)));
                LeftEdited.Visible = leftBRAM.edited;
            }
        }

        private void RightSave_Click(object sender, EventArgs e) {
            if (rightBRAM.loaded) {
                SaveFile(ref rightBRAM, SaveFileBrowser(Path.GetDirectoryName(RightAddress.Text), Path.GetFileName(RightAddress.Text)));
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
            selectedBRAM.freeSpace += selectedBRAM.saves[index].length;
            selectedBRAM.edited = true;

            LeftEdited.Visible = leftBRAM.edited;
            RightEdited.Visible = rightBRAM.edited;

            selectedBRAM.saves.RemoveAt(index);
            selectedList.Items.RemoveAt(index);

            if (leftBRAM.loaded)
                LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
            if (rightBRAM.loaded)
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

            BRAMEntry entry = selectedBRAM.saves[selectedList.SelectedIndex];
            if (otherBRAM.freeSpace < entry.length) {
                MessageBox.Show("Not enough space in destination BRAM.");
                return;
            }
            
            foreach (BRAMEntry otherEntry in otherBRAM.saves) {
                if (otherEntry.name == entry.name) {
                    if (MessageBox.Show("Filename already exists, which can cause issues. Are you sure you want to copy?", "", MessageBoxButtons.YesNo) == DialogResult.No) {
                        return;
                    }
                }
            }

            otherBRAM.saves.Add(entry);
            otherList.Items.Add(String.Format("{0} ({1}B)", entry.name, entry.length));
            otherBRAM.freeSpace -= entry.length;
            otherBRAM.edited = true;
            LeftEdited.Visible = leftBRAM.edited;
            RightEdited.Visible = rightBRAM.edited;

            LeftFreeSpace.Text = String.Format("Free: {0}B", leftBRAM.freeSpace);
            RightFreeSpace.Text = String.Format("Free: {0}B", rightBRAM.freeSpace);
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
            if (index > 0) {
                SwapListItems(selectedBRAM.saves, index, index - 1);
                SwapListItems(selectedList.Items, index, index - 1);
                selectedList.SelectedIndex--;
                selectedBRAM.edited = true;
                LeftEdited.Visible = leftBRAM.edited;
                RightEdited.Visible = rightBRAM.edited;
            }
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
            if (index < selectedList.Items.Count - 1) {
                SwapListItems(selectedBRAM.saves, index, index + 1);
                SwapListItems(selectedList.Items, index, index + 1);
                selectedList.SelectedIndex++;
                selectedBRAM.edited = true;
                LeftEdited.Visible = leftBRAM.edited;
                RightEdited.Visible = rightBRAM.edited;
            }
        }

        // Toolbar

        private void newBRAMToolStripMenuItem_Click(object sender, EventArgs e) {
            byte[] newFileBytes = new byte[] { 0x48, 0x55, 0x42, 0x4D, 0x00, 0x88, 0x10, 0x80 };
            byte[] newFile = new byte[2048];

            WriteBytes(newFileBytes, newFile, 0, 8);
            string newFilePath = SaveFileBrowser();
            if (newFilePath.Length > 0) {
                File.WriteAllBytes(newFilePath, newFile);

                if (!leftBRAM.loaded) {
                    OpenAndLoadFile(newFilePath, ref leftBRAM, ref LeftList, ref LeftAddress, ref LeftFreeSpace);
                }
                else if (!rightBRAM.loaded) {
                    OpenAndLoadFile(newFilePath, ref rightBRAM, ref RightList, ref RightAddress, ref RightFreeSpace);
                }
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) {
            if (leftBRAM.edited || rightBRAM.edited) {
                if (MessageBox.Show("There are unsaved changes. Are you sure you want to clear?", "", MessageBoxButtons.YesNo) == DialogResult.No) {
                    return;
                }
            }
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

        // Misc

        private void LeftList_SelectedIndexChanged(object sender, EventArgs e) {
            if (LeftList.Focused)
                RightList.ClearSelected();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (leftBRAM.edited || rightBRAM.edited) {
                if (MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "", MessageBoxButtons.YesNo) == DialogResult.No ){
                    e.Cancel = true;
                }
            }
        }

        private void RightList_SelectedIndexChanged(object sender, EventArgs e) {
            if (RightList.Focused)
                LeftList.ClearSelected();
        }
    }
}
