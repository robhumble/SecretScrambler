using pinNote.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace pinNote.CryptoTool
{

    //Based off of example found here: https://msdn.microsoft.com/en-us/library/system.security.cryptography.rijndaelmanaged.aspx
    public class TripleDESCryptoTool : iCryptoTool
    {
        private readonly EncryptionTypeEnum EncryptionType = EncryptionTypeEnum.TripleDES;

        private int keySize = 192;

        public string EncryptRun(string message, string password)
        {
            byte[] encryptionResult;

            //message as bytes
            //byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            var IV = GetCurrentIV();

            using (PasswordDeriveBytes pw = new PasswordDeriveBytes(password, IV))
            {
                byte[] pwBytes = pw.GetBytes(keySize / 8);

                using (TripleDESCryptoServiceProvider TripleDESprovider = new TripleDESCryptoServiceProvider())
                {
                    TripleDESprovider.Key = pwBytes;
                    TripleDESprovider.IV = IV;

                    ICryptoTransform encryptor = TripleDESprovider.CreateEncryptor(TripleDESprovider.Key, TripleDESprovider.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(message);
                            }
                            encryptionResult = ms.ToArray();
                        }
                    }
                }
            }

            return Convert.ToBase64String(encryptionResult);
        }


        public string DecryptRun(string message, string password)
        {
            string decryptionResult = null;

            //message as bytes
            byte[] messageBytes = Convert.FromBase64String(message);// = Encoding.ASCII.GetBytes(message);

            var IV = GetCurrentIV();

            using (PasswordDeriveBytes pw = new PasswordDeriveBytes(password, IV))
            {
                byte[] pwBytes = pw.GetBytes(keySize / 8);

                using (TripleDESCryptoServiceProvider TripleDESprovider = new TripleDESCryptoServiceProvider())
                {
                    TripleDESprovider.Key = pwBytes;
                    TripleDESprovider.IV = IV;

                    ICryptoTransform decryptor = TripleDESprovider.CreateDecryptor(TripleDESprovider.Key, TripleDESprovider.IV);

                    using (MemoryStream ms = new MemoryStream(messageBytes))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                decryptionResult = sr.ReadToEnd();
                            }

                        }
                    }
                }
            }

            return decryptionResult;
        }

        public EncryptionTypeEnum GetEncryptionType()
        {
            return EncryptionType;
        }

        public byte[] GetCurrentIV()
        {
            var hardCoded = "3d5jxa22";

            var global = Properties.Settings.Default.CustomGlobalIV;
            var customTripleDES = Properties.Settings.Default.CustomTripleDesIV;

            var ivStr = (!string.IsNullOrEmpty(customTripleDES)) ? customTripleDES : (!string.IsNullOrEmpty(global)) ? global : hardCoded;

            byte[] IV = Encoding.ASCII.GetBytes(ivStr);

            return IV;
        }

        public string GenerateNewIVString()
        {
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.KeySize = keySize;
            tdes.GenerateIV();

            var ivStr = Encoding.ASCII.GetString(tdes.IV);

            return ivStr;
        }
    }
}
