﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
    }
}
