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
        private Form activeForm;

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
        /*
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

        }

        private void mouseUp_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        */
        //Drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void mouseDown_Event(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        
        //child form
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        

        private void aboutButton_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAbout(), sender);
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            
        }

        
    }
}