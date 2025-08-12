namespace StoreManagementSystem_Trung.GUI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployeeManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCustomerManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAccountManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategoryManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFunctions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCreateNewOrder = new System.Windows.Forms.ToolStripMenuItem();
            // =========================================================
            // == BỔ SUNG KHAI BÁO menuOrderHistory VÀO ĐÂY ==
            // =========================================================
            this.menuOrderHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSalesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystem,
            this.menuManagement,
            this.menuFunctions,
            this.menuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1067, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLogout,
            this.toolStripMenuItem1,
            this.menuExit,
            this.menuChangePassword});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(70, 24);
            this.menuSystem.Text = "System";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(207, 26);
            this.menuLogout.Text = "Log Out";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(204, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(207, 26);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuChangePassword
            // 
            this.menuChangePassword.Name = "menuChangePassword";
            this.menuChangePassword.Size = new System.Drawing.Size(207, 26);
            this.menuChangePassword.Text = "Change Password";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);
            // 
            // menuManagement
            // 
            this.menuManagement.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProductManagement,
            this.menuEmployeeManagement,
            this.menuCustomerManagement,
            this.menuAccountManagement,
            this.menuCategoryManagement});
            this.menuManagement.Name = "menuManagement";
            this.menuManagement.Size = new System.Drawing.Size(111, 24);
            this.menuManagement.Text = "Management";
            // 
            // menuProductManagement
            // 
            this.menuProductManagement.Name = "menuProductManagement";
            this.menuProductManagement.Size = new System.Drawing.Size(250, 26);
            this.menuProductManagement.Text = "Product Management";
            this.menuProductManagement.Click += new System.EventHandler(this.menuProductManagement_Click);
            // 
            // menuEmployeeManagement
            // 
            this.menuEmployeeManagement.Name = "menuEmployeeManagement";
            this.menuEmployeeManagement.Size = new System.Drawing.Size(250, 26);
            this.menuEmployeeManagement.Text = "Employee Management";
            this.menuEmployeeManagement.Click += new System.EventHandler(this.menuEmployeeManagement_Click);
            // 
            // menuCustomerManagement
            // 
            this.menuCustomerManagement.Name = "menuCustomerManagement";
            this.menuCustomerManagement.Size = new System.Drawing.Size(250, 26);
            this.menuCustomerManagement.Text = "Customer Management";
            this.menuCustomerManagement.Click += new System.EventHandler(this.menuCustomerManagement_Click);
            // 
            // menuAccountManagement
            // 
            this.menuAccountManagement.Name = "menuAccountManagement";
            this.menuAccountManagement.Size = new System.Drawing.Size(250, 26);
            this.menuAccountManagement.Text = "Account Management";
            this.menuAccountManagement.Click += new System.EventHandler(this.menuAccountManagement_Click);
            // 
            // menuCategoryManagement
            // 
            this.menuCategoryManagement.Name = "menuCategoryManagement";
            this.menuCategoryManagement.Size = new System.Drawing.Size(250, 26);
            this.menuCategoryManagement.Text = "Category Management";
            this.menuCategoryManagement.Click += new System.EventHandler(this.menuCategoryManagement_Click);
            // 
            // menuFunctions
            // 
            this.menuFunctions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCreateNewOrder,
            // =================================================================
            // == BỔ SUNG menuOrderHistory VÀO DANH SÁCH DropDownItems ==
            // =================================================================
            this.menuOrderHistory,
            this.menuSalesReport});
            this.menuFunctions.Name = "menuFunctions";
            this.menuFunctions.Size = new System.Drawing.Size(85, 24);
            this.menuFunctions.Text = "Functions";
            // 
            // menuCreateNewOrder
            // 
            this.menuCreateNewOrder.Name = "menuCreateNewOrder";
            this.menuCreateNewOrder.Size = new System.Drawing.Size(224, 26);
            this.menuCreateNewOrder.Text = "Create New Order";
            this.menuCreateNewOrder.Click += new System.EventHandler(this.menuCreateNewOrder_Click);
            // 
            // menuOrderHistory
            // 
            // =========================================================
            // == KHỞI TẠO CÁC THUỘC TÍNH CHO menuOrderHistory ==
            // =========================================================
            this.menuOrderHistory.Name = "menuOrderHistory";
            this.menuOrderHistory.Size = new System.Drawing.Size(224, 26);
            this.menuOrderHistory.Text = "Order History";
            this.menuOrderHistory.Click += new System.EventHandler(this.menuOrderHistory_Click);
            // 
            // menuSalesReport
            // 
            this.menuSalesReport.Name = "menuSalesReport";
            this.menuSalesReport.Size = new System.Drawing.Size(224, 26);
            this.menuSalesReport.Text = "Sales Report";
            this.menuSalesReport.Click += new System.EventHandler(this.menuSalesReport_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(55, 24);
            this.menuHelp.Text = "Help";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(133, 26);
            this.menuAbout.Text = "About";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 528);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1067, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(184, 20);
            this.lblUserInfo.Text = "User: [Name] | Role: [Role]";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true; // Đảm bảo thuộc tính này là true
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "Store Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuManagement;
        private System.Windows.Forms.ToolStripMenuItem menuProductManagement;
        private System.Windows.Forms.ToolStripMenuItem menuEmployeeManagement;
        private System.Windows.Forms.ToolStripMenuItem menuCustomerManagement;
        private System.Windows.Forms.ToolStripMenuItem menuFunctions;
        private System.Windows.Forms.ToolStripMenuItem menuCreateNewOrder;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblUserInfo;
        private System.Windows.Forms.ToolStripMenuItem menuAccountManagement;
        private System.Windows.Forms.ToolStripMenuItem menuSalesReport;
        private System.Windows.Forms.ToolStripMenuItem menuCategoryManagement;
        private System.Windows.Forms.ToolStripMenuItem menuChangePassword;
        // =========================================================
        // == BỔ SUNG KHAI BÁO BIẾN CHO menuOrderHistory ==
        // =========================================================
        private System.Windows.Forms.ToolStripMenuItem menuOrderHistory;
    }
}