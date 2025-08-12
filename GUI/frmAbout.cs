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

namespace StoreManagementSystem_Trung.GUI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Simply close the form
            this.Close();
        }

        private void linkLblGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // URL to your GitHub profile or project repository
            string url = "https://github.com/Pham-trung123/ASM_DDD_BH01781_PhamHieuTrung.git";

            try
            {
                // Open the URL in the user's default web browser
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open the link. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
