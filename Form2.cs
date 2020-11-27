using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace GH
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, System.EventArgs e)
        {
            //this.Location = new Point(Properties.Settings.Default.Form1_location.X + 100, Properties.Settings.Default.Form1_location.Y + 20);
            //VersionLb.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            System.Version version;
            try
            {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch
            {
                version = Assembly.GetExecutingAssembly().GetName().Version;
            }
            VersionLb.Text = "Version " + version.Major + "." + version.Minor + "B";

        }

        private void PambrunLLb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://pambrun.net");
        }

        private void Btn_Fermer_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
