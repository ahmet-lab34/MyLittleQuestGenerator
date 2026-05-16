using System;
using System.Threading;

class EscortGame
{
    private readonly string _npcName;
    private readonly int _pathLength;
    private int _npcPosition;
    private int _playerHealth;
    private Random _rnd;

    public EscortGame(EscortQuest escortQuest)
    {
        _npcName = escortQuest.Name ?? "Unknown NPC";
        _pathLength = 10; // distance to destination
        _npcPosition = 0;
        _playerHealth = 5; // player health
        _rnd = new Random();
    }

    public void Main()
    {
        StoryLine();

        while (_npcPosition < _pathLength && _playerHealth > 0)
        {
            WriteLine($"\nNPC {_npcName} is at position {_npcPosition}/{_pathLength}");
            WriteLine($"Your health: {_playerHealth}");
            WriteLine("Choose your action: (F) Move Forward / (H) Heal / (S) Wait");

            string input = ReadLine()?.ToUpper() ?? "";

            switch (input)
            {
                case "F":
                    MoveForward();
                    break;
                case "H":
                    Heal();
                    break;
                case "S":
                    Wait();
                    break;
                default:
                    WriteLine("Invalid input! Use F/H/S.");
                    continue;
            }

            RandomEvent();
        }

        if (_npcPosition >= _pathLength)
            WriteLine($"\nCongratulations! {_npcName} reached the destination safely!");
        else
            WriteLine($"\nOh no! {_npcName} could not make it. Quest failed.");
    }

    private void MoveForward()
    {
        _npcPosition++;
        WriteLine($"{_npcName} moves forward!");
    }

    private void Heal()
    {
        _playerHealth++;
        WriteLine("You take a moment to heal. +1 Health");
    }

    private void Wait()
    {
        WriteLine("You wait and stay alert...");
    }

    private void RandomEvent()
    {
        int chance = _rnd.Next(0, 100);

        if (chance < 30) // 30% chance of enemy attack
        {
            WriteLine($"An enemy ambushes {_npcName}!");
            if (_rnd.Next(0, 2) == 0)
            {
                _npcPosition--; // NPC moves back
                WriteLine($"{_npcName} was forced to retreat!");
            }
            else
            {
                _playerHealth--;
                WriteLine($"You got hit while protecting {_npcName}! Health -1");
            }
        }
        else if (chance < 40) // 10% chance of trap
        {
            WriteLine($"{_npcName} triggered a trap! Delayed movement...");
            Thread.Sleep(1000);
        }
    }

    private void StoryLine()
    {
        WriteLine($"Escort Quest: Protect {_npcName} to safety!");
        Thread.Sleep(500);
        WriteLine($"{_npcName} will follow your lead, but danger lurks along the path.");
        WriteLine("Guide them safely to the destination using your actions.\n");
        ReadKey(true);
    }
}
