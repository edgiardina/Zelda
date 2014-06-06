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
    public partial class Flame : Projectile
    {
        private TimeSpan TimeOfExistence;
        private TimeSpan TimeToExpire = new TimeSpan(0, 0, 1);

        public Flame(Microsoft.Xna.Framework.Game game, Character Ownr, Direction Dir)
            : base(game, Ownr)
        {
            // TODO: Construct any child components here
            this.Speed = 1;
            this.ObjectDirection = Dir;
            this.Owner = Ownr;
            this.TimeOfExistence = new TimeSpan(0, 0, 0, 0, 0);
            this.DamageFactor = 1;
            this.CurrentSprite = new Sprite(game.Content.Load<Texture2D>("Sprites\\Projectiles\\Fire"), 2, 32, 32);
            Music.Play("Candle");
        }


        public override void Update(GameTime gameTime)
        {            

            TimeOfExistence = TimeOfExistence.Add(gameTime.ElapsedGameTime);

            if (TimeOfExistence.Ticks >= TimeToExpire.Ticks)
            {
                isValidActive = false;
            }
            base.Update(gameTime);
        }

    }

}
