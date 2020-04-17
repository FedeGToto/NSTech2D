using NSTech2D.RenderEngine;
using OpenTK;

namespace NSTech2D.Engine.UI.Text
{
    class Font
    {
        protected int numCol;
        protected int firstVal;

        public string textureName { get; protected set; }
        public Texture texture { get; protected set; }
        public int characterWidth { get; protected set; }
        public int characterHeight { get; protected set; }

        public Font(string textureName, string texturePath, int numColumns, int firstCharacterASCIIValue, int charWidth, int charHeight)
        {
            this.textureName = textureName;
            texture = GraphicsManager.AddTexture(this.textureName, texturePath);
            firstVal = firstCharacterASCIIValue;
            characterWidth = charWidth;
            characterHeight = charHeight;
            numCol = numColumns;
        }

        public virtual Vector2 GetOffset(char c)
        {
            int cVal = c;
            int delta = cVal - firstVal;
            int x = delta % numCol;
            int y = delta / numCol;

            return new Vector2(x * characterWidth, y * characterHeight);
        }
    }
}
