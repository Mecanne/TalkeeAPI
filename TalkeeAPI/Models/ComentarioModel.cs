using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TalkeeAPI.Models
{
    public class ComentarioModel
    {
        [Key]
        public int ComentarioID { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
        public string Comentario { get; set; }
    }
}
