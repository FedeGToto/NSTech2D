using System.Collections.Generic;

namespace NSTech2D.Engine
{
    static class UpdateManager
    {
        static List<IUpdatable> items;

        static UpdateManager()
        {
            items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IUpdatable item)
        {
            items.Remove(item);
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Update();
            }
        }

        public static void ClearAll()
        {
            items.Clear();
        }
    }
}
