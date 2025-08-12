using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoreManagementSystem_Trung.BLL;

namespace StoreManagementSystem_Trung.GUI
{
    public partial class frmChangePassword : Form
    {
        private readonly AccountBLL _accountBLL = new AccountBLL();

        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // --- Client-side validation ---
            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirmation password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // --- Call BLL to perform the change ---
            // We need the current user's username from the session
            string username = GetCurrentUsername(); // We need to implement this

            string result = _accountBLL.ChangePassword(username, oldPassword, newPassword);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCurrentUsername()
        {
            // This requires us to store the Username in the UserSession as well.
            // For now, let's assume we can get it. We'll fix this in the next step.
            // Placeholder: return "admin";
            return UserSession.Username;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
