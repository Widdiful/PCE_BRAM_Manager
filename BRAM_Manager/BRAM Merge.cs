using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRAM_Manager {
    public partial class BRAM_Merge : Form {
        MainForm mainForm;
        MainForm.BRAM mergeBRAM;
        List<MainForm.BRAMEntry> SavesToMerge = new List<MainForm.BRAMEntry>();
        int freeSpace = 0;

        public BRAM_Merge() {
            InitializeComponent();
        }

        public void InitialiseForm(MainForm main, string[] paths) {
            mainForm = main;

            // initialise new BRAM
            mergeBRAM = new MainForm.BRAM();
            byte[] newFileBytes = new byte[] { 0x48, 0x55, 0x42, 0x4D, 0x00, 0x88, 0x10, 0x80 };
            byte[] newFile = new byte[2048];
            byte[] newHeader = new byte[16];

            main.WriteBytes(newFileBytes, newFile, 0, 8);
            mergeBRAM.data = newFile;
            main.WriteBytes(newFileBytes, newHeader, 0, 8);
            mergeBRAM.header = newHeader;
            mergeBRAM.nextSlot = 16;
            mergeBRAM.freeSpace = freeSpace = 2032;
            mergeBRAM.saves = new List<MainForm.BRAMEntry>();

            // Find all save data and add it to a list
            MergeAddress.Text = "";
            MergeList.Items.Clear();
            foreach (string path in paths) {
                MainForm.BRAM newBRAM = main.ReadFile(path);
                if (newBRAM.loaded) {
                    foreach (MainForm.BRAMEntry save in newBRAM.saves) {
                        SavesToMerge.Add(save);
                        MergeList.Items.Add(String.Format("{0} ({1}B)", save.name, save.length));
                    }
                    MergeAddress.Text += String.Format("\"{0}\" ", path);
                }
            }

            MergeFreeSpace.Text = String.Format("Free: {0}B", mergeBRAM.freeSpace);
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

            string path = mainForm.SaveFileBrowser();
            if (path == "") {
                return;
            }

            // Add saved files to BRAM and save it
            foreach (int item in MergeList.SelectedIndices) {
                mergeBRAM.saves.Add(SavesToMerge[item]);
            }

            mainForm.SaveFile(ref mergeBRAM, path);
            mainForm.QuickOpen(path);

            Close();
        }
    }
}
