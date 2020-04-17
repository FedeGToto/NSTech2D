namespace NSTech2D.Engine
{
    abstract class Scene
    {
        public bool isPlaying { get; protected set; }

        public Scene nextScene;

        public Scene()
        {

        }

        public virtual void Start()
        {
            isPlaying = true;
        }

        public virtual Scene OnExit()
        {
            isPlaying = false;
            return nextScene;
        }

        public abstract void Input();

        public virtual void Update()
        {

        }

        public abstract void Draw();
    }
}
