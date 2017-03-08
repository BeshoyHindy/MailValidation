using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailValidation.Verify.Validation;

namespace MailValidation.WindowsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            MailVerify mailVerify = new MailVerify();
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show(mailVerify.Validate(txtEmail.Text).ToString());
            }
        }
    }
}
