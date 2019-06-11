﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class Followers
    {
        [Key]
        public int UserID { get; set; }
        public int FollowerID { get; set; }
    }
}
