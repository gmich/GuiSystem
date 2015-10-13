using GuiSystem.Input;
using Microsoft.Xna.Framework;

namespace GuiSystem
{
    public class Gui : DrawableGameComponent
    {
        private readonly InputManager inputManager;
        public Gui(Game game): base(game)
        {
            inputManager = new InputManager();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Flush();

            base.Update(gameTime);
        }
    }
}
