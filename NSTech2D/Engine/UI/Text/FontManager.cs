﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.UI.Text
{
    static class FontManager
    {
        private static Dictionary<string, Font> fonts;
        private static Font defaultFont;

        public static void Init()
        {
            fonts = new Dictionary<string, Font>();
            defaultFont = null;
        }

        public static Font AddFont(string fontName, string texturePath, int numColumns, int firstCharacterASCIIValue, int charWidth, int charHeight)
        {
            Font f = new Font(fontName, texturePath, numColumns, firstCharacterASCIIValue, charWidth, charHeight);
            fonts.Add(fontName, f);
            if (defaultFont == null)
            {
                defaultFont = f;
            }
            return f;
        }

        public static Font GetFont(string fontName)
        {
            if (fonts.ContainsKey(fontName))
            {
                return fonts[fontName];
            }

            return defaultFont;
        }

        public static void ClearAll()
        {
            fonts.Clear();
            defaultFont = null;
        }
    }
}
