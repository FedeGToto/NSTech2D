using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.Tiled
{
    class TileType
    {
        public int Id { get; }
        public string ImagePath { get; }
        public int Width { get; }
        public int Height { get; }
        public int OffX { get; }
        public int OffY { get; }

        public TileType(int id, string tsImgPath, int tileW, int tileH, int offX, int offY)
        {
            this.Id = id;
            this.ImagePath = tsImgPath;
            this.Width = tileW;
            this.Height = tileH;
            this.OffX = offX;
            this.OffY = offY;
        }

        public override string ToString()
        {
            return "id: " + Id + " (" + OffX + "," + OffY + ")";
        }
    }
}
