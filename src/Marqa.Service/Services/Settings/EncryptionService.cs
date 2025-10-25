using System.Security.Cryptography;
using System.Text;
using Marqa.Service.Helpers;

namespace Marqa.Service.Services.Settings;

public class EncryptionService : IEncryptionService
{
    private readonly string _encryptionKey;
    private readonly byte[] _keyBytes;
    private readonly byte[] _ivBytes;

    public EncryptionService()
    {
        _encryptionKey = SettingConstant.EncryptionKey;
            
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(_encryptionKey));
            _keyBytes = hash.Take(32).ToArray();
            _ivBytes = hash.Take(16).ToArray();
        }
    }

    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        try
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _keyBytes;
                aes.IV = _ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occured in Encrypt: {ex.Message}", ex);
        }
    }

    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        try
        {
            using (var aes = Aes.Create())
            {
                aes.Key = _keyBytes;
                aes.IV = _ivBytes;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error occured in Decrypt: {ex.Message}", ex);
        }
    }
}