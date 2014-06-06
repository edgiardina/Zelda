
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace Test_Game
{
    public enum MapType
    {
        Overworld, Underworld, Cave
    }
    /// <summary>
    /// This is the container for the current map screen the user is viewing.
    /// </summary>
    public class View : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch SpriteContainer;

        /// <summary>
        /// The background of the Map
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Type of Map Area ( to determine music and other things)
        /// </summary>
        public MapType Type;

        protected Cue MusicManager;

        public View(Microsoft.Xna.Framework.Game game)
            : base(game)
        {



        }

        protected override void LoadContent()
        {
            SpriteContainer = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
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


