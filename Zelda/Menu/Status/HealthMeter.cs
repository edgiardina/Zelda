using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Game.Menu.Status
{
    /// <summary>
    /// Health Meter is the display of hearts that shows up in the Status Display bar while playing / paused
    /// </summary>
    class HealthMeter : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private int NumberOfContainerHearts;

        public HealthMeter(Microsoft.Xna.Framework.Game game)
            : base(game)
        {


        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            
            base.Initialize();
        }

    }
}
