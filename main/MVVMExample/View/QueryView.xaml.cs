using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace MVVMExample.View
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class QueryView : Window
    {
        public QueryView()
        {
            InitializeComponent();
        }
        private void dataTable_Loaded(object sender, RoutedEventArgs e)
        {
            //setting mysql connect
            String connectionString = "dataSource=localhost;username=root;PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select * from calculate.data", connection);
            MySqlDataReader data = cmd.ExecuteReader();
            dataGrid.ItemsSource = data;
        }
    }
}
