
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Test_Game.Menus;
#endregion

namespace Test_Game.World
{
    /// <summary>
    /// This holds the specific area of the map the user is viewing.
    /// </summary>
    public partial class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Menus.StatusDisplay Display;

        SpriteBatch SpriteContainer;
        /// <summary>
        /// Tiles that make up landscape (trees, rocks, plain field, etc)
        /// </summary>
        public List<ScreenTile> Tiles = new List<ScreenTile>();

        /// <summary>
        /// How big the game tiles are
        /// </summary>
        private Point TileSize = new Point(16, 16);
        /// <summary>
        /// How many tiles for this screen. 
        /// </summary>
        private const int TilesOnScreen = 360;

        public Screen(Microsoft.Xna.Framework.Game game)
            : base(game)
        {

        }

        protected override void LoadContent()
        {
            SpriteContainer = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        /// <summary>
        /// Load the individual tile elements onto the screen
        /// </summary>
        private void LoadTiles()
        {
            for (int i = 0; i < TilesOnScreen; i++)
            {
                Tiles.Add(new ScreenTile(this.Game, i, Game.Content.Load<Texture2D>("Sprites\\MapTiles\\level_004")));
            }           
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteContainer.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            foreach (ScreenTile Tile in Tiles)
            {
                SpriteContainer.Draw(Tile.Texture, new Rectangle(Tile.Position.X, Tile.Position.Y, Tile.Texture.Height * 2, Tile.Texture.Width * 2), Color.White);
            }
            SpriteContainer.End();
            Display.Draw(gameTime);            
            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            LoadTiles();
            Display = new StatusDisplay(this.Game);
            Display.Initialize();
            base.Initialize();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Display.Update(gameTime);
            base.Update(gameTime);
        }
    }
}


