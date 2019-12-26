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
            this.tableLayout_base = new System.Windows.Forms.TableLayoutPanel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepByStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runThroughToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRunningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenshotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.promptTypingFormWhenCreatingGeneralStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayout_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayout_mainWithoutCmd = new System.Windows.Forms.TableLayoutPanel();
            this.scriptsTabControl = new System.Windows.Forms.TabControl();
            this.statesListPanel = new System.Windows.Forms.Panel();
            this.tableLayout_cmd = new System.Windows.Forms.TableLayoutPanel();
            this.txtCmdUserInput = new System.Windows.Forms.TextBox();
            this.rtxtCmdOutput = new System.Windows.Forms.RichTextBox();
            this.panelInfoContainer = new System.Windows.Forms.Panel();
            this.wholeWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentWorkingScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withScriptNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withoutScriptNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayout_base.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.tableLayout_main.SuspendLayout();
            this.tableLayout_mainWithoutCmd.SuspendLayout();
            this.tableLayout_cmd.SuspendLayout();
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
            this.tableLayout_base.Size = new System.Drawing.Size(1881, 912);
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
            this.screenshotToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1881, 28);
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
            this.newScriptToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.newScriptToolStripMenuItem.Text = "New Script";
            this.newScriptToolStripMenuItem.Click += new System.EventHandler(this.NewScriptToolStripMenuItem_Click);
            // 
            // openScriptToolStripMenuItem
            // 
            this.openScriptToolStripMenuItem.Name = "openScriptToolStripMenuItem";
            this.openScriptToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.openScriptToolStripMenuItem.Text = "Open Script";
            this.openScriptToolStripMenuItem.Click += new System.EventHandler(this.OpenScriptToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.saveToolStripMenuItem.Text = "Save Script";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.saveAsToolStripMenuItem.Text = "Save Script as...";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepByStepToolStripMenuItem,
            this.runThroughToolStripMenuItem,
            this.stopRunningToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // stepByStepToolStripMenuItem
            // 
            this.stepByStepToolStripMenuItem.Name = "stepByStepToolStripMenuItem";
            this.stepByStepToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.stepByStepToolStripMenuItem.Text = "Step by step";
            this.stepByStepToolStripMenuItem.Click += new System.EventHandler(this.StepByStepToolStripMenuItem_Click);
            // 
            // runThroughToolStripMenuItem
            // 
            this.runThroughToolStripMenuItem.Name = "runThroughToolStripMenuItem";
            this.runThroughToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.runThroughToolStripMenuItem.Text = "Run through";
            this.runThroughToolStripMenuItem.Click += new System.EventHandler(this.RunThroughToolStripMenuItem_Click);
            // 
            // stopRunningToolStripMenuItem
            // 
            this.stopRunningToolStripMenuItem.Name = "stopRunningToolStripMenuItem";
            this.stopRunningToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.stopRunningToolStripMenuItem.Text = "Stop";
            this.stopRunningToolStripMenuItem.Click += new System.EventHandler(this.StopRunningToolStripMenuItem_Click);
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
            this.tableLayout_main.Size = new System.Drawing.Size(1567, 880);
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
            this.tableLayout_mainWithoutCmd.Size = new System.Drawing.Size(1561, 635);
            this.tableLayout_mainWithoutCmd.TabIndex = 0;
            // 
            // scriptsTabControl
            // 
            this.scriptsTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptsTabControl.Font = new System.Drawing.Font("Consolas", 9F);
            this.scriptsTabControl.Location = new System.Drawing.Point(282, 2);
            this.scriptsTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.scriptsTabControl.Name = "scriptsTabControl";
            this.scriptsTabControl.SelectedIndex = 0;
            this.scriptsTabControl.Size = new System.Drawing.Size(1276, 631);
            this.scriptsTabControl.TabIndex = 0;
            this.scriptsTabControl.SelectedIndexChanged += new System.EventHandler(this.ScriptsTabControl_SelectedIndexChanged);
            this.scriptsTabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScriptsTabControl_KeyDown);
            this.scriptsTabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScriptsTabControl_MouseUp);
            // 
            // statesListPanel
            // 
            this.statesListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statesListPanel.Location = new System.Drawing.Point(3, 2);
            this.statesListPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.statesListPanel.Name = "statesListPanel";
            this.statesListPanel.Size = new System.Drawing.Size(273, 631);
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
            this.tableLayout_cmd.Size = new System.Drawing.Size(1561, 237);
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
            this.txtCmdUserInput.Size = new System.Drawing.Size(1555, 18);
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
            this.rtxtCmdOutput.Size = new System.Drawing.Size(1555, 205);
            this.rtxtCmdOutput.TabIndex = 1;
            this.rtxtCmdOutput.Text = "";
            // 
            // panelInfoContainer
            // 
            this.panelInfoContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfoContainer.Location = new System.Drawing.Point(1576, 30);
            this.panelInfoContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelInfoContainer.Name = "panelInfoContainer";
            this.panelInfoContainer.Size = new System.Drawing.Size(302, 880);
            this.panelInfoContainer.TabIndex = 2;
            // 
            // wholeWindowToolStripMenuItem
            // 
            this.wholeWindowToolStripMenuItem.Name = "wholeWindowToolStripMenuItem";
            this.wholeWindowToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.wholeWindowToolStripMenuItem.Text = "Whole window";
            this.wholeWindowToolStripMenuItem.Click += new System.EventHandler(this.WholeWindowToolStripMenuItem_Click);
            // 
            // currentWorkingScriptToolStripMenuItem
            // 
            this.currentWorkingScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withScriptNameToolStripMenuItem,
            this.withoutScriptNameToolStripMenuItem});
            this.currentWorkingScriptToolStripMenuItem.Name = "currentWorkingScriptToolStripMenuItem";
            this.currentWorkingScriptToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.currentWorkingScriptToolStripMenuItem.Text = "Current working script";
            // 
            // withScriptNameToolStripMenuItem
            // 
            this.withScriptNameToolStripMenuItem.Name = "withScriptNameToolStripMenuItem";
            this.withScriptNameToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.withScriptNameToolStripMenuItem.Text = "With script name";
            this.withScriptNameToolStripMenuItem.Click += new System.EventHandler(this.WithScriptNameToolStripMenuItem_Click);
            // 
            // withoutScriptNameToolStripMenuItem
            // 
            this.withoutScriptNameToolStripMenuItem.Name = "withoutScriptNameToolStripMenuItem";
            this.withoutScriptNameToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.withoutScriptNameToolStripMenuItem.Text = "Without script name";
            this.withoutScriptNameToolStripMenuItem.Click += new System.EventHandler(this.WithoutScriptNameToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1881, 912);
            this.Controls.Add(this.tableLayout_base);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "State Machine Simulator - G03";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.tableLayout_base.ResumeLayout(false);
            this.tableLayout_base.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.tableLayout_main.ResumeLayout(false);
            this.tableLayout_mainWithoutCmd.ResumeLayout(false);
            this.tableLayout_cmd.ResumeLayout(false);
            this.tableLayout_cmd.PerformLayout();
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
        public System.Windows.Forms.Panel panelInfoContainer;
        private System.Windows.Forms.ToolStripMenuItem stepByStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runThroughToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem promptTypingFormWhenCreatingGeneralStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopRunningToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtxtCmdOutput;
        private System.Windows.Forms.ToolStripMenuItem wholeWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentWorkingScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withScriptNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withoutScriptNameToolStripMenuItem;
    }
}

