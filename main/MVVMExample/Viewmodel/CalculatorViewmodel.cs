using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVMExample.Model;
using MVVMExample.View;

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
            Buttons.Add(new ButtonViewmodel { BtnColor="LightGray",GridColumn = 0, GridRow = 0, Content = "D", PressBtn = new RelayCommand(o => Clear()) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "LightGray", GridColumn = 1, GridRow = 0, Content = "C", PressBtn = new RelayCommand(o => Clear()) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "LightGray", GridColumn = 2, GridRow = 0, Content = "AC", PressBtn = new RelayCommand(o => Clear()) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "DarkOrange", GridColumn = 3, GridRow = 0, Content = "÷", PressBtn = new RelayCommand(o=> Print("/")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 1, Content = "7", PressBtn = new RelayCommand(o => Print("7")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 1, Content = "8", PressBtn = new RelayCommand(o => Print("8")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 1, Content = "9", PressBtn = new RelayCommand(o => Print("9")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "DarkOrange", GridColumn = 3, GridRow = 1, Content = "x", PressBtn = new RelayCommand(o => Print("*")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 2, Content = "4", PressBtn = new RelayCommand(o => Print("4")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 2, Content = "5", PressBtn = new RelayCommand(o => Print("5")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 2, Content = "6", PressBtn = new RelayCommand(o => Print("6")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "DarkOrange", GridColumn = 3, GridRow = 2, Content = "-", PressBtn = new RelayCommand(o => Print("-")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 3, Content = "1", PressBtn = new RelayCommand(o => Print("1")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 1, GridRow = 3, Content = "2", PressBtn = new RelayCommand(o => Print("2")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 3, Content = "3", PressBtn = new RelayCommand(o => Print("3")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "DarkOrange", GridColumn = 3, GridRow = 3, Content = "+", PressBtn = new RelayCommand(o => Print("+")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 0, GridRow = 4, Content = "0", GridColumnSpan = 2, PressBtn = new RelayCommand(o => Print("0")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "#FF4D4D4D", GridColumn = 2, GridRow = 4, Content = ".", PressBtn = new RelayCommand(o => Print(".")) });
            Buttons.Add(new ButtonViewmodel { BtnColor = "DarkOrange", GridColumn = 3, GridRow = 4,  Content = "=", PressBtn = new RelayCommand(o => Calculate()) });
        }

        private int sum = 0;  //store the result
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
            sum = 0;
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

        public String PostFix;
        void Calculate()
        {
            int top = 0;
            char[] stack = new char[5];
            foreach(char c in Content)
            {
                switch (c)
                {
                    default:
                        PostFix += c;
                        break;
                    case '+':
                    case '-':
                    case 'x':
                    case '÷':
                        while (priority(stack[top]) >= priority(c))
                        {
                            PostFix += stack[top--];
                        }
                        stack[++top] = c;
                        break;
                }
                while (top > 0)
                {
                    PostFix += stack[top--];
                }
                foreach(char s in PostFix)
                {
                    System.Console.WriteLine(s);
                }
            }
        }

        void Insert()
        {
            
        }
        void Query()
        {
           
        }
    }
}
