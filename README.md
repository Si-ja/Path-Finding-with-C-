# Path Finding with C#

## Description

This is a small scaled project that implements two things:
* Random map generation
* A path finding algorithm: Breadth First Search

## Contents:

### Random Map Generation

The random map generator works based on few principles.

* All rooms are made to fill space of 5x5 cells
* Rooms are not drawn beforehand individually. Rather only 4 sprites exist that allow for generation of each individual room's walls. This means that adding more rooms is as easy as making a copy of an old class that allows for rooms generation and adding it into the pool. Currently the pool is presend on line `83` in the `Form1.cs` file: 
```C#
 String[] choices = { "Square", "Plus", "L", "Corner", "Long1", "Long2", "Funnel", "Empty" };
````
* There are several type of rooms (e.g. L-shaped, Square, Empty, etc.). Each room is just a 2D matrix/2D Array of boolean values.
* All rooms generate as closed ones (meaning closed on all sides by walls). Afterwards, it is evaluated whether each room has an adjacent room and walls between them are deleted.
* The grid where the rooms are generated is 25x25 cells. Meaning with room dimensions, there can be up to 25 rooms on the map, if no empty room are randomly chosen for generation.

### Start and Finish

* The start cell from which  the path will needed to be found to the end cell can be placed by the user after the map has been generated. It is represented with a sprite of a key.
* The end  cell to which the path will needed to be found from the start cell can be placed by the user after the map  has been generated. It is represented with a sprite of a door/gate.

### Path Finding Algorithms

Currently only 1 path finding algorithms is implemented: Breadth First Search.

* Breadth First Search (Modified):
1. First modification - the algorithm is forced to never look back at the cell it just moved from while expanding in search.
2. Second modification - the algorithm skips keeping track of cells that have already been visited once in general.
3. The whole algorithm is based on only looking in 4 main direction: Up, Down, Left, Right. This means that every move has the same weight, as 1 diagonal  move can be more beneficial than 1 vertical + 1 horizontal, hence not equivalent.

The search from the starting field looks the following way (where the black cell is the start cell, the yellow is the open path and the green is the potential path the algorithm is evaluating):

##TODO EXAMPLE

## Final Product:

The program itself looks the following way:

##TODO EXAMPLE
