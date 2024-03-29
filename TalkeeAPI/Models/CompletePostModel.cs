﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{

    public class CompletePostModel
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
    }
}
