using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.AssistanceTools
{
    /// <summary>
    /// Prepare an initial fully empty template for information on how the rooms are structured.
    /// </summary>
    class InitialTemplate
    {
        /// <summary>
        /// Creates a 25 x 25 array of empty values. Essentially a field where new walls and empty spaces can be generated.
        /// </summary>
        /// <returns>An array[25][25] of boolean values, all set to false indicating that the map is fully empty at this point.</returns>
        public static bool[][] prepareEmptyTemplate()
        {
            bool[][] emptyTemplate = new bool[25][];
            for (int i = 0; i < 25; i++)
            {
                List<bool> tempEntries = new List<bool>();
                for (int j = 0; j < 25; j++)
                {
                    tempEntries.Add(false);
                }
                bool[] tempEntriesArray = tempEntries.ToArray();
                emptyTemplate[i] = tempEntriesArray;
            }
            return emptyTemplate;
        }
    }
}
