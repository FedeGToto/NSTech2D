using NSTech2D.RenderEngine;
using OpenTK;

namespace NSTech2D.Engine
{
    class TileObj: GameObject
    {
        private int xOff;
        private int yOff;

        public TileObj(string textureName, 
            int tOffX, int tOffY,
            int posX, int posY,
            int width, int height) : base(textureName)
        {
            sprite = new Sprite(width, height);
            sprite.position.X = posX;
            sprite.position.Y = posY;
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            xOff = tOffX;
            yOff = tOffY;

            isActive = true;
        }

        public override void Draw()
        {
           if (isActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, (int)width, (int)height);
            }
        }
    }
}
