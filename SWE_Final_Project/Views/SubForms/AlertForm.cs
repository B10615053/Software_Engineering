using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views.SubForms {
    public partial class AlertForm: Form {
        public AlertForm(string alertTitle, string alertMsg) {
            InitializeComponent();

            // set the form title
            Text = alertTitle;
            // set the alert message
            txtShowAlertMessage.Text = alertMsg;
        }

        // close the alert form
        private void BtnConfirmAtAlertForm_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
