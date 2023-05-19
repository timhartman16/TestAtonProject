using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hash
{
    public class HashConverter
    {
        public static string ConvertPasswordToHash(string input)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashValue = SHA256.HashData(messageBytes);
            return Convert.ToHexString(hashValue);
        }
    }
}
