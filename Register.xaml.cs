using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace stock_manager_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
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
        string GetRichTextBlockContent(RichTextBox richTextBlock)
        {
            TextRange textRange = new TextRange(richTextBlock.Document.ContentStart, richTextBlock.Document.ContentEnd);
            return textRange.Text.Trim();
        }

        private void RegisterDown(object sender, MouseButtonEventArgs e)
        {
            string mail = GetRichTextBlockContent(Email);
            string Dep = GetRichTextBlockContent(Department);
            string username = GetRichTextBlockContent(Username);
            string password = new System.Net.NetworkCredential(string.Empty, Password.SecurePassword).Password;
            string hash = SHA512Encrypt(password);
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Register WHERE Username=@user AND Email=@email";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectSql, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user", username);
                    selectCommand.Parameters.AddWithValue("@email", mail);
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("This Email or Username is already registered! Please use another Username and Email to register the system!");
                        }
                        else
                        {
                            // 3. 如果 User 記錄不存在，創建一個新的記錄
                            string insertSqlRegister = "INSERT INTO Register (Username, Password, Email, Department) VALUES (@user, @password, @email, @department)";
                            using (SQLiteCommand insertCommand = new SQLiteCommand(insertSqlRegister, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@user", username);
                                insertCommand.Parameters.AddWithValue("@password", hash);
                                insertCommand.Parameters.AddWithValue("@email", mail);
                                insertCommand.Parameters.AddWithValue("@department", Dep);
                                insertCommand.ExecuteNonQuery();
                            }

                            string selectSqlUID = "SELECT UID FROM User ORDER BY UID DESC LIMIT 1";
                            using (SQLiteCommand selectCommandUID = new SQLiteCommand(selectSqlUID, connection))
                            {
                                object result = selectCommandUID.ExecuteScalar();
                                int uid = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                                uid++;

                                string insertSqlUser = "INSERT INTO User (Username, Password, UID) VALUES (@user, @password, @uid)";
                                using (SQLiteCommand insertCommand = new SQLiteCommand(insertSqlUser, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@user", username);
                                    insertCommand.Parameters.AddWithValue("@password", hash);
                                    insertCommand.Parameters.AddWithValue("@uid", uid);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            string selectSqlRightUID = "SELECT UID FROM Right ORDER BY UID DESC LIMIT 1";
                            using (SQLiteCommand selectCommandRrightUID = new SQLiteCommand(selectSqlRightUID, connection))
                            {
                                object result = selectCommandRrightUID.ExecuteScalar();
                                int uid = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                                uid++;

                                string insertSqlRightUser = "INSERT INTO Right (UID, Right) VALUES (@uid, @right)";
                                using (SQLiteCommand insertCommand = new SQLiteCommand(insertSqlRightUser, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@uid", uid);
                                    insertCommand.Parameters.AddWithValue("@right", "1");
                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                            connection.Close();
                            Login login = new Login();
                            login.Show();
                            this.Close();
                        }
                    }
                }
            }

        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Did you want to exit it? Those data isn't save it or maybe will be lost.", "Company management system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
