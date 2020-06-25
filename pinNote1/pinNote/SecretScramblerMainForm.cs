using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SecretScrambler.CryptoTool;
using SecretScrambler.Models;
using SecretScrambler.Logic;

namespace SecretScrambler
{
    public partial class NoteWindow : Form
    {
        public string noteText = string.Empty;

        public SecurityModel _currentSecurityModel;

        private List<iCryptoTool> _currentCryptoTools;

        public string _currentFileName;


        public NoteWindow()
        {
            InitializeComponent();

            InitializeSecurityObjects();
        }

        #region Basic Functions - Open, Save, New, Save As

        /// <summary>
        /// Prep for window for new text content (Clear).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var confirmWindowResult = MessageBox.Show("Are you sure you want to clear your current session?", "Confirm clear", MessageBoxButtons.YesNo);

            if (confirmWindowResult == DialogResult.Yes)
            {
                NoteTextBox1.Text = string.Empty;
                ClearCurrentFile();
                InitializeSecurityObjects();
            }         
        }

        /// <summary>
        /// Open any file (encrypted or not) and place contents into the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, EventArgs e)
        {
            //  'open a file browser
            OpenFileDialog openFileBrowser = new OpenFileDialog();
            openFileBrowser.ShowDialog();

            string fileText = string.Empty;

            try
            {
                fileText = GetFileContents(openFileBrowser.FileName);
                NoteTextBox1.Text = fileText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetCurrentFile( openFileBrowser.FileName);            
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_currentFileName))
            {
                SaveAs_Click(sender, e);
            }
            else
            {
                try
                {
                    WriteTextToFile(_currentFileName, NoteTextBox1.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        /// <summary>
        /// Save As current window text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileBrowser = new SaveFileDialog();

            saveFileBrowser.DefaultExt = ".txt";
            saveFileBrowser.Filter = " Text (*.txt) |*.txt| All Files (*.*) |*.* ";

            saveFileBrowser.ShowDialog();

            try
            {
                WriteTextToFile(saveFileBrowser.FileName, NoteTextBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SetCurrentFile(saveFileBrowser.FileName);
        }

        #endregion

        #region Encryption - Encrypt Current, Decrypt Current

        /// <summary>
        /// Encrypt the text currently in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encryptCurrentWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                keyForm popup = new keyForm();

                popup.ShowDialog();
                _currentSecurityModel = popup.getSecurityModel();

                //build crypto tools based off user input
                _currentCryptoTools = CryptoManager.BuildCryptoToolsFromSecurityModel(_currentSecurityModel).ToList();
                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Popup failed : " + ex);
                InitializeSecurityObjects();
                return;
            }

            try
            {
                var encryptedText = CryptoManager.RunSelectedEncryption(_currentCryptoTools, NoteTextBox1.Text, _currentSecurityModel.Password);
                NoteTextBox1.Text = encryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InitializeSecurityObjects();
                return;
            }

            InitializeSecurityObjects();
        }

        /// <summary>
        /// Decrypt the text currently in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void decryptCurrentWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                keyForm popup = new keyForm();
                popup.ShowDialog();

                _currentSecurityModel = popup.getSecurityModel();

                //build crypto tools based off user input
                _currentCryptoTools = CryptoManager.BuildCryptoToolsFromSecurityModel(_currentSecurityModel).ToList();
                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Popup failed : " + ex);
                InitializeSecurityObjects();
                return;
            }

            try
            {
                var currentText = NoteTextBox1.Text;

                //use password to decrypt
                string decryptedText = CryptoManager.RunSelectedDecryption(_currentCryptoTools, currentText, _currentSecurityModel.Password);

                NoteTextBox1.Text = decryptedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InitializeSecurityObjects();
                return;
            }


            //now that the text has been done reset security state
            InitializeSecurityObjects();

        }

        #endregion

        #region EXPERIMENTAL - Encrypt & Save, Decrypt & Open

