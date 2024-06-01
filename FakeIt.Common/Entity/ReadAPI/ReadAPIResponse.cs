using FakeIt.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIt.Common.Entity.ReadAPI
{
    public class ReadAPIResponse : ResponseBase
    {
        public dynamic Response { get; set; }
    }
}
