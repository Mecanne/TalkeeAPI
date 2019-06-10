using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class PostModel
    {
        [Key]
        public int PostID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int likes { get; set; }
    }
}
