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
                Login login = new Login();
                login.Show();
                Thread.Sleep(1000);
                this.Close();
            }

            else
            {
                MessageBox.Show($"Failed to Load or initial SQL Database! Please confirm your SQL Database file isn't locked or the location is correct!\nError: ERR_SQLDBMaster_FailToLoad \nFailed loading Database location on '{connectionString}'", "SQL Database Load Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
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
        //
    }
}
