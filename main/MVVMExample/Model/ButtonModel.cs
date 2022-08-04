using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMExample.Model
{
    public class ButtonModel
    {
        public int GridColumn { get; set; }
        public int GridRow { get; set; }
        public int GridColumnSpan { get; set; }
        public string Content { get; set; }
        public string BtnColor { get; set; }
        public string FontSize { get; set; }
    }
}
