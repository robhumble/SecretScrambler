namespace pinNote
{
    partial class NoteWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opennoEncryptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savenormalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NoteTextBox1 = new System.Windows.Forms.TextBox();
            this.encryptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.encryptSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decryptAndOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.encryptionToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(625, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opennoEncryptionToolStripMenuItem,
            this.savenormalToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // opennoEncryptionToolStripMenuItem
            // 
            this.opennoEncryptionToolStripMenuItem.Name = "opennoEncryptionToolStripMenuItem";
            this.opennoEncryptionToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.opennoEncryptionToolStripMenuItem.Text = "Open";
            this.opennoEncryptionToolStripMenuItem.Click += new System.EventHandler(this.OpenNoEncryption_Click);
            // 
            // savenormalToolStripMenuItem
            // 
            this.savenormalToolStripMenuItem.Name = "savenormalToolStripMenuItem";
            this.savenormalToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.savenormalToolStripMenuItem.Text = "Save";
            this.savenormalToolStripMenuItem.Click += new System.EventHandler(this.SaveNoEncryption_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // NoteTextBox1
            // 
            this.NoteTextBox1.Location = new System.Drawing.Point(12, 27);
            this.NoteTextBox1.Multiline = true;
            this.NoteTextBox1.Name = "NoteTextBox1";
            this.NoteTextBox1.Size = new System.Drawing.Size(601, 416);
            this.NoteTextBox1.TabIndex = 2;
            // 
            // encryptionToolStripMenuItem
            // 
            this.encryptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryptSaveToolStripMenuItem,
            this.decryptAndOpenToolStripMenuItem});
            this.encryptionToolStripMenuItem.Name = "encryptionToolStripMenuItem";
            this.encryptionToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.encryptionToolStripMenuItem.Text = "Encryption";
            // 
            // encryptSaveToolStripMenuItem
            // 
            this.encryptSaveToolStripMenuItem.Name = "encryptSaveToolStripMenuItem";
            this.encryptSaveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.encryptSaveToolStripMenuItem.Text = "Encrypt And Save";
            this.encryptSaveToolStripMenuItem.Click += new System.EventHandler(this.SaveEncrypt_Click);
            // 
            // decryptAndOpenToolStripMenuItem
            // 
            this.decryptAndOpenToolStripMenuItem.Name = "decryptAndOpenToolStripMenuItem";
            this.decryptAndOpenToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.decryptAndOpenToolStripMenuItem.Text = "Decrypt And Open";
            this.decryptAndOpenToolStripMenuItem.Click += new System.EventHandler(this.DecryptOpen_Click);
            // 
            // NoteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 455);
            this.Controls.Add(this.NoteTextBox1);
            this.Controls.Add(this.MainMenuStrip);
            this.Name = "NoteWindow";
            this.Text = "pinNote";
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opennoEncryptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savenormalToolStripMenuItem;
        private System.Windows.Forms.TextBox NoteTextBox1;
        private System.Windows.Forms.ToolStripMenuItem encryptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem encryptSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decryptAndOpenToolStripMenuItem;
    }
}

