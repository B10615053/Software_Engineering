namespace SWE_Final_Project.Views.SubForms {
    partial class AlertForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.txtShowAlertMessage = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel_baseAtAlertForm = new System.Windows.Forms.TableLayoutPanel();
            this.btnNoAtAlertForm = new System.Windows.Forms.Button();
            this.btnCancelAtAlertForm = new System.Windows.Forms.Button();
            this.btnYesAtAlertForm = new System.Windows.Forms.Button();
            this.tableLayoutPanel_baseAtAlertForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShowAlertMessage
            // 
            this.txtShowAlertMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtShowAlertMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel_baseAtAlertForm.SetColumnSpan(this.txtShowAlertMessage, 3);
            this.txtShowAlertMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtShowAlertMessage.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtShowAlertMessage.Location = new System.Drawing.Point(13, 13);
            this.txtShowAlertMessage.Margin = new System.Windows.Forms.Padding(13);
            this.txtShowAlertMessage.Multiline = true;
            this.txtShowAlertMessage.Name = "txtShowAlertMessage";
            this.txtShowAlertMessage.ReadOnly = true;
            this.txtShowAlertMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtShowAlertMessage.Size = new System.Drawing.Size(512, 143);
            this.txtShowAlertMessage.TabIndex = 1;
            // 
            // tableLayoutPanel_baseAtAlertForm
            // 
            this.tableLayoutPanel_baseAtAlertForm.ColumnCount = 3;
            this.tableLayoutPanel_baseAtAlertForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_baseAtAlertForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_baseAtAlertForm.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.btnNoAtAlertForm, 0, 1);
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.btnCancelAtAlertForm, 0, 1);
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.txtShowAlertMessage, 0, 0);
            this.tableLayoutPanel_baseAtAlertForm.Controls.Add(this.btnYesAtAlertForm, 2, 1);
            this.tableLayoutPanel_baseAtAlertForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_baseAtAlertForm.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_baseAtAlertForm.Name = "tableLayoutPanel_baseAtAlertForm";
            this.tableLayoutPanel_baseAtAlertForm.RowCount = 2;
            this.tableLayoutPanel_baseAtAlertForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_baseAtAlertForm.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel_baseAtAlertForm.Size = new System.Drawing.Size(538, 239);
            this.tableLayoutPanel_baseAtAlertForm.TabIndex = 1;
            // 
            // btnNoAtAlertForm
            // 
            this.btnNoAtAlertForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnNoAtAlertForm.Location = new System.Drawing.Point(192, 182);
            this.btnNoAtAlertForm.Margin = new System.Windows.Forms.Padding(13);
            this.btnNoAtAlertForm.Name = "btnNoAtAlertForm";
            this.btnNoAtAlertForm.Size = new System.Drawing.Size(148, 44);
            this.btnNoAtAlertForm.TabIndex = 3;
            this.btnNoAtAlertForm.Text = "No";
            this.btnNoAtAlertForm.UseVisualStyleBackColor = true;
            this.btnNoAtAlertForm.Click += new System.EventHandler(this.BtnNoAtAlertForm_Click);
            // 
            // btnCancelAtAlertForm
            // 
            this.btnCancelAtAlertForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnCancelAtAlertForm.Location = new System.Drawing.Point(13, 182);
            this.btnCancelAtAlertForm.Margin = new System.Windows.Forms.Padding(13);
            this.btnCancelAtAlertForm.Name = "btnCancelAtAlertForm";
            this.btnCancelAtAlertForm.Size = new System.Drawing.Size(148, 44);
            this.btnCancelAtAlertForm.TabIndex = 2;
            this.btnCancelAtAlertForm.Text = "Cancel";
            this.btnCancelAtAlertForm.UseVisualStyleBackColor = true;
            this.btnCancelAtAlertForm.Click += new System.EventHandler(this.BtnCancelAtAlertForm_Click);
            // 
            // btnYesAtAlertForm
            // 
            this.btnYesAtAlertForm.Font = new System.Drawing.Font("Consolas", 12F);
            this.btnYesAtAlertForm.Location = new System.Drawing.Point(371, 182);
            this.btnYesAtAlertForm.Margin = new System.Windows.Forms.Padding(13);
            this.btnYesAtAlertForm.Name = "btnYesAtAlertForm";
            this.btnYesAtAlertForm.Size = new System.Drawing.Size(148, 44);
            this.btnYesAtAlertForm.TabIndex = 0;
            this.btnYesAtAlertForm.Text = "Yes";
            this.btnYesAtAlertForm.UseVisualStyleBackColor = true;
            this.btnYesAtAlertForm.Click += new System.EventHandler(this.BtnConfirmAtAlertForm_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 239);
            this.Controls.Add(this.tableLayoutPanel_baseAtAlertForm);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlertForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AlertForm";
            this.tableLayoutPanel_baseAtAlertForm.ResumeLayout(false);
            this.tableLayoutPanel_baseAtAlertForm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtShowAlertMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_baseAtAlertForm;
        private System.Windows.Forms.Button btnYesAtAlertForm;
        private System.Windows.Forms.Button btnNoAtAlertForm;
        private System.Windows.Forms.Button btnCancelAtAlertForm;
    }
}