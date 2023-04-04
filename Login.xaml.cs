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
using System.Windows.Controls;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;

namespace stock_manager_system
{
    /// <summary>
    /// Interaction logic for 
    /// .xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Show();

        }
        //Password protection
        static string SHA512Encrypt(string data)
        {
            using var sha512 = SHA512.Create();
            byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            data = sb.ToString();
            return data;
        }
        static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    stream.Close();
                }
            }
        }


        private void login_process(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string GetRichTextBlockContent(RichTextBox richTextBlock)
            {
                TextRange textRange = new TextRange(richTextBlock.Document.ContentStart, richTextBlock.Document.ContentEnd);
                return textRange.Text.Trim();
            }
            string username = GetRichTextBlockContent(Username);
            string password = new System.Net.NetworkCredential(string.Empty, Password.SecurePassword).Password;
            string hash = SHA512Encrypt(password);
            if (username == null)
            {
                MessageBox.Show("Account is empty, please type again!", "Account Manager");
            }
            if (password == null)
            {
                MessageBox.Show("Password is empty, please type again!", "Account Manager");
            }
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;
            bool CheckUser(string sqlusername, string sqlpassword)
            {
                bool result = false;
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM User WHERE Username=@username AND Password=@password", connection))
                    {
                        command.Parameters.AddWithValue("@username", sqlusername);
                        command.Parameters.AddWithValue("@password", sqlpassword);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            result = reader.HasRows;
                        }
                    }
                    connection.Close();
                }
                return result;
            }
            bool result = CheckUser(username, hash);
            if (result)
            {
                //MessageBox.Show("Haha");
                    MessageBox.Show("Login Suceessful", "SQL Login Manager");
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
                {
                    connection.Open();

                    // 1. 檢查 User 記錄是否存在
                    string selectSql = "SELECT * FROM Login WHERE User=@user";
                    using (SQLiteCommand selectCommand = new SQLiteCommand(selectSql, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@user", username);
                        using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 2. 如果 User 記錄已經存在，更新 LatestLoginTime
                                string updateSql = "UPDATE Login SET LatestLogin=@latestLogin WHERE User=@user";
                                using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@latestLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    updateCommand.Parameters.AddWithValue("@user", username);
                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // 3. 如果 User 記錄不存在，創建一個新的記錄
                                string insertSql = "INSERT INTO Login (FirstLogin, LatestLogin, User) VALUES (@firstLogin, @latestLogin, @user)";
                                using (SQLiteCommand insertCommand = new SQLiteCommand(insertSql, connection))
                                {
                                    DateTime now = DateTime.Now;
                                    insertCommand.Parameters.AddWithValue("@firstLogin", now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    insertCommand.Parameters.AddWithValue("@latestLogin", now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    insertCommand.Parameters.AddWithValue("@user", username);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    connection.Close();
                }
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Your Username or Password is wrong! Please re-try again! If you forgot the Username or Password, Please contect to Admin.", "SQL Login Manager");
            }
        }
        public class LoginDataAccess
        {
            private SQLiteConnection connection;

            public LoginDataAccess(string connectionString)
            {
                connection = new SQLiteConnection(connectionString);
            }
            public void LogLogin(string username)
            {
                string sql = "INSERT INTO Login (loginTime, loginDate, User) VALUES (@loginTime, @loginDate, @user)";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@loginTime", DateTime.Now.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@loginDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@user", username);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        private void ExitApp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Did you want to exit it? Those data isn't save it or maybe will be lost.", "Company management system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void register(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Close();
        }

        }
}
