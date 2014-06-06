using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace Test_Game.Menus
{
    public partial class MainMenu : Menu
    {
        public MainMenu(Microsoft.Xna.Framework.Game game)
            : base(game)
        {
            Options = new List<MenuOption>();
            Options.Add(new MenuOption(new Point(125, 70), MenuOptionType.SaveGame1, "Save 1"));
            Options.Add(new MenuOption(new Point(125, 140), MenuOptionType.SaveGame2, "Save 2"));
            Options.Add(new MenuOption(new Point(125, 210), MenuOptionType.SaveGame3, "Save 3"));
            Options.Add(new MenuOption(new Point(125, 280), MenuOptionType.Copy, "Copy"));
            Options.Add(new MenuOption(new Point(125, 300), MenuOptionType.Delete, "Delete"));
            Options.Add(new MenuOption(new Point(125, 320), MenuOptionType.Register, "Register"));

            CurrentOption = Options[0];
            CurrentOptionIndex = 0;
            CursorPosition = CurrentOption.Position;

        }

        protected override void ExecuteMenuItem()
        {
            if (CurrentOption.Type == MenuOptionType.Register)
            {
                //StartGame For now; Register Later. 
                this.Game.Components.Remove(this);
                this.Game.Components.Add(new World.Play(this.Game));
            }
            else if (CurrentOption.Type == MenuOptionType.Copy)
            {
                if (!Guide.IsVisible)
                {
                    //Guide.BeginShowKeyboardInput(PlayerIndex.One, "Input", "Input", "Input", null, null);
                    Guide.ShowSignIn(1, false);                    
                }               
            }
            else if (CurrentOption.Type == MenuOptionType.Delete)
            {
                /*
                if (!Guide.IsVisible)
                {
                    if (Gamer.SignedInGamers.Count > 0)
                    {
                        Guide.ShowPlayers(PlayerIndex.One);                        
                    }
                    else
                    {
                        Guide.ShowSignIn(1, false);                         
                    }
                }
                 * */
            }

            base.ExecuteMenuItem();
        }

    }
}
