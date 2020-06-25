using SecretScrambler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecretScrambler.CryptoTool
{
    public interface iCryptoTool
    {
        /// <summary>
        /// Encrypt a message using the given password.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string EncryptRun(string Message, string password);

        /// <summary>
        /// Decrypt a message using the given password.
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string DecryptRun(string Message, string password);

        /// <summary>
        /// Get the encryption type enum that represents the crypto tool.
        /// </summary>
        /// <returns></returns>
        EncryptionTypeEnum GetEncryptionType();

        /// <summary>
        /// Get the initialization vector for the current crypto tool.
        /// </summary>
        /// <returns></returns>
        byte[] GetCurrentIV();

        /// <summary>
        /// Generate a new Initialization Vector string.
        /// </summary>
        /// <returns></returns>
        string GenerateNewIVString();
       
    }
}
