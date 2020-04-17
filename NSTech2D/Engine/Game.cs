using NSTech2D.RenderEngine;
using NSTech2D.Scenes;
using System;

namespace NSTech2D.Engine
{
    static class Game
    {
        public static Window Win;
        public static bool isRunning;

        public static Scene currentScene { get; private set; }
        public static float DeltaTime { get { return Win.deltaTime; } }

        public static float UnitSize { get; private set; }
        public static float OptimalUnitSize {get; private set; }
        public static float OptimalScreenHeight { get; private set; }

        public static void Init()
        {
            //Init the game
            Win = new Window(1280, 720, "NS Tech 2D");
            Win.SetVSync(true);
            Win.SetDefaultOrthographicSize(10);
            OptimalScreenHeight = 1080;

            UnitSize = Win.Height / Win.OrthoHeight;
            OptimalUnitSize = OptimalScreenHeight / Win.OrthoHeight;

            //Setup scenes
            StartupScene startupScene = new StartupScene();
            startupScene.nextScene = null;
            currentScene = startupScene;
        }

        public static float PixelsToUnits(float pixelSize)
        {
            return pixelSize / OptimalUnitSize;
        }

        public static void Play()
        {
            currentScene.Start();
            isRunning = true;
            while (Win.IsOpened && isRunning)
            {

                //Change scene
                if (!currentScene.isPlaying)
                {
                    // Need to change scene
                    Scene nextScene = currentScene.OnExit();
                    GC.Collect();

                    if (nextScene != null)
                    {
                        currentScene = nextScene;
                        currentScene.Start();
                    }
                    else
                    {
                        return;
                    }
                }

                //GameLoop
                currentScene.Input();
                currentScene.Update();
                currentScene.Draw();
                Win.Update();
            }
        }

        public static RandomTimer RandomTimer(int timeMin, int timeMax)
        {
            return new RandomTimer(timeMin, timeMax);
        }
    }
}