        /// <summary>
        /// Open With Pin/Password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptOpen_Click(object sender, EventArgs e)
        {
            //open a file browser
            OpenFileDialog openFileBrowser = new OpenFileDialog();
            openFileBrowser.ShowDialog();

            string fileText = string.Empty;

            try
            {
                fileText = GetFileContents(openFileBrowser.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InitializeSecurityObjects();
                return;
            }

            keyForm popup = new keyForm();
            popup.ShowDialog();

            _currentSecurityModel = popup.getSecurityModel();

            //build crypto tools based off user input
            _currentCryptoTools = CryptoManager.BuildCryptoToolsFromSecurityModel(_currentSecurityModel).ToList();
            popup.Dispose();

            //use password to decrypt
            String decryptedText = CryptoManager.RunSelectedDecryption(_currentCryptoTools, fileText, _currentSecurityModel.Password);

            NoteTextBox1.Text = decryptedText;


            //now that the text has been done reset security state
            InitializeSecurityObjects();
            SetCurrentFile(openFileBrowser.FileName);
        }

        /// <summary>
        /// Save With Pin/Password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                keyForm popup = new keyForm();

                popup.ShowDialog();
                _currentSecurityModel = popup.getSecurityModel();

                //build crypto tools based off user input
                _currentCryptoTools = CryptoManager.BuildCryptoToolsFromSecurityModel(_currentSecurityModel).ToList();
                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("popup failed : " + ex);
                InitializeSecurityObjects();
                return;
            }

            SaveFileDialog saveFileBrowser = new SaveFileDialog();
            saveFileBrowser.ShowDialog();

            string encryptedText = string.Empty;

            try
            {
                encryptedText = CryptoManager.RunSelectedEncryption(_currentCryptoTools, NoteTextBox1.Text, _currentSecurityModel.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InitializeSecurityObjects();
                return;
            }

            try
            {
                WriteTextToFile(saveFileBrowser.FileName, encryptedText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //now that the text has been done reset security state
            InitializeSecurityObjects();
            SetCurrentFile(saveFileBrowser.FileName);
        }

        /// <summary>
        /// Show the GenerateIV Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateNewIVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var generateIvPopup = new GenerateIVForm();
            generateIvPopup.ShowDialog();
            generateIvPopup.Dispose();            
        }

        #endregion

        #region Help Menu

        /// <summary>
        /// Display the "About" message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SecretScrambler"
                + Environment.NewLine + " Created by Rob Humble"
                + Environment.NewLine + " Contact: humbot1@gmail.com"
                + Environment.NewLine 
                + Environment.NewLine + " MIT License"
                + Environment.NewLine + "Copyright(c) 2020 Robert Humble"
                );
        }

        #endregion Help Menu

        #region Private Helpers

        /// <summary>
        /// Reset/Set objects to null.
        /// </summary>
        private void InitializeSecurityObjects()
        {
            _currentSecurityModel = new SecurityModel();
            _currentCryptoTools = new List<iCryptoTool>();
        }

        /// <summary>
        /// Read text from a specified file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileContents(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new Exception("No File Selected!");

            StreamReader fileReader;
            String fileText = "";

            try
            {
                fileReader = new StreamReader(fileName);
                fileText = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error opening file: " + Environment.NewLine + ex);
            }

            return fileText;
        }

        /// <summary>
        /// Write the given text to a file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="textToWrite"></param>
        private void WriteTextToFile(string fileName, string textToWrite)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new Exception("No File Selected!");
            try
            {
                StreamWriter fileWriter = new StreamWriter(fileName);
                fileWriter.Write(textToWrite);
                fileWriter.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving file: " + Environment.NewLine + ex);
            }

        }

        /// <summary>
        /// Set The Text variable equal to whats present in the main window.
        /// </summary>
        private void SetNoteTextFromTextBox()
        {
            noteText = NoteTextBox1.Text;
        }

        private void SetCurrentFile(string fileName)
        {
            _currentFileName = fileName;
            this.Text = "SecretScrambler ~" + fileName;
        }

        private void ClearCurrentFile()
        {
            _currentFileName = string.Empty;
            this.Text = "SecretScrambler";
        }

        private void DebugRecorder(String inBound)
        {
            bool use = false;

            if (use)
            {
                StreamWriter fileWriter = File.AppendText("C:\\Users\\Rob\\Desktop\\DebugLog.txt");
                String fileText = "";

                fileWriter.WriteLine(inBound);
                fileWriter.Close();
            }

        }

        #endregion Private Helpers        
    }

}
