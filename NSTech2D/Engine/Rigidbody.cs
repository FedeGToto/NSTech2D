using NSTech2D.Engine.Collisions;
using OpenTK;

namespace NSTech2D.Engine
{
    enum RigidBodyType { Player = 1, PlayerBullet = 2, Enemy = 4, EnemyBullet = 8, PowerUp = 16, TileObj = 32 }
    class Rigidbody
    {
        protected uint collisionsMask;

        public RigidBodyType Type;
        public Vector2 Velocity;

        public GameObject GameObject;
        public bool IsGravityAffected;
        public bool IsCollisionsAffected;

        public float Friction;

        public Collider Collider { get; set; }

        public Vector2 Position { get { return GameObject.position; } set { GameObject.position = value; } }

        public bool IsActive { get { return GameObject.isActive; } set { GameObject.isActive = value; } }

        public Rigidbody(GameObject owner)
        {
            GameObject = owner;
            PhysicsManager.AddItem(this);
            IsCollisionsAffected = true;
        }

        protected virtual void ApplyFriction()
        {
            if (Friction > 0 && Velocity.Length != 0)
            {
                float fAmount = Friction * Game.DeltaTime;

                float newVelocityLength = Velocity.Length - fAmount;

                if (newVelocityLength < 0)
                {
                    newVelocityLength = 0;
                }

                Velocity = Velocity.Normalized() * newVelocityLength;
            }
        }

        public virtual void Update()
        {
            if (IsGravityAffected)
            {
                Velocity.Y += PhysicsManager.G * Game.DeltaTime;
            }

            ApplyFriction();

            GameObject.position += Velocity * Game.DeltaTime;
        }

        public virtual bool Collides(Rigidbody other, ref Collision collisionInfo)
        {
            return Collider.Collides(other.Collider, ref collisionInfo);
        }

        //add RigidBodyType to collisions types list
        public void AddCollisionType(uint add)
        {
            collisionsMask |= add;
        }

        //checks if type is in collisions types list
        public bool CollisionTypeMatches(RigidBodyType type)
        {
            return ((uint)type & collisionsMask) != 0;
        }

        public virtual void Destroy()
        {
            GameObject = null;
            if (Collider != null)
            {
                Collider.Destroy();
                Collider = null;
            }
            PhysicsManager.RemoveItem(this);
        }
    }
}
