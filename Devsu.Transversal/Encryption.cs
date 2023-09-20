using System;
using System.Text;
using System.Security.Cryptography;

namespace Devsu.Transversal
{
    public class Encryption
    {
        public string StringToSHA256(string t)
        {
            string hash = String.Empty;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(t));
                //Convertir HASH Bytes en string
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }
            return hash;
        }

    }
}
