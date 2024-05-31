using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeIt.Common.DTOs.CreateAPI
{
    public class CreateStaticRequest
    {
        public string URL { get; set; }
        public dynamic Response { get; set; }
    }
}
