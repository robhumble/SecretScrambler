using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.ExceptionHandling
{
    //Not used currently
   public class rhCryptoException : Exception
    {
       public string cryptoTool { get; set; }

       public string cryptoUserPresentationMessage { get; set; }        
    }
}
