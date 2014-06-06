using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
 /*
namespace Test_Game.Device
{
    public enum ControllerType
    {
        Keyboard,GamePad
    }

    public enum NesButtons
    {
        A,B,Select,Start
    }

    public class Button
    {
        private NesButtons Val;
        private bool pressed = false;

        public NesButtons Value
        {
            get { return Val; }           
        }

       
        public bool IsPressed
        {

        }
       
        public Button()
        {
            
        }

        public Button(NesButtons Start)
        {
            Val = Start;
        }
    }

    public enum Direction
    {
        Up,Down,Left,Right,None
    }


    public class DirectionalPad
    {
        private Direction Dir;

        public Direction Direction
        {
            get { return Dir; }
            set { Dir = value; }
        }

        public DirectionalPad(Direction D)
        {
            Dir = D;
        }
    }

    public class Controller : Microsoft.Xna.Framework.GameComponent
    {
        private ControllerType Type;

        private Button Abutton = new Button(NesButtons.A);
        private Button Bbutton = new Button(NesButtons.B);
        private Button Selectbutton = new Button(NesButtons.Select);
        private Button Startbutton = new Button(NesButtons.Start);
        private DirectionalPad Dirpad = new DirectionalPad(Direction.None);

        public ControllerType ControlType
        {
            get { return Type; }
        }

        public Controller()
        {
            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                Type = ControllerType.GamePad;
            }
            else
            {
                Type = ControllerType.Keyboard;                
            }
        }

        //here we map the controller to the buttons on the NES




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here           

            if (ControlType == ControllerType.Keyboard)
            {
                //check buttons
                KeyboardState currentState = Keyboard.GetState();
                Keys[] currentKeys = currentState.GetPressedKeys();
                //check for up and down arrow keys
                foreach (Keys key in currentKeys)
                {
                    //Directional Arrows
                    switch (key)
                    {
                        case Keys.Left:
                            Dirpad.Direction = Direction.Left;
                            break;
                        case Keys.Right:
                            Dirpad.Direction = Direction.Right;
                            break;
                        case Keys.Up:
                            Dirpad.Direction = Direction.Up;
                            break;
                        case Keys.Down:
                            Dirpad.Direction = Direction.Down;
                            break;
                        default:
                            Dirpad.Direction = Direction.None;
                    }

                    //Buttons
                    switch (key) 
                    { 
                        case Keys.A:
                            Abutton.Value = NesButtons.A;
                        case Keys.B:
                            Bbutton.Value = NesButtons.B;
                        case Keys.Enter:
                            Startbutton.Value = NesButtons.Start;

                    
                    
                    }
                       
                }
            }
            else
            {                
                //foreach(){


                //}
            }

            base.Update(gameTime);
        }


    }
}
 */