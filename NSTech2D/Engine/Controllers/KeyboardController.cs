using NSTech2D.RenderEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine.Controllers
{
    class KeyboardController : Controller
    {
        public KeyboardController(int controllerIndex) : base(controllerIndex)
        {
        }

        public override float GetHorizontal()
        {
            float direction = 0;

            if (Game.Win.GetKey(KeyCode.D))
            {
                direction = 1;
            }
            else if (Game.Win.GetKey(KeyCode.A))
            {
                direction = -1;
            }

            return direction;
        }

        public override float GetVertical()
        {
            float direction = 0;

            if (Game.Win.GetKey(KeyCode.W))
            {
                direction = -1;
            }
            else if (Game.Win.GetKey(KeyCode.S))
            {
                direction = 1;
            }

            return direction;
        }

        public override bool IsFirePressed()
        {
            return Game.Win.GetKey(KeyCode.SpaceBar);
        }
        public override bool IsJumpPressed()
        {
            return Game.Win.GetKey(KeyCode.W);
        }
    }
}
