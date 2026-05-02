using System.Runtime.InteropServices.Marshalling;
using System.Timers;


// Fetching game
class FetchingGame {
    // Game logic
    public void Main() {

        // Game map size
        int gridWidth = 5;
        int gridHeight = 5;

        // Player Default location
        int playerX = 0;
        int playerY = 0;

        // Using randomization
        Random rnd = new Random();

        // Using random to place the object that is going to be fetched
        int itemX = rnd.Next(0, gridWidth);
        int itemY = rnd.Next(0, gridHeight);

        // if the player find the item this is going to be true and the game ends
        bool hasItem = false;

        // Text for story
        StoryLine();

        // While hasItem is false continue the game
        while (!hasItem) {

            // Reads the player's actions
            string input = ReadLine() ?? "".ToUpper();

            // Controls the player using WASD
            switch (input) {
                case "W":
                    if (playerY < gridHeight - 1) playerY++;
                    else WriteLine("You can't move up. Edge of the world !");
                    break;

                case "S":
                    if (playerY > 0) playerY--;
                    else WriteLine("You can't move down. Edge of the world !");
                    break;

                case "D":
                    if (playerX < gridWidth - 1) playerX++;
                    else WriteLine("You can't move left. Edge of the world !");
                    break;

                case "A":
                    if (playerX > 0) playerX--;
                    else WriteLine("You can't move right, Edge of the world !");
                    break;

                    // If the player typed anything other than WASD
                default:
                    WriteLine("Invalid output use only W/A/S/D !");
                    break;
            }

            // If the player is in the position of the item
            if (playerX == itemX && playerY == itemY) {
                hasItem = true;
                WriteLine($"You found the item at ({itemX}, {itemY}) congrats !");
            }
            else WriteLine("Nothing here bruH");
        }
        // storyline
        void StoryLine() {


            WriteLine("Hello there");
            Thread.Sleep(1000);
            WriteLine("Our quest here is simple.");
            WriteLine("You are not able to see or hear anything !");
            WriteLine("Fetch the item using W/A/S/D in you keyboard");
            WriteLine("Shall we start?");
            // player can start by pressing a button
            ReadKey(true);
            WriteLine("You can start !\n");
        }
    }

}
