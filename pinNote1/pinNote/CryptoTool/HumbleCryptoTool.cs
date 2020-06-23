using pinNote.Enums;
using pinNote.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.CryptoTool
{
    public class HumbleCryptoTool : iCryptoTool
    {
        private readonly EncryptionTypeEnum EncryptionType = EncryptionTypeEnum.Humble;

        public string EncryptRun(string message, string password)
        {
            //Requires int

            int shiftBy = TransformHelper.PasswordStrToKeyInt(password);

            String shiftedString = "";
            byte[] bytesToShift = Encoding.Unicode.GetBytes(message);
            //byte[] bytesToShift = Convert.FromBase64String(message);

            int iterator = 0;

            foreach (byte b in message)
            {
                byte temp;

                if (iterator % 2 == 0)
                {
                    temp = (byte)(b + shiftBy);
                }
                else
                {
                    temp = (byte)(b - shiftBy);
                }

                shiftedString += Convert.ToChar(temp);

                iterator++;
            }

            return shiftedString;
        }


        public string DecryptRun(string message, string password)
        {
            //Requires int
            int shiftBy = TransformHelper.PasswordStrToKeyInt(password);

            String shiftedString = "";
            byte[] bytesToShift = Encoding.Unicode.GetBytes(message);
            //byte[] bytesToShift = Convert.FromBase64String(message);
            int iterator = 0;

            foreach (byte b in message)
            {
                byte temp;

                if (iterator % 2 == 0)
                {
                    temp = (byte)(b - shiftBy);
                }
                else
                {
                    temp = (byte)(b + shiftBy);
                }

                shiftedString += Convert.ToChar(temp);

                iterator++;
            }



            return shiftedString;
        }

        public EncryptionTypeEnum GetEncryptionType()
        {
            return EncryptionType;
        }

    }
}
