using System.Configuration;
using System.Security.Cryptography;
namespace SmartAceModel.SecurityLayer
{
    public class SecurityLayer
    {
        public string SecuredConnection()
        {
            string server = GetConfigValue("Server");
            string database = GetConfigValue("Database");
            //  string uid = Decrypt(GetConfigValue("User"));
            // string password = Decrypt(GetConfigValue("Password"));

            // return "Data Source=" + server + "; Initial Catalog=" + database + "; UID=" +
            //  uid + "; Password=" + password + "; Integrated Security=false;";

            return "Data Source=" + server + "; Initial Catalog=" + database + "; Integrated Security=SSPI;";
        }

        

        string key = "12345";
        public string Encrypt(string value)
        {
            return new CrytoLibrary.SymmetricEncryption(CrytoLibrary.SymmetricEncryption.EncryptionType.DES)
                .Encrypt(value, key);
        }

        public string Decrypt(string value)
        {
            return new CrytoLibrary.SymmetricEncryption(CrytoLibrary.SymmetricEncryption.EncryptionType.DES)
                .Decrypt(value, key);
        }

        string GetConfigValue(string _key)
        {
            return ConfigurationManager.AppSettings[_key];
        }
    }
}

