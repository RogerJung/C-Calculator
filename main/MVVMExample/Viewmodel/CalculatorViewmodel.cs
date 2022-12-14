using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVMExample.Model;
using MVVMExample.View;
using MySql.Data.MySqlClient;

namespace MVVMExample.Viewmodel
{
    class CalculatorViewmodel:ViewModelBase
    {
        public RelayCommand InsertCmd { get; set; }
        public RelayCommand QueryCmd { get; set; }
        private CalculatorModel _calculatorModel;
        public CalculatorViewmodel()
        {
            _calculatorModel = new CalculatorModel();
            InsertCmd = new RelayCommand(o => Insert());
            QueryCmd = new RelayCommand(o => Query());
            Loadcalculator();
        }

        //setting mysql connect
        static string connectionString = "dataSource=localhost;username=root;PASSWORD=;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        public string Content
        {
            get { return _calculatorModel.Content; }
            set { _calculatorModel.Content = value; OnPropertyChanged(); }
        }
        public string Preorder
        {
            get { return _calculatorModel.Preorder; }
            set { _calculatorModel.Preorder = value; OnPropertyChanged(); }
        }
        public string Postorder
        {
            get { return _calculatorModel.Postorder; }
            set { _calculatorModel.Postorder = value; OnPropertyChanged(); }
        }
        public string Decimal
        {
            get { return _calculatorModel.Decimal; }
            set { _calculatorModel.Decimal = value; OnPropertyChanged(); }
        }
        public string Binary
        {
            get { return _calculatorModel.Binary; }
            set { _calculatorModel.Binary = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ButtonViewmodel> _buttons;
        public ObservableCollection<ButtonViewmodel> Buttons 
        { 
            get { return _buttons; } 
            set { _buttons = value; OnPropertyChanged(); }
        }

        public void Loadcalculator()
        {
            Buttons = new ObservableCollection<ButtonViewmodel>();
            Buttons.Add(new ButtonViewmodel { FontSize = "35",BtnColor="LightGray",GridColumn = 0, GridRow = 0, Content = "D", PressBtn = new RelayCommand(o => Delete()) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "LightGray", GridColumn = 1, GridRow = 0, Content = "C", PressBtn = new RelayCommand(o => Clear()) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "LightGray", GridColumn = 2, GridRow = 0, Content = "AC", PressBtn = new RelayCommand(o => Clear()) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "DarkOrange", GridColumn = 3, GridRow = 0, Content = "÷", PressBtn = new RelayCommand(o=> Print("÷")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 1, Content = "7", PressBtn = new RelayCommand(o => Print("7")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 1, Content = "8", PressBtn = new RelayCommand(o => Print("8")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 1, Content = "9", PressBtn = new RelayCommand(o => Print("9")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "DarkOrange", GridColumn = 3, GridRow = 1, Content = "x", PressBtn = new RelayCommand(o => Print("x")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 2, Content = "4", PressBtn = new RelayCommand(o => Print("4")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 2, Content = "5", PressBtn = new RelayCommand(o => Print("5")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 2, Content = "6", PressBtn = new RelayCommand(o => Print("6")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "DarkOrange", GridColumn = 3, GridRow = 2, Content = "-", PressBtn = new RelayCommand(o => Print("-")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 3, Content = "1", PressBtn = new RelayCommand(o => Print("1")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 3, Content = "2", PressBtn = new RelayCommand(o => Print("2")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 3, Content = "3", PressBtn = new RelayCommand(o => Print("3")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "DarkOrange", GridColumn = 3, GridRow = 3, Content = "+", PressBtn = new RelayCommand(o => Print("+")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "20", BtnColor = "#FFEB5B15", GridColumn = 0, GridRow = 4, Content = "Insert", PressBtn = new RelayCommand(o => Insert()) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 4, Content = "0",  PressBtn = new RelayCommand(o => Print("0")) });
            Buttons.Add(new ButtonViewmodel { FontSize = "20", BtnColor = "#FFEB5B15", GridColumn = 2, GridRow = 4, Content = "Query", PressBtn = new RelayCommand(o => Query()) });
            Buttons.Add(new ButtonViewmodel { FontSize = "35", BtnColor = "DarkOrange", GridColumn = 3, GridRow = 4,  Content = "=", PressBtn = new RelayCommand(o => Calculate()) });
        }

        int flag = 0;

        void Print(string str)
        {
            if (Content == "0")
                Content = str;
            else
                Content += str;
        }
        void Clear()
        {
            Content = "0";
            flag = 0;
            Postorder = null;
            Preorder = null;
            Decimal = null;
            Binary = null;
        }

        void Delete()
        {
            Content = Content.Substring(0, Content.Length - 1);
        }

        int priority(char op)
        {
            switch (op)
            {
                case '+': case '-': return 1;
                case 'x': case '÷': return 2;
                default: return 0;
            }
        }
        void inToPostOrder(String s)
        {
            int top = 0;
            char[] stack = new char[10];
            foreach (char c in s)  //inToPostFix
            {
                switch (c)
                {
                    default:
                        Postorder += c;
                        break;
                    case '+':
                    case '-':
                    case 'x':
                    case '÷':
                        while (priority(stack[top]) >= priority(c))
                        {
                            Postorder += stack[top--];
                        }
                        stack[++top] = c;
                        break;
                }
            }
            while (top > 0)
            {
                Postorder += stack[top--];
            }
            Value(Postorder);
        }


        void inToPreOrder(String s)
        {
            char[] chars = s.ToCharArray();
            String reverse = string.Empty;
            int top = 0;
            char[] stack = new char[10];
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                reverse += chars[i];
            }
            foreach (char c in reverse)  //inToPrefix
            {
                switch (c)
                {
                    default:
                        Preorder += c;
                        break;
                    case '+':
                    case '-':
                    case 'x':
                    case '÷':
                        while (priority(stack[top]) >= priority(c))
                        {
                            Preorder += stack[top--];
                        }
                        stack[++top] = c;
                        break;
                }
            }
            while (top > 0)
            {
                Preorder += stack[top--];
            }
            char[] temp = Preorder.ToCharArray();
            Preorder = String.Empty;
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                Preorder += temp[i];
            }
        }

        void Value(String s)
        {
            int top  = 0;
            int[] stack = new int[10];
            foreach (char c in s)
            {
                switch (c)
                {
                    default:
                        stack[top] = c - '0';
                        top++;
                        break;
                    case '+':
                        top--;
                        stack[top - 1] = stack[top-1] + stack[top];
                        break;
                    case '-':
                        top--;
                        stack[top - 1] = stack[top-1] - stack[top];
                        break;
                    case 'x':
                        top--;
                        stack[top - 1] = stack[top-1] * stack[top];
                        break;
                    case '÷':
                        top--;
                        stack[top - 1] = stack[top-1] / stack[top];
                        break;
                }
            }
            Decimal = stack[top - 1].ToString();
            ToBinary(stack[top - 1]);
        }

        void ToBinary(int sum)
        {
            int i = 0;
            char[] chars = new char[15];
            while (sum >= 1)
            {
                if (sum % 2 == 0)
                    chars[i] = '0';
                else
                    chars[i] = '1';
                sum /= 2;
                i++;
            }
            while(i > 0)
            {
                Binary += chars[i];
                i--;
            }
        }

        void Calculate()
        {
            if (flag == 0)
            {
                inToPostOrder(Content);
                inToPreOrder(Content);
            }
            flag = 1;
        }

        void Insert()
        {
            connection.Open();
            MySqlCommand cmd1 = new MySqlCommand("select * from calculate.data where Inorder  = \'" + Content + "\'", connection);
            int numRow = Convert.ToInt32(cmd1.ExecuteScalar());
            if (numRow > 0)
            {
                MessageBox.Show("Database already have same data!", "Error");
            }
            else
            {
                MySqlCommand cmd2 = new MySqlCommand("Insert Into calculate.data (Inorder,Preorder,Postorder,Deci,Bin) values (\'" + Content + "\',\'" + Preorder + "\',\'" + Postorder + "\',\'" + Decimal + "\',\'" + Binary + "\')", connection);
                int index = cmd2.ExecuteNonQuery();
                MessageBox.Show("Insert sucessfully!","Message");
            }
            connection.Close();
        }

        void Query()
        {
            Window window = new QueryView();
            window.Show();
        }
    }
}
