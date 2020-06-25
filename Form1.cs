using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PathFinder.Rooms;
using PathFinder.Field;
using System.Collections;
using System.Drawing.Imaging;
using PathFinder.AssistanceTools;
using System.IO;
using System.Globalization;

namespace PathFinder
{
    public partial class PathFinder : Form
    {
        // Original Template of the map build with [true] - where there is a wall and [false] - where there is an empty space
        public bool[][] rooms_template = new bool[25][];

        // Remade template of the original map, which holds "#" where there is a wall, " " - where there is a space, 
        // "O" - where there is a start point and "X" for where there is an end point
        public String[][] rooms_template_redux = new String[25][];

        public int[] current_key = new int[2] { 0, 0 };  // Coordinates where on a map a start point is located
        public int[] current_gate = new int[2] { 0, 0 }; // Coordinates where on a map an end point is located

        public PathFinder()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Pass
        }

        private void PathFinder_Load(object sender, EventArgs e)
        {
            // Make sure the combo box for drawing of the start and finish points has a pre-selected value when the program initiates
            cBox_Objects.SelectedIndex = 0;
        }

        /// <summary>
        /// A button that generates the field in which the whole path finding process can be organized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Generate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            MainField field = new MainField();
            Cell cell = new Cell();
            List<Room> rooms = new List<Room>();
            Graphics g = this.CreateGraphics();
            lbl_Status.Text = "Status: Nothing";

            // Reset the key and gate positions with each new map created (start and end points)
            this.current_key[0] = 0;
            this.current_key[1] = 0;

            this.current_gate[0] = 0;
            this.current_gate[1] = 0;

            this.rooms_template = InitialTemplate.prepareEmptyTemplate();

            /** Generate a second copy of an empty array that will later match the original one. It will be needed to clear spaces
            For connecting joins between the rooms. This can be only properly done if one map will never change after being populated
             As dynamically changing map would require a much bigger set of rules to account for when looking where room connections are present.
            **/
            bool[][] rooms_template_permenant = new bool[25][];
            rooms_template_permenant = InitialTemplate.prepareEmptyTemplate();

            /** Initiate an array of all possible room types that can be generated. The names are only representations of rooms
            as a random number generator will allow to pick one by one a room for generation and based on which room is picked
            it will be added to the collection of all rooms for geeneration. Which will later be drawn and connected among each other
            if connections between them actually exist
            **/
            String[] choices = { "Square", "Plus", "L", "Corner", "Long1", "Long2", "Funnel", "Empty" }; // X, FullPlus - redacted

            // ------------------ PREPARE THE MAZE --------------------------------
            // Generate a list of rooms with their information. The whole screen should have 25 rooms
            for (int i = 0; i < 25; i++)
            {
                // Generate a random number that will decide what the room will be
                int room_value = rnd.Next(0, choices.Length);
                String chosen_room = choices[room_value];

                // Add the chosen room to the whole collection
                switch (chosen_room)
                {
                    case "Square":
                        SquareRoom squareRoom = new SquareRoom();
                        rooms.Add(squareRoom);
                        break;
                    case "Plus":
                        PlusRoom plusRoom = new PlusRoom();
                        rooms.Add(plusRoom);
                        break;
                    case "L":
                        LRoom lRoom = new LRoom();
                        rooms.Add(lRoom);
                        break;
                    case "Empty":
                        EmptyRoom emptyRoom = new EmptyRoom();
                        rooms.Add(emptyRoom);
                        break;
                    case "Corner":
                        CornerRoom cornerRoom = new CornerRoom();
                        rooms.Add(cornerRoom);
                        break;
                    case "Long1":
                        LongRoom1 longRoom1 = new LongRoom1();
                        rooms.Add(longRoom1);
                        break;
                    case "Long2":
                        LongRoom2 longRoom2 = new LongRoom2();
                        rooms.Add(longRoom2);
                        break;
                    case "FullPlus":
                        FullPlusRoom fullPlusRoom = new FullPlusRoom();
                        rooms.Add(fullPlusRoom);
                        break;
                    case "Funnel":
                        FunnelRoom funnelRoom = new FunnelRoom();
                        rooms.Add(funnelRoom);
                        break;
                    case "X":
                        XRoom xRoom = new XRoom();
                        rooms.Add(xRoom);
                        break;
                }
            }

            /** Populate the information about walls that rooms add to the map in a matrix format (2D array to be more speicific here)
            We iterrate through rooms for each case and build it all into a sort of a coordinate matrix type deal
            **/
            int row = 0;
            int col = 0;

            int true_row = 0;
            int true_col = 0;

