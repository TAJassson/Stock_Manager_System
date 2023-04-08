using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace stock_manager_system
{
    /// <summary>
    /// MainPages.xaml 的互動邏輯
    /// </summary>
    public partial class MainPages : Window
    {
        public MainPages()
        {
            InitializeComponent();
            searchbox.Text = "Type here to search!";
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hi");
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Did you want to exit it? Those data isn't save it or maybe will be lost.", "Company management system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private async void ShowItemSQL(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.Visibility == Visibility.Collapsed)
            {
                dataGrid.Visibility = Visibility.Visible;
            }
            else
            {
                dataGrid.Visibility = Visibility.Collapsed;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM item", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }

        private async void ModifyData(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                foreach (DataRowView row in dataGrid.Items)
                {
                    int id = Convert.ToInt32(row["id"]);
                    string column1 = row["column1"].ToString();
                    string column2 = row["column2"].ToString();

                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE item SET column1 = @column1, column2 = @column2 WHERE id = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@column1", column1);
                        cmd.Parameters.AddWithValue("@column2", column2);
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"{rowsAffected} rows affected.");
                    }
                }

                con.Close();
            }

            ShowItemSQL(null, null); //重新加載數據
        }

        private async void insertSQL_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void cleartext(object sender, RoutedEventArgs e)
        {
            if (searchbox.Text == "Type here to search!")
            {
                searchbox.Text = "";
            }
        }

        private void df_text(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchbox.Text))
            {
                searchbox.Text = "Type here to search!";
            }
        }

    }
}
