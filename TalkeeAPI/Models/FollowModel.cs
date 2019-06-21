using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class FollowModel
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int FollowID { get; set; }
    }
}
