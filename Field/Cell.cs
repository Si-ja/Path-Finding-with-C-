using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Field
{
    /// <summary>
    /// Class representing how individual cells act and how much space they take in the whole program
    /// </summary>
    class Cell
    {
        // Tracker that our pixels are 16x16
        public int width { get; set; } = 16;
        public int height { get; set; } = 16;
        public int pixels { get; set; } = 25;
    }
}
