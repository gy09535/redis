using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreSample.Models
{
    public class ManageNode
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public string Discription { get; set; }


        public List<ManageNode> ChildNodes { get; set; }
    }
}