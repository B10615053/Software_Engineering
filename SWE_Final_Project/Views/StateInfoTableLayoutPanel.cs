using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;

namespace SWE_Final_Project.Views {
    class StateInfoTableLayoutPanel: TableLayoutPanel {
        public StateInfoTableLayoutPanel(StateView stateView) {
            // set dock style
            Dock = DockStyle.Fill;
            // set column count, seems like this statement is no need
            ColumnCount = 2;
            // set the font into Consolas
            Font = new Font("Consolas", 9.0F, FontStyle.Regular);

            // average the column sizes
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            // init the components
            initializeComponents(stateView);
        }

        private void initializeComponents(StateView stateView) {
            // label: type
            TextBox lblType = new TextBox();
            lblType.Text = "Type";
            lblType.BorderStyle = BorderStyle.None;
            lblType.ReadOnly = true;
            Controls.Add(lblType, 0, 0);

            // text-box: type
            TextBox txtShowType = new TextBox();
            txtShowType.Text = stateView.TheTag;
            txtShowType.BorderStyle = BorderStyle.None;
            txtShowType.Enabled = false;
            Controls.Add(txtShowType, 1, 0);

            // label: content text
            TextBox lblText = new TextBox();
            lblText.Text = "Text";
            lblText.BorderStyle = BorderStyle.None;
            lblText.ReadOnly = true;
            Controls.Add(lblText, 0, 1);

            // text-box: content text
            TextBox txtShowText = new TextBox();
            if (stateView is StartStateView || stateView is EndStateView) {
                txtShowText.Text = "";
                txtShowType.BorderStyle = BorderStyle.None;
                txtShowText.Enabled = false;
            }
            else {
                txtShowText.Text = stateView.StateContent;
            }
            Controls.Add(txtShowText, 1, 1);

            // label: location x
            TextBox lblLocX = new TextBox();
            lblLocX.Text = "Loc X";
            lblLocX.BorderStyle = BorderStyle.None;
            lblLocX.ReadOnly = true;
            Controls.Add(lblLocX, 0, 2);

            // text-box: location x
            TextBox txtShowLocX = new TextBox();
            txtShowLocX.Text = (stateView.Location.X).ToString();
            Controls.Add(txtShowLocX, 1, 2);

            // label: location y
            TextBox lblLocY = new TextBox();
            lblLocY.Text = "Loc Y";
            lblLocY.BorderStyle = BorderStyle.None;
            lblLocY.ReadOnly = true;
            Controls.Add(lblLocY, 0, 3);

            // text-box: location y
            TextBox txtShowLocY = new TextBox();
            txtShowLocY.Text = (stateView.Location.Y).ToString();
            Controls.Add(txtShowLocY, 1, 3);

            /* ============================= */

            // for pressing enter key at txt-show-loc-x and txt-show-loc-y
            KeyPressEventHandler keyPressLoc = (sender, e) => {
                // press enter
                if (e.KeyChar == 13) {
                    try {
                        int newX = int.Parse(txtShowLocX.Text.ToString());
                        int newY = int.Parse(txtShowLocY.Text.ToString());

                        stateView.relocateState(newX, newY, false);
                    } catch (FormatException) {
                        // show the alert form to hint user
                        AlertForm alertForm = new AlertForm("Wrong number format",
                            "Only integer is allowed.");
                        alertForm.ShowDialog();
                    }
                }
            };

            txtShowLocX.KeyPress += keyPressLoc;
            txtShowLocY.KeyPress += keyPressLoc;

            // for pressing enter key at txt-show-text
            KeyPressEventHandler keyPressText = (sender, e) => {
                // press enter
                if (e.KeyChar == 13) {
                    string newStateContent = txtShowText.Text.ToString().Trim();
                    if (stateView.StateContent != newStateContent)
                        stateView.setStateContent(newStateContent);
                }
            };

            if (txtShowText.Enabled)
                txtShowText.KeyPress += keyPressText;
        }
    }
}
