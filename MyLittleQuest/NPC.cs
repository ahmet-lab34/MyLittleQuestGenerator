
// Class for the NPC's
public class NPC
{
    #region Declaring values
    // NPC values
    public string Name { get; set; }
    public string Role { get; set; }

    // Keeps history of quests given
    public List<Quest> ActiveQuests { get; private set; }
    public List<Quest> CompletedQuests { get; private set; }
    public List<Quest> QuestHistory { get; private set; }

    // Static random to avoid calling it several times
    private static Random rnd = new Random();

    // NPC can be called using this and setting its  name and role
    public NPC(string name, string role)
    {
        Name = name;
        Role = role;

        // Load saved quests and populate NPC lists
        ActiveQuests = QuestSaves.LoadQuests() ?? new List<Quest>();
        CompletedQuests = QuestSaves.LoadQuests(completed: true) ?? new List<Quest>();
        QuestHistory = QuestSaves.LoadQuests(history: true) ?? new List<Quest>();
    }
    #endregion

    //----- Menu Methods No NPC -----
    #region Methods No NPC

    // Method: gives random quest between the existing three
    public Quest GiveRandomQuest()
    {
        Quest quest = rnd.Next(0, 3) switch
        {
            0 => QuestManager.RandomFetchQuest(),
            1 => QuestManager.RandomHuntQuest(),
            _ => QuestManager.RandomEscortQuest()
        };
        // Use pattern matching to safely check for HuntQuest
        if (quest is HuntQuest huntQuest) {

            // You can now pass these values to the actual game
            var huntingGame = new HuntingGame(huntQuest);
            huntingGame.Main();
        }
        else if (quest is FetchQuest fetchQuest) {

            // Start fetch game
            var fetchGame = new FetchingGame();
            fetchGame.Main();
        }
        else if (quest is EscortQuest escortQuest) {

            // Start escort logic here...
        }

        // After finding the quest it adds it to ActiveQuests
        ActiveQuests.Add(quest);

        // Saves it to Json (Long term memory)
        QuestSaves.Save(quest);

        // Executes the quest
        return quest;
    }

    // Method: gives specific type of quest by typing the quest type
    public Quest GiveQuest(string type)
    {
        // Sets the name here
        Quest quest = type.ToLower() switch
        {
            // if any of the three is called it sets it to watever it is
            "fetch" => QuestManager.RandomFetchQuest(),
            "hunt" => QuestManager.RandomHuntQuest(),
            "escort" => QuestManager.RandomEscortQuest(),
            // Handling invalid
            _ => throw new ArgumentException("Invalid quest type")
        };

        // After deciding the quest it adds it to ActiveQuests
        ActiveQuests.Add(quest);

        // Saves it to Json (Long term memory)
        QuestSaves.Save(quest);

        // Executes the quest
        return quest;
    }


    // Method: Shows the active quests from the ActiveQuests 
    public Quest? ShowActiveQuests()
    {
        // If no quests available
        if (ActiveQuests.Count == 0)
        {
            WriteLine("No active quests.");
            return null;
        }

        WriteLine("Active Quests:");

        //For loop to display every quest
        for (int i = 0; i < ActiveQuests.Count; i++)
        {
            var quest = ActiveQuests[i];
            WriteLine($"{i + 1}. {quest.Title} ({quest.GetType().Name}) | Difficulty: {quest.Difficulty}");
        }

        // Player enters the quest he wants using numbers that are attached to the quest
        Write("Enter the number of the quest you want to select: ");
        string? input = ReadLine();

        // Takes the player's response to choose the quest
        if (int.TryParse(input, out int choice) &&
            choice > 0 && choice <= ActiveQuests.Count)
        {
            // removes 1 from the response because the code starts with 0 while the text with 1
            Quest selectedQuest = ActiveQuests[choice - 1];

            Clear();
            WriteLine($"Starting quest: {selectedQuest.Title}...");
            Thread.Sleep(2000);

            // Starts the specific selected quest
            //selectedQuest.StartQuest();
            WriteLine("Press any key to continue.");
            ReadKey(true);
            Clear();

            // Execute type-specific logic
            switch (selectedQuest)
            {
                // if it is fetch
                case FetchQuest fetch:
                    fetch.StartTheGame();
                    break;
                // if it is hunt
                case HuntQuest hunt:
                    hunt.StartTheGame();
                    break;
                // if it is escort
                case EscortQuest escort:
                    escort.StartTheGame();
                    break;
                // Error handling
                default:
                    WriteLine("Unknown quest type.");
                    break;
            }

            // Move to CompletedQuests and persist
            CompleteQuest(selectedQuest);

            // returns the quest
            return selectedQuest;
        }

        // Error handling
        WriteLine("Invalid choice.");
        return null;
    }

    // Shows completed quests
    public Quest? ShowCompletedQuests()
    {
        // No quests
        if (CompletedQuests.Count == 0)
        {
            WriteLine("No completed quests.");
            return null;
        }

        // Lists every completed quest
        WriteLine("Quests that can be completed: ");
        for (int i = 0; i < CompletedQuests.Count; i++)
        {
            var quest = CompletedQuests[i];
            WriteLine($"{i + 1}. {quest.Title} ({quest.GetType().Name}) | Difficulty: {quest.Difficulty}");
        }

        // LEts the player choose one to confirm
        Write("Enter the number of the quest you want to complete: ");
        string? input = ReadLine();

        // Reads the player's response
        if (int.TryParse(input, out int value) &&
            value > 0 && value <= CompletedQuests.Count)
        {
            Quest completedQuest =  CompletedQuests[value - 1];

            // Saves to History
            SaveToHistory(completedQuest);
        }

        return null;
    }

