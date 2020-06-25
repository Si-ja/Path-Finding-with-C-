using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    class CornerRoom : Room
    {
        /// <summary>
        /// Class holding information on how a room with inside corners looks like
        /// </summary>
        public override bool[][] room { get; set; } = new bool[5][];
        public override string room_name { get; set; } = "L shaped Room";
        public CornerRoom()
        {
            InitializeCornerRoom();
        }

        private void InitializeCornerRoom()
        {
            room[0] = new bool[5] { true, true, true, true, true };
            room[1] = new bool[5] { true, true, false, true, true };
            room[2] = new bool[5] { true, false, false, false, true };
            room[3] = new bool[5] { true, true, false, true, true };
            room[4] = new bool[5] { true, true, true, true, true };
        }
    }
}
