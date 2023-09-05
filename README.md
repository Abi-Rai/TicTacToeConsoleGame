# Tic-Tac-Toe

### Description
A simple console game of Tic-Tac-Toe made using .Net 7 Console app C# version 11, Visual Studio 2022 community edition. 

There are three projects.
* TicTacToe - Main console app which runs the game
* TicTacToeBenchmarks - Some benchmarks I ran to test out some initial theories, more on that [here](TicTacToeBenchmarks/README.md). 
* TicTacToeTests - XUnit test project
### Instructions
The game can be started by running the TicTacToe project. 

User interaction is all done in the console via key press inputs and text inputs. The Instructions will vary for each section of the game and will be displayed on the console.

On the first prompt user can either elect to start game with the default settings or change the settings.

You can take turns playing both players moves if you feel adventerous or select to play against a ~~an AI~~ Computer. Not much of an AI atm, it simply selects a `Random` cell from the remaining available cells. 

### Benchmarking to choose Data Structure.

For Tic-Tac-Toe, which only requires 9 cells to store the game boards' state, a simple array `char[] Board = new char[9]` should suffice. However, I wanted to test how the other data structures/collections would perform.

* Single-dimensional array `char[]`
* Multi-Dimensional array `char[,]` 
* Jagged array `char[][]` 

[See Benchmarks](TicTacToeBenchmarks/README.md)

After the benchmarking I decided to save myself some headache and just use a Single-Dimensional array. However, I included the option to play 4x4 and 5x5 board size games, even 2x2 games where every second move is a win :handshake:. 


