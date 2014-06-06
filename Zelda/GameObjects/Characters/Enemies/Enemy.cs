
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
#endregion

namespace Test_Game.Characters
{
    /// <summary>
    /// Represents a computer controlled enemy
    /// </summary>
    public abstract partial class Enemy : Character
    {
        public Enemy(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            // TODO: Construct any child components here
           
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


