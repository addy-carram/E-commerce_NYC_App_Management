using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace e_commerce_NYC
{
    public class HashHelper
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combined = new byte[passwordBytes.Length + salt.Length];

                Buffer.BlockCopy(passwordBytes, 0, combined, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, combined, passwordBytes.Length, salt.Length);

                byte[] hash = sha.ComputeHash(combined);
                return Convert.ToBase64String(hash);
            }
        }
    }
    }
