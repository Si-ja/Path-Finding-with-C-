using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    /// <summary>
    /// Class holding information on how an X shaped room looks in the variable "room".
    /// </summary>
    class XRoom : Room
    {
        public override bool[][] room { get; set; } = new bool[5][];
        public override string room_name { get; set; } = "X shaped Room";
        public XRoom()
        {
            InitializeXRoom();
        }

        private void InitializeXRoom()
        {
            room[0] = new bool[5] { true, true, true, true, true };
            room[1] = new bool[5] { true, true, false, true, true };
            room[2] = new bool[5] { false, false, true, false, false };
            room[3] = new bool[5] { true, true, false, true, true };
            room[4] = new bool[5] { true, true, true, true, true };
        }
    }
}
