using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSTech2D.Engine
{
    public class RandomTimer
    {
        private int timeMin;
        private int timeMax;
        private Random rand;
        private float remainingSeconds;


        public RandomTimer(int timeMin, int timeMax)
        {
            this.timeMin = timeMin;
            this.timeMax = timeMax;
            this.rand = new Random();

            Reset();
        }

        public void Tick()
        {
            remainingSeconds -= Game.DeltaTime;
            if (remainingSeconds < 0) remainingSeconds = 0;
        }

        public bool IsOver()
        {
            return remainingSeconds <= 0;
        }

        public void Reset()
        {
            this.remainingSeconds = rand.Next(timeMin, timeMax);
        }
    }
}
