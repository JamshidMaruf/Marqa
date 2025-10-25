namespace Marqa.Service.Services.Settings;

public interface IEncryptionService
{
    string Encrypt(string plainText);
    string Decrypt(string cipherText);
}