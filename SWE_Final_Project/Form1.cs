using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE_Final_Project.Views;
using SWE_Final_Project.Views.States;

namespace SWE_Final_Project {
    public partial class Form1: Form {
        public Form1() {
            InitializeComponent();

            // put the 3 types of states on the left side of GUI
            putOnStatesList();
        }

        private void putOnStatesList() {
            int panelW = statesListPanel.Width;
            int panelH = statesListPanel.Height;

            int locX = panelW / 2;

            StateView startStateView = new StartStateView(locX, 50, "", false);
            statesListPanel.Controls.Add(startStateView);
        }

        public void InstantiatedStateView_Click(object sender, MouseEventArgs e) {

        }

        // ==============================================================
        // user events

        // add the whole new script
        private void NewScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            ScriptTabPage tabPage = new ScriptTabPage("YES");
            scriptsTabControl.TabPages.Add(tabPage);
        }

        // save the script at the current tab
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
