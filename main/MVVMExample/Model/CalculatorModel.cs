using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMExample.Model
{
    public class CalculatorModel
    {
        public string Content { get; set; } = "0";
        public string Preorder { get; set; }
        public string Postorder { get; set; }
        public string Decimal { get; set; }
        public string Binary { get; set; }
    }
}
