using NSTech2D.RenderEngine;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.UI
{
    class ProgressBar : GameObject
    {
        protected Sprite barSprite;
        protected Texture barTexture;

        protected int barWidth;

        protected Vector2 barOffset;
        public override Vector2 position { get { return sprite.position; } set { sprite.position = value; barSprite.position = value + barOffset; } }
        public ProgressBar(string textureName, string barName, Vector2 offset) : base(textureName, DrawLayer.GUI)
        {
            sprite.pivot = Vector2.Zero;
            //sprite.Camera = CameraMgr.GetCamera("GUI");
            isActive = true;

            barOffset = offset;

            barTexture = GraphicsManager.GetTexture(barName);
            barSprite = new Sprite(Game.PixelsToUnits(barTexture.Width), Game.PixelsToUnits(barTexture.Height));
            //barSprite.Camera = sprite.Camera;
            barWidth = (int)barTexture.Width;
        }

        public virtual void Scale(float scale)
        {
            if (scale < 0)
            {
                scale = 0;
            }
            barSprite.scale.X = scale;
            barWidth = (int)(barTexture.Width * scale);

            barSprite.SetMultiplyTint((1 - scale) * 50, scale * 2, scale, 1);
        }

        public override void Draw()
        {
            if (isActive)
            {
                base.Draw();
                barSprite.DrawTexture(barTexture, 0, 0, barWidth, barTexture.Height);
            }
        }
    }
}
