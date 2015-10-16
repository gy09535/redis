using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataConvertDLL;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var dtTable = new DataTable();
            dtTable.Columns.Add(new DataColumn("MyName"));
            dtTable.Columns.Add(new DataColumn("MyPassword"));
            var data = dtTable.NewRow();
            data["MyName"] = "GuYang";
            data["MyPassword"] = "123456789";
            dtTable.Rows.Add(data);
            ICollection<TestBo> list = new List<TestBo>();

            IConvertFactory factory = new DataAdapterFactory();

            factory.Fill(ref list, dtTable);

            foreach (TestBo obj in list)
            {
                Console.WriteLine(obj.Name);
                Console.WriteLine(obj.Password);
            }
            Console.Read();
        }
    }
}
