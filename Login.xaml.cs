using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Diagnostics;
using System;
using System.Windows;
using System.Data.SqlClient;
using 
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

        public void sqlconnect()
        {

        }
        
        public void sqllogin()
        {


        }

        public void connecttoserver()
        {

        }
        //Password protection
        private void SHA512Encrypt(string data)
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
        //method: string message to using this void with
        //string message = "123";
        //SHA512Encrypt (message); //return result

        //isn't support the bool or float, the data need using data.ToString(); send.
        void MD5Decrypt(string password, string inputdata)
        {
            //Convert a string to byte array
            //  string inputdata;
            byte[] getdata = Convert.FromBase64String(inputdata);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));//Get hash key
                //Decrypt data by hash key
                /*
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(inputdata, 0, inputdata.Length); //?? idk what error on this :(
                    //    txtDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                    string outputdata = Convert.ToBase64String(results);
                }
                */
            }
        }
        //method: using this void with string password and message 
        //string message = "123";
        //string password = "123";
        //string result = MD5Decrypt (message, password).ToString();
        //MessageBox.Show (result);
        void MD5Encrypt(string password, string inputdata)
        {

        }
        //
    }
}
