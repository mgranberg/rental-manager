using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Helpers;
public static class EncryptionHelper
{
    private static readonly string _key
        = "E546C8DF278CD5931069B522E695D4F2"; // TODO store in a secure place

    public static string Encrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

       byte[] valueBytes = Encoding.UTF8.GetBytes(_key.PadRight(32));
        using Aes aes = Aes.Create();   
        aes.Key = valueBytes;
        aes.IV = new byte[16];

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] plainBytes = Encoding.UTF8.GetBytes(value);
        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        return Convert.ToBase64String(encryptedBytes);
    }

    public static string Decrypt(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        byte[] valueBytes = Encoding.UTF8.GetBytes(_key.PadRight(32));
        using Aes aes = Aes.Create();
        aes.Key = valueBytes;
        aes.IV = new byte[16];

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        byte[] encryptedBytes = Convert.FromBase64String(value);
        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}