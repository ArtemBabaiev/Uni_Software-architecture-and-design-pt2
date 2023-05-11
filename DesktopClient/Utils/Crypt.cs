using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Utils
{
    internal class Crypt
    {
        public static string Ecnrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromHexString(ConfigurationManager.AppSettings.Get("CryptKey"));
                aes.IV = Convert.FromHexString(ConfigurationManager.AppSettings.Get("CryptIV"));
                byte[] encrypted;

                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                    return Convert.ToHexString(encrypted);
                }
            }
        }

        public static string Decrypt(string hexCipher)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromHexString(ConfigurationManager.AppSettings.Get("CryptKey"));
                aes.IV = Convert.FromHexString(ConfigurationManager.AppSettings.Get("CryptIV"));
                byte[] decrypted;


                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromHexString(hexCipher)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decrypted = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }
                return Encoding.UTF8.GetString(decrypted);
            }
        }
    }
}
