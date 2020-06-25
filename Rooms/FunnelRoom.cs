using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    /// <summary>
    /// Class holding information on how a Funnel shaped room looks
    /// </summary>
    public class FunnelRoom : Room
    {
            public override bool[][] room { get; set; } = new bool[5][];
            public override String room_name { get; set; } = "Funnel Room";
            public FunnelRoom()
            {
            InitializeFunnelRoom();
            }

            private void InitializeFunnelRoom()
            {
                room[0] = new bool[5] { true, true, true, true, true };
                room[1] = new bool[5] { true, false, false, false, true };
                room[2] = new bool[5] { true, true, false, true, true };
                room[3] = new bool[5] { false, true, false, true, false };
                room[4] = new bool[5] { false, true, true, true, false };
            }
    }
}
