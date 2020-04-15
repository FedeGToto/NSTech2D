using NSTech2D.RenderEngine;

namespace NSTech2D.Engine
{
    static class Game
    {
        public static Window Win;
        public static void Init()
        {
            //Init the game
            Win = new Window(1280, 720, "NS Tech 2D");
        }

        public static void Play()
        {
            while(Win.IsOpened)
            {
                //GameLoop
            }
        }
    }
}
