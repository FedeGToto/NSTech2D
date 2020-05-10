using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.Collisions
{
    class ColliderFactory
    {
        public static Collider CreateCircleFor(GameObject obj)
        {
            float halfDiagonal = (float)(Math.Sqrt(obj.width * obj.width + obj.height * obj.height)) * 0.5f;
            return new CircleCollider(obj.RigidBody, halfDiagonal);
        }

        public static Collider CreateBoxFor(GameObject obj)
        {
            return new BoxCollider(obj.RigidBody, obj.width, obj.height);
        }
    }
}
