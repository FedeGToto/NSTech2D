using System;
using NSTech2D.Engine;
using NSTech2D.Engine.UI.Text;
using NSTech2D.RenderEngine;
using OpenTK;

namespace NSTech2D.Scenes
{
    class StartupScene : Scene
    {
        float fpsCounter;
        TextObject fpsText;

        public override void Start()
        {
            LoadTexture();

            FontManager.Init();
            Font stdFont = FontManager.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);

            fpsText = new TextObject(new Vector2(0, 0));

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
