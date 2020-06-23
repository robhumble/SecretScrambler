using pinNote.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pinNote
{
    public partial class keyForm : Form
    {
        private SecurityModel _securityModel { get; set; }

        public keyForm()
        {
            _securityModel = new SecurityModel();
            InitializeComponent();
        }

        public SecurityModel getSecurityModel()
        {
            return _securityModel;
        }

        private void keyButton_Click(object sender, EventArgs e)
        {
            //Populate Security Model based off user selections
            try
            {
                _securityModel.Password = keyBox.Text;
                //Check user selected security checkboxes
                _securityModel.SelectedAES = AESCheckbox.Checked;
                _securityModel.SelectedTripleDES = TripleDESCheckbox.Checked;
                _securityModel.SelectedHumbleCrypt = HumbleCryptCheckbox.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to capture key.  Error: " + ex);
            }

            Hide();
        }

    }
}
