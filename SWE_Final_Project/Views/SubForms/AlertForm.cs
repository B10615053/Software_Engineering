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
        // comprehensive constructor
        /// <summary>
        /// DialogResult could be Yes, No, Cancel
        /// </summary>
        public AlertForm(string alertTitle, string alertMsg,
            bool showCancelBtn, bool showNoBtn, bool showYesBtn,
            string cancelBtnStr = "Cancel", string noBtnStr = "No", string yesBtnStr = "Yes") {
            InitializeComponent();

            // set the form title
            Text = alertTitle;
            // set the alert message
            txtShowAlertMessage.Text = alertMsg;

            // set visibilities of buttons
            if (!showCancelBtn)
                btnCancelAtAlertForm.Visible = false;
            if (!showNoBtn)
                btnNoAtAlertForm.Visible = false;
            if (!showYesBtn)
                btnYesAtAlertForm.Visible = false;

            // set texts of buttons
            btnCancelAtAlertForm.Text = cancelBtnStr;
            btnNoAtAlertForm.Text = noBtnStr;
            btnYesAtAlertForm.Text = yesBtnStr;
        }

        // constructor: only yes-btn shows w/ the name of confirm
        public AlertForm(string alertTitle, string alertMsg) {
            InitializeComponent();

            // set the form title
            Text = alertTitle;
            // set the alert message
            txtShowAlertMessage.Text = alertMsg;

            // invisualize cancel-button and no-button
            btnCancelAtAlertForm.Visible = false;
            btnNoAtAlertForm.Visible = false;

            // set text of yes-button to "Confirm"
            btnYesAtAlertForm.Text = "Confirm";
        }

        // confirm and close the alert form
        private void BtnConfirmAtAlertForm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Yes;
            Close();
        }

        // repudiate and close the alert form
        private void BtnNoAtAlertForm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.No;
        }

        // cancel and close the alert form
        private void BtnCancelAtAlertForm_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
