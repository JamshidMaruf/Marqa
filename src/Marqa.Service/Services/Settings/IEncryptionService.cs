using Marqa.Service.Services;

namespace Marqa.Service.Services.Settings;

public interface IEncryptionService : IScopedService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}