            foreach (Room room in rooms)
            {
                /**
                 * rooms are build itteratevelly cell by cell, left to right. Consider the following example of a 2x2 room:
                 * Step 1: #
                 * 
                 * Step 2: ##
                 * 
                 * Step 3: ##
                 *         #
                 *         
                 * Step 4: ##
                 *         ##
                 *         
                 * Finish
                 * Same applies for 5 by 5. When the next room is processed, it is built in the same fashion next to the original one or down
                 * if 5 rooms per line have been constructed
                 **/
                // In the beginning we reset our position to what we are filling now on the y axis
                int position_y = 16 + (5 * cell.height * col);

                // While we still have new space to fill in a designated area - means our room is not fully drawn
                foreach (bool[] roomLine in room.room) // roomLine - just means a specific row we are moving by building a room
                {
                    // We move through each value in a row
                    int position_x = 16 + (5 * cell.width * row);
                    foreach (bool roomIndividual in roomLine) // roomIndividual - just means a seperate room cell we fill by moving through the previous line
                    {
                        if (roomIndividual)
                        {
                            true_row = (position_x / cell.width) - 1;
                            true_col = (position_y / cell.height) - 1;
                            this.rooms_template[true_row][true_col] = roomIndividual;
                            rooms_template_permenant[true_row][true_col] = roomIndividual;
                        }
                        position_x += cell.width;
                    }
                    position_y += cell.height;
                }
                row += 1;
                if (row % 5 == 0)
                {
                    row = 0;
                    col += 1;
                }
            }

            // ------------------ CLEANING THE MAZE --------------------------------
            /**
             * Now the goal is to go over the maze again, applying few rules and deleting spaces between rooms that can be easily connected.
             * In reality, there are only 4 rules that need to be applied to clean the maze.
             * Condition: All rooms are closed, and their external walls need to be evaluated for whether they need to be taken down or not.
             * The walls that need to be evaluated are always located on the X axis on lines: 0, 4, 5, 9, 10, 14, 15, 19, 20, 24, 25
             * The walls that need to be evaluated are always located on the Y axis on lines: 0, 4, 5, 9, 10, 14, 15, 19, 20, 24, 25
             * Because rooms are not connected beyond lines 0 and 25 on both axes, we can ommmit them. 
             * 
             * The condition is simple:
             *                           IF the walls of the given room neighbour 1. an open space on one side next to the space evaluated
             *                                                                    2. a closed space on the other side of the space evaluated
             *                                                                    3. an open space on the side of the cell that fits criteria 2.
             *                           THEN rooms can be connected and the given space should be deleted.
             **/
            // Go over first iterration on the X axis (i.e. 4, 9, 14, 19, 24)
            for (int x = 4; x < rooms_template_permenant.Length; x += 5)
            {
                for (int y = 0; y < rooms_template_permenant.Length; y++)
                {
                    try
                    {
                        if (rooms_template_permenant[y][x - 1] == false && rooms_template_permenant[y][x] == true && rooms_template_permenant[y][x + 1] == true && rooms_template_permenant[y][x + 2] == false)
                        {
                            this.rooms_template[y][x] = false;
                        }
                    }
                    catch
                    {
                        // Do nothing for now
                    }
                }
            }


            // Go over second iterration on the X axis (i.e. 5, 10, 15, 20)
            for (int x = 5; x < rooms_template_permenant.Length; x += 5)
            {
                for (int y = 0; y < rooms_template_permenant.Length; y++)
                {
                    try
                    {
                        if (rooms_template_permenant[y][x - 2] == false && rooms_template_permenant[y][x - 1] == true && rooms_template_permenant[y][x] == true && rooms_template_permenant[y][x + 1] == false)
                        {
                            this.rooms_template[y][x] = false;
                        }
                    }
                    catch
                    {
                        // Do nothing for now
                    }
                }
            }

            // Go over first iterration on the Y axis (i.e. 4, 9, 14, 19, 24)
            for (int x = 0; x < rooms_template_permenant.Length; x++)
            {
                for (int y = 4; y < rooms_template_permenant.Length; y += 5)
                {
                    try
                    {
                        if (rooms_template_permenant[y - 1][x] == false && rooms_template_permenant[y][x] == true && rooms_template_permenant[y + 1][x] == true && rooms_template_permenant[y + 2][x] == false)
                        {
                            this.rooms_template[y][x] = false;
                        }
                    }
                    catch
                    {
                        // Do nothing for now
                    }
                }
            }

            // Go over the second iterration on the Y axis (i.e. 5, 10, 15, 20)
            for (int x = 0; x < rooms_template_permenant.Length; x++)
            {
                for (int y = 5; y < rooms_template_permenant.Length; y += 5)
                {
                    try
                    {
                        if (rooms_template_permenant[y - 2][x] == false && rooms_template_permenant[y - 1][x] == true && rooms_template_permenant[y][x] == true && rooms_template_permenant[y + 1][x] == false)
                        {
                            rooms_template[y][x] = false;
                        }
                    }
                    catch
                    {
                        // Do nothing for now
                    }
                }
            }

