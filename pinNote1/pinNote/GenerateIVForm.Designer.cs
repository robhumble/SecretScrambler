namespace pinNote
{
    partial class GenerateIVForm
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
            this.AesButton = new System.Windows.Forms.Button();
            this.TripleDesButton = new System.Windows.Forms.Button();
            this.GenerateIvTextBox = new System.Windows.Forms.TextBox();
            this.GenerateButtonExplainLabel = new System.Windows.Forms.Label();
            this.TextDescriptionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AesButton
            // 
            this.AesButton.Location = new System.Drawing.Point(39, 124);
            this.AesButton.Name = "AesButton";
            this.AesButton.Size = new System.Drawing.Size(150, 77);
            this.AesButton.TabIndex = 0;
            this.AesButton.Text = "AES";
            this.AesButton.UseVisualStyleBackColor = true;
            this.AesButton.Click += new System.EventHandler(this.AesButton_Click);
            // 
            // TripleDesButton
            // 
            this.TripleDesButton.Location = new System.Drawing.Point(221, 124);
            this.TripleDesButton.Name = "TripleDesButton";
            this.TripleDesButton.Size = new System.Drawing.Size(150, 77);
            this.TripleDesButton.TabIndex = 1;
            this.TripleDesButton.Text = "TripleDES";
            this.TripleDesButton.UseVisualStyleBackColor = true;
            this.TripleDesButton.Click += new System.EventHandler(this.TripleDesButton_Click);
            // 
            // GenerateIvTextBox
            // 
            this.GenerateIvTextBox.Location = new System.Drawing.Point(39, 266);
            this.GenerateIvTextBox.Multiline = true;
            this.GenerateIvTextBox.Name = "GenerateIvTextBox";
            this.GenerateIvTextBox.Size = new System.Drawing.Size(332, 93);
            this.GenerateIvTextBox.TabIndex = 2;
            // 
            // GenerateButtonExplainLabel
            // 
            this.GenerateButtonExplainLabel.AutoSize = true;
            this.GenerateButtonExplainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateButtonExplainLabel.Location = new System.Drawing.Point(35, 49);
            this.GenerateButtonExplainLabel.Name = "GenerateButtonExplainLabel";
            this.GenerateButtonExplainLabel.Size = new System.Drawing.Size(362, 20);
            this.GenerateButtonExplainLabel.TabIndex = 3;
            this.GenerateButtonExplainLabel.Text = "Click to generate a new Initialization Vector:";
            // 
            // TextDescriptionLabel
            // 
            this.TextDescriptionLabel.AutoSize = true;
            this.TextDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDescriptionLabel.Location = new System.Drawing.Point(35, 233);
            this.TextDescriptionLabel.Name = "TextDescriptionLabel";
            this.TextDescriptionLabel.Size = new System.Drawing.Size(112, 20);
            this.TextDescriptionLabel.TabIndex = 4;
            this.TextDescriptionLabel.Text = "Your new IV:";
            // 
            // GenerateIVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 443);
            this.Controls.Add(this.TextDescriptionLabel);
            this.Controls.Add(this.GenerateButtonExplainLabel);
            this.Controls.Add(this.GenerateIvTextBox);
            this.Controls.Add(this.TripleDesButton);
            this.Controls.Add(this.AesButton);
            this.Name = "GenerateIVForm";
            this.Text = "GenerateIVForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AesButton;
        private System.Windows.Forms.Button TripleDesButton;
        private System.Windows.Forms.TextBox GenerateIvTextBox;
        private System.Windows.Forms.Label GenerateButtonExplainLabel;
        private System.Windows.Forms.Label TextDescriptionLabel;
    }
}