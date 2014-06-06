
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Test_Game.World
{   
    public partial class ScreenTile 
    {

        /// <summary>
        /// the on-screen appearance of the Tile
        /// </summary>
        public Texture2D Texture;

        /// <summary>
        /// Whether the tile is passable by the player
        /// </summary>
        public Boolean Passable;

        /// <summary>
        /// The tile's position in the playing area.
        /// </summary>
        public Point Position;

        protected int TileNumber;

        /// <summary>
        /// Individual Landscape Tiles.
        /// </summary>
        /// <param name="game"></param>
        public ScreenTile(Microsoft.Xna.Framework.Game game, int TileNum, Texture2D Tex)            
        {
            TileNumber = TileNum;
            //Layout the tiles according to what number tile this is.
            Position.X = ((TileNumber % 16 ) * 32);
            //the plus 3 here moves the map below the status bar at the top
            Position.Y = (((TileNumber / 16 ) + 3) * 32)  ;
            // TODO: Construct any child components here
            this.Texture = Tex;

        }
        
    }
}


