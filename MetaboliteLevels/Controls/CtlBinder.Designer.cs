namespace MetaboliteLevels.Controls
{
    partial class CtlBinder<T>
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._errorProvider = new MetaboliteLevels.Controls.CtlError(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._cmsRevertButton = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._mnuUndoChanges = new System.Windows.Forms.ToolStripMenuItem();
            this._mnuSetToDefault = new System.Windows.Forms.ToolStripMenuItem();
            this._cmsRevertButton.SuspendLayout();
            // 
            // _cmsRevertButton
            // 
            this._cmsRevertButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._mnuUndoChanges,
            this._mnuSetToDefault});
            this._cmsRevertButton.Name = "_cmsRevertButton";
            this._cmsRevertButton.Size = new System.Drawing.Size(168, 48);
            // 
            // _mnuUndoChanges
            // 
            this._mnuUndoChanges.Name = "_mnuUndoChanges";
            this._mnuUndoChanges.Size = new System.Drawing.Size(167, 22);
            this._mnuUndoChanges.Text = "Undo changes";
            // 
            // _mnuSetToDefault
            // 
            this._mnuSetToDefault.Name = "_mnuSetToDefault";
            this._mnuSetToDefault.Size = new System.Drawing.Size(167, 22);
            this._mnuSetToDefault.Text = "Restore to default";
            this._cmsRevertButton.ResumeLayout(false);

        }

        #endregion

        private CtlError _errorProvider;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip _cmsRevertButton;
        private System.Windows.Forms.ToolStripMenuItem _mnuUndoChanges;
        private System.Windows.Forms.ToolStripMenuItem _mnuSetToDefault;
    }
}
