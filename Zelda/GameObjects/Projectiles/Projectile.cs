#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Test_Game.Graphics;
#endregion

namespace Test_Game
{
    
    /// <summary>
    /// Projectile Object
    /// </summary>   
    public partial class Projectile : GameObject
    {
        public Character Owner;
        public bool isValidActive = true;
        public int DamageFactor;
        
        public Projectile(Microsoft.Xna.Framework.Game game, Character Ownr)
            : base(game)
        {
            // TODO: Construct any child components here
            this.Speed = 1;
            this.Owner = Ownr;
        }
     
        public override void Update(GameTime gameTime)
        {
            if (Active == true)
            {
                //handle frames for animation
                bool newFrame = updateFrames(gameTime);

               //Some States need to be reset after a time period without keypress, such as attacking
                //CheckStateChange(newFrame);
                this.Move(this.ObjectDirection);
            }
            base.Update(gameTime);

        } 

    }
     
}
