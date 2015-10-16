using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using DataConvertDLL;

namespace ConsoleApplication1
{
    [DataTableAdapter]
    public class TestBo
    {
        [TableColumnName("MyName")]
        public string Name { get; set; }

        [TableColumnName("MyPassword")]
        public String Password
        {
            get;
            set;
        }
    }
}
