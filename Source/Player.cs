using Raylib_cs;

namespace Starfall
{
    public class Player : Entity
    {
        public Player(int id, string name, string sprite, float x, float y) : base(id, name, sprite, x, y)
        {
            // TODO: fill this out
        }

        public override void Update(float deltaTime)
        {
            // TODO: impl
        }

        // This is generic so we can just call base.Draw()
        public override void Draw()
        {
            base.Draw();
        }
    }
}