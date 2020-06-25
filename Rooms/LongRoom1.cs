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
    public class LongRoom1 : Room
    {
            public override bool[][] room { get; set; } = new bool[5][];
            public override String room_name { get; set; } = "Long (1) Room";
            public LongRoom1()
            {
                InitializeLong1Room();
            }

            private void InitializeLong1Room()
            {
                room[0] = new bool[5] { false, false, false, false, false };
                room[1] = new bool[5] { true, true, true, true, true };
                room[2] = new bool[5] { true, false, false, false, true };
                room[3] = new bool[5] { true, true, true, true, true };
                room[4] = new bool[5] { false, false, false, false, false };
            }
    }
}
