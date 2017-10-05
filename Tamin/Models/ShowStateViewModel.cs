using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tamin.Models
{
    public class ShowStateViewModel
    {
        public int SeeToday { get; set; }

        public int SeeYesterday { get; set; }

        public int SeeSum { get; set; }

        public int OnlineUser { get; set; }
    }
}