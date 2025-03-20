using System;
using System.Security.Cryptography;
using System.Text;

namespace uniManagementApp.Models;
public static class PasswordHasher
{
    private const int SaltSize = 16; // 128-bit
    private const int KeySize = 32;  // 256-bit
    private const int Iterations = 10000; // Number of hashing iterations

    public static string HashPassword(string password)
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = pbkdf2.GetBytes(KeySize);
                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(key)}";
            }
        }
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedKey = Convert.FromBase64String(parts[1]);

        using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] enteredKey = pbkdf2.GetBytes(KeySize);
            return CryptographicOperations.FixedTimeEquals(enteredKey, storedKey);
        }
    }
}
