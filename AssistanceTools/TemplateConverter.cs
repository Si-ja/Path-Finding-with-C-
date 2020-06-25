using PathFinder.Field;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Field;

namespace PathFinder.AssistanceTools
{
    /// <summary>
    /// Class that helps convert our original boolean array into a string array with more options to how it can be built.
    /// </summary>
    class TemplateConverter
    {
        /// <summary>
        /// Method that helps change the representation of the boolean array into a String type. All [true] values are replaced with "#"
        /// and every other space defined with [false] and representing and empty space is represnented with an empty string of size 1: " ";
        /// </summary>
        /// <param name="original_template">An original array is required of bool[][] type</param>
        /// <returns>Return gives an updated version of the 2D array but in a String format.</returns>
        public static String[][] redefineTemplate(bool[][] original_template)
        {
            String[][] new_Template = new String[25][];
            for (int i = 0; i < original_template.Length; i++)
            {
                List<String> tempEntries = new List<String>();
                for (int j = 0; j < original_template[i].Length; j++)
                {
                    tempEntries.Add(" ");
                }
                String[] tempEntriesArray = tempEntries.ToArray();
                new_Template[i] = tempEntriesArray;
            }



            for (int y = 0; y < original_template.Length; y++)
            {
                for (int x = 0; x < original_template[y].Length; x++)
                {
                    if (original_template[y][x] == true)
                    {
                        new_Template[y][x] = "#";
                    }
                }
            }
            return new_Template;
        }

        /// <summary>
        /// The new template will update the old maze, but will add the position of the start key and the end gate.
        /// Those will be represented in the maze with a "O" and "X" respectivelly. 
        /// </summary>
        /// <param name="given_Template">The 2D String[][] of the original maze already converted from the previous bool[][] state</param>
        /// <param name="key">Instance of the key/start and it's coordinates where it is placed on the map.</param>
        /// <param name="gate">Instance of the gate/end and it's coordinates where it is placed on the map.</param>
        /// <returns></returns>
        public static String[][] StartFinishAdder(String[][] given_Template, int[] key, int[] gate)
        {
            Cell cell = new Cell();
            String[][] new_Template = new String[25][];
            for (int i = 0; i < given_Template.Length; i++)
            {
                List<String> tempEntries = new List<String>();
                for (int j = 0; j < given_Template[i].Length; j++)
                {
                    tempEntries.Add(" ");
                }
                String[] tempEntriesArray = tempEntries.ToArray();
                new_Template[i] = tempEntriesArray;
            }

            for (int y = 0; y < given_Template.Length; y++)
            {
                for (int x = 0; x < given_Template[y].Length; x++)
                {
                    if (given_Template[y][x] == " ")
                    {
                        new_Template[y][x] = " ";
                    }
                    else if (given_Template[y][x] == "#")
                    {
                        new_Template[y][x] = "#";
                    }
                }
            }

            // Now draw the Start and finish
            Int32 start_x = key[0] / cell.width - 1;
            Int32 start_y = key[1] / cell.height - 1;

            Int32 end_x = gate[0] / cell.width - 1;
            Int32 end_y = gate[1] / cell.height - 1;

            new_Template[start_y][start_x] = "O";
            new_Template[end_y][end_x] = "X";
            return new_Template;
        }
    }
}
