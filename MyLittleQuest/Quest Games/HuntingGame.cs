using System;
using System.Threading;

class HuntingGame
{
    private readonly string _enemyToHunt;

    public HuntingGame(HuntQuest huntQuest)
    {
        // Using the values that were defined by inheritence
        _enemyToHunt = huntQuest.EnemyToHunt ?? "Unknown Enemy";
    }

    public void Main()
    {
        Random rnd = new Random();
        int enemyHealth = 3; // Must "hunt" it 3 times to defeat
        int maxAttempts = 10;
        int attempts = 0;

        // Enemy position (0-9) on a line
        int enemyPosition = rnd.Next(0, 10);

        StoryLine();

        // While neither the enemy nor the player is dead: Continue
        while (enemyHealth > 0 && attempts < maxAttempts)
        {
            WriteLine($"\nAttempts left: {maxAttempts - attempts}");
            WriteLine("Where do you want to track the enemy? (Enter a number 0-9)");

            string input = ReadLine() ?? "";
            if (!int.TryParse(input, out int guess) || guess < 0 || guess > 9)
            {
                WriteLine("There are numbers between 0 and 9, what're you tryna do?");
                continue;
            }

            attempts++;

            if (guess == enemyPosition)
            {
                enemyHealth--;
                WriteLine($"Hit! You injured the {_enemyToHunt}. Remaining health: {enemyHealth}");
                // Enemy moves to a new random position after being hit
                enemyPosition = rnd.Next(0, 10);
            }
            else if (guess < enemyPosition)
            {
                WriteLine("The enemy is further ahead!");
            }
            else
            {
                WriteLine("The enemy is behind you!");
            }
        }

        if (enemyHealth == 0)
            WriteLine($"\nCongratulations! You successfully hunted the {_enemyToHunt}!");
        else
            WriteLine($"\nYou ran out of attempts. The {_enemyToHunt} escaped!");
    }

    void StoryLine()
    {
        WriteLine("Welcome to the Hunting Quest!");
        Thread.Sleep(500);
        WriteLine($"Your prey: {_enemyToHunt}");
        WriteLine("Use your tracking skills to locate it along the trail (positions 0-9).");
        WriteLine("Be careful! You have limited attempts to hunt it.");
        ReadKey(true);
        WriteLine("The hunt begins!\n");
    }
}