    // Method: Complete the specific quest
    public void CompleteQuest(Quest quest)
    {
        // No Quest
        if (!ActiveQuests.Contains(quest))
        {
            WriteLine("Quest not found in active quests.");
            return;
        }

        quest.CompleteQuest();

        // Removes from active quests to add to Completed quests list
        ActiveQuests.Remove(quest);
        CompletedQuests.Add(quest);

        // Saves it to long term memory
        QuestSaves.Save(quest, completed: true);

        // Remove from active JSON
        var activeSaved = QuestSaves.LoadQuests();
        activeSaved.RemoveAll(q => q.Title == quest.Title);
        QuestSaves.SaveQuests(activeSaved);

        WriteLine($"Quest '{quest.Title}' completed!");
    }
    // Method: Save a specific quest to history
    public void SaveToHistory(Quest quest)
    {
        // No quest
        if (!CompletedQuests.Contains(quest))
        {
            WriteLine("Quest not found in completed quests.");
            return;
        }
        // Removes it from Completed quest and adds it to Quest History list
        CompletedQuests.Remove(quest);
        QuestHistory.Add(quest);

        // Adds to long term memory
        QuestSaves.Save(quest, history: true);

        // Json Handling
        var completeSaved = QuestSaves.LoadQuests(completed: true);

        completeSaved.RemoveAll(q => q.Title == quest.Title);

        QuestSaves.SaveQuests(completeSaved, completed: true);

        WriteLine($"Quest '{quest.Title}' has been confirmed,\n" +
            $"The game information will be under the Quest history section");
    }
    #endregion

    // ---------- Menu Methods NPC ----------
    #region Methods With NPC

    // Method: Generates new quest for by a specific NPC
    static void GenerateNewQuest(NPC npc)
    {
        Clear();
        Quest quest = npc.GiveRandomQuest();
        WriteLine($"New quest generated: {quest.Title}");
    }

    // Method: View active quest of the NPC
    static void ViewActiveQuests(NPC npc)
    {
        Clear();
        npc.ShowActiveQuests();
    }

    // Method: Confirms the NPC-specific Quest
    static void ConfirmQuest(NPC npc)
    {
        Clear();
        if (npc.CompletedQuests.Count == 0)
        {
            WriteLine("No active quests to complete.");
            return;
        }

        WriteLine("Select a quest to complete:");
        for (int i = 0; i < npc.CompletedQuests.Count; i++)
        {
            WriteLine($"{i + 1}. {npc.CompletedQuests[i].Title}");
        }

        Write("Enter number: ");
        string? input = ReadLine();

        if (int.TryParse(input, out int choice) &&
            choice > 0 && choice <= npc.CompletedQuests.Count)
        {
            npc.SaveToHistory(npc.CompletedQuests[choice - 1]);
        }
        else
        {
            WriteLine("Invalid choice.");
        }
    }

    // Method: Shows the Quest history
    public void ShowQuestHistory()
    {
        Clear();
        WriteLine($"{Name} - {Role} Quest History:");

        foreach (var quest in QuestHistory)
        {
            string status = CompletedQuests.Contains(quest) ? "Completed" : "Active";
            WriteLine($"- {quest.Title} | Difficulty: {quest.Difficulty} | Status: {status}");
        }
    }

    // Exits the Application
    void Exit()
    {
        Clear();
        WriteLine("Exiting...");
        WriteLine("Thanks for playing our game!");
        Thread.Sleep(2000);
        Environment.Exit(0);
    }
    #endregion

    // ---------------- Menu Visual ----------------
    #region Menu Running
    // Text-Based Menu

    // NPC name
    static void Main()
    {
        NPC npc = new NPC("Captain Thalia", "Guard Captain");
        RunGameMenu(npc);
    }

    // Runs the game by the NPC
    static void RunGameMenu(NPC npc)
    {
        // Continues the game while running is true
        // This can be used to open menu for a specific NPC to be able to close later
        // If there is just one menu, the while(running) can be removed
        bool running = true;

        while (running)
        {
            TextMainMenu();

            Write("Enter your choice: ");
            string? input = ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    GenerateNewQuest(npc);
                    break;

                case 2:
                    ViewActiveQuests(npc);
                    break;

                case 3:
                    ConfirmQuest(npc);
                    break;

                case 4:
                    npc.ShowQuestHistory();
                    break;

                case 5:
                    npc.Exit();
                    break;

                default:
                    WriteLine("Invalid choice. Enter 1-5.");
                    break;
            }

            WriteLine(); // spacing
        }
    }

    // Visuals for menu
    static void TextMainMenu()
    {
        WriteLine("=== Quest Generator ===");
        WriteLine("1. Generate a new quest");
        WriteLine("2. View active quests");
        WriteLine("3. Complete a quest");
        WriteLine("4. View NPC quest history");
        WriteLine("5. Exit");
    }
    #endregion
}
