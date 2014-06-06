
#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Test_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public partial class TestGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        private static ContentManager content;

        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);
            
            //Set Window size to be NES resolution x 2 (since all textures are already scaled)
            graphics.PreferredBackBufferHeight = 720; // 448;
            graphics.PreferredBackBufferWidth = 1280; //512;
           
            content = new ContentManager(Services);
           
            LoadGame();
            Components.Add(new GamerServicesComponent(this));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Music.Initialize();
            base.Initialize();

        }

        /// <summary>
        /// Load your graphics content.  If  is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="">Which type of content to load.</param>
        protected override void LoadContent()
        {
           
        }


        /// <summary>
        /// Unload your graphics content.  If un is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="un">Which type of content to unload.</param>
        protected override void UnloadContent()
        {
            content.Unload();
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //check to see if the user is quitting
            CheckQuit();

            /* Update Game Objects */
            //Music.Update();

            //GamerServicesDispatcher.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// Checks to see if the user has indicated that he/she wishes to quit the game.
        /// </summary>
        private void CheckQuit()
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();
            //check for up and down arrow keys
            foreach (Keys key in currentKeys)
            {
                if (key == Keys.Escape)
                    this.Exit();
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }

        void LoadGame()
        {
            //First thing we push to the user is the title screen. The title screen effectively
            //listens for movement to other screens, accessing its parents class components and 
            //pushing/popping them on/off the stack as needed.
            this.Components.Add(new TitleScreen(this));
        }

        /// <summary>
        /// Content Manager Singleton
        /// </summary>
        public static ContentManager ContentMgr
        {
            get { return content as ContentManager; }
        }

    }
}