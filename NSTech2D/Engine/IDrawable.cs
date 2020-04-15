namespace NSTech2D.Engine
{
    interface IDrawable
    {
        DrawLayer Layer { get; }
        void Draw();
    }
}