            // ------------------ DRAW THE MAZE --------------------------------

            // Load all of our objects that are required to draw our map
            Image image1 = new Bitmap("..\\..\\Graphics\\PNG\\Wall1.png", true);
            Image image2 = new Bitmap("..\\..\\Graphics\\PNG\\Wall2.png", true);
            Image image3 = new Bitmap("..\\..\\Graphics\\PNG\\Wall3.png", true);
            Image image4 = new Bitmap("..\\..\\Graphics\\PNG\\Wall4.png", true);

            TextureBrush tBrush1 = new TextureBrush(image1);
            TextureBrush tBrush2 = new TextureBrush(image2);
            TextureBrush tBrush3 = new TextureBrush(image3);
            TextureBrush tBrush4 = new TextureBrush(image4);

            // Clean everything before drawing if there are instances of an old map being present
            Brush myOldLaceBrush = new SolidBrush(color: Color.OldLace);
            g.FillRectangle(brush: myOldLaceBrush, x: field.x, y: field.y, width: field.width, height: field.height);

            // As we have all values of our maze in a 2D Matrix (array), we can just draw them one by one.
            int place_x = field.x;
            int place_y = field.y;
            for (int i = 0; i < this.rooms_template.Length; i++)
            {
                for (int j = 0; j < this.rooms_template[i].Length; j++)
                {
                    bool roomState = this.rooms_template[i][j];
                    if (roomState)
                    {
                        // Chose a random brush to paint with, as we want all of our walls to be randomly different
                        int rnd_brush = rnd.Next(0, 4);
                        switch (rnd_brush)
                        {
                            case 0:
                                g.FillRectangle(tBrush1, x: place_x, y: place_y, width: cell.width, height: cell.height);
                                break;
                            case 1:
                                g.FillRectangle(tBrush2, x: place_x, y: place_y, width: cell.width, height: cell.height);
                                break;
                            case 2:
                                g.FillRectangle(tBrush3, x: place_x, y: place_y, width: cell.width, height: cell.height);
                                break;
                            case 3:
                                g.FillRectangle(tBrush4, x: place_x, y: place_y, width: cell.width, height: cell.height);
                                break;
                            default:
                                break;
                        }
                    }
                    place_x += cell.width;
                }
                place_x = field.x;
                place_y += cell.height;
            }

