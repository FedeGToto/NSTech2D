using System;
using System.Collections.Generic;
using System.Xml;
using NSTech2D.Engine;
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
            Font stdFont = FontManager.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);

            fpsText = new TextObject(new Vector2(0, 0));

            //Load scene graphics
            TmxReader reader = new TmxReader("Assets/map-8x8.tmx");
            TileSet ts = reader.TileSet;
            TileGrid tg = reader.TileGrid;


            // Build TileObj list from Tiled Model
            GraphicsManager.AddTexture(ts.ImgPath, ts.ImgPath);
            tileObjs = new List<TileObj>();
            for (int i = 0; i < tg.Size(); i++)
            {
                TileInstance inst = tg.At(i);
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
            }

            base.Start();
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
