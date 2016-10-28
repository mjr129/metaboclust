namespace MetaboliteLevels.Gui.Forms.Editing
{
    partial class FrmEditFileLoadInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctlButton2 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlButton1 = new MetaboliteLevels.Gui.Controls.CtlButton();
            this.ctlTitleBar1 = new MetaboliteLevels.Gui.Controls.CtlTitleBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(747, 334);
            this.panel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.ctlButton2);
            this.flowLayoutPanel1.Controls.Add(this.ctlButton1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 421);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(747, 50);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ctlButton2
            // 
            this.ctlButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ctlButton2.Image = global::MetaboliteLevels.Properties.Resources.MnuCancel;
            this.ctlButton2.Location = new System.Drawing.Point(615, 5);
            this.ctlButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlButton2.Name = "ctlButton2";
            this.ctlButton2.Size = new System.Drawing.Size(128, 40);
            this.ctlButton2.TabIndex = 1;
            this.ctlButton2.Text = "Cancel";
            this.ctlButton2.UseDefaultSize = true;
            this.ctlButton2.UseVisualStyleBackColor = true;
            // 
            // ctlButton1
            // 
            this.ctlButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ctlButton1.Image = global::MetaboliteLevels.Properties.Resources.MnuAccept;
            this.ctlButton1.Location = new System.Drawing.Point(479, 5);
            this.ctlButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlButton1.Name = "ctlButton1";
            this.ctlButton1.Size = new System.Drawing.Size(128, 40);
            this.ctlButton1.TabIndex = 0;
            this.ctlButton1.Text = "OK";
            this.ctlButton1.UseDefaultSize = true;
            this.ctlButton1.UseVisualStyleBackColor = true;
            this.ctlButton1.Click += new System.EventHandler(this.ctlButton1_Click);
            // 
            // ctlTitleBar1
            // 
            this.ctlTitleBar1.AutoSize = true;
            this.ctlTitleBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctlTitleBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlTitleBar1.HelpText = null;
            this.ctlTitleBar1.Location = new System.Drawing.Point(0, 0);
            this.ctlTitleBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctlTitleBar1.MinimumSize = new System.Drawing.Size(384, 0);
            this.ctlTitleBar1.Name = "ctlTitleBar1";
            this.ctlTitleBar1.Size = new System.Drawing.Size(747, 87);
            this.ctlTitleBar1.SubText = "View and modify the fields used when loading data files";
            this.ctlTitleBar1.TabIndex = 1;
            this.ctlTitleBar1.Text = "File load information";
            this.ctlTitleBar1.WarningText = null;
            // 
            // FrmEditFileLoadInfo
            // 
            this.AcceptButton = this.ctlButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ctlButton2;
            this.ClientSize = new System.Drawing.Size(747, 471);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctlTitleBar1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmEditFileLoadInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File load info";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Controls.CtlButton ctlButton1;
        private Controls.CtlButton ctlButton2;
        private Controls.CtlTitleBar ctlTitleBar1;
    }
}