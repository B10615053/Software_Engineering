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
    public partial class TypingForm: Form {
        internal static string userTypedResultText = null;

        private bool mIsNullOrWhiteSpaceResultAllowed;

        public TypingForm(string title, string hintForUser = null, bool isNullOrWhiteSpaceResultAllowed = true) {
            InitializeComponent();
            Text = title;
            mIsNullOrWhiteSpaceResultAllowed = isNullOrWhiteSpaceResultAllowed;

            // set the hint label or invisualize it
            if (hintForUser is null) {
                lblHintForUser.Visible = false;
                tableLayoutPanel1.RowStyles.RemoveAt(0);
            }
            else
                lblHintForUser.Text = hintForUser;

            // focus on the text-box initially
            txtLetUserEnterAtTypingForm.Select();
        }

        // cancel the action, not submitting
        private void BtnCancelAtTypingForm_Click(object sender, EventArgs e) {
            // set the dialog-result to cancel, and close the form
            DialogResult = DialogResult.Cancel;
        }

        // submit and return the text user typed
        private void BtnConfirmAtTypingForm_Click(object sender, EventArgs e) {
            // get the trimmed user-typed text
            userTypedResultText = txtLetUserEnterAtTypingForm.Text.ToString().Trim();

            // null input, no submission
            if (mIsNullOrWhiteSpaceResultAllowed == false && string.IsNullOrEmpty(userTypedResultText))
                new AlertForm("Null input", "Null input or just all white-spaces in your input texts.").ShowDialog();
            // set the dialog-result to OK, and close the form
            else
                DialogResult = DialogResult.OK;
        }

        // press keys at typing text-box
        private void TxtLetUserEnterAtTypingForm_KeyPress(object sender, KeyPressEventArgs e) {
            // ENTER pressed, perform the click from submission button
            if (e.KeyChar == 13)
                btnConfirmAtTypingForm.PerformClick();
        }
    }
}
