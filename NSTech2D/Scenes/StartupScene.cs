using System;
using System.Collections.Generic;
using NSTech2D.Engine;
using NSTech2D.Engine.Collisions;
using NSTech2D.Engine.Tiled;
using NSTech2D.Engine.UI.Text;
using NSTech2D.RenderEngine;
using OpenTK;

namespace NSTech2D.Scenes
{
    class StartupScene : Scene
    {
        float fpsCounter;
        TextObject fpsText;

        private List<TileObj> tileObjs;

        public override void Start()
        {
            LoadTexture();

            FontManager.Init();
            Font stdFont = FontManager.AddFont("stdFont", "Assets/Fonts/textSheet.png", 15, 32, 20, 20);

            fpsText = new TextObject(new Vector2(0, 0));

            TmxReader reader = new TmxReader("Assets/Levels/map-8x8.tmx");
            TileSet ts = reader.TileSet;
            List<Layer> layers = reader.Layers;

            GraphicsManager.AddTexture(ts.ImgPath, ts.ImgPath);
            tileObjs = new List<TileObj>();

            foreach(Layer each in layers)
            {
                AddTilesFor(each, tileObjs);
            }

            base.Start();
        }

        private void AddTilesFor(Layer layer, List<TileObj> tileObjs)
        {
            DrawLayer engineLayer = DrawLayer.Playground;
            if (layer.Props.Has("drawLayer"))
            {
                string drawLayer = layer.Props.GetString("drawLayer");
                engineLayer = (DrawLayer)Enum.Parse(typeof(DrawLayer), drawLayer);
            }

            for (int i = 0; i < layer.Grid.Size(); i++)
            {
                TileInstance inst = layer.Grid.At(i);
                if (inst == null) continue;
                string texture = inst.Type.ImagePath;
                int tOffX = inst.Type.OffX;
                int tOffY = inst.Type.OffY;
                int width = inst.Type.Width;
                int height = inst.Type.Height;
                int posX = inst.PosX;
                int posY = inst.PosY;
                TileObj obj = new TileObj(texture, tOffX, tOffY, posX, posY, width, height);
                tileObjs.Add(obj);
                if(inst.Type.Props.Has("collidable") && inst.Type.Props.GetBool("collidable"))
                {
                    obj.RigidBody = new Rigidbody(obj);
                    obj.RigidBody.Collider = ColliderFactory.CreateBoxFor(obj);
                    obj.RigidBody.Type = RigidBodyType.TileObj;
                }
            }
        }

        protected virtual void LoadTexture()
        {

        }

        public override void Update()
        {
            if (fpsCounter <= 0)
            {
                fpsText.Text = $"FPS: {((int)(1f / Game.DeltaTime)).ToString()}";
                fpsCounter = 1;
            } else
            {
                fpsCounter -= Game.DeltaTime;
            }
            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
        }

        public override void Draw()
        {
            DrawManager.Draw();
        }

        public override void Input()
        {
            
        }

        public override Scene OnExit()
        {
            UpdateManager.ClearAll();
            DrawManager.ClearAll();
            PhysicsManager.ClearAll();
            GraphicsManager.ClearAll();
            FontManager.ClearAll();

            return base.OnExit();
        }
    }
}
