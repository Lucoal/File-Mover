using System.Diagnostics;
using System.Runtime.InteropServices;

namespace File_Mover
{
    public partial class Form1 : Form
    {
        //round window
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        public string fileName = "";

        //scelta sorgente
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                fromPath.Text = ofd.FileName;
                fileName = Path.GetFileName(fromPath.Text);
            }
        }

        //scelta destinazione
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
            if(toPath.Text != "")
            {
                string temp = $"{toPath.Text}\\{fileName}";
                toPath.Text = Path.GetDirectoryName(fromPath.Text);
                fromPath.Text = temp;
            }
            else
            {
                MessageBox.Show("Destination path is null", "Alert");
            }
            
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
                MessageBox.Show("File can't be moved", "Error");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }


        //borderless window draggable
        bool mouseDown;
        private Point offset;
        private void mouseDown_Event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void mouseMove_Event(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currentScreenPos = PointToScreen(e.Location);
                Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
            }
        }

        private void mouseUp_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

     
    }
}