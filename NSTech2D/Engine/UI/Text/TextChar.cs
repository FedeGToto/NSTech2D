using OpenTK;

namespace NSTech2D.Engine.UI.Text
{
    class TextChar : GameObject
    {
        protected char character;
        protected Vector2 textureOffset;
        protected Font font;

        public char Character { get { return character; } set { character = value; ComputeOffset(); } }

        public TextChar(Vector2 spritePosition, char character, Font font) : base(font.textureName, DrawLayer.GUI, Game.PixelsToUnits(font.characterWidth), Game.PixelsToUnits(font.characterHeight))
        {
            this.sprite.position = spritePosition;
            this.font = font;
            this.character = character;
            sprite.pivot = Vector2.Zero;

            ComputeOffset();
        }

        protected void ComputeOffset()
        {
            textureOffset = font.GetOffset(this.character);
        }

        public override void Draw()
        {
            if (isActive)
                sprite.DrawTexture(texture, (int)textureOffset.X, (int)textureOffset.Y, font.characterWidth, font.characterHeight);
        }
    }
}
