using System;
using System.Xml;

namespace NSTech2D.Engine.Tiled
{
    class TmxReader
    {
        public TileSet TileSet { get; }
        public TileGrid TileGrid { get; }

        public TmxReader(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            XmlNode nodeMap = doc.SelectSingleNode("map");

            TileSet = TmxNodeParser.ParseTileset(nodeMap);
            TileGrid = TmxNodeParser.ParseGrid(nodeMap, TileSet);
        }
    }
}
