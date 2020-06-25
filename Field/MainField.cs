using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Field
{
    /// <summary>
    /// Class allowing to represent the size of the field in which the whole simulation process takes place
    /// </summary>
    class MainField
    {
        public int x { get; set; } = 16; // Initial start point at X-axis
        public int y { get; set; } = 16; // Initial start point at Y-axis
        public int width { get; set; } = 416;  // End point of X-axis
        public int height { get; set; } = 416; // End point of Y-axis
    }
}
