using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Diagnostics;
using System;
using System.Windows;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Media.Animation;

namespace stock_manager_system
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoadingSQL : Window
    {

        public LoadingSQL()
        {
            InitializeComponent();
            this.Show();
            msg_text.Text = "\n\n\n Loading configuration file.\nPlease wait!";
            
            if (File.Exists("./config.xml"))
            {
                sqldbload();
            }
            else
            {
                MessageBox.Show("Missing config.xml, please reinstall the app!", "XML Load Manager");
                Close();
            }
            
        }

        public void sqldbload()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;
            if (connectionString == null)
            {
            MessageBox.Show("config.xml failed to read the location of SQLite DB, please check the config.xml sql location is correct! \nError: ERR_Config_FailedToReadSQL_Location\nReason: Connection string is empty", "XML Load Manager", MessageBoxButton.OK, MessageBoxImage.Error);
             Close();
            }
            else if (!connectionString.Contains(".db"))
            {
           MessageBox.Show($"config.xml failed to read the location of SQLite DB, please check the config.xml sql location is correct! \nError: ERR_Config_FailedToReadSQL_Location\nReason: Connection string does not contain '.db'\n'{connectionString}'", "XML Load Manager", MessageBoxButton.OK, MessageBoxImage.Error);
           Close();
            }
            if (File.Exists(connectionStringNode.InnerText))
            {
                /* Login login = new Login();
                 login.Show();
                 Thread.Sleep(1000);
                 this.Close();
                */
                msg_text.Text = "\n\n\n Connecting to Auth Server.\nPlease wait!";
                serverauth();
            }

            else
            {
                MessageBox.Show($"Failed to Load or initial SQL Database! Please confirm your SQL Database file isn't locked or the location is correct!\nError: ERR_SQLDBMaster_FailToLoad \nFailed loading Database location on '{connectionString}'", "SQL Database Load Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void serverauth()
        {
                try
                {
                    string hostname = Dns.GetHostName();
                    string hashauth = MD5Encrypt(hostname);
                    string address = "http://connecttocmtserver.net/" + $"{hostname}/auth.md5.txt";
                    WebClient webClient = new WebClient();
                    string log = webClient.DownloadString(address);
                    if (log == hashauth)
                    {
                            Login login = new Login();
                            login.Show();
                            this.Close();
                    }
                    else
                    {
                        string message = $"Failure to pass the authentication process, the application will automatically close! You can send your error.log back to the developer to resolved the error.\n\nnErrorMessage: CMT_ERR_FailAuthProcess_Type1\n\nRefKey:{hashauth}";
                        MessageBox.Show(message, "Auth Handle Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                        string path = "Error.Log";

                        using (StreamWriter writer = new StreamWriter(path))
                        {
                            writer.Write($"{DateTime.Now}: " + message);
                        }
                    }
            }
            finally
                {
                }
            Close();
        }
        public static string MD5Encrypt(string s)
        {
            string hash = "CMTSAdminHash@ToolsMD5Auth";
            byte[] data = UTF8Encoding.UTF8.GetBytes(s);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Encrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    string outdata = Convert.ToBase64String(results, 0, results.Length);
                    return outdata.ToString();
                }
            }
        }
    }
}