            // Update the representation that we will want of our matrix to be in, for the future
            this.rooms_template_redux = TemplateConverter.redefineTemplate(original_template: this.rooms_template);
        }

        /// <summary>
        /// A method that allows for the user to set end and start points on the map.
        /// Note: it is allowed to draw the starting point where the end point is. It only makes sense for that to be possible in the real world.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PathFinder_MouseDown(object sender, MouseEventArgs e)
        {
            // Create an object for start or finish
            MainField field = new MainField();
            Cell cell = new Cell();
            Graphics g = this.CreateGraphics();
           
            // Make several checks first
            // 1. Has the field been generated, so the action can be performed?
            if (this.rooms_template[0] == null) // Just checking for the first row, as if it has values - others should
            {
                return;
            }

            // 2. Check if the mouse press is withing the boarders
            int toFindX = e.X;
            int toFindY = e.Y;

            if (toFindX <= field.x || toFindX >= field.width || toFindY <= field.y || toFindY >= field.height)
            {
                return;
            }

            // 3. Check if the location we are pressing on does not already have a wall in it's place. Later we can reuse this information 
            // To draw the needed graphics for our start and exit conditions.
            int X_coord = (toFindX / cell.width) - 1;
            int Y_coord = (toFindY / cell.height) - 1;

            if (this.rooms_template[Y_coord][X_coord] == true)
            {
                return;
            }

            // Otherwise we want to draw our graphics of the start and goal points. We just need to know what the user wants
            String task_object = cBox_Objects.SelectedItem.ToString();
            Int32 place_x = X_coord * cell.width + cell.width;
            Int32 place_y = Y_coord * cell.height + cell.height;
            if (task_object == "Start")
            {
                Image key_image = new Bitmap("..\\..\\Graphics\\PNG\\Key.png", true);
                TextureBrush tBrushStart = new TextureBrush(key_image);

                // Overwrite our old key if it exists
                if (this.current_key[0] != 0 && this.current_key[1] != 0)
                {
                    Brush myOldLaceBrush = new SolidBrush(color: Color.OldLace);
                    g.FillRectangle(brush: myOldLaceBrush, x: this.current_key[0], y: this.current_key[1], width: cell.width, height: cell.height);
                }
                g.FillRectangle(tBrushStart, x: place_x, y: place_y, width: cell.width, height: cell.height);
                this.current_key[0] = place_x;
                this.current_key[1] = place_y;
            }
            else if (task_object == "Finish")
            {
                Image gate_image = new Bitmap("..\\..\\Graphics\\PNG\\Gate.png", true);
                TextureBrush tBrushFinish = new TextureBrush(gate_image);

                // Overwrite our old gate if it exists
                if (this.current_gate[0] != 0 && this.current_gate[1] != 0)
                {
                    Brush myOldLaceBrush = new SolidBrush(color: Color.OldLace);
                    g.FillRectangle(brush: myOldLaceBrush, x: this.current_gate[0], y: this.current_gate[1], width: cell.width, height: cell.height);
                }
                g.FillRectangle(tBrushFinish, x: place_x, y: place_y, width: cell.width, height: cell.height);
                this.current_gate[0] = place_x;
                this.current_gate[1] = place_y;
            }
        }

        /// <summary>
        /// This method calls to search of the optimal path for the Path Finding algorithms. Currently there is only 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Find_Click(object sender, EventArgs e)
            {
            // Do few check beforehand
            // 1. Does the start and end exist (they can only exist if the maze exists, so we do not need to check for it additionally)?
            if ((this.current_key[0] == 0 && this.current_key[1] == 0) || (this.current_gate[0] == 0 && this.current_gate[1] == 0))
            {
                return;
            }

            // 2. Now we do need to update the template through which the search will be done. As it needs to see new start and end points
            String[][] rooms_Path = TemplateConverter.StartFinishAdder(given_Template: this.rooms_template_redux, key: this.current_key, gate: this.current_gate);
            PathFindingAlgorithms pathFindingAlgorithms = new PathFindingAlgorithms();
            // Find directions we need to go through to reach our destination point
            String answer_Path = pathFindingAlgorithms.BreadthFirstSearch(maze: rooms_Path, key: this.current_key, gate: this.current_gate);

            // Check if the path exists, and if so, draw it. Otherwise, let us know that it does not exist.
            if (answer_Path == "There is no answer!")
            {
                lbl_Status.Text = "Status: No Path";
                return;
            }

            List<int> x_coordList = pathFindingAlgorithms.DrawPath_X(path: answer_Path, key: this.current_key);
            List<int> y_coordList = pathFindingAlgorithms.DrawPath_Y(path: answer_Path, key: this.current_key);

            // Let's use the green brush to draw over our fields
            Cell cell = new Cell();
            MainField field = new MainField();
            Graphics g = this.CreateGraphics();
            Brush myAquamarineBrush = new SolidBrush(color: Color.Aquamarine);
            for (int i = 0; i < x_coordList.Count - 1; i++)
            {
                int x_FieldPosition = field.x + cell.width * x_coordList[i];
                int y_FieldPosition = field.y + cell.height * y_coordList[i];
                g.FillRectangle(brush: myAquamarineBrush, x: x_FieldPosition, y: y_FieldPosition, width: cell.width, height: cell.height);
            }
            lbl_Status.Text = "Status: Solved";
        }

        /// <summary>
        /// A button click that cleans a found path if it has been actually found.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClearPath_Click(object sender, EventArgs e)
        {
            // Check if the map for the path finding exists
            if (this.rooms_template[0] == null)
            {
                return;
            }

            if ((this.current_key[0] == 0 && this.current_key[1] == 0) || (this.current_gate[0] == 0 && this.current_gate[1] == 0))
            {
                return;
            }

            String[][] rooms_Path = TemplateConverter.StartFinishAdder(given_Template: this.rooms_template_redux, key: this.current_key, gate: this.current_gate);
            // Let's use the old brush to draw over our fields
            Cell cell = new Cell();
            MainField field = new MainField();
            Graphics g = this.CreateGraphics();
            Brush myOldLaceBrush = new SolidBrush(color: Color.OldLace);

            // We need to go through every cell in the maze and only cover whatever is not walls our our objects. So empty spots
            // As when the path is generated - it does not actually act as any additional object on the map.
            for (int i = 0; i < rooms_Path.Length; i++)
            {
                for (int j = 0; j < rooms_Path[i].Length; j++)
                {
                    if (rooms_Path[i][j] == " ")
                    {
                        int x_draw = field.x + cell.width * j;
                        int y_draw = field.y + cell.height * i;
                        g.FillRectangle(brush: myOldLaceBrush, x: x_draw, y: y_draw, width: cell.width, height: cell.height);
                    }
                }
            }
            lbl_Status.Text = "Status: Nothing";
        }
    }
}