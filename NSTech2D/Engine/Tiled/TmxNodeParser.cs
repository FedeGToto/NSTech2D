using System.Xml;

namespace NSTech2D.Engine.Tiled
{
    class TmxNodeParser
    {
        public static TileSet ParseTileset(XmlNode nodeMap)
        {
            XmlNode tilesetNode = nodeMap.SelectSingleNode("tileset");
            int tileW = int.Parse(tilesetNode.Attributes.GetNamedItem("tilewidth").InnerText);
            int tileH = int.Parse(tilesetNode.Attributes.GetNamedItem("tileheight").InnerText);
            XmlNode tsImgNode = tilesetNode.SelectSingleNode("image");
            string tsImgPath = "Assets/" + tsImgNode.Attributes.GetNamedItem("source").InnerText;
            int tsImgWidth = int.Parse(tsImgNode.Attributes.GetNamedItem("width").InnerText);
            int tsImgHeigh = int.Parse(tsImgNode.Attributes.GetNamedItem("height").InnerText);

            return TileSetFactory.Create(tileW, tileH, tsImgPath, tsImgWidth, tsImgHeigh);
        }

        public static TileGrid ParseGrid(XmlNode nodeMap, TileSet ts)
        {
            XmlNode layerNode = nodeMap.SelectSingleNode("layer");
            int layerCols = int.Parse(layerNode.Attributes.GetNamedItem("width").InnerText);
            int layerRows = int.Parse(layerNode.Attributes.GetNamedItem("height").InnerText);
            XmlNode dataNode = layerNode.SelectSingleNode("data");
            string csvString = dataNode.InnerText;
            csvString = csvString.Replace("\r\n", "").Replace("\n", "");

            return TileGridFactory.Create(layerRows, layerCols, csvString, ts);
        }
    }
}
