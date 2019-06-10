using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class PostComentarioModel
    {
        [Key]
        public int PostID { get; set; }
        public int ComentarioID { get; set; }
    }
}
