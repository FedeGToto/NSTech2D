using System;
using OpenTK;
using NSTech2D.RenderEngine;

namespace NSTech2D.Engine
{
    class GameObject : IUpdatable, IDrawable
    {
        protected Sprite sprite;
        protected Texture texture;
        protected int textureOffsetX;
        protected int textureOffsetY;
        protected DrawLayer layer;

        //Rigidbody

        public virtual Vector2 position
        {
            get { return sprite.position; }
            set
            { sprite.position = value; }
        }

        public bool isActive;

        public float width { get { return sprite.Width; } }
        public float height { get { return sprite.Height} }

        public int x { get { return (int)sprite.position.X; } set { sprite.position.X = value; } }
        public int y { get { return (int)sprite.position.Y; } set { sprite.position.Y = value; } }

        public Vector2 Forward
        {
            get { return new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)); }
            set { sprite.Rotation = (float)Math.Atan2(value.Y, value.X); }
        }

        public DrawLayer Layer { get { return layer; } }

        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, float width = 0, float height = 0)
        {
            texture = GraphicsManager.GetTexture(textureName);
            sprite = new Sprite(width > 0 ? width : Game.PixelsToUnits(texture.Width), height > 0 ? height : Game.PixelsToUnits(texture.Height));
            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);
            this.layer = layer;

            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            if (isActive)
            {
                sprite.DrawTexture(texture, textureOffsetX, textureOffsetY, (int)(sprite.Width * Game.OptimalUnitSize), (int)(sprite.Height * Game.OptimalUnitSize));
            }
        }

        //OnCollide

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;
            
            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);

            //Remove rigidbody, if any
        }
    }
}
