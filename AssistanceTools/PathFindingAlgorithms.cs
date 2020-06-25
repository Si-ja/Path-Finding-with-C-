using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Field;

namespace PathFinder.AssistanceTools
{
    /// <summary>
    /// Class containing path finding algorithms that can be used.
    /// Currently there is only 1 class: Slightly modified Breadth First Search
    /// </summary>
    class PathFindingAlgorithms
    {
        // This is the information that will not change

        private String[] directions = { "L", "U", "R", "D" }; // Possible directions we can move (Left, Up, Right, Down). Side moving is now allowed
        // Conditions on how one move can be made from a standing point (even though HashMaps might be more efficient, let's make it in 4 variables)
        // As this is a much smaller example. Representations is [X, Y]
        private int[] left = { -1, 0 };
        private int[] up = { 0, -1 };
        private int[] right = { 1, 0 };
        private int[] down = { 0, 1 };

        /// <summary>
        /// (Modified) Breadth First Search algorithm for path finding.
        /// </summary>
        /// <param name="maze">Original maze that needs to be traversed with Start and End point indicated in it.</param>
        /// <param name="key">Coordinates as stored in the main Program about the Starting point</param>
        /// <param name="gate">Coordinates as stored in the main Program about the End point</param>
        /// <returns>A string of directions, where each letter indicates what move to make (Up, Left, Down, Right) to get most
        /// optimally to the destination as we need it.</returns>
        public String BreadthFirstSearch(String[][] maze, int[] key, int[] gate)
        {
            // Create conditions from which we start the whole search
            MainField field = new MainField();
            Cell cell = new Cell();

            // Essentially a queue is used based on the First In First Out methods. It stores informaiton on all posible moves and updates in an iterative fashion
            List<String> queue = new List<String>();
            queue.Add("");
            int moves_added = 0;

            String answer = "";
            bool solution = false;

            Int32 start_x = key[0] / cell.width - 1;
            Int32 start_y = key[1] / cell.height - 1;

            Int32 end_x = gate[0] / cell.width - 1;
            Int32 end_y = gate[1] / cell.height - 1;

            // When all the variables are prepared - now it is possible to move through our maze.
            // Go through the maze until we find a solution (if we reach a dead point - it will be addressed further in the cycle)
            while (!solution)
            {
                // Take the first element in the queue and remove it
                String fifo = "";
                if (queue.Count > 0) // Do check if there are any alternative moves that can be build on top of existing or the pool of opportunities was cleared
                {
                    fifo = queue[0];
                    queue.RemoveAt(0);
                    moves_added = 0;
                }
                else
                {
                    return "There is no answer!";
                }

                // Now generate new possible directions that can be achieved with what we already know
                foreach (String direction in this.directions)
                {
                    // 1st MODIFICATION: Adding a statement for the algorithm to not go back and loop on itself...too much
                    // Essentially, we are telling the algorithm to never check back in the cell it just moved from. 
                    try
                    {
                        Char last_move_added = fifo[fifo.Length - 1];
                        Char new_move_added_opposite = new Char();
                        switch (direction)
                        {
                            case "L":
                                new_move_added_opposite = 'R';
                                break;
                            case "U":
                                new_move_added_opposite = 'D';
                                break;
                            case "R":
                                new_move_added_opposite = 'L';
                                break;
                            case "D":
                                new_move_added_opposite = 'U';
                                break;
                        }

                        if (last_move_added == new_move_added_opposite)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                        // Nothing
                    }

                    // Generate a new direction individually
                    String temp_direction = fifo + direction;
                    // Check if it is a valid one - as in one that can even exist
                    bool validPath = validPathCheck(path: temp_direction, maze: maze, start_x: start_x, start_y: start_y);
                    if (validPath)
                    {
                        queue.Add(temp_direction);
                        moves_added++;

                        // 2nd MODIFICATION: We additionally want to block the path where we went meaning we apply a "#" block to such field
                        // It makes sure the algorithm never goes in something of a cubical loop and will be forced to look arround for paths
                        // to the place it has already visited.
                        maze = UpdateMazeBlocks(path: temp_direction, maze: maze, start_x: start_x, start_y: start_y);
                    }
                }

                // Check if the queue is empty at this point, as if it is, no path can be found to our target
                if (queue.Count == 0)
                {
                    return "There is no answer!";
                }

                // Now we need to check if we have found the answer to our problem
                // Only check last 4 new added paths. Otherwise we overflow the amount of calculations that need to be performed
                for (int i = queue.Count; i > (queue.Count - moves_added); i--)
                {
                    String current_path_considered = queue[i - 1];
                    bool finalPath = finalPathCheck(path: current_path_considered, maze: maze, start_x: start_x, start_y: start_y, end_x: end_x, end_y: end_y);
                    if (finalPath)
                    {
                        solution = true;
                        answer = current_path_considered;
                    }
                }

            }
            return answer;
        }

