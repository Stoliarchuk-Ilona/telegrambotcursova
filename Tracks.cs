using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Tracks
    {
        public Toptracks Toptracks { get; set; }
    }
    public class Toptracks
    {
        public List<Track> Track { get; set; }
    }
    public class Track
    {
        public string Name { get; set; }
    }
}
