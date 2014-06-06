#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Test_Game.Characters;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Test_Game.World
{

    public enum PlayState
    {
        Paused, Playing, Dead
    }

    /// <summary>
    /// This object represents the gameplay. It gets instantiated via the Game Menu, and calls the game menu on a quit action
    /// </summary>
    public class Play : View
    {

        internal Play(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            // TODO: Construct any child components here

        }

        PlayState State;
        Hero Player;
        List<Enemy> EnemyList;
        List<Projectile> ProjectileList;
        Screen CurrentScreen;

        private int waveNumber = 1;

        // bool drewEnemy = false;

        /*  public const float YCeiling = 96;
        public const float YFloor = 448;
        public const float XCeiling = 0;
        public const float XFloor = 512;*/

        public static Rectangle GameBoundaries
        {
            get { return new Rectangle(0,96, 1280, 624); }
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {

            /*Note: Game components draw in a weird order on initialization. It would be expected that game components would
                'stack', but instead they seem to go in reverse. So add the ones you want on top first, then the ones below them afterwards;              
            */
            //The player's character
            Player = new Hero(this.Game);        
            //Here, we load the game components that live in the 'play' space. These are the Screen (which holds background tiles, menus, etc)            

            EnemyList = new List<Enemy>();
            EnemyList.Add(new Octorok(this.Game, new Point(224, 175)));

            ProjectileList = new List<Projectile>();
            
            //Load map tiles.
            /* Here, right now we're drawing blank. But what we really want to load is a class of 
             * Type Screen that's inherited and has a specific tile layout. */
            CurrentScreen = new Screen(this.Game);
            Game.Components.Add(CurrentScreen);

            State = PlayState.Playing;
            
            //this.MusicManager = Music.Play("Overworld");    
            base.Initialize();
        }

        /// <summary>
        /// Here, create the new Screen object and assign it to currentscreen, move the player, populate the enemies
        /// </summary>
        private void LoadNewScreen()
        {
            //Move Player to opposite end of screen (top to bottom or left to right, on scroll)
            Player.Position = new Point(250, 250);
            //Recalculate enemies, regenerate them.
            EnemyList.Clear();

        }

        /// <summary>
        /// The Play object does not draw anything. Instead, it loads the items that draw themselves, such as characters, 
        /// widgets, items, maps, etc
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Player.Draw(gameTime);
            foreach (Enemy E in EnemyList)
            {
                E.Draw(gameTime);
            }

            foreach (Projectile P in ProjectileList)
            {
                P.Draw(gameTime);
            }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (State == PlayState.Dead)
            {
               
                //Play Dead Music
                // run dialog
            }
            else if (State == PlayState.Playing)
            {
                //General Updates
                Player.Update(gameTime);
                foreach (Enemy E in EnemyList)
                {
                    E.Update(gameTime);
                }

                foreach (Projectile P in ProjectileList)
                {
                    P.Update(gameTime);
                }

                //Change Game State ?
                if (Player.HitPoints < 0)
                {
                    RunDeadState();
                }
                else
                {
                    this.EnemyUpdate(gameTime);
                    this.ProjectileUpdate();

                    //Test CoDe, populate random enemies, etc
                    //AddNewEnemy();
                }
            }
            else if (State == PlayState.Paused)
            {
                PausePlay();
            }

            base.Update(gameTime);
        }

        private void EnemyUpdate(GameTime gt)
        {
            List<Enemy> EnemiesToKill = new List<Enemy>();
            foreach (Enemy E in EnemyList)
            {
                // Check for physical enemy intersection.
                if (Character.Intersects(Player, E))
                {                    
                    Player.RegisterHit(E.DamageFactor,E.ObjectDirection);
                }

                //Check for Player and other obje

                //Check enemy and other enemy interaction
                
                //check Enemy / Projectile Intersection
                foreach(Projectile P in ProjectileList){
                    if (GameObject.Intersects(E, P))
                    {
                        E.RegisterHit(P.DamageFactor,P.ObjectDirection);
                    }
                }

                //Check to See if Player is Attacking and hits an enemy
                if (Player.State == CharacterState.Attacking && Character.Intersects(Player.AttackBox, E))
                {
                    Player.Attack(E);
                }

                //Check for dead enemies, if so, remove them from the playing area
                if (E.HitPoints <= 0)
                {                    
                    EnemiesToKill.Add(E);
                }
            }

            foreach (Enemy En in EnemiesToKill)
            {
                EnemyList.Remove(En);
            }

            if (EnemyList.Count <= 0)
            {
                CreateNewEnemyWave();
            }
        }

        private void CreateNewEnemyWave()
        {
            Random R = new Random();
            Point NewEnemyPosition;
            bool doesEnemyPlayerIntersect;
            Enemy NewEnemy;
            for (int i = 0; i < (waveNumber * 4); i++)
            {
                //Avoid spawning Enemy On Player
                do
                {
                    NewEnemyPosition = new Point(R.Next(Play.GameBoundaries.Left, Play.GameBoundaries.Right), R.Next(Play.GameBoundaries.Top, Play.GameBoundaries.Bottom));
                    NewEnemy = new Octorok(this.Game, NewEnemyPosition);  
                    doesEnemyPlayerIntersect = GameObject.Intersects(NewEnemy, Player);
                } 
                while (doesEnemyPlayerIntersect);
                                
                EnemyList.Add(NewEnemy);
            }

            waveNumber++;           
        }

        private void ProjectileUpdate()
        {
            #region Projectile Processing

            //Player Object Count
            int PlayerProjectileCount = 0;
            List<Projectile> ProjectilesToRemove = new List<Projectile>();
            foreach (Projectile P in ProjectileList)
            {
                if (P.isValidActive == false)
                {
                    ProjectilesToRemove.Add(P);
                }

                if (P.Owner == Player && P.isValidActive)
                {
                    PlayerProjectileCount++;
                }

                //check to see if player intersects projectile
                if(GameObject.Intersects(P,Player))
                {

                }
            }

            foreach (Projectile Pr in ProjectilesToRemove)
            {
               ProjectileList.Remove(Pr);               
            }

            //Check to see if Player is dropping an object
            if (Player.CharacterCreatedProjectile != null && PlayerProjectileCount == 0)
            {
                ProjectileList.Add(Player.CharacterCreatedProjectile);                
            }
            Player.CharacterCreatedProjectile = null;

            #endregion
        }

        /// <summary>
        /// Test function; just generate enemies if E is pressed.
        /// </summary>
        /// 
        /*
        private void AddNewEnemy()
        {
            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();
            //check for Hero Commands
            if (currentKeys.Length > 0)
            {
                foreach (Keys KeyPressed in currentKeys)
                {
                    if (KeyPressed == Keys.E)
                    {
                        Random R = new Random();

                       

                        Enemy NewEnemy = new Octorok(this.Game, new Point(R.Next(Play.GameBoundaries.Left, Play.GameBoundaries.Right), R.Next(Play.GameBoundaries.Top, Play.GameBoundaries.Bottom)));
                        EnemyList.Add(NewEnemy);                        
                    }
                }
            }
        }
        */
        private void PausePlay()
        {
            Player.Active = false;
            foreach (Enemy E in EnemyList)
            {
                E.Active = false;
            }
            //Load 
        }

        private void ResumePlay()
        {
            Player.Active = true;
            foreach (Enemy E in EnemyList)
            {
                E.Active = true;
            }
        }

        private void RunDeadState()
        {
            
            //this.MusicManager.Stop(Microsoft.Xna.Framework.Audio.AudioStopOptions.Immediate);
            //Cue Dead Music

            State = PlayState.Dead;
            Player.Active = false;
            //remove components ?       

            //Load User Dialog                      
            this.RemoveAllComponents();
            this.Game.Components.Add(new Menus.DeadMenu(this.Game));
        }

        private void RemoveAllComponents()
        {            
            Game.Components.Remove(CurrentScreen);            
            this.Game.Components.Remove(this);
        }

     
    }
}


