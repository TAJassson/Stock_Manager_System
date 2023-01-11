using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Diagnostics;
using System;
using System.Windows;
using System.Text;

namespace stock_manager_system
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }


        // Password Protection
        //SHA512
        private void SHAEncrypt(string data)
        {
            using var sha512 = SHA512.Create();
            byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            data = sb.ToString();
        }

        //MD5, String Password and inputdata
        private void btnMD5Decrypt_Click(string password, string inputdata)
        {
            //Convert a string to byte array
            //  string inputdata;
            byte[] getdata = Convert.FromBase64String(inputdata);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));//Get hash key
                //Decrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(inputdata, 0, inputdata.Length);
                    //    txtDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                    string outputdata = Convert.ToBase64String(results);
                }
            }
        }
        // Password Protection
    }
}