        /// <summary>
        /// The visited paths are being blocked so they cannot be re-used again for new solutions finding.
        /// </summary>
        /// <param name="path">Path that needs to be checked.</param>
        /// <param name="maze">Maze that is being traversed</param>
        /// <param name="start_x">Coordinates of the Start point on X-axis</param>
        /// <param name="start_y">Coordinates of the Start point on Y-axis</param>
        /// <returns></returns>
        private String[][] UpdateMazeBlocks(String path, String[][] maze, int start_x, int start_y)
        {
            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'L':
                        start_x += this.left[0];
                        start_y += this.left[1];
                        break;
                    case 'U':
                        start_x += this.up[0];
                        start_y += this.up[1];
                        break;
                    case 'R':
                        start_x += this.right[0];
                        start_y += this.right[1];
                        break;
                    case 'D':
                        start_x += this.down[0];
                        start_y += this.down[1];
                        break;
                }
            }
            maze[start_y][start_x] = "#";
            return maze;

        }

        /// <summary>
        /// Check if the path that is given to our maze actually exists and reachable. We do not want to consider it if it hits a wall
        /// or a previously visited point.
        /// </summary>
        /// <param name="path">Path we want to check on wether it exists.</param>
        /// <param name="maze">Maze we want to traverse to check if our path is reachable in it</param>
        /// <param name="start_x">Position of our start point over th X-axis</param>
        /// <param name="start_y">Position of our start point over th Y-axis</param>
        /// <returns>true or false - validation on whether our path exists or not</returns>
        private bool validPathCheck(String path, String[][] maze, int start_x, int start_y)
        {
            bool validPath = false;
            // First understand what each letter in directions will mean for our initial position change
            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'L':
                        start_x += this.left[0];
                        start_y += this.left[1];
                        break;
                    case 'U':
                        start_x += this.up[0];
                        start_y += this.up[1];
                        break;
                    case 'R':
                        start_x += this.right[0];
                        start_y += this.right[1];
                        break;
                    case 'D':
                        start_x += this.down[0];
                        start_y += this.down[1];
                        break;
                }
            }
            try
            {
                if (maze[start_y][start_x] != "#")
                {
                    validPath = true;
                }
            }
            catch
            {
                // Nothing
            }
            return validPath;
        }

        /// <summary>
        /// Check if our final path hits the location where our end goal is.
        /// </summary>
        /// <param name="path">Path we want to check</param>
        /// <param name="maze">Maze we want to traverse</param>
        /// <param name="start_x">Start position on the X-axis</param>
        /// <param name="start_y">Start position on the Y-axis</param>
        /// <param name="end_x">End position on the X-axis</param>
        /// <param name="end_y">End position on the Y-axis</param>
        /// <returns>true or false - validation on whether our path hits the final end point or not</returns>
        private bool finalPathCheck(String path, String[][] maze, int start_x, int start_y, int end_x, int end_y)
        {
            bool finalPath = false;
            // We need to check where we will end up with in our path
            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'L':
                        start_x += this.left[0];
                        start_y += this.left[1];
                        break;
                    case 'U':
                        start_x += this.up[0];
                        start_y += this.up[1];
                        break;
                    case 'R':
                        start_x += this.right[0];
                        start_y += this.right[1];
                        break;
                    case 'D':
                        start_x += this.down[0];
                        start_y += this.down[1];
                        break;
                }
            }

            if (start_x == end_x && start_y == end_y)
            {
                finalPath = true;
            }

            return finalPath;
        }

        /// <summary>
        /// Generate a set of X coordinate movements that helps us reach the final point.
        /// </summary>
        /// <param name="path">Path we need to go through</param>
        /// <param name="key">Location of our start point as stored in the main program</param>
        /// <returns></returns>
        public List<int> DrawPath_X (String path, int[] key)
        {
            Cell cell = new Cell();
            Int32 start_x = key[0] / cell.width - 1;
            List<int> x_path = new List<int>();
            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'L':
                        start_x += this.left[0];
                        x_path.Add(start_x);
                        break;
                    case 'U':
                        start_x += this.up[0];
                        x_path.Add(start_x);
                        break;
                    case 'R':
                        start_x += this.right[0];
                        x_path.Add(start_x);
                        break;
                    case 'D':
                        start_x += this.down[0];
                        x_path.Add(start_x);
                        break;
                }
            }
            return x_path;
        }

        /// <summary>
        /// Generate a set of Y coordinate movements that helps us reach the final point.
        /// </summary>
        /// <param name="path">Path we need to go through</param>
        /// <param name="key">Location of our start point as stored in the main program</param>
        /// <returns></returns>
        public List<int> DrawPath_Y(String path, int[] key)
        {
            Cell cell = new Cell();
            Int32 start_y = key[1] / cell.height - 1;
            List<int> y_path = new List<int>();
            foreach (char direction in path)
            {
                switch (direction)
                {
                    case 'L':
                        start_y += this.left[1];
                        y_path.Add(start_y);
                        break;
                    case 'U':
                        start_y += this.up[1];
                        y_path.Add(start_y);
                        break;
                    case 'R':
                        start_y += this.right[1];
                        y_path.Add(start_y);
                        break;
                    case 'D':
                        start_y += this.down[1];
                        y_path.Add(start_y);
                        break;
                }
            }
            return y_path;
        }
    }
}