﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.Logic
{
    /// <summary>
    ///Helps transform data, assistance for crypto tools
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// Get an integer representation of a password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int PasswordStrToKeyInt(string password)
        {
            int returnKey = 0;

            int x = 0;

            //orignally wanted this to work like this 
            //a=1,b=2, c=3
            //x = foreach( a = (1*10^0) + b =(1*10^1) + c= (1*10^2))
            //x=123, but this might work out fine
            foreach (char c in password)
            {
                int val = (int)char.GetNumericValue(c);

                //TODO: this logic is not exactly what I wanted, may need to re-evaluate
                returnKey += val * (int)Math.Pow(10, x);

                x++;
            }

            return returnKey;
        }

        /// <summary>
        /// Ascii byte array - unused
        /// </summary>
        /// <param name="textToAscii"></param>
        /// <returns></returns>
        public static string AsciiEncoder(string textToAscii)
        {
            string inAscii = "";
            byte[] asciiArray = Encoding.ASCII.GetBytes(textToAscii);

            foreach (byte b in asciiArray)
            {
                inAscii += Convert.ToChar(b);
            }

            return inAscii;
        }

        /// <summary>
        /// Get a valid int - unused
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int ToValidInt(int number)
        {

            int validnumber = number;

            if (validnumber < 0)
            {
                while (validnumber < 0)
                {
                    validnumber = validnumber + 255;
                }
            }

            // if (validnumber > 255)
            // {
            //  while (validnumber > 255)
            //{
            //    validnumber = validnumber - 255;
            // }
            //  }

            if (validnumber > 255 || validnumber < 0)
            {
                //MessageBox.Show("this is a bad number: " + validnumber);
            }
            return validnumber;

        }

        /// <summary>
        /// get revised key - unused
        /// </summary>
        /// <param name="currentKey"></param>
        /// <returns></returns>
        public static int GetRevisedKey(int currentKey)
        {
            int revisedKey = currentKey;
            /*
               int divBy = currentKey % 3;

               if (divBy < 2)
               {
                   //divBy = 10; 
                   return 255;
               }

               while (revisedKey > 255)
               {
                   revisedKey = revisedKey / divBy;
               }
               */

            //- ------------------------------

            double doubleKey = Convert.ToDouble(revisedKey);
            // double subBy = revisedKey * .255;

            // while (doubleKey > 255)
            //  {
            //       doubleKey = doubleKey - subBy;
            // }


            if (doubleKey < 0)
            {
                doubleKey += 255;
            }



            return Convert.ToInt32(doubleKey);
            //return 1;
        }


    }
}