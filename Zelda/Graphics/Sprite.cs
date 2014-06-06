using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test_Game.Graphics
{
    /*
    public class GameTexture : Texture2D
    {
        protected int NumberOfFrames;
        protected int CurrentFrame;     
    }
     * */

    /// <summary>
    /// A 2D Texture with metadata such as number of frames, viewable area, etc.
    /// </summary>
    public class Sprite
    {
        public Texture2D Texture;
        public int CountOfFrames;
        public int CurrentFrame;
        public int Width;
        public int Height;
        private Vector2 CenterOfSprite;

        public void Dispose()
        {
            Texture.Dispose();
        }

        /// <summary>
        /// Create a Sprite that can animate
        /// </summary>
        /// <param name="Tex"></param>
        /// <param name="FrameCount"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>        
        public Sprite(Texture2D Tex, int FrameCount, int Width, int Height)
        {
            Texture = Tex;
            this.CurrentFrame = 0;
            this.CountOfFrames = FrameCount;
            this.Width = Width;
            this.Height = Height;
            CenterOfSprite = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Create a non-Animating Sprite
        /// </summary>
        /// <param name="Tex">The Texture2D object to display with this sprite</param>
        public Sprite(Texture2D Tex)
        {
            Texture = Tex;
            this.CurrentFrame = 0;
            this.CountOfFrames = 1;
            this.Width = Tex.Width;
            this.Height = Tex.Height;
            CenterOfSprite = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Draw the Current Sprite in the provided SpriteBatch
        /// </summary>
        /// <param name="Batch">SpriteBatch to use to render</param>
        /// <param name="Pt">Point on the 2d Plane to Draw the sprite</param>
        public void Draw(SpriteBatch Batch, Point Pt)
        {
            Batch.Draw(this.Texture, new Rectangle(Pt.X, Pt.Y, this.Width, this.Height), new Rectangle(this.CurrentFrame * this.Width, 0, this.Width, this.Height), Color.White, MathHelper.ToRadians(0), CenterOfSprite, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the Current Sprite in the provided SpriteBatch
        /// </summary>
        /// <param name="Batch">SpriteBatch to use to render</param>
        /// <param name="Pt">Point on the 2d Plane to Draw the sprite</param>
        /// <param name="BackgroundColor">Background color to use when drawing</param>
        public void Draw(SpriteBatch Batch, Point Pt, Color BackgroundColor)
        {
            Batch.Draw(this.Texture, new Rectangle(Pt.X, Pt.Y, this.Width, this.Height), new Rectangle(this.CurrentFrame * this.Width, 0, this.Width, this.Height), BackgroundColor, MathHelper.ToRadians(0), CenterOfSprite, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the current Sprite with a specific Color and Intensity of that color
        /// </summary>
        /// <param name="Batch">SpriteBatch to use to render</param>
        /// <param name="Pt">Point on the 2d Plane to Draw the sprite</param>
        /// <param name="SpriteColor">Color to Tint the Sprite</param>
        /// <param name="ColorStrength">Floating Point 0 - 255 Value of Intensity of Tint</param>
        public void Draw(SpriteBatch Batch, Point Pt, Color SpriteColor, float ColorStrength)
        {
            Batch.Draw(this.Texture, new Rectangle(Pt.X, Pt.Y, this.Width, this.Height), new Rectangle(this.CurrentFrame * this.Width, 0, this.Width, this.Height), new Color(SpriteColor.R, SpriteColor.G, SpriteColor.B, (byte)ColorStrength), MathHelper.ToRadians(0), CenterOfSprite, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw the Current Sprite in the provided SpriteBatch, in a certain direction. Note some
        /// GameObjects can be just pointed, and the sprite automatically rotated since it is just a rotated sprite, like the octorok
        /// </summary>
        /// <param name="Batch">SpriteBatch to use to render</param>
        /// <param name="Pt">Point on the 2d Plane to Draw the sprite</param>  
        /// <param name="Direction">Direction to Face the Sprite</param>
        public void Draw(SpriteBatch Batch, Point Pt, Direction Direction)
        {
            //assume pointing up?
            float AngleToPoint = MathHelper.ToRadians(180);
            //Vector2 VectorToRotate = new Vector2(this.Width, this.Height);

            if (Direction == Direction.Down)
            {
                AngleToPoint = MathHelper.ToRadians(0);
                //VectorToRotate = new Vector2(0, 0);
            }
            else if (Direction == Direction.Left)
            {
                AngleToPoint = MathHelper.ToRadians(90);
                //VectorToRotate = new Vector2(0, this.Height);
            }
            else if (Direction == Direction.Right)
            {
                AngleToPoint = MathHelper.ToRadians(270);
                //VectorToRotate = new Vector2(this.Width, 0);
            }

            Batch.Draw(this.Texture, new Rectangle(Pt.X, Pt.Y, this.Width, this.Height), new Rectangle(this.CurrentFrame * this.Width, 0, this.Width, this.Height), Color.White, AngleToPoint, CenterOfSprite, SpriteEffects.None, 0);
        }

    }
}
