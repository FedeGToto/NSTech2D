using NSTech2D.RenderEngine;
using System.Collections.Generic;

namespace NSTech2D.Engine
{
    static class GraphicsManager
    {
        private static Dictionary<string, Texture> textures;

        static GraphicsManager()
        {
            textures = new Dictionary<string, Texture>();
        }

        public static Texture AddTexture(string name, string path)
        {
            Texture t = new Texture(path);
            textures.Add(name, t);

            return t;
        }

        public static Texture GetTexture(string name)
        {
            Texture t = null;

            if (textures.ContainsKey(name))
            {
                t = textures[name];
            }

            return t;
        }

        public static void ClearAll()
        {
            textures.Clear();
        }
    }
}
