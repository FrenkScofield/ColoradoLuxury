using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ColoradoLuxury.Core.Cryptography
{
    public static class Cryptography
    {
        private static byte[] EncryptionKey { get; set; }
        private static byte[] Global_IV { get; set; }

        public static void TripleDESImplementation(string encryptionKey, string IV)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentNullException("'encryptionKey' parameter cannot be null.", "encryptionKey");
            }
            if (string.IsNullOrEmpty(IV))
            {
                throw new ArgumentException("'IV' parameter cannot be null or empty.", "IV");
            }
            EncryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            // Ensures length of 24 for encryption key
            Trace.Assert(EncryptionKey.Length == 24, "Encryption key must be exactly 24 characters of ASCII text (24 bytes)");
            Global_IV = Encoding.UTF8.GetBytes(IV);
            // Ensures length of 8 for init. vector
            Trace.Assert(Global_IV.Length == 8, "Init. vector must be exactly 8 characters of ASCII text (8 bytes)");
        }

        /// Encrypts a text block
        public static string Encrypt(this string textToEncrypt)
        {
            try
            {
                TripleDESImplementation("fblsQBxfNs6nQ10wsRcMFwCN", "25ywte53");

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = EncryptionKey;
                tdes.IV = Global_IV;
                byte[] buffer = Encoding.UTF8.GetBytes(textToEncrypt); //Encoding.ASCII.GetBytes(textToEncrypt);
                return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                //DALC.ErrorLog(string.Format("Cryptography.Encrypt catch error: (Text: {0})", textToEncrypt, er.Message));
                return string.Empty;
            }
        }

        /// Decrypts an encrypted text block
        public static string Decrypt(this string textToDecrypt)
        {
            try
            {
                TripleDESImplementation("fblsQBxfNs6nQ10wsRcMFwCN", "25ywte53");
                byte[] buffer = Convert.FromBase64String(textToDecrypt);
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = EncryptionKey;
                des.IV = Global_IV;
                return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception ex)
            {
                //DALC.ErrorLog(string.Format("Cryptography.Encrypt catch error: (Text: {0})", textToDecrypt, er.Message));
                return string.Empty;
            }
        }

        //Şifrəmə sistemi (SHA1)
        public static string SHA1(this string Value)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(Value));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        //Sha1 - özəl
        public static string SHA1Special(this string Value)
        {
            SHA1 ShaEncrp = new SHA1CryptoServiceProvider();
            Value = string.Format("{0}{1}{0}", "CSAASADM", Value);
            byte[] buffer = Encoding.UTF8.GetBytes(Value);
            byte[] result = ShaEncrp.ComputeHash(buffer);
            return Convert.ToBase64String(result);
        }

        //public static bool HashValidator(string Input, string Hash)
        //{
        //    //Crypt validator
        //    return (Input + MySettings.API.PrivateKey.Decrypt()).SHA1().ToUpper() == Hash.ToUpper();
        //}
    }
}



