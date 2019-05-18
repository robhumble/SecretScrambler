using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.CryptoTool
{
    interface iCryptoTool
    {
        string EncryptRun(string Message, string password);
        string DecryptRun(string Message, string password);

        string GetEncryptionType();
    }
}
