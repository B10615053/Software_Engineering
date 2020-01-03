namespace SWE_Final_Project {
    partial class Form1 {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayout_base = new System.Windows.Forms.TableLayoutPanel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepByStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRunningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wholeWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentWorkingScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withScriptNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withoutScriptNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayout_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayout_mainWithoutCmd = new System.Windows.Forms.TableLayoutPanel();
            this.scriptsTabControl = new System.Windows.Forms.TabControl();
            this.statesListPanel = new System.Windows.Forms.Panel();
            this.tableLayout_cmd = new System.Windows.Forms.TableLayoutPanel();
            this.txtCmdUserInput = new System.Windows.Forms.TextBox();
            this.rtxtCmdOutput = new System.Windows.Forms.RichTextBox();
            this.panelInfoContainer = new System.Windows.Forms.Panel();
            this.tableLayoutPanelAtRightSide = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelInfoPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelExistedObjectsContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblExistedLinks = new System.Windows.Forms.Label();
            this.cbbExistedLinks = new System.Windows.Forms.ComboBox();
            this.cbbExistedStates = new System.Windows.Forms.ComboBox();
            this.lblExistedStates = new System.Windows.Forms.Label();
            this.tableLayout_base.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.tableLayout_main.SuspendLayout();
            this.tableLayout_mainWithoutCmd.SuspendLayout();
            this.tableLayout_cmd.SuspendLayout();
            this.panelInfoContainer.SuspendLayout();
            this.tableLayoutPanelAtRightSide.SuspendLayout();
            this.tableLayoutPanelExistedObjectsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayout_base
            // 
            this.tableLayout_base.ColumnCount = 2;
            this.tableLayout_base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.67889F));
            this.tableLayout_base.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.32111F));
            this.tableLayout_base.Controls.Add(this.mainMenuStrip, 0, 0);
            this.tableLayout_base.Controls.Add(this.tableLayout_main, 0, 1);
            this.tableLayout_base.Controls.Add(this.panelInfoContainer, 1, 1);
            this.tableLayout_base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout_base.Location = new System.Drawing.Point(0, 0);
            this.tableLayout_base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayout_base.Name = "tableLayout_base";
            this.tableLayout_base.RowCount = 2;
            this.tableLayout_base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout_base.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_base.Size = new System.Drawing.Size(1924, 912);
            this.tableLayout_base.TabIndex = 0;
            // 
            // mainMenuStrip
            // 
            this.tableLayout_base.SetColumnSpan(this.mainMenuStrip, 2);
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.runToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.screenshotToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1924, 28);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptToolStripMenuItem,
            this.openScriptToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newScriptToolStripMenuItem
            // 
            this.newScriptToolStripMenuItem.Name = "newScriptToolStripMenuItem";
            this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.newScriptToolStripMenuItem.Text = "New Script (Ctrl, N)";
            this.newScriptToolStripMenuItem.Click += new System.EventHandler(this.NewScriptToolStripMenuItem_Click);
            // 
            // openScriptToolStripMenuItem
            // 
            this.openScriptToolStripMenuItem.Name = "openScriptToolStripMenuItem";
            this.openScriptToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.openScriptToolStripMenuItem.Text = "Open Script (Ctrl, O)";
            this.openScriptToolStripMenuItem.Click += new System.EventHandler(this.OpenScriptToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.saveToolStripMenuItem.Text = "Save Script (Ctrl, S)";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(279, 26);
            this.saveAsToolStripMenuItem.Text = "Export as JPG (Ctrl, Shift, S)";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepByStepToolStripMenuItem,
            this.stopRunningToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.runToolStripMenuItem.Text = "Run (F5)";
            // 
            // stepByStepToolStripMenuItem
            // 
            this.stepByStepToolStripMenuItem.Enabled = false;
            this.stepByStepToolStripMenuItem.Name = "stepByStepToolStripMenuItem";
            this.stepByStepToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.stepByStepToolStripMenuItem.Text = "Start";
            this.stepByStepToolStripMenuItem.Click += new System.EventHandler(this.StepByStepToolStripMenuItem_Click);
            // 
            // stopRunningToolStripMenuItem
            // 
            this.stopRunningToolStripMenuItem.Enabled = false;
            this.stopRunningToolStripMenuItem.Name = "stopRunningToolStripMenuItem";
            this.stopRunningToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.stopRunningToolStripMenuItem.Text = "Stop";
            this.stopRunningToolStripMenuItem.Click += new System.EventHandler(this.StopRunningToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.validateToolStripMenuItem.Text = "Validate (F6)";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.ValidateToolStripMenuItem_Click);
            // 
            // screenshotToolStripMenuItem
            // 
            this.screenshotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wholeWindowToolStripMenuItem,
            this.currentWorkingScriptToolStripMenuItem});
            this.screenshotToolStripMenuItem.Name = "screenshotToolStripMenuItem";
            this.screenshotToolStripMenuItem.Size = new System.Drawing.Size(100, 24);
            this.screenshotToolStripMenuItem.Text = "Screenshot";
            // 
            // wholeWindowToolStripMenuItem
            // 
            this.wholeWindowToolStripMenuItem.Name = "wholeWindowToolStripMenuItem";
            this.wholeWindowToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.wholeWindowToolStripMenuItem.Text = "Whole window (F11)";
            this.wholeWindowToolStripMenuItem.Click += new System.EventHandler(this.WholeWindowToolStripMenuItem_Click);
            // 
            // currentWorkingScriptToolStripMenuItem
            // 
            this.currentWorkingScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withScriptNameToolStripMenuItem,
            this.withoutScriptNameToolStripMenuItem});
            this.currentWorkingScriptToolStripMenuItem.Enabled = false;
            this.currentWorkingScriptToolStripMenuItem.Name = "currentWorkingScriptToolStripMenuItem";
            this.currentWorkingScriptToolStripMenuItem.Size = new System.Drawing.Size(237, 26);
            this.currentWorkingScriptToolStripMenuItem.Text = "Script";
            // 
            // withScriptNameToolStripMenuItem
            // 
            this.withScriptNameToolStripMenuItem.Name = "withScriptNameToolStripMenuItem";
            this.withScriptNameToolStripMenuItem.Size = new System.Drawing.Size(312, 26);
            this.withScriptNameToolStripMenuItem.Text = "With script name (Ctrl, F11)";
            this.withScriptNameToolStripMenuItem.Click += new System.EventHandler(this.WithScriptNameToolStripMenuItem_Click);
            // 
            // withoutScriptNameToolStripMenuItem
            // 
            this.withoutScriptNameToolStripMenuItem.Name = "withoutScriptNameToolStripMenuItem";
            this.withoutScriptNameToolStripMenuItem.Size = new System.Drawing.Size(312, 26);
            this.withoutScriptNameToolStripMenuItem.Text = "Without script name (Shift, F11)";
            this.withoutScriptNameToolStripMenuItem.Click += new System.EventHandler(this.WithoutScriptNameToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // promptTypingFormWhenCreatingGeneralStateToolStripMenuItem
            // 
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.CheckOnClick = true;
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.Name = "promptTypingFormWhenCreatingGeneralStateToolStripMenuItem";
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.Size = new System.Drawing.Size(419, 26);
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.Text = "Prompt typing box when creating general state";
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.Click += new System.EventHandler(this.PromptTypingFormWhenCreatingGeneralStateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // tableLayout_main
            // 
            this.tableLayout_main.ColumnCount = 1;
            this.tableLayout_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayout_main.Controls.Add(this.tableLayout_mainWithoutCmd, 0, 0);
            this.tableLayout_main.Controls.Add(this.tableLayout_cmd, 0, 1);
            this.tableLayout_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout_main.Location = new System.Drawing.Point(3, 30);
            this.tableLayout_main.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayout_main.Name = "tableLayout_main";
            this.tableLayout_main.RowCount = 2;
            this.tableLayout_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.tableLayout_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayout_main.Size = new System.Drawing.Size(1603, 880);
            this.tableLayout_main.TabIndex = 1;
            // 
            // tableLayout_mainWithoutCmd
            // 
            this.tableLayout_mainWithoutCmd.AutoScroll = true;
            this.tableLayout_mainWithoutCmd.ColumnCount = 2;
            this.tableLayout_mainWithoutCmd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.87316F));
            this.tableLayout_mainWithoutCmd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.12684F));
            this.tableLayout_mainWithoutCmd.Controls.Add(this.scriptsTabControl, 1, 0);
            this.tableLayout_mainWithoutCmd.Controls.Add(this.statesListPanel, 0, 0);
            this.tableLayout_mainWithoutCmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout_mainWithoutCmd.Location = new System.Drawing.Point(3, 2);
            this.tableLayout_mainWithoutCmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayout_mainWithoutCmd.Name = "tableLayout_mainWithoutCmd";
            this.tableLayout_mainWithoutCmd.RowCount = 1;
            this.tableLayout_mainWithoutCmd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_mainWithoutCmd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 635F));
            this.tableLayout_mainWithoutCmd.Size = new System.Drawing.Size(1597, 635);
            this.tableLayout_mainWithoutCmd.TabIndex = 0;
            // 
            // scriptsTabControl
            // 
            this.scriptsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptsTabControl.Font = new System.Drawing.Font("Consolas", 9F);
            this.scriptsTabControl.Location = new System.Drawing.Point(288, 2);
            this.scriptsTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scriptsTabControl.Name = "scriptsTabControl";
            this.scriptsTabControl.SelectedIndex = 0;
            this.scriptsTabControl.Size = new System.Drawing.Size(1306, 631);
            this.scriptsTabControl.TabIndex = 0;
            this.scriptsTabControl.SelectedIndexChanged += new System.EventHandler(this.ScriptsTabControl_SelectedIndexChanged);
            this.scriptsTabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScriptsTabControl_KeyDown);
            this.scriptsTabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.scriptsTabControl_MouseDown);
            this.scriptsTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScriptsTabControl_MouseUp);
            // 
            // statesListPanel
            // 
            this.statesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statesListPanel.Location = new System.Drawing.Point(3, 2);
            this.statesListPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.statesListPanel.Name = "statesListPanel";
            this.statesListPanel.Size = new System.Drawing.Size(279, 631);
            this.statesListPanel.TabIndex = 1;
            // 
            // tableLayout_cmd
            // 
            this.tableLayout_cmd.BackColor = System.Drawing.Color.Black;
            this.tableLayout_cmd.ColumnCount = 1;
            this.tableLayout_cmd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_cmd.Controls.Add(this.txtCmdUserInput, 0, 1);
            this.tableLayout_cmd.Controls.Add(this.rtxtCmdOutput, 0, 0);
            this.tableLayout_cmd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout_cmd.Location = new System.Drawing.Point(3, 641);
            this.tableLayout_cmd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayout_cmd.Name = "tableLayout_cmd";
            this.tableLayout_cmd.RowCount = 2;
            this.tableLayout_cmd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout_cmd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout_cmd.Size = new System.Drawing.Size(1597, 237);
            this.tableLayout_cmd.TabIndex = 1;
            // 
            // txtCmdUserInput
            // 
            this.txtCmdUserInput.BackColor = System.Drawing.Color.Black;
            this.txtCmdUserInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCmdUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCmdUserInput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCmdUserInput.ForeColor = System.Drawing.SystemColors.Window;
            this.txtCmdUserInput.Location = new System.Drawing.Point(3, 211);
            this.txtCmdUserInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCmdUserInput.Name = "txtCmdUserInput";
            this.txtCmdUserInput.Size = new System.Drawing.Size(1591, 18);
            this.txtCmdUserInput.TabIndex = 0;
            // 
            // rtxtCmdOutput
            // 
            this.rtxtCmdOutput.BackColor = System.Drawing.Color.Black;
            this.rtxtCmdOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtCmdOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtCmdOutput.Font = new System.Drawing.Font("Consolas", 13F);
            this.rtxtCmdOutput.ForeColor = System.Drawing.Color.White;
            this.rtxtCmdOutput.Location = new System.Drawing.Point(3, 2);
            this.rtxtCmdOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtxtCmdOutput.Name = "rtxtCmdOutput";
            this.rtxtCmdOutput.Size = new System.Drawing.Size(1591, 205);
            this.rtxtCmdOutput.TabIndex = 1;
            this.rtxtCmdOutput.Text = "";
            // 
            // panelInfoContainer
            // 
            this.panelInfoContainer.Controls.Add(this.tableLayoutPanelAtRightSide);
            this.panelInfoContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfoContainer.Location = new System.Drawing.Point(1612, 30);
            this.panelInfoContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelInfoContainer.Name = "panelInfoContainer";
            this.panelInfoContainer.Size = new System.Drawing.Size(309, 880);
            this.panelInfoContainer.TabIndex = 2;
            // 
            // tableLayoutPanelAtRightSide
            // 
            this.tableLayoutPanelAtRightSide.ColumnCount = 1;
            this.tableLayoutPanelAtRightSide.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAtRightSide.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelAtRightSide.Controls.Add(this.tableLayoutPanelInfoPanel, 0, 0);
            this.tableLayoutPanelAtRightSide.Controls.Add(this.tableLayoutPanelExistedObjectsContainer, 0, 1);
            this.tableLayoutPanelAtRightSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAtRightSide.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelAtRightSide.Name = "tableLayoutPanelAtRightSide";
            this.tableLayoutPanelAtRightSide.RowCount = 2;
            this.tableLayoutPanelAtRightSide.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAtRightSide.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAtRightSide.Size = new System.Drawing.Size(309, 880);
            this.tableLayoutPanelAtRightSide.TabIndex = 0;
            // 
            // tableLayoutPanelInfoPanel
            // 
            this.tableLayoutPanelInfoPanel.ColumnCount = 1;
            this.tableLayoutPanelInfoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfoPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelInfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfoPanel.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelInfoPanel.Name = "tableLayoutPanelInfoPanel";
            this.tableLayoutPanelInfoPanel.RowCount = 1;
            this.tableLayoutPanelInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfoPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 434F));
            this.tableLayoutPanelInfoPanel.Size = new System.Drawing.Size(303, 434);
            this.tableLayoutPanelInfoPanel.TabIndex = 0;
            // 
            // tableLayoutPanelExistedObjectsContainer
            // 
            this.tableLayoutPanelExistedObjectsContainer.ColumnCount = 1;
            this.tableLayoutPanelExistedObjectsContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelExistedObjectsContainer.Controls.Add(this.lblExistedLinks, 0, 2);
            this.tableLayoutPanelExistedObjectsContainer.Controls.Add(this.cbbExistedLinks, 0, 3);
            this.tableLayoutPanelExistedObjectsContainer.Controls.Add(this.cbbExistedStates, 0, 1);
            this.tableLayoutPanelExistedObjectsContainer.Controls.Add(this.lblExistedStates, 0, 0);
            this.tableLayoutPanelExistedObjectsContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelExistedObjectsContainer.Location = new System.Drawing.Point(3, 443);
            this.tableLayoutPanelExistedObjectsContainer.Name = "tableLayoutPanelExistedObjectsContainer";
            this.tableLayoutPanelExistedObjectsContainer.RowCount = 4;
            this.tableLayoutPanelExistedObjectsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelExistedObjectsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelExistedObjectsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanelExistedObjectsContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelExistedObjectsContainer.Size = new System.Drawing.Size(303, 129);
            this.tableLayoutPanelExistedObjectsContainer.TabIndex = 1;
            // 
            // lblExistedLinks
            // 
            this.lblExistedLinks.AutoSize = true;
            this.lblExistedLinks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblExistedLinks.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistedLinks.Location = new System.Drawing.Point(3, 73);
            this.lblExistedLinks.Name = "lblExistedLinks";
            this.lblExistedLinks.Size = new System.Drawing.Size(297, 20);
            this.lblExistedLinks.TabIndex = 3;
            this.lblExistedLinks.Text = "Links (0)";
            // 
            // cbbExistedLinks
            // 
            this.cbbExistedLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbExistedLinks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbExistedLinks.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbExistedLinks.FormattingEnabled = true;
            this.cbbExistedLinks.Location = new System.Drawing.Point(3, 96);
            this.cbbExistedLinks.Name = "cbbExistedLinks";
            this.cbbExistedLinks.Size = new System.Drawing.Size(297, 28);
            this.cbbExistedLinks.TabIndex = 1;
            this.cbbExistedLinks.SelectedIndexChanged += new System.EventHandler(this.CbbExistedLinks_SelectedIndexChanged);
            // 
            // cbbExistedStates
            // 
            this.cbbExistedStates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbbExistedStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbExistedStates.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbExistedStates.FormattingEnabled = true;
            this.cbbExistedStates.Location = new System.Drawing.Point(3, 33);
            this.cbbExistedStates.Name = "cbbExistedStates";
            this.cbbExistedStates.Size = new System.Drawing.Size(297, 28);
            this.cbbExistedStates.TabIndex = 0;
            this.cbbExistedStates.SelectedIndexChanged += new System.EventHandler(this.CbbExistedStates_SelectedIndexChanged);
            // 
            // lblExistedStates
            // 
            this.lblExistedStates.AutoSize = true;
            this.lblExistedStates.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblExistedStates.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExistedStates.Location = new System.Drawing.Point(3, 10);
            this.lblExistedStates.Name = "lblExistedStates";
            this.lblExistedStates.Size = new System.Drawing.Size(297, 20);
            this.lblExistedStates.TabIndex = 2;
            this.lblExistedStates.Text = "States (0)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 912);
            this.Controls.Add(this.tableLayout_base);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "State-of-the-art Simulator - SOTAS";
            this.tableLayout_base.ResumeLayout(false);
            this.tableLayout_base.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tableLayout_main.ResumeLayout(false);
            this.tableLayout_mainWithoutCmd.ResumeLayout(false);
            this.tableLayout_cmd.ResumeLayout(false);
            this.tableLayout_cmd.PerformLayout();
            this.panelInfoContainer.ResumeLayout(false);
            this.tableLayoutPanelAtRightSide.ResumeLayout(false);
            this.tableLayoutPanelExistedObjectsContainer.ResumeLayout(false);
            this.tableLayoutPanelExistedObjectsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayout_base;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newScriptToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayout_main;
        private System.Windows.Forms.TableLayoutPanel tableLayout_mainWithoutCmd;
        private System.Windows.Forms.TabControl scriptsTabControl;
        private System.Windows.Forms.ToolStripMenuItem openScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenshotToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayout_cmd;
        private System.Windows.Forms.TextBox txtCmdUserInput;
        private System.Windows.Forms.Panel statesListPanel;
        private System.Windows.Forms.Panel panelInfoContainer;
        private System.Windows.Forms.ToolStripMenuItem stepByStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promptTypingFormWhenCreatingGeneralStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRunningToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtxtCmdOutput;
        private System.Windows.Forms.ToolStripMenuItem wholeWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentWorkingScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withScriptNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withoutScriptNameToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAtRightSide;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfoPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelExistedObjectsContainer;
        private System.Windows.Forms.ComboBox cbbExistedStates;
        private System.Windows.Forms.ComboBox cbbExistedLinks;
        private System.Windows.Forms.Label lblExistedStates;
        private System.Windows.Forms.Label lblExistedLinks;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

