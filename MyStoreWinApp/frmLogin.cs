using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
            MessageBox.Show(config["ConnectionStrings:MyStoreDB]"]);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
