using PicoSystem.Framework.Graphics;

namespace PicoSystem.Framework.Scripting
{
    public abstract class PicoScript
    {
        protected PicoGame Game;

        public void __SetGameReference(PicoGame game)
        {
            Game = game;
        }

        public abstract void Init();

        public abstract void Update(float dt);

        public abstract void Draw(PicoGfx gfx);
    }
}
