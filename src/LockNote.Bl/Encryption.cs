using System.Security.Cryptography;
using System.Text;

namespace LockNote.Bl;

public static class Encryption
{
public static string Encrypt(string plaintext, string password)
    {
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
        
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        using (var passwordBytes = new Rfc2898DeriveBytes(password, salt, 100000))
        {
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = passwordBytes.GetBytes(32);
                encryptor.IV = passwordBytes.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    // Store salt at the beginning of the encrypted data
                    ms.Write(salt, 0, salt.Length);

                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                        cs.FlushFinalBlock(); // Ensure all bytes are written
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string encrypted, string password)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encrypted);

        // Extract salt from the encrypted data
        byte[] salt = new byte[16];
        Array.Copy(encryptedBytes, 0, salt, 0, 16);

        using (var passwordBytes = new Rfc2898DeriveBytes(password, salt, 100000))
        {
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = passwordBytes.GetBytes(32);
                encryptor.IV = passwordBytes.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedBytes, 16, encryptedBytes.Length - 16); // Skip salt
                        cs.FlushFinalBlock(); // Ensure proper decryption
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}

