using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Test_Game.Menus
{
    class DeadMenu : Menu
    {
         public DeadMenu(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            Options = new List<MenuOption>();
            Options.Add(new MenuOption(new Point(125, 70),  MenuOptionType.Retry, "Retry"));
            Options.Add(new MenuOption(new Point(125, 140), MenuOptionType.Save, "Save"));
            Options.Add(new MenuOption(new Point(125, 210), MenuOptionType.Quit, "Quit"));
          
            CurrentOption = Options[0];
            CurrentOptionIndex = 0;
            CursorPosition = CurrentOption.Position;
        }

        protected override void ExecuteMenuItem()
        {
            if (CurrentOption.Type == MenuOptionType.Retry)
            {
                //StartGame For now; Register Later. 
                this.Game.Components.Remove(this);
                this.Game.Components.Add(new World.Play(this.Game));
            }

            if (CurrentOption.Type == MenuOptionType.Quit)
            {
                //StartGame For now; Register Later. 
                Game.Exit();
            }

            base.ExecuteMenuItem();
        }

    }
}
