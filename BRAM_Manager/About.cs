using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace BRAM_Manager
{
	public partial class About : Form
	{
		public About() {
			InitializeComponent();

			VersionNumLabel.Text += Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/Widdiful/PCE_BRAM_Manager");
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			System.Diagnostics.Process.Start("https://github.com/Widdiful/PCE_BRAM_Manager/wiki");
		}
	}

	public class PictureBoxWithInterpolation : PictureBox
	{
		public InterpolationMode InterpolationMode { get; set; }

		protected override void OnPaint(PaintEventArgs pe) {
			pe.Graphics.InterpolationMode = InterpolationMode;
			base.OnPaint(pe);
		}
	}
}
