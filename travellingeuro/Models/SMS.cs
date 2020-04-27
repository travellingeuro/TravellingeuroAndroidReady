using System;
using System.Collections.Generic;
using System.Text;

namespace travellingeuro.Models
{
    public partial class SMS
    {
        public SMS()
        {

        }

        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
    }
}
