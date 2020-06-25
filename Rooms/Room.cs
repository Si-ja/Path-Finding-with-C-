using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Rooms
{
    /// <summary>
    /// Original Abstract class from which all rooms are build. A room needs to have walls and a name.
    /// </summary>
    public abstract class Room
    {
        public abstract bool[][] room { get; set; }
        public abstract String room_name { get; set; }
    }
}
