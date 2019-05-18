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

namespace pinNote
{
    public partial class NoteWindow : Form
    {
        public String noteText = string.Empty;

        //public string currentPassword = string.Empty;

        public SecurityModel _currentSecurityModel;


        //public int currentKey = 0;

        // private iCryptoTool CryptoTool;

        private List<iCryptoTool> _currentCryptoTools;

        public NoteWindow()
        {
            InitializeComponent();

            InitializeSecurityObjects();

            // CryptoTool = new HumbleCryptoTool();// new HumbleCryptoTool();
            //  MessageBox.Show("this is 33" + Convert.ToChar());
        }





        #region Menu: w/ password

        //Open With Pin
        private void DecryptOpen_Click(object sender, EventArgs e)
        {

            //open a file browser
            OpenFileDialog openFileBrowser = new OpenFileDialog();
            openFileBrowser.ShowDialog();



            StreamReader fileReader;
            String fileText = "";

            try
            {


                fileReader = new StreamReader(openFileBrowser.FileName.ToString());
                fileText = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + Environment.NewLine + ex);
            }

            //-----------------------------------------------------

            keyForm popup = new keyForm();

            popup.ShowDialog();

            //switching to password
            //currentKey = popup.getKey();
            //currentPassword = popup.getPassword();
            _currentSecurityModel = popup.getSecurityModel();

            //build crypto tools based off user input
            BuildCryptoToolsFromSecurityModel();


            // MessageBox.Show("hello world, here's your pin: " + currentKey);
            popup.Dispose();


            // String decryptedText = noteDecrypt(fileText);
            //String decryptedText = CryptoTool.DecryptRun(fileText, currentKey);
            //String decryptedText = CryptoTool.DecryptRun(fileText, currentPassword);

            //use password to decrypt
            String decryptedText = RunSelectedDecryption(fileText, _currentSecurityModel.Password);


            NoteTextBox1.Text = decryptedText;

            //now that the text has been done reset security state
            InitializeSecurityObjects();
        }

        //Save With Pin
        private void SaveEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                keyForm popup = new keyForm();

                popup.ShowDialog();


                //switching to password
                //currentKey = popup.getKey();
                //currentPassword = popup.getPassword();
                _currentSecurityModel = popup.getSecurityModel();

                //build crypto tools based off user input
                BuildCryptoToolsFromSecurityModel();

