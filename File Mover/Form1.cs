using System.Diagnostics;

namespace File_Mover
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string fileName = "";

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                fromPath.Text = ofd.FileName;
                fileName = Path.GetFileName(fromPath.Text);
            }
        }

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                toPath.Text = fbd.SelectedPath;
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            string temp = $"{toPath.Text}\\{fileName}";
            toPath.Text = Path.GetDirectoryName(fromPath.Text);
            fromPath.Text = temp; 
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            try
            {
                File.Move(fromPath.Text, toPath.Text + $"\\{fileName}");
                MessageBox.Show($"{fileName} has been moved!"); 
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }
    }
}