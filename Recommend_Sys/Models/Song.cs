using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommend_Sys.Models
{
    public class Song
    {
        public string name { get; set; }
        public int id { get; set; }
        public string artist_name { get; set; }
        public string album_name { get; set; }
        public string url { get; set; }
    }
}
