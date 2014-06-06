
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

    public enum Direction
    {
        Up, Down, Left, Right
    }

    public enum CharacterState
    {
        Moving,
        Attacking,
        Standing,
        Hit
    }

    /// <summary>
    /// A character is a default class for a player, enemy, or other active element on the screen.
    /// You should probably not declare a type of Character, but instead inherit from it to make your own character 'types'
    /// </summary>   
    public abstract partial class Character : GameObject
    {
        /// <summary>
        /// Characters remaining life
        /// </summary>
        public int HitPoints;

        /// <summary>
        /// Time the Character has been in the current state
        /// </summary>
        protected int StateTimer = 0;

        /// <summary>
        /// Damage Multiplier for character (added after damage factor for weapon taken into account
        /// </summary>
        public int DamageFactor;

        public CharacterState State = CharacterState.Standing;

        public Character(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        protected override void Move(Direction Directn)
        {
            //don't allow Character to move if character is being hit.
            if (State != CharacterState.Hit)
            {
                State = CharacterState.Moving;
            }
            base.Move(Directn);

            //Clamp character so he doesn't go out of bounds;            
            Rectangle GameArea = World.Play.GameBoundaries;
            this.Position.X = (int)MathHelper.Clamp((float)this.Position.X, GameArea.Left + (CurrentSprite.Width / 2), GameArea.Right - (CurrentSprite.Width / 2));
            this.Position.Y = (int)MathHelper.Clamp((float)this.Position.Y, GameArea.Top + (CurrentSprite.Height / 2), GameArea.Bottom - (CurrentSprite.Height / 2));

        }

        public void ProjectileIntersection(Projectile P)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //handle frames for animation
            bool newFrame = updateFrames(gameTime);
            //Some States need to be reset after a time period without keypress, such as attacking
            CheckStateChange(newFrame);
            base.Update(gameTime);
        }


        public virtual void RegisterHit(int DamageFactor, Direction AttackDirection)
        {
            if (this.State != CharacterState.Hit)
            {
                //figure out damage factor. 
                this.HitPoints -= DamageFactor;
                //this.State = PlayerState.Hit;

                //knock enemy back.
                if (AttackDirection == Direction.Up)
                    this.Position.Y -= 20;
                if (AttackDirection == Direction.Down)
                    this.Position.Y += 20;
                if (AttackDirection == Direction.Right)
                    this.Position.X += 20;
                if (AttackDirection == Direction.Left)
                    this.Position.X -= 20;

                //Temporarily Mark Player Invulnerable
                this.State = CharacterState.Hit;
            }
        }


        /// <summary>
        /// Check to see if Character should be removed from current state based on amount of time. 
        /// </summary>
        /// <param name="nf"></param>
        protected void CheckStateChange(bool nf)
        {
            if (nf)
            {
                if (this.State == CharacterState.Attacking)
                {
                    //Check Attack Timer
                    if (StateTimer >= CurrentSprite.CountOfFrames)
                    {
                        this.State = CharacterState.Standing;
                        this.CurrentSprite.CurrentFrame = 0;
                        //Restore Character Sprite to standing
                        PointObjectInDirection(this.ObjectDirection);

                        StateTimer = 0;
                    }
                    else
                    {
                        StateTimer++;
                    }
                }

                if (this.State == CharacterState.Hit)
                {
                    if (StateTimer >= 3)
                    {
                        this.State = CharacterState.Standing;
                        StateTimer = 0;
                    }
                    else
                    {
                        StateTimer++;
                    }
                }
            }
        }


    }
}