                // MessageBox.Show("hello world, here's your pin: " + currentKey);
                popup.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("popup failed : " + ex);
            }


            SaveFileDialog saveFileBrowser = new SaveFileDialog();
            saveFileBrowser.ShowDialog();


            try
            {
                StreamWriter fileWriter = new StreamWriter(saveFileBrowser.FileName.ToString());
                String fileText = "";

                //fileText = CryptoTool.EncryptRun(NoteTextBox1.Text, currentKey);
                //fileText = CryptoTool.EncryptRun(NoteTextBox1.Text, currentPassword);

                fileText = RunSelectedEncryption(NoteTextBox1.Text, _currentSecurityModel.Password);


                fileWriter.Write(fileText);
                fileWriter.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + Environment.NewLine + ex);
            }


            //now that the text has been done reset security state
            InitializeSecurityObjects();
        }

        #endregion Menu: w/ password


        #region Menu: w/o password

        //Open w/o pin
        private void OpenNoEncryption_Click(object sender, EventArgs e)
        {
            //  'open a file browser
            OpenFileDialog openFileBrowser = new OpenFileDialog();
            openFileBrowser.ShowDialog();


            //'MessageBox.Show(openFileBrowser.ToString())

            //'populate submission field with file
            //Dim fileReader As StreamReader = StreamReader.Null
            //Dim fileText As String

            StreamReader fileReader;
            String fileText = "";




            try
            {


                fileReader = new StreamReader(openFileBrowser.FileName.ToString());
                fileText = fileReader.ReadToEnd();
                fileReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file: " + Environment.NewLine + ex);
            }


            NoteTextBox1.Text = fileText;
        }

        //Save w/o pin
        private void SaveNoEncryption_Click(object sender, EventArgs e)
        {


            SaveFileDialog saveFileBrowser = new SaveFileDialog();

            // saveFileBrowser.AddExtension = True
            saveFileBrowser.DefaultExt = ".txt";

            // saveFileBrowser.CreatePrompt = True
            // saveFileBrowser.FileName = "AS_ScriptFile"

            saveFileBrowser.Filter = " Text (*.txt) |*.txt| All Files (*.*) |*.* ";


            saveFileBrowser.ShowDialog();



            // 'MessageBox.Show(openFileBrowser.ToString())

            try
            {
                // 'populate submission field with file

                StreamWriter fileWriter = new StreamWriter(saveFileBrowser.FileName.ToString());
                String fileText = "";


                //'fileWriter.



                fileText = NoteTextBox1.Text;
                fileWriter.Write(fileText);
                fileWriter.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + Environment.NewLine + ex);
            }
        }

        #endregion


        #region Help Menu
        //About Message
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CryptoWriter"
                + Environment.NewLine + " Created by Robert Humble"
                + Environment.NewLine + " Contact: rh1269@gmail.com");
        }
        #endregion Help Menu




        #region Private Helpers

        //Reset/Set objects to null
        private void InitializeSecurityObjects()
        {
            _currentSecurityModel = new SecurityModel();
            _currentCryptoTools = new List<iCryptoTool>();
        }


        private void BuildCryptoToolsFromSecurityModel()
        {
            //in case i don't want to use class var later
            var securityModel = _currentSecurityModel;

            var cryptoTools = new List<iCryptoTool>();

            if (securityModel.SelectedAES)
                cryptoTools.Add(new AESCryptoTool());

            if (securityModel.SelectedTripleDES)
                cryptoTools.Add(new TripleDESCryptoTool());

            if (securityModel.SelectedHumbleCrypt)
                cryptoTools.Add(new HumbleCryptoTool());


            //set the cryptoTools to the current  
            _currentCryptoTools = cryptoTools;
        }

        private string RunSelectedEncryption(string message, string password)
        {
            string result = message;

            try
            {

                if (_currentCryptoTools.Count > 0)
                {
                    foreach (var tool in _currentCryptoTools)
                    {
                        //CATCHING THE ERRORS HERE(on per tool basis) MAY GIVE TOO MUCH INFORMATION AWAY TO THE USER

                        // try
                        // {
                        result = tool.EncryptRun(result, password);
                        // }
                        // catch (Exception e)
                        // { 
                        //     MessageBox.Show("Encryption Failed"
                        //           + Environment.NewLine + "Failed on Type: " + tool.GetEncryptionType());                               
                        //}

                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Encryption Failed");
            }

            return result;
        }

        private string RunSelectedDecryption(string message, string password)
        {
            string result = message;

            //Reverse The list  

            _currentCryptoTools.Reverse();

            try
            {

                if (_currentCryptoTools.Count > 0)
                {
                    foreach (var tool in _currentCryptoTools)
                    {
                        //CATCHING THE ERRORS HERE(on per tool basis) MAY GIVE TOO MUCH INFORMATION AWAY TO THE USER

                        // try{

                        result = tool.DecryptRun(result, password);
                        //  }
                        // catch (Exception e)
                        // { 
                        //    MessageBox.Show("Decryption Failed"
                        //            + Environment.NewLine + "Failed on Type: " + tool.GetEncryptionType());                               
                        // }
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Decryption Failed"
                        + Environment.NewLine 
                        + Environment.NewLine + "Please check your password and selected encryption types.");
            }

            return result;
        }

        //Set The Text variable equal to whats present in the main window
        private void setNoteTextFromTextBox()
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


                //'fileWriter.



                fileWriter.WriteLine(inBound);
                fileWriter.Close();
            }

        }


        #endregion Private Helpers





    }











}
