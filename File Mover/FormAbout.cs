using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Mover
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void VisitLink()
        {
            linkLabel1.LinkVisited = true;
            var ps = new ProcessStartInfo("https://github.com/Lucoal")
            {
                UseShellExecute = true
            };
            Process.Start(ps);
        }
    }
}
