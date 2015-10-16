using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  System.Runtime.Serialization;

namespace DataContract
{
    [DataContract]
    public class UserBo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
