
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Test_Game.Physics;
using Test_Game.Graphics;
#endregion

namespace Test_Game
{    
    /// <summary>
    /// Any Object that appears in game and has a screen presence in the main playing area is a GameObject
    /// You should probably not declare a type of GameObject, but instead inherit from it to make your own game 'types'
    /// </summary>   
    public abstract partial class GameObject
    {
        protected SpriteBatch SpriteContainer;      
        private float _time;
        protected Game game;

        /// <summary>
        /// Determines whether the object is active; AKA whether its update method should be drawn.
        /// </summary>
        public bool Active = true;

        /// <summary>
        /// The object's position in the playing area.
        /// </summary>
        public Point Position;

        /// <summary>
        /// The speed the object moves at, in points per update
        /// </summary>
        public int Speed;

        /// <summary>
        /// the direction the object is facing
        /// </summary>
        public Direction ObjectDirection;

        protected Sprite LeftTexture;
        protected Sprite RightTexture;
        protected Sprite UpTexture;
        protected Sprite DownTexture;

        protected Rectangle BoundingBox;

        /// <summary>
        /// the on-screen appearance of the character
        /// </summary>
        public Sprite CurrentSprite;

        public GameObject(Microsoft.Xna.Framework.Game game)            
        {           
            SpriteContainer = new SpriteBatch(game.GraphicsDevice);
            this.game = game;       
        }


        public virtual void Draw(GameTime gameTime)
        {
            SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);
            CurrentSprite.Draw(SpriteContainer, this.Position);
            SpriteContainer.End();           
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            BoundingBox.Width = CurrentSprite.Width;
            BoundingBox.Height = CurrentSprite.Height;
            // TODO: Add your update code here
            BoundingBox.X = Position.X - (CurrentSprite.Width / 2);
            BoundingBox.Y = Position.Y - (CurrentSprite.Height / 2);           
        }
                
        protected void PointObjectInDirection(Direction Directn)
        {
            this.ObjectDirection = Directn;

            //TODO: change this code to look for sprites depending on the specific character. build a sprite lookup table?
            switch (Directn)
            {
                case Direction.Up:
                    this.CurrentSprite = UpTexture; 
                    break;
                case Direction.Down:
                    this.CurrentSprite = DownTexture; 
                    break;
                case Direction.Left:
                    this.CurrentSprite = LeftTexture; 
                    break;
                case Direction.Right:
                    this.CurrentSprite = RightTexture;
                    break;
            }
        }

        /// <summary>
        /// Determine whether an arbitrary rectangle and a GameObject's bounding box intersect in the 2D plane
        /// </summary>
        /// <param name="a">Rectangle  to check</param>
        /// <param name="b">Gameobject  to check</param>
        /// <returns>Boolean results of intersection check</returns>
        public static bool Intersects(Rectangle a, GameObject b)
        {
            if (Collision.Intersects(a, b.BoundingBox))
            {
                //For now, Only collide boundingboxes. the code below here would handle spritemap collisions for transparent pixels.
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determine whether two characters intersect in the 2D plane
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Boolean results of intersection check</returns>
        public static bool Intersects(GameObject a, GameObject b)
        {
            if (Collision.Intersects(a.BoundingBox, b.BoundingBox))
            {
                //For now, Only collide boundingboxes. the code below here would handle spritemap collisions for transparent pixels.
                return true;

                ulong[] bitsA = new ulong[a.CurrentSprite.Width * a.CurrentSprite.Height];
                a.CurrentSprite.Texture.GetData<ulong>(bitsA);

                ulong[] bitsB = new ulong[b.CurrentSprite.Width * b.CurrentSprite.Height];
                b.CurrentSprite.Texture.GetData<ulong>(bitsB);

                int x1 = Math.Max(a.BoundingBox.X, b.BoundingBox.X);
                int x2 = Math.Min(a.BoundingBox.X + a.BoundingBox.Width, b.BoundingBox.X + b.BoundingBox.Width);

                int y1 = Math.Max(a.BoundingBox.Y, b.BoundingBox.Y);
                int y2 = Math.Min(a.BoundingBox.Y + a.BoundingBox.Height, b.BoundingBox.Y + b.BoundingBox.Height);

                for (int y = y1; y < y2; ++y)
                {
                    for (int x = x1; x < x2; ++x)
                    {
                        if (((bitsA[(x - a.BoundingBox.X) + (y - a.BoundingBox.Y) * a.CurrentSprite.Width] & 0xFF000000) >> 24) > 20 &&
                            ((bitsB[(x - b.BoundingBox.X) + (y - b.BoundingBox.Y) * b.CurrentSprite.Width] & 0xFF000000) >> 24) > 20)
                            return true;
                    }
                }
            }

            return false;
        }

        protected bool updateFrames(GameTime gameTime)
        {
            _time += gameTime.ElapsedGameTime.Milliseconds;

            if ((_time / 1000) > .25f)
            {
                CurrentSprite.CurrentFrame++;
                if (CurrentSprite.CurrentFrame == CurrentSprite.CountOfFrames)
                    CurrentSprite.CurrentFrame = 0;
                _time = 0.0f;
                return true;
            }
            return false;
        }

        protected virtual void Move(Direction Directn)
        {
            //if the user changed direction, change the sprite used to draw him
            if (this.ObjectDirection != Directn)
            {
                PointObjectInDirection(Directn);
            }

            //move the character
            //must keep character on the screen.

            //Check tile intersection (can we walk on this tile)

            switch (Directn)
            {
                case Direction.Up:
                    this.Position.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Position.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.Position.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.Position.X += this.Speed;
                    break;
            }
         }  

    }
}


