using pinNote.CryptoTool;
using pinNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.Logic
{
    public static class CryptoManager
    {
        /// <summary>
        /// Build a List of the selected CryptoTools.
        /// </summary>
        /// <param name="securityModel"></param>
        /// <returns></returns>
        public static IList<iCryptoTool> BuildCryptoToolsFromSecurityModel(SecurityModel securityModel)
        {
            var cryptoTools = new List<iCryptoTool>();

            if (securityModel.SelectedAES)
                cryptoTools.Add(new AESCryptoTool());

            if (securityModel.SelectedTripleDES)
                cryptoTools.Add(new TripleDESCryptoTool());

            if (securityModel.SelectedHumbleCrypt)
                cryptoTools.Add(new HumbleCryptoTool());
           
            return cryptoTools;
        }

        /// <summary>
        /// Encrypt a string using all specified CryptoTools.
        /// </summary>
        /// <param name="cryptoTools"></param>
        /// <param name="message"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string RunSelectedEncryption(IList<iCryptoTool> cryptoTools, string message, string password)
        {
            string result = message;

            try
            {

                if (cryptoTools.Count > 0)
                {
                    foreach (var tool in cryptoTools)
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
                //MessageBox.Show("Encryption Failed");
                Console.WriteLine("Encryption Failed!");
                throw e;
            }

            return result;
        }

        /// <summary>
        /// Decrypt a string using all specified CryptoTools.
        /// </summary>
        /// <param name="cryptoTools"></param>
        /// <param name="message"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string RunSelectedDecryption(IList<iCryptoTool> cryptoTools, string message, string password)
        {
            string result = message;

            //Reverse The list
            cryptoTools = cryptoTools.Reverse().ToList();

            try
            {

                if (cryptoTools.Count > 0)
                {
                    foreach (var tool in cryptoTools)
                    {
                        //CATCHING THE ERRORS HERE(on per tool basis) MAY GIVE TOO MUCH INFORMATION AWAY TO THE USER

                        //try{

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
                //MessageBox.Show("Decryption Failed"
                //        + Environment.NewLine
                //        + Environment.NewLine + "Please check your password and selected encryption types.");

                Console.WriteLine(
                    "Decryption Failed"
                        + Environment.NewLine
                        + Environment.NewLine + "Please check your password and selected encryption types."
                    );
                throw e;

            }

            return result;
        }

    }
}
