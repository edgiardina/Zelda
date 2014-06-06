
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Test_Game.Menu
{

    enum Direction
    {
        Up, Down
    }

    enum MenuOptions
    {
        SaveGame1, SaveGame2, SaveGame3, Register, Copy, Delete
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class MainMenu : View
    {
        public MainMenu(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

            CurrentOption = MenuOptions.Register;
            CursorPosition = new Point(125, 210);
            TimeLastKeyPress = DateTime.Now;
        }

        private MenuOptions CurrentOption;
        private Texture2D CursorTexture;
        private Point CursorPosition;
        private DateTime TimeLastKeyPress;

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {          
            SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);
            SpriteContainer.Draw(this.Texture, new Rectangle(0, 0, 548, 480), Color.White);
            SpriteContainer.Draw(this.CursorTexture, new Rectangle(CursorPosition.X, CursorPosition.Y, CursorTexture.Width, CursorTexture.Height), Color.White);
            SpriteContainer.End();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here


            //get keyboard keys
            if (TimeLastKeyPress.AddSeconds(.25) <= DateTime.Now)
            {
                KeyboardState currentState = Keyboard.GetState();
                Keys[] currentKeys = currentState.GetPressedKeys();
                //check for Hero Commands
                foreach (Keys KeyPressed in currentKeys)
                {
                    //Here, see what key is pressed, and do the appropriate Menu Actions.
                    switch (KeyPressed)
                    {
                        case Keys.Up:
                            MoveMenu(Direction.Up);
                            break;
                        case Keys.Down:
                            MoveMenu(Direction.Down);
                            break;
                        case Keys.Enter:
                            ExecuteMenuItem();
                            break;
                    }
                }

                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
                {
                    MoveMenu(Direction.Up);
                }
                if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
                {
                    MoveMenu(Direction.Down);
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                    ExecuteMenuItem();
            }

            base.Update(gameTime);
        }

        private void ExecuteMenuItem()
        {
            if (CurrentOption == MenuOptions.Register)
            {
                //StartGame For now; Register Later. 
                this.Game.Components.Add(new World.Play(this.Game));
                this.Game.Components.RemoveAt(0);
         
            }
        }

        private void MoveMenu(Direction Dir)
        {
            if (Dir == Direction.Up)
            {
                if (CurrentOption == MenuOptions.SaveGame1)
                {
                    CurrentOption = MenuOptions.Delete;
                }
                else
                    CurrentOption--;
            }
            else
            {
                if (CurrentOption == MenuOptions.Delete)
                {
                    CurrentOption = MenuOptions.SaveGame1;
                }
                else
                    CurrentOption++;
            }

            //reposition cursor
            switch (CurrentOption)
            {
                case MenuOptions.SaveGame1:
                    CursorPosition.Y = 150;
                    break;
                case MenuOptions.SaveGame2:
                    CursorPosition.Y = 175;
                    break;
                case MenuOptions.SaveGame3:
                    CursorPosition.Y = 240;
                    break;
                case MenuOptions.Register:
                    CursorPosition.Y = 305;
                    break;
                case MenuOptions.Copy:
                    CursorPosition.Y = 350;
                    break;
                case MenuOptions.Delete:
                    CursorPosition.Y = 395;
                    break;
            }

            //this is to prevent the game from moving the cursor too fast.
            TimeLastKeyPress = DateTime.Now;

        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            this.Texture = Texture2D.FromFile(GraphicsDevice, "..\\..\\..\\sprites\\menu.png");
            CursorTexture = Texture2D.FromFile(GraphicsDevice, "..\\..\\..\\sprites\\items\\heart.png");
            base.LoadGraphicsContent(loadAllContent);
        }
    }
}


