using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMExample.Model;

namespace MVVMExample.Viewmodel
{
    public class ButtonViewmodel: ViewModelBase
    {
        private ButtonModel _buttonModel;
        public RelayCommand PressBtn { get; set; }
        public ButtonViewmodel()
        {
            _buttonModel = new ButtonModel();
            //pressBtn = new RelayCommand(o => PressBtn());
        }
        public int GridColumn 
        { 
            get { return _buttonModel.GridColumn; }
            set { _buttonModel.GridColumn = value; OnPropertyChanged(); }
        }
        public int GridRow
        {
            get { return _buttonModel.GridRow; }
            set { _buttonModel.GridRow = value; OnPropertyChanged(); }
        }
        public int GridColumnSpan
        {
            get { return _buttonModel.GridColumnSpan; }
            set { _buttonModel.GridColumnSpan = value; OnPropertyChanged(); }
        }
        public string Content
        {
            get { return _buttonModel.Content; }
            set { _buttonModel.Content = value; OnPropertyChanged(); }
        }

        public string BtnColor {
            get { return _buttonModel.BtnColor; }
            set { _buttonModel.BtnColor = value; OnPropertyChanged(); }
        }
    }
}
