
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Test_Game.Graphics;
using Test_Game.Items;
#endregion

namespace Test_Game.Characters
{

    /// <summary>
    /// This is the main character object, to be controlled by the game-player
    /// </summary>
    public partial class Hero : Character
    {
        private Sprite DownAttackTexture;
        private Sprite UpAttackTexture;
        private Sprite SideAttackTexture;
        private TimeSpan CurrentTimeInState;

        private List<Weapon> WeaponList;
        private Weapon SelectedWeapon;

        /// <summary>
        /// This object is read by the Play Object to see if the player did anything to create an object
        /// (drop a bomb, shoot an arrow, etc)
        /// </summary>
        public Projectile CharacterCreatedProjectile;

        /// <summary>
        /// Range in pixels of attack 
        /// </summary>
        public Rectangle AttackBox;


        public Hero(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            // Initial Character Set-up
            this.Position.X = 224;
            this.Position.Y = 256;
            this.Speed = 3;

            this.HitPoints = 25;
            this.DamageFactor = 5;

            this.PointObjectInDirection(Direction.Down);
            this.State = CharacterState.Standing;
            this.CurrentTimeInState = TimeSpan.Zero;

            WeaponList = new List<Weapon>();
            WeaponList.Add(new BlueCandle(this.game));
            SelectedWeapon = WeaponList[0];

            this.CurrentSprite = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\Link"));
            LeftTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\Link-Left"));
            RightTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\Link-Right"));
            UpTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\Link-Up"));
            DownTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\Link"));
            DownAttackTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\link_attack_down"), 3, 32, 54);
            UpAttackTexture = new Sprite(game.Content.Load<Texture2D>("Sprites\\Hero\\link_attack_up"), 3, 32, 54);

        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Active == true)
            {
                //read character input
                readInput();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Override of Character Draw. Drawing a hero may need an offset due to attacking state.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            if (this.State == CharacterState.Attacking)
            {
                SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);
                CurrentSprite.Draw(SpriteContainer, this.Position);
                SpriteContainer.End();
            }
            else if (this.State == CharacterState.Hit)
            {
                float pulsate = GetPulsingValue(gameTime);
                    //float pulsate = (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds * 6) + 1;
                SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);
                CurrentSprite.Draw(SpriteContainer, this.Position, Color.Red, pulsate);
                SpriteContainer.End();
            }
            else
            {
                base.Draw(gameTime);
            }
        }

        private void PressBButton()
        {

            //depending on SelectedWeapon, perform actions
            if (this.SelectedWeapon is BlueCandle && this.CharacterCreatedProjectile == null)
            {
                Flame F = new Flame(this.game, this, this.ObjectDirection);
                Rectangle ProjDimens = new Rectangle();
                //Draw Bounding Box for Attack. 
                if (this.ObjectDirection == Direction.Down)
                {
                    //ProjDimens = new Rectangle(BoundingBox.X, BoundingBox.Bottom, BoundingBox.Width, BoundingBox.Height);
                    F.Position.X = this.Position.X;
                    F.Position.Y = BoundingBox.Bottom;
                }
                else if (this.ObjectDirection == Direction.Up)
                {
                    //ProjDimens = new Rectangle(BoundingBox.X, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height);
                    F.Position.X = this.Position.X;
                    F.Position.Y = BoundingBox.Top;
                }
                else if (this.ObjectDirection == Direction.Right)
                {
                    //ProjDimens = new Rectangle(BoundingBox.Right, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height);
                    F.Position.Y = this.Position.Y;
                    F.Position.X = this.BoundingBox.Right;
                }
                else if (this.ObjectDirection == Direction.Left)
                {
                    //ProjDimens = new Rectangle(BoundingBox.Left, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height);
                    F.Position.Y = this.Position.Y;
                    F.Position.X = this.BoundingBox.Left;
                }               
                this.CharacterCreatedProjectile = F;
            }
        }

        private void PressAButton()
        {
            this.State = CharacterState.Attacking;

            //Draw Bounding Box for Attack. 
            if (this.ObjectDirection == Direction.Down)
            {
                AttackBox = new Rectangle(BoundingBox.Left, BoundingBox.Bottom, BoundingBox.Width, BoundingBox.Height);
                //change texture to sword animation
                this.CurrentSprite = DownAttackTexture;
            }
            else if (this.ObjectDirection == Direction.Up)
            {
                AttackBox = new Rectangle(BoundingBox.Left, BoundingBox.Top - BoundingBox.Height, BoundingBox.Width, BoundingBox.Height);
                this.CurrentSprite = UpAttackTexture;
            }
            else if (this.ObjectDirection == Direction.Right)
            {
                AttackBox = new Rectangle(BoundingBox.Right, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height);
            }
            else if (this.ObjectDirection == Direction.Left)
            {
                AttackBox = new Rectangle(BoundingBox.Left - BoundingBox.Width, BoundingBox.Top, BoundingBox.Width, BoundingBox.Height);
            }
            //Play Associated Sounds

        }



        private void readInput()
        {
            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();
            //check for Hero Commands
            if (currentKeys.Length > 0)
            {
                foreach (Keys KeyPressed in currentKeys)
                {
                    //Here, see what key is pressed, and do the appropriate Hero Actions.
                    switch (KeyPressed)
                    {
                        case Keys.Up:
                            Move(Direction.Up);
                            break;
                        case Keys.Down:
                            Move(Direction.Down);
                            break;
                        case Keys.Left:
                            Move(Direction.Left);
                            break;
                        case Keys.Right:
                            Move(Direction.Right);
                            break;
                        case Keys.LeftControl:
                            PressAButton();
                            break;
                        case Keys.LeftAlt:
                            PressBButton();
                            break;
                    }
                }
                if (currentKeys.Length == 0)
                {
                    PointObjectInDirection(ObjectDirection);
                }
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                Move(Direction.Up);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
            {
                Move(Direction.Down);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                Move(Direction.Left);
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                Move(Direction.Right);
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                PressAButton();
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                PressBButton();
            }
        }

        internal void Attack(Enemy E)
        {
            E.RegisterHit(this.DamageFactor, this.ObjectDirection);
        }

        public override void RegisterHit(int DamageFactor, Direction AttackDirection)
        {
            //TODO: Play Player-hit Sound Effect
            base.RegisterHit(DamageFactor, AttackDirection);
        }

        internal float GetPulsingValue(GameTime Gtime)
        {
            int min = 150;
            int max = 255;
            int speed = 8;

            return min + ((float)Math.Sin(Gtime.TotalGameTime.TotalSeconds * speed) + 1) / 2 * (max - min);
        }
    }
}


