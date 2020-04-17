using NSTech2D.RenderEngine;

namespace NSTech2D.Engine.Tiled
{
    class TileObj : GameObject
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

            xOff = tOffX;
            yOff = tOffY;

            isActive = true;
        }

        public override void Draw()
        {
            if (isActive)
            {
                sprite.DrawTexture(texture, xOff, yOff, (int) width, (int) height);
            }
        }
    }
}
