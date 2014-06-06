using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Test_Game.Graphics;

namespace Test_Game.Characters
{
    class Octorok : Enemy
    {
        Random random = new Random();
        public Octorok(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            this.DamageFactor = 5;
            this.HitPoints = 10;       
            this.Speed = 1;
            this.CurrentSprite = new Sprite(TestGame.ContentMgr.Load<Texture2D>("Sprites\\Enemies\\Octorok\\Octorok_down"), 2, 32, 32);          

        }


        public Octorok(Microsoft.Xna.Framework.Game game, Point Position)
            : base(game)
        {
            this.DamageFactor = 5;
            this.HitPoints = 10;
            this.Speed = 1;
            this.CurrentSprite = new Sprite(TestGame.ContentMgr.Load<Texture2D>("Sprites\\Enemies\\Octorok\\Octorok_down"), 2, 32, 32);
            this.Position = Position;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //handle frames for animation
            updateFrames(gameTime);

            //Handle AI
            ArtificialIntelligence();

            base.Update(gameTime);
        }


        private void ArtificialIntelligence()
        {
            Rectangle GameArea = World.Play.GameBoundaries;
            //Octorok Moves until bumped
            if (this.BoundingBox.Left == GameArea.Left)
            {
                this.ObjectDirection = Direction.Right;
            }
            if (this.BoundingBox.Top == GameArea.Top)
            {
                this.ObjectDirection = Direction.Down;
            }
            if (this.BoundingBox.Bottom == GameArea.Bottom)
            {
                this.ObjectDirection = Direction.Up;
            }
            if (this.BoundingBox.Right == GameArea.Right)
            {
                this.ObjectDirection = Direction.Left;
            }

            Move(this.ObjectDirection);
        }

        /// <summary>
        /// Octorok Drawing rotates based on direction
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {

            SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);
            CurrentSprite.Draw(SpriteContainer, this.Position, this.ObjectDirection);
            SpriteContainer.End();


        }
    }
}
