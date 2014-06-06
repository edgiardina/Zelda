
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Test_Game.Physics;
#endregion

namespace Test_Game.Menus
{

    public enum Direction
    {
        Up, Down
    }

    public enum MenuOptionType
    {
        SaveGame1, SaveGame2, SaveGame3, Register, Copy, Delete, Retry, Save, Quit
    }
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class Menu : View
    {
        public Menu(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            // TODO: Construct any child components here


            TimeLastKeyPress = DateTime.Now;
        }

        protected int CurrentOptionIndex;
        protected MenuOption CurrentOption;
        protected List<MenuOption> Options;
        protected Texture2D CursorTexture;

        private Spring CursorSpring;
        /// <summary>
        /// Position of the Cursor on the Screen.
        /// </summary>
        protected Point CursorPosition;
    
        protected DateTime TimeLastKeyPress;
        protected SpriteFont MenuFont;

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            ContentManager content = new ContentManager(Game.Services);
            //Load Menu Fonts
            MenuFont = content.Load<SpriteFont>("Fonts\\SpriteFont1");
            CursorSpring = new Test_Game.Physics.Spring();
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteContainer.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            //SpriteContainer.Draw(this.Texture, new Rectangle(0, 0, 548, 480), Color.White);            
            SpriteContainer.Draw(this.CursorTexture, new Rectangle(CursorPosition.X - 50, CursorPosition.Y, CursorTexture.Width, CursorTexture.Height), Color.White);
            foreach (MenuOption M in Options)
            {
                //SpriteContainer.Draw(this.CursorTexture, new Rectangle(M.Position.X, M.Position.Y, CursorTexture.Width, CursorTexture.Height), Color.White);                    
                SpriteContainer.DrawString(MenuFont, M.Text, new Vector2(M.Position.X, M.Position.Y), Color.White);
            }

            SpriteContainer.End();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            
            // TODO: Add your update code here
            if (!Guide.IsVisible)
            {
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

                //if Cursor is not in desired spot due to menu up/down movement, ease it to the desired spot on a spring.                
                if (CursorPosition.Y != CurrentOption.Position.Y)
                {                   
                    CursorPosition.Y = (int)CursorSpring.UpdateSprungObject((float)CursorPosition.Y, (float)CurrentOption.Position.Y, (float)gameTime.ElapsedGameTime.TotalSeconds);                                                              
                }               
                
                //CursorPosition = CurrentOption.Position;
            }
            base.Update(gameTime);
        }

        protected virtual void ExecuteMenuItem()
        {

        }

        private void MoveMenu(Direction Dir)
        {
            if (Dir == Direction.Up)
            {
                if (CurrentOptionIndex == 0)
                {
                    CurrentOption = Options[Options.Count - 1];
                    CurrentOptionIndex = Options.Count - 1;
                }
                else
                {
                    CurrentOption = Options[CurrentOptionIndex - 1];
                    CurrentOptionIndex--;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (CurrentOptionIndex == Options.Count - 1)
                {
                    CurrentOption = Options[0];
                    CurrentOptionIndex = 0;
                }
                else
                {
                    CurrentOption = Options[CurrentOptionIndex + 1];
                    CurrentOptionIndex++;
                }
            }

            //TODO: Play Heart Sound
            //DesiredCursorPosition.Y = CurrentOption.Position.Y;            
            
            //CursorPosition.Y = CurrentOption.Position.Y;

            //this is to prevent the game from moving the cursor too fast.
            TimeLastKeyPress = DateTime.Now;

        }

        protected override void LoadContent()
        {
            this.Texture = Game.Content.Load<Texture2D>("Sprites\\menu");
            CursorTexture = Game.Content.Load<Texture2D>("Sprites\\Items\\heart");
            base.LoadContent();
        }
    }

    public partial class MenuOption
    {
        /// <summary>
        /// Top-right point of the Menu Option
        /// </summary>
        public Point Position;
        public MenuOptionType Type;
        public string Text;

        public MenuOption(Point Position, MenuOptionType Type, string Text)
        {
            this.Position = Position;
            this.Type = Type;
            this.Text = Text;
        }
    }
}


