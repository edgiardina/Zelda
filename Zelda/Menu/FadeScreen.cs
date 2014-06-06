using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace Test_Game.Menu
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FadeScreen : View
    {
        private float FadeAmount;
        private Texture2D FadeTexture;
        private Color ColorToFade;
        private View TransitionScreen;
        private double FadeStartTime;
        private View OldScreen;

        public FadeScreen(Game game, View ScreenFrom, View ScreenToTransitionTo, Color ColorToFadeWith)            :base(game)
        {
            // TODO: Construct any child components here
            this.TransitionScreen = ScreenToTransitionTo;
            this.ColorToFade = ColorToFadeWith;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            FadeAmount += (.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (FadeAmount > 1.5)
            {
                this.Game.Components.Remove(this);
                this.Game.Components.Remove(OldScreen);
                this.Game.Components.Add(TransitionScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int width = GraphicsDevice.Viewport.Width / 2;
            int height = GraphicsDevice.Viewport.Height / 2;
            SpriteContainer.Begin(SpriteBlendMode.AlphaBlend);

            Vector4 NewColor = ColorToFade.ToVector4();
            //Set Transparency
            NewColor.W = FadeAmount; 
            SpriteContainer.Draw(FadeTexture,Vector2.Zero,new Color(NewColor));
            SpriteContainer.End();
            base.Draw(gameTime);
        }

        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            FadeTexture = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 1, TextureUsage.None, SurfaceFormat.Color);
            int PixelCount = GraphicsDevice.Viewport.Width * GraphicsDevice.Viewport.Height;
            Color[] PixelData = new Color[PixelCount];
            //Random RndNum = new Random();

            for (int i = 0; i < PixelCount; i++)
            {
                PixelData[i] = this.ColorToFade;
            }

            FadeTexture.SetData<Color>(PixelData);

            base.LoadGraphicsContent(loadAllContent);
        }
    }
}