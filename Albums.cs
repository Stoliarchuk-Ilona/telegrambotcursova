using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Albums
    {
        public Topalbums Topalbums { get; set; }
    }
    public class Topalbums
    {
        public List<Album> Album { get; set; }
    }
    public class Album
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
