using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AddToMyCart.ServiceLayer
{
    public static class SHA256HashGenerator
    {
        public static string GenerateHash(string inputData)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                StringBuilder builder = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    builder.Append(bt.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
