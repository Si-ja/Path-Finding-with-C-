using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    class FullPlusRoom : Room
    {
        /// <summary>
        /// Class holding information on how a Plus shaped room looks like"
        /// </summary>
        public override bool[][] room { get; set; } = new bool[5][];
        public override String room_name { get; set; } = "Plus shaped Room";
        public FullPlusRoom()
        {
            InitializeFullPlusRoom();
        }

        private void InitializeFullPlusRoom()
        {
            room[0] = new bool[5] { true, true, true, true, true };
            room[1] = new bool[5] { true, false, true, false, true };
            room[2] = new bool[5] { true, true, true, true, true };
            room[3] = new bool[5] { true, false, true, false, true };
            room[4] = new bool[5] { true, true, true, true, true };
        }
    }
}
