using SecretScrambler.CryptoTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecretScrambler
{
    public partial class GenerateIVForm : Form
    {
        public GenerateIVForm()
        {
            InitializeComponent();
        }

        private void AesButton_Click(object sender, EventArgs e)
        {
            var aesCryptoTool = new AESCryptoTool();

            var newIV = aesCryptoTool.GenerateNewIVString();

            GenerateIvTextBox.Text = newIV;
        }

        private void TripleDesButton_Click(object sender, EventArgs e)
        {
            var tripleDesCryptoTool = new TripleDESCryptoTool();

            var newIV = tripleDesCryptoTool.GenerateNewIVString();

            GenerateIvTextBox.Text = newIV;
        }
    }
}
