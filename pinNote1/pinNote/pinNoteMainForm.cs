using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using pinNote.CryptoTool;
using pinNote.Models;
using pinNote.Logic;

namespace pinNote
{
    public partial class NoteWindow : Form
    {
        public string noteText = string.Empty;

        public SecurityModel _currentSecurityModel;

        private List<iCryptoTool> _currentCryptoTools;

        public NoteWindow()
        {
            InitializeComponent();

            InitializeSecurityObjects();           
        }


        #region Menu: w/ password

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
        }

        #endregion Menu: w/ password


        #region Menu: w/o password

        /// <summary>
        /// Open w/o pin/password.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenNoEncryption_Click(object sender, EventArgs e)
        {
            //  'open a file browser
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
            }

            NoteTextBox1.Text = fileText;
        }

        /// <summary>
        /// Save w/o pin/password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveNoEncryption_Click(object sender, EventArgs e)
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
            MessageBox.Show("CryptoWriter"
                + Environment.NewLine + " Created by Robert Humble"
                + Environment.NewLine + " Contact: humbot1@gmail.com");
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
