using System.Security.Cryptography;
using System.Text;
using TestLib.Abstractions;

namespace TestLib.Classes.Network
{
    public class Sha256Encryptor : IEncryptor
    {
        public string Encrypt(string data)
        {
            return Encoding.UTF8.GetString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
