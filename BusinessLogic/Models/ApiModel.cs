﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
   public class ApiModel
    {
        public string PageToken { get; set; }
        public string ChannelId { get; set; }
        public string SearchQuery { get; set; }
        public string OrderBy { get; set; }
    }
}
