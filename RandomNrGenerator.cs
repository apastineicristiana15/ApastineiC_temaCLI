using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temaLab4
{
    internal class RandomNrGenerator
    {
        private Random rand;

        public RandomNrGenerator()
        {
            rand = new Random();
        }

        public int GetRandomInt(int low, int high)
        {
            return rand.Next(low, high);
        }

        public Color GetRandomColor()
        {
            return Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

    }
}
