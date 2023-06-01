using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonPlaceholder.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }

        //Realationship
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
