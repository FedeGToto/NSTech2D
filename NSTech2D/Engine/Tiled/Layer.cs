using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.Tiled
{
    class Layer
    {
        public string Name { get; internal set; }
        public TileGrid Grid { get; internal set; }
        public TileProperties Props { get; internal set; }

        public Layer(string name, TileGrid tg)
        {
            Name = name;
            Grid = tg;
            Props = new TileProperties();
        }
    }
}
