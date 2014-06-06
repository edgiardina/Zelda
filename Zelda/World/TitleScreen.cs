
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Test_Game.Menus;
#endregion

namespace Test_Game
{
    /// <summary>
    /// Title Screen.
    /// </summary>
    public partial class TitleScreen : View
    {

        public TitleScreen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {

            // TODO: Construct any child components here

        }

        protected override void LoadContent()
        {

            this.Texture = Game.Content.Load<Texture2D>("Sprites\\logo");
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //Music.Initialize();


            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            int width = GraphicsDevice.Viewport.Width / 2;
            int height = GraphicsDevice.Viewport.Height / 2;
            SpriteContainer.Begin(SpriteSortMode.BackToFront, BlendState.Additive);
           
            SpriteContainer.Draw(this.Texture, new Vector2(width, height), null, Color.White, 0, new Vector2(this.Texture.Width / 2, this.Texture.Height / 2), 1, SpriteEffects.None, 0.0f);
            SpriteContainer.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            // Proceed to Main Menu if Start Pressed 
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                this.ProceedToMenu();
            }

            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();
            //check for up and down arrow keys
            foreach (Keys key in currentKeys)
            {
                if (key == Keys.Enter)
                    this.ProceedToMenu();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Unloads the current component and loads the Main Menu
        /// </summary>
        private void ProceedToMenu()
        {
            this.Game.Components.Remove(this);
            this.Game.Components.Add(new Menu.FadeScreen(this.Game, this, new MainMenu(this.Game),Color.Black));            
        }
    }
}


