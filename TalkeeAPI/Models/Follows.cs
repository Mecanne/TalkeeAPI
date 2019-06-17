using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class Follows
    {
        [Key]
        public int FollowID { get; set; }
        public int UserID { get; set; }
        public int FollowedID { get; set; }
    }
}
