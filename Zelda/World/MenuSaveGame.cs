#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Test_Game.World
{
    /// <summary>
    /// An Individual save game menu graphic
    /// </summary>
    public class MenuSaveGame : Microsoft.Xna.Framework.GameComponent
    {
        public MenuSaveGame(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }


        private Texture2D HeroTexture;
        private List<Texture2D> Hearts;



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


