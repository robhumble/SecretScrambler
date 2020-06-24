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
    public class AESCryptoTool : iCryptoTool
    {
        private readonly EncryptionTypeEnum EncryptionType = EncryptionTypeEnum.AES;

        private int keySize = 256;

        public string EncryptRun(string message, string password)
        {
            byte[] encryptionResult;

            //message as bytes
            // byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            var IV = GetCurrentIV();

            using (PasswordDeriveBytes pw = new PasswordDeriveBytes(password, IV))
            {
                byte[] pwBytes = pw.GetBytes(keySize / 8);

                using (RijndaelManaged rij = new RijndaelManaged())
                {
                    rij.Key = pwBytes;
                    rij.IV = IV;

                    ICryptoTransform encryptor = rij.CreateEncryptor(rij.Key, rij.IV);

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
            byte[] messageBytes = Convert.FromBase64String(message);

            var IV = GetCurrentIV();

            using (PasswordDeriveBytes pw = new PasswordDeriveBytes(password, IV))
            {
                byte[] pwBytes = pw.GetBytes(keySize / 8);

                using (RijndaelManaged rij = new RijndaelManaged())
                {
                    rij.Key = pwBytes;
                    rij.IV = IV;

                    ICryptoTransform decryptor = rij.CreateDecryptor(rij.Key, rij.IV);

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
            var hardCoded = "v95da2y6d8xc3v2r";

            var global = Properties.Settings.Default.CustomGlobalIV;
            var customAes = Properties.Settings.Default.CustomAesIV;

            var ivStr = (!string.IsNullOrEmpty(customAes))? customAes: (!string.IsNullOrEmpty(global)) ? global : hardCoded;

            byte[] IV = Encoding.ASCII.GetBytes(ivStr);

            return IV;
        }

        public string GenerateNewIVString() 
        {
            var r = new RijndaelManaged();
            r.KeySize = keySize;
            r.GenerateIV();

            var ivStr = Encoding.ASCII.GetString(r.IV);

            return ivStr;
        }

    }
}
