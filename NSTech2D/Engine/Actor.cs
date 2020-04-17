using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine
{
    class Actor : GameObject
    {
        public Actor(string textureName, DrawLayer layer = DrawLayer.Playground, float width = 0, float height = 0) : base(textureName, layer, width, height)
        {
        }
    }
}
