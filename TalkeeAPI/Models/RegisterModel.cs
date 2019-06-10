using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class RegisterModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
}
