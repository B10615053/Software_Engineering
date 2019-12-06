using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;

namespace SWE_Final_Project.Views {
    public class StateInfoTableLayoutPanel: TableLayoutPanel {
        // constructor w/ a state-view
        public StateInfoTableLayoutPanel(StateView stateView) {
            doConstruction(stateView);
        }

        // constructor w/ a link-view
        public StateInfoTableLayoutPanel(LinkView linkView) {
            doConstruction(linkView);
        }

        /* ============================================================= */

        // construct the basic settings and components
        private void doConstruction(PictureBox view) {
            // set dock style
            Dock = DockStyle.Fill;

            // set column count, seems like this statement is no need
            ColumnCount = 2;

            // set the font into Consolas
            Font = new Font("Consolas", 9.0F, FontStyle.Regular);

            // average-lize the column sizes
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));

            // initialize the components
            if (view is StateView)
                initializeComponents(view as StateView);
            else if (view is LinkView)
                initializeComponents(view as LinkView);
        }

        // init w/ a state-view
        private void initializeComponents(StateView stateView) {
            #region components defines
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

            /* === */

            // label: content text
            TextBox lblText = new TextBox();
            lblText.Text = "Text";
            lblText.BorderStyle = BorderStyle.None;
            lblText.ReadOnly = true;
            Controls.Add(lblText, 0, 1);

            // text-box: content text
            TextBox txtShowText = new TextBox();
            txtShowText.Name = "txtShowText";
            if (stateView is StartStateView || stateView is EndStateView) {
                txtShowText.Text = "";
                txtShowType.BorderStyle = BorderStyle.None;
                txtShowText.Enabled = false;
            }
            else {
                txtShowText.Text = stateView.StateContent;
            }
            Controls.Add(txtShowText, 1, 1);

            /* === */

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

            /* === */

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
            #endregion

            /* ============================= */
            /* ============================= */
            /* ============================= */
            /* ============================= */
            /* ============================= */
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

            /* ============================= */

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

            /* ============================= */

            txtShowText.KeyDown += TxtShowText_KeyDown;
        }

        // init w/ a link-view
        private void initializeComponents(LinkView linkView) {
            #region components defines
            // label: content text
            TextBox lblText = new TextBox();
            lblText.Text = "Text";
            lblText.BorderStyle = BorderStyle.None;
            lblText.ReadOnly = true;
            Controls.Add(lblText, 0, 0);

            // text-box: content text
            TextBox txtShowText = new TextBox();
            txtShowText.Name = "txtShowText";
            txtShowText.Text = linkView.Model.LinkText;
            Controls.Add(txtShowText, 1, 0);

            /* === */

            // label: src state
            TextBox lblSrcState = new TextBox();
            lblSrcState.Text = "Source";
            lblSrcState.BorderStyle = BorderStyle.None;
            lblSrcState.ReadOnly = true;
            Controls.Add(lblSrcState, 0, 1);

            // text-box: src state
            TextBox txtShowSrcState = new TextBox();
            txtShowSrcState.Text = linkView.Model.SrcStateModel.ContentText;
            Controls.Add(txtShowSrcState, 1, 1);

            /* === */

            // label: src state port
            TextBox lblSrcStatePort = new TextBox();
            lblSrcStatePort.Text = "Src Port";
            lblSrcStatePort.BorderStyle = BorderStyle.None;
            lblSrcStatePort.ReadOnly = true;
            Controls.Add(lblSrcStatePort, 0, 2);

            // list-box: src state port
            ComboBox cbbSrcStatePort = new ComboBox();
            cbbSrcStatePort.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (PortType portType in Enum.GetValues(typeof(PortType)))
                if (portType != PortType.NONE)
                    cbbSrcStatePort.Items.Add(portType.ToString("g"));
            cbbSrcStatePort.SelectedIndex = (int) linkView.Model.SrcPortType;
            Controls.Add(cbbSrcStatePort, 1, 2);

            /* === */

            // label: dst state
            TextBox lblDstState = new TextBox();
            lblDstState.Text = "Destination";
            lblDstState.BorderStyle = BorderStyle.None;
            lblDstState.ReadOnly = true;
            Controls.Add(lblDstState, 0, 3);

            // text-box: dst state
            TextBox txtShowDstState = new TextBox();
            txtShowDstState.Text = linkView.Model.DstStateModel.ContentText;
            Controls.Add(txtShowDstState, 1, 3);

            /* === */

            // label: dst state port
            TextBox lblDstStatePort = new TextBox();
            lblDstStatePort.Text = "Dst Port";
            lblDstStatePort.BorderStyle = BorderStyle.None;
            lblDstStatePort.ReadOnly = true;
            Controls.Add(lblDstStatePort, 0, 4);

            // list-box: dst state port
            ComboBox cbbDstStatePort = new ComboBox();
            cbbDstStatePort.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (PortType portType in Enum.GetValues(typeof(PortType)))
                if (portType != PortType.NONE)
                    cbbDstStatePort.Items.Add(portType.ToString("g"));
            cbbDstStatePort.SelectedIndex = (int) linkView.Model.DstPortType;
            Controls.Add(cbbDstStatePort, 1, 4);
            #endregion

            /* ============================= */
            /* ============================= */
            /* ============================= */
            /* ============================= */
            /* ============================= */
            /* ============================= */

            // for index-changing at src & dst ports
            EventHandler eventHandlerPort = (sender, e) => {
                // get the port names of src & dst
                string srcPortName = cbbSrcStatePort.SelectedItem as string;
                string dstPortName = cbbDstStatePort.SelectedItem as string;

                // get the original port-types of src & dst
                PortType origSrcPortType = linkView.Model.SrcPortType;
                PortType origDstPortType = linkView.Model.DstPortType;

                // get the new port-types of src & dst
                PortType srcPortType = (PortType) Enum.Parse(typeof(PortType), srcPortName);
                PortType dstPortType = (PortType) Enum.Parse(typeof(PortType), dstPortName);

                // set src & dst
                linkView.setSrcAndDstPorts(srcPortType, dstPortType);

                // adjust at the script
                Program.form.adjustLinkViewAtCurrentScript(linkView.Model, true);

                // modify the terminal (source & destination) states as well
                linkView.Model.SrcStateModel.changePortOfCertainLink(linkView.Model, origSrcPortType, srcPortType, true);
                linkView.Model.DstStateModel.changePortOfCertainLink(linkView.Model, origDstPortType, dstPortType, false);
            };
            cbbSrcStatePort.SelectedIndexChanged += eventHandlerPort;
            cbbDstStatePort.SelectedIndexChanged += eventHandlerPort;

            /* ============================= */

            // for pressing enter key at txt-show-text
            KeyPressEventHandler keyPressText = (sender, e) => {
                // press enter
                if (e.KeyChar == 13) {
                    string newLinkText = txtShowText.Text.ToString().Trim();
                    if (linkView.Model.LinkText != newLinkText)
                        linkView.setLinkText(newLinkText);
                }
            };
            if (txtShowText.Enabled)
                txtShowText.KeyPress += keyPressText;

            /* ============================= */

            txtShowText.KeyDown += TxtShowText_KeyDown;
        }

        private void CbbSrcStatePort_SelectedIndexChanged(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        /* ============================================================= */

        // key down event at txt-show-text, for the convenience of saving the script
        private void TxtShowText_KeyDown(object sender, KeyEventArgs e) {
            // Ctrl + S
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                Program.form.saveCertainScript(ModelManager.CurrentSelectedScriptIndex);
        }
    }
}
