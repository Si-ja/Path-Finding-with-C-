using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    /// <summary>
    /// Class holding information on how an L room looks in the variable "room".
    /// </summary>
    public class LRoom : Room
    {
        public override bool[][] room { get; set; } = new bool[5][];
        public override String room_name { get; set; } = "L shaped Room";
        public LRoom()
        {
            InitializeLRoom();
        }

        private void InitializeLRoom()
        {
            room[0] = new bool[3] { true, true, true };
            room[1] = new bool[3] { true, false, true };
            room[2] = new bool[5] { true, false, true, true, true };
            room[3] = new bool[5] { true, false, false, false, true };
            room[4] = new bool[5] { true, true, true, true, true };
        }
    }
}
