
using System;

namespace PickfordsIntranet.WebAdmin.Utility
{
    /// <summary>
    /// RandomPassword is an utility claas to generate the random password
    /// </summary>
    public static class RandomPassword
    {
        public static string GeneratePassword(int passwordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[passwordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}
