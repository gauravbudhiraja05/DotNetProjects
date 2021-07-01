using System;
using System.IO;
using System.Security.Cryptography;

namespace HiveReport.WebAdmin.Helper
{
    public class Crypto
    {
        public static string Password { get; }

        //Encrypt a string into a string using a password 
        //Uses Encrypt(byte[], byte[], byte[]) 
        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password,
                new byte[] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });

            //Now get the key/IV and do the encryption using the function that accepts byte arrays. 
            //Using PasswordDeriveBytes object we are first getting 32 bytes for the Key 
            //(the default Rijndael key length is 256bit = 32bytes) and then 16 bytes for the IV. 
            //IV should always be the block size, which is by default 16 bytes (128 bit) for Rijndael. 
            //If you are using DES/TripleDES/RC2 the block size is 8 bytes and so should be the IV size. 
            //You can also read KeySize/BlockSize properties off the algorithm to find out the sizes. 

            byte[] encryptedData = Encrypt(clearBytes, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));


            return Convert.ToBase64String(encryptedData);
        }

        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            //Create a MemoryStream that is going to accept the encrypted bytes 
            MemoryStream memoryStream = new MemoryStream();

            //Create a symmetric algorithm. 
            //We are going to use Rijndael because it is strong and available on all platforms. 
            //You can use other algorithms, to do so substitute the next line with something like 
            //TripleDES alg = TripleDES.Create(); 

            Rijndael alg = Rijndael.Create();

            //Now set the key and the IV. 
            //We need the IV (Initialization Vector) because the algorithm is operating in its default 
            //mode called CBC (Cipher Block Chaining). The IV is XORed with the first block (8 byte) 
            //of the data before it is encrypted, and then each encrypted block is XORed with the 
            //following block of plaintext. This is done to make encryption more secure. 
            //There is also a mode called ECB which does not need an IV, but it is much less secure.

            alg.Key = Key;
            alg.IV = IV;

            //Create a CryptoStream through which we are going to be pumping our data. 
            //CryptoStreamMode.Write means that we are going to be writing data to the stream 
            //and the output will be written in the MemoryStream we have provided. 

            CryptoStream cryptoStream = new CryptoStream(memoryStream, alg.CreateEncryptor(), CryptoStreamMode.Write);

            //Write the data and make it do the encryption 

            cryptoStream.Write(clearData, 0, clearData.Length);

            //Close the crypto stream (or do FlushFinalBlock). 
            //This will tell it that we have done our encryption and there is no more data coming in, 
            //and it is now a good time to apply the padding and finalize the encryption process. 

            cryptoStream.Close();

            //Now get the encrypted data from the MemoryStream. 
            //Some people make a mistake of using GetBuffer() here, which is not the right way. 

            byte[] encryptedData = memoryStream.ToArray();

            return encryptedData;
        }
    }
}
