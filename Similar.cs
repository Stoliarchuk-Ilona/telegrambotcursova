using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot
{
    public class Similar
    {
        public Similarartists Similarartists { get; set; }
    }
    public class Similarartists
    {
        public List<ArtistSim> Artist { get; set; }
    }
    public class ArtistSim
    {
        public string Name { get; set; }
    }
}
