using GuiSystem.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GuiSystem.Structure;
using GuiSystem.Style;

namespace GuiSystem
{
    public class GuiGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private readonly GuiService gui;

        public GuiGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gui = new GuiService(graphics,
                Content,
                ()=>GraphicsDevice.Viewport.Bounds,
                builder => 
                builder
                    .Down(new DummyElement())
                    .Up()
                    .Down(new DummyElement())
                    .Add(new DummyElement(),new DummyElement()));

            gui.Style.Attach(
                ElementSelector.ByID(id =>  id == "Dummy"),
                (rule, time) =>
                rule.BackgroundColorProvider = () => Color.White * (float)time,
                AnimationSpan.Seconds(1.0));            
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gui.Update(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gui.Render();
            base.Draw(gameTime);
        }
    }
}
