using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace MessageContract
{
    [MessageContract]
    public class MessageSample
    {
        [MessageHeader]
        public string Header { get; set; }

        [MessageBodyMember]
        public string Text { get; set; }
    }
}
