using System;
using OpenTK;

namespace NSTech2D.Engine
{
    class GameObject : IUpdatable, IDrawable
    {
        //sprite
        //texture
        protected int textureOffsetX;
        protected int textureOffsetY;
        protected DrawLayer layer;

        //Rigidbody

        //public virtual Vector2 position;

        public bool isActive;

        public float width;
        public float height;

        public int x;
        public int y;

        public DrawLayer Layer { get { return layer; } }

        public GameObject(string textureName, DrawLayer layer = DrawLayer.Playground, float width = 0, float height = 0)
        {
            this.layer = layer;

            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);
        }

        public virtual void Update()
        {
            
        }

        public void Draw()
        {
            if (isActive)
            {
                //DrawSprite
            }
        }

        //OnCollide

        public virtual void Destroy()
        {
            //Remove sprite
            //Remove texture

            UpdateManager.RemoveItem(this);
            DrawManager.RemoveItem(this);

            //Remove rigidbody, if any
        }
    }
}
