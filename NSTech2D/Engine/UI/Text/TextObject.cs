using OpenTK;
using System.Collections.Generic;

namespace NSTech2D.Engine.UI.Text
{
    class TextObject
    {
        protected List<TextChar> sprites;
        protected string text;
        protected bool isActive;
        protected Font font;
        protected float hSpace;

        public Vector2 Position;
    }
}
