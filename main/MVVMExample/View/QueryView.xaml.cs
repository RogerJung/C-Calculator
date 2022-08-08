using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridCell = System.Windows.Controls.DataGridCell;

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

        DataTable dt = new DataTable();
        ObservableCollection<char[]> showdata = new ObservableCollection<char[]>();

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

        public string selectedData = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String connectionString = "dataSource=localhost;username=root;PASSWORD=;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("Delete from calculate.data where Inorder=\'" + selectedData + "\'", connection);
            MySqlDataReader data = cmd.ExecuteReader();
            dataGrid.ItemsSource = data;
            dataTable_Loaded(sender, e);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cells = dataGrid.SelectedCells;
            StringBuilder sb = new StringBuilder();
            if (cells.Any())
            {
                foreach(var cell in cells)
                {
                    sb.Append((cell.Column.GetCellContent(cell.Item) as TextBlock).Text);
                    sb.Append(" ");
                }
                string[] words = sb.ToString().Split(' ');
                System.Console.WriteLine(words[1]);
                selectedData = words[1];
            }
            /*DataGrid dataGrid1 = sender
             * 
             * as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromIndex(dataGrid1.SelectedIndex);
            DataGridCell RowColumn = dataGrid1.Columns[1].GetCellContent(row).Parent as DataGridCell;
            string CellValue = ((TextBlock)RowColumn.Content).Text;
            System.Console.WriteLine(CellValue);
            selectedData = CellValue;*/
        }
    }
}
