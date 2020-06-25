using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    /// <summary>
    /// Class holding information on how a Long (Type 2) shaped room looks
    /// </summary>
    public class LongRoom2 : Room
    {
            public override bool[][] room { get; set; } = new bool[5][];
            public override String room_name { get; set; } = "Long (2) Room";
            public LongRoom2()
            {
                InitializeLong2Room();
            }

            private void InitializeLong2Room()
            {
                room[0] = new bool[5] { false, true, true, true, false };
                room[1] = new bool[5] { false, true, false, true, false };
                room[2] = new bool[5] { false, true, false, true, false };
                room[3] = new bool[5] { false, true, false, true, false };
                room[4] = new bool[5] { false, true, true, true, false };
            }
    }
}
