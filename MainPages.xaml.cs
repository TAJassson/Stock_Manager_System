using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
            string[] ver = File.ReadAllLines("./Local\\version.hash");
            foreach (string clientv in ver)
            {
                vermsg.Text = clientv;
            }
            // searchbox.Text = "Type here to search!";
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Hi");
        }
        //ExitApp
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Did you want to exit it? Those data isn't save it or maybe will be lost.", "Company management system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        //ExitApp

        private async void ShowItemSQL(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid_Item.Visibility == Visibility.Collapsed)
            {
                dataGrid_Item.Visibility = Visibility.Visible;
                ReloadItemSQL.Visibility = Visibility.Visible;
                SaveSQL_Item.Visibility = Visibility.Visible;
                //
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;
                dataGrid_Order.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;
                ReloadstockSQL.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                //
            }
            else
            {
                dataGrid_Order.Visibility = Visibility.Collapsed;
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
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
                        dataGrid_Item.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }

        private async void ShowStockSQL(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid_Stock.Visibility == Visibility.Collapsed)
            {
                dataGrid_Stock.Visibility = Visibility.Visible;
                SaveSQL_stock.Visibility = Visibility.Visible;
                ReloadstockSQL.Visibility = Visibility.Visible;
                //
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Order.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;
                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                //
            }
            else
            {
                dataGrid_Order.Visibility = Visibility.Collapsed;
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Stock", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid_Stock.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }

        private async void ShowOrderSQL(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid_Order.Visibility == Visibility.Collapsed)
            {
                dataGrid_Order.Visibility = Visibility.Visible;
                ReloadpunchesSQL.Visibility = Visibility.Visible;
                SaveSQL_punches.Visibility = Visibility.Visible;
                //
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                //
            }
            else
            {
                dataGrid_Order.Visibility = Visibility.Collapsed;
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Punches", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid_Order.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }

        private async void ShowJobSQL(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid_Job.Visibility == Visibility.Collapsed)
            {
                dataGrid_Job.Visibility = Visibility.Visible;
                ReloadjobSQL.Visibility = Visibility.Visible;
                SaveSQL_job.Visibility = Visibility.Visible;
                //
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Order.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
                //
            }
            else
            {
                dataGrid_Order.Visibility = Visibility.Collapsed;
                dataGrid_Item.Visibility = Visibility.Collapsed;
                dataGrid_Stock.Visibility = Visibility.Collapsed;
                dataGrid_Job.Visibility = Visibility.Collapsed;

                ReloadItemSQL.Visibility = Visibility.Collapsed;
                SaveSQL_Item.Visibility = Visibility.Collapsed;

                ReloadstockSQL.Visibility = Visibility.Collapsed;
                SaveSQL_stock.Visibility = Visibility.Collapsed;

                ReloadjobSQL.Visibility = Visibility.Collapsed;
                SaveSQL_job.Visibility = Visibility.Collapsed;
                ReloadpunchesSQL.Visibility = Visibility.Collapsed;
                SaveSQL_punches.Visibility = Visibility.Collapsed;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Job", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid_Job.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }

        //Not in use
        private async void insertSQL_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void cleartext_item(object sender, RoutedEventArgs e)
        {
            /* if (searchbox.Text == "Type here to search!")
             {
                 searchbox.Text = "";
             }
            */
        }
        private void df_text(object sender, RoutedEventArgs e)
        {/*
            if (string.IsNullOrWhiteSpace(searchbox.Text))
            {
                searchbox.Text = "Type here to search!";
            }
            */
        }
        //Not In use

        //StartPDF
        private void Howtouse(object sender, MouseButtonEventArgs e)
        {
            string filepatch = "\\Local\\Files\\Tutorial.pdf";
            if (File.Exists(filepatch))
            {
                Process.Start(filepatch);
            }
            else
            {
                MessageBox.Show("");
            }
        }
        //StartPDF

        //Not Yet Develop function, pupup message
        private void Admin(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Function not yet develop or your user isn't have right to open admin control! Please contect to developer and your line manager to register your account to admin write.", "Company management system", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        //Not Yet Develop function

        //ReloadSQLButton
        private async void ReloadstockSQLList(object sender, MouseButtonEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Stock", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid_Stock.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }
        private async void ReloadItemSQLList(object sender, MouseButtonEventArgs e)
        {
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
                        dataGrid_Item.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }
        private async void ReloadJobSQLList(object sender, MouseButtonEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Job", con))
                {
                    using (SQLiteDataReader reader = (SQLiteDataReader)await cmd.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        reader.Close();
                        dataGrid_Job.ItemsSource = dataTable.DefaultView;
                    }
                }
                con.Close();
            }
        }
        //ReloadSQLButton
        //SaveButton
        private async void ModifyPunchesSQLData(object sender, MouseButtonEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            dataGrid_Order.CanUserSortColumns = false;  // 暫停排序功能

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                foreach (DataRowView row in dataGrid_Order.ItemsSource)
                {
                    string ItemSN = row["ItemSN"].ToString();

                    // 檢查資料庫中是否已存在相同的 ItenSN
                    using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT * FROM item WHERE Punches = @ItemSN", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                        using (SQLiteDataReader reader = await checkCmd.ExecuteReaderAsync() as SQLiteDataReader)
                        {
                            if (reader.Read())
                            {
                                // 資料庫中已經存在相同的 ItenSN，檢查是否需要更新
                                string ItemName = row["ItemName"].ToString();
                                string ItemType = row["ItemType"].ToString();
                                string ItemModel = row["ItemModel"].ToString();
                                string ItemCount = row["ItemCount"].ToString();
                                string PunchesOrderNumber = row["PunchesOrderNumber"].ToString();
                                string OrderStatus = row["OrderStatus"].ToString();

                                bool shouldUpdate = false;

                                if (!reader["ItemName"].ToString().Equals(ItemName)
                                    || !reader["ItemType"].ToString().Equals(ItemType)
                                    || !reader["ItemModel"].ToString().Equals(ItemModel)
                                    || !reader["ItemCount"].ToString().Equals(ItemCount)
                                    || !reader["PunchesOrderNumber"].ToString().Equals(PunchesOrderNumber)
                                    || !reader["OrderStatus"].ToString().Equals(OrderStatus)
                                    || !reader["ItemSN"].ToString().Equals(OrderStatus))
                                {
                                    shouldUpdate = true;
                                }

                                if (shouldUpdate)
                                {
                                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Punches SET ItemName = @ItemName, ItemType = @ItemType, ItemModel = @ItemModel, ItemCount = @ItemCount, PunchesOrderNumber = @PunchesOrderNumber , OrderStatus = @OrderStatus ,WHERE ItemSN = @ItemSN", con))
                                    {
                                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                                        cmd.Parameters.AddWithValue("@ItemType", ItemType);
                                        cmd.Parameters.AddWithValue("@ItemModel", ItemModel);
                                        cmd.Parameters.AddWithValue("@ItemCount", ItemCount);
                                        cmd.Parameters.AddWithValue("@PunchesOrderNumber", PunchesOrderNumber);
                                        cmd.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                                        cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                            else
                            {
                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO item (ItemName, ItemType, ItemModel, ItemStatus, ItemSN, Client) VALUES (@ItemName, @ItemType, @ItemModel, @ItemStatus, @ItemSN, @Client)", con))
                                {
                                    cmd.Parameters.AddWithValue("@ItemName", row["ItemName"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemType", row["ItemType"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemModel", row["ItemModel"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemCount", row["ItemCount"].ToString());
                                    cmd.Parameters.AddWithValue("@PunchesOrderNumber", row["PunchesOrderNumber"].ToString());
                                    cmd.Parameters.AddWithValue("@OrderStatus", row["OrderStatus"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }

                con.Close();
            }

            dataGrid_Order.CanUserSortColumns = true;
            ShowOrderSQL(null, null);
            dataGrid_Order.Visibility = Visibility.Visible;
        }

        private async void ModifyStockSQLData(object sender, MouseButtonEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            dataGrid_Stock.CanUserSortColumns = false;

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                foreach (DataRowView row in dataGrid_Stock.ItemsSource)
                {
                    string ItemSN = row["ItemSN"].ToString();

                    using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT * FROM Stock WHERE ItemSN = @ItemSN", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                        using (SQLiteDataReader reader = await checkCmd.ExecuteReaderAsync() as SQLiteDataReader)
                        {
                            if (reader.Read())
                            {
                                // 資料庫中已經存在相同的 ItenSN，檢查是否需要更新
                                string Client = row["Client"].ToString();
                                string ItemName = row["ItemName"].ToString();
                                string ItemModel = row["ItemModel"].ToString();
                                string ItemType = row["ItemType"].ToString();
                                string ItemStatus = row["ItemStatus"].ToString();
                                string PunchesOrderNumber = row["PunchesOrderNumber"].ToString();
                                bool shouldUpdate = false;
                                if (!reader["ItemName"].ToString().Equals(ItemName)
                                    || !reader["ItemType"].ToString().Equals(ItemType)
                                    || !reader["ItemModel"].ToString().Equals(ItemModel)
                                    || !reader["PunchesOrderNumber"].ToString().Equals(PunchesOrderNumber)
                                    || !reader["ItemSN"].ToString().Equals(ItemSN)
                                    || !reader["ItemStatus"].ToString().Equals(ItemStatus)
                                   || !reader["Client"].ToString().Equals(Client))
                                {
                                    shouldUpdate = true;
                                }

                                if (shouldUpdate)
                                {
                                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Stock SET ItemName = @ItemName, ItemType = @ItemType, ItemModel = @ItemModel, ItemCount = @ItemCount, PunchesOrderNumber = @PunchesOrderNumber , OrderStatus = @OrderStatus ,WHERE ItemSN = @ItemSN", con))
                                    {
                                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                                        cmd.Parameters.AddWithValue("@ItemType", ItemType);
                                        cmd.Parameters.AddWithValue("@ItemModel", ItemModel);
                                        cmd.Parameters.AddWithValue("@ItemStatus", ItemStatus);
                                        cmd.Parameters.AddWithValue("@PunchesOrderNumber", PunchesOrderNumber);
                                        cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                        cmd.Parameters.AddWithValue("@Client", Client);
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                            else
                            {
                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Stock (ItemName, ItemType, ItemModel, ItemStatus, ItemSN, Client, PunchesOrderNumber) VALUES (@ItemName, @ItemType, @ItemModel, @ItemStatus, @ItemSN, @Client, @PunchesOrderNumber)", con))
                                {
                                    cmd.Parameters.AddWithValue("@ItemName", row["ItemName"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemType", row["ItemType"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemModel", row["ItemModel"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemStatus", row["ItemStatus"].ToString());
                                    cmd.Parameters.AddWithValue("@PunchesOrderNumber", row["PunchesOrderNumber"].ToString());
                                    cmd.Parameters.AddWithValue("@Client", row["Client"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }

                con.Close();
            }

            dataGrid_Stock.CanUserSortColumns = true;  // 恢復排序功能
            ShowStockSQL(null, null); //重新加載數據
            dataGrid_Stock.Visibility = Visibility.Visible;
        }

        private async void ModifyItemSQLData(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            dataGrid_Item.CanUserSortColumns = false;  // 暫停排序功能

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                foreach (DataRowView row in dataGrid_Item.ItemsSource)
                {
                    string ItemSN = row["ItemSN"].ToString();

                    using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT * FROM item WHERE ItemSN = @ItemSN", con))
                    {
                        checkCmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                        using (SQLiteDataReader reader = await checkCmd.ExecuteReaderAsync() as SQLiteDataReader)
                        {
                            if (reader.Read())
                            {
                                string ItemName = row["ItemName"].ToString();
                                string ItemType = row["ItemType"].ToString();
                                string ItemModel = row["ItemModel"].ToString();
                                string ItemStatus = row["ItemStatus"].ToString();
                                string Client = row["Client"].ToString();

                                bool shouldUpdate = false;

                                if (!reader["ItemName"].ToString().Equals(ItemName)
                                    || !reader["ItemType"].ToString().Equals(ItemType)
                                    || !reader["ItemModel"].ToString().Equals(ItemModel)
                                    || !reader["ItemStatus"].ToString().Equals(ItemStatus)
                                    || !reader["Client"].ToString().Equals(Client))
                                {
                                    shouldUpdate = true;
                                }

                                if (shouldUpdate)
                                {
                                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE item SET ItemName = @ItemName, ItemType = @ItemType, ItemModel = @ItemModel, ItemStatus = @ItemStatus, Client = @Client WHERE ItemSN = @ItemSN", con))
                                    {
                                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                                        cmd.Parameters.AddWithValue("@ItemType", ItemType);
                                        cmd.Parameters.AddWithValue("@ItemModel", ItemModel);
                                        cmd.Parameters.AddWithValue("@ItemStatus", ItemStatus);
                                        cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                        cmd.Parameters.AddWithValue("@Client", Client);

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                            else
                            {
                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO item (ItemName, ItemType, ItemModel, ItemStatus, ItemSN, Client) VALUES (@ItemName, @ItemType, @ItemModel, @ItemStatus, @ItemSN, @Client)", con))
                                {
                                    cmd.Parameters.AddWithValue("@ItemName", row["ItemName"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemType", row["ItemType"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemModel", row["ItemModel"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemStatus", row["ItemStatus"].ToString());
                                    cmd.Parameters.AddWithValue("@ItemSN", ItemSN);
                                    cmd.Parameters.AddWithValue("@Client", row["Client"].ToString());

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }

                con.Close();
            }

            dataGrid_Item.CanUserSortColumns = true;  // 恢復排序功能
            ShowItemSQL(null, null); //重新加載數據
            dataGrid_Item.Visibility = Visibility.Visible;
        }

        private async void ModifyJobDBSQLData(object sender, MouseButtonEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNode connectionStringNode = doc.SelectSingleNode("//ClientSetting//Database/ConnectionString");
            string connectionString = connectionStringNode.InnerText;

            dataGrid_Job.CanUserSortColumns = false;  // 暫停排序功能

            using (SQLiteConnection con = new SQLiteConnection($"Data Source={connectionString};Version=3;"))
            {
                await con.OpenAsync();

                foreach (DataRowView row in dataGrid_Job.ItemsSource)
                {
                    string JobCode = row["JobCode"].ToString();

                    using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT * FROM Job WHERE JobCode = @JobCode", con))
                    {
                        checkCmd.Parameters.AddWithValue("@JobCode", JobCode);
                        using (SQLiteDataReader reader = await checkCmd.ExecuteReaderAsync() as SQLiteDataReader)
                        {
                            if (reader.Read())
                            {
                                string Engineer = row["Engineer"].ToString();
                                string Client = row["Client"].ToString();
                                string Comment = row["Comment"].ToString();
                                string JobStatus = row["JobStatus"].ToString();

                                bool shouldUpdate = false;

                                if (!reader["Engineer"].ToString().Equals(Engineer)
                                    || !reader["Client"].ToString().Equals(Client)
                                    || !reader["Comment"].ToString().Equals(Comment)
                                    || !reader["JobStatus"].ToString().Equals(JobStatus))
                                {
                                    shouldUpdate = true;
                                }

                                if (shouldUpdate)
                                {
                                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Job SET Engineer = @Engineer, Client = @Client, Comment = @Comment, JobStatus = @JobStatus, Client = @Client WHERE JobCode = @JobCode", con))
                                    {
                                        cmd.Parameters.AddWithValue("@Engineer", Engineer);
                                        cmd.Parameters.AddWithValue("@Client", Client);
                                        cmd.Parameters.AddWithValue("@Comment", Comment);
                                        cmd.Parameters.AddWithValue("@JobStatus", JobStatus);
                                        cmd.Parameters.AddWithValue("@JobCode", JobCode);
                                        cmd.Parameters.AddWithValue("@Client", Client);

                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                            else
                            {
                                using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Job (Engineer, Client, Comment, JobStatus, JobCode, Client) VALUES (@Engineer, @Client, @Comment, @JobStatus, @JobCode, @Client)", con))
                                {
                                    cmd.Parameters.AddWithValue("@Engineer", row["Engineer"].ToString());
                                    cmd.Parameters.AddWithValue("@Client", row["Client"].ToString());
                                    cmd.Parameters.AddWithValue("@Comment", row["Comment"].ToString());
                                    cmd.Parameters.AddWithValue("@JobStatus", row["JobStatus"].ToString());
                                    cmd.Parameters.AddWithValue("@JobCode", JobCode);
                                    cmd.Parameters.AddWithValue("@Client", row["Client"].ToString());

                                    await cmd.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                }

                con.Close();
            }

            dataGrid_Item.CanUserSortColumns = true;  // 恢復排序功能
            ShowItemSQL(null, null); //重新加載數據
            dataGrid_Item.Visibility = Visibility.Visible;
        }
    }
    //SaveButton
}
   
