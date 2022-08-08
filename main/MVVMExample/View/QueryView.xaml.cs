﻿using MySql.Data.MySqlClient;
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
        //setting mysql connect
        string connectionString = "Data Source=127.0.0.1;User ID=root;Password=;DataBase=calculate;Charset=utf8;";
        MySqlConnection con;
        MySqlDataAdapter adapter;
        DataSet ds;
        DataTable dt;

        public QueryView()
        {
            InitializeComponent();

            UpdateMySQLData();
        }

        private void UpdateMySQLData()
        {
            if (con == null)
            {
                con = new MySqlConnection(connectionString);
                con.Open();
            }
            if (adapter == null)
            {
                adapter = new MySqlDataAdapter("select * from data", con);
            }
            if (ds == null)
            {
                ds = new DataSet();
            }
            ds.Clear();
            adapter.Fill(ds, "data");
            if (dt == null)
            {
                dt = ds.Tables["data"];
            }
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;
            if (index == -1) return;

            dt.Rows[index].Delete();
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            adapter.Update(dt);
            dt.AcceptChanges();
        }
    }
}
