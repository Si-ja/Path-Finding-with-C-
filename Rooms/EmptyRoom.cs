using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    class EmptyRoom : Room
    {
        /// <summary>
        /// Class holding information on how an empty room looks like.
        /// </summary>
        public override bool[][] room { get; set; } = new bool[5][];
        public override String room_name { get; set; } = "Empty Room";
        public EmptyRoom()
        {
            InitializeEmptyRoom();
        }

        private void InitializeEmptyRoom()
        {
            room[0] = new bool[5] { false, false, false, false, false };
            room[1] = new bool[5] { false, false, false, false, false };
            room[2] = new bool[5] { false, false, false, false, false };
            room[3] = new bool[5] { false, false, false, false, false };
            room[4] = new bool[5] { false, false, false, false, false };
        }
    }
}
