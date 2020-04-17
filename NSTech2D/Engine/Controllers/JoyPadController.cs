using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.Controllers
{
    class JoyPadController : Controller
    {
        public JoyPadController(int controllerIndex) : base(controllerIndex)
        {
        }

        public override float GetHorizontal()
        {
            float direction;

            if (Game.Win.JoystickRight(index))
            {
                direction = 1;
            }
            else if (Game.Win.JoystickLeft(index))
            {
                direction = -1;
            }
            else
            {
                direction = Game.Win.JoystickAxisLeft(index).X;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction;

            if (Game.Win.JoystickUp(index))
            {
                direction = -1;
            }
            else if (Game.Win.JoystickDown(index))
            {
                direction = 1;
            }
            else
            {
                direction = Game.Win.JoystickAxisLeft(index).Y;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Win.JoystickA(index);
        }

        public override bool IsJumpPressed()
        {
            return Game.Win.JoystickB(index);
        }
    }
}
