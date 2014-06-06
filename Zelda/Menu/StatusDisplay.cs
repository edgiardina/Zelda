#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Test_Game.Menu
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StatusDisplay : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch SpriteContainer;
        SpriteFont Font1;
        public StatusDisplay(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            //Components to Initialize: Life Meter, weapon display, map,
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteContainer.Begin();
            ContentManager content = new ContentManager(Game.Services);
            Font1 = content.Load<SpriteFont>("Fonts\\SpriteFont1");
            SpriteContainer.DrawString(Font1, "- LIFE -", new Vector2(368, 30), Color.Red);
            SpriteContainer.End();
            base.Draw(gameTime);
        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {            
            SpriteContainer = new SpriteBatch(GraphicsDevice);
         
            base.LoadGraphicsContent(loadAllContent);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}


