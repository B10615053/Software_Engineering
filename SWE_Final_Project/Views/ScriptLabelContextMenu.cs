using SWE_Final_Project.Managers;
using SWE_Final_Project.Views.SubForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views {
    public class ScriptLabelContextMenu: ContextMenu {
        // the view of tab-control
        private TabControl mTabControl;

        /* ===================================================== */

        // constructor
        public ScriptLabelContextMenu(TabControl tabControl) {
            // add the functionalities
            MenuItems.Add("Rename");
            MenuItems.Add("Save");
            MenuItems.Add("Close");

            // delegate all menu-items events
            foreach (MenuItem item in MenuItems)
                item.Click += item_Click;

            // get the tab-control
            mTabControl = tabControl;
        }

        /* ===================================================== */

        // item click event
        private void item_Click(object sender, EventArgs e) {
            // get the selected item
            MenuItem item = sender as MenuItem;

            // rename
            if (item.Text == "Rename")
                renameScript();

            // save
            else if (item.Text == "Save")
                saveScript();

            // close
            else if (item.Text == "Close")
                closeScript();
        }

        // do renaming the script
        private void renameScript() {
            DialogResult result = new TypingForm("Rename the script", "Type the new title for your script.", false).ShowDialog();
            if (result == DialogResult.OK) {
                ModelManager.renameScript(TypingForm.userTypedResultText, false);
                mTabControl.SelectedTab.Text = TypingForm.userTypedResultText + "*";
            }
        }

        // do saving the script
        private void saveScript() {
            Program.form.saveCertainScript(mTabControl.SelectedIndex);
        }

        // do closing the script
        private void closeScript() {
            // has unsaved changes in the current working-on script
            if (ModelManager.getScriptModelByIndex(mTabControl.SelectedIndex).HaveUnsavedChanges) {
                AlertForm alertForm = new AlertForm("Alert", "The script has been modified and it's unsaved. Do you want to save it?", true, true, true);
                DialogResult result = alertForm.ShowDialog();

                bool doesSaveSuccessfully = true;

                // don't do closing
                if (result == DialogResult.Cancel)
                    return;
                // yes, do saving, then closing
                else if (result == DialogResult.Yes)
                    doesSaveSuccessfully = Program.form.saveCertainScript(mTabControl.SelectedIndex);

                // not actually saving
                if (!doesSaveSuccessfully)
                    return;
            }

            // close the script
            ModelManager.closeScript();
            mTabControl.TabPages.RemoveAt(mTabControl.SelectedIndex);
        }
    }
}
