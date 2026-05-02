using System.Text.Json.Serialization;
// Unique identifier - type for every mission type

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(FetchQuest), "fetch")]
[JsonDerivedType(typeof(HuntQuest), "hunt")]
[JsonDerivedType(typeof(EscortQuest), "escort")]

// Main abstract class that will hold the base values
public abstract class Quest {
    // Abstract class values
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Difficulty { get; set; }
    public string? Reward {  get; set; }
    public string? QuestGiver { get; set; }

    // Empty Construction
    public Quest() { }


    // Values to be set for specific missions
    public Quest(string? title, string? description,
        int difficulty, string? reward, string? questGiver) {

        Title = title;
        Description = description;
        Difficulty = difficulty;
        Reward = reward;
        QuestGiver = questGiver;
    }

    // Basic Quest information before starting the quest
    protected void QuestStartBaseInfo() {
        WriteLine($"Quest Started: {Title}");
        WriteLine($"Description: {Description}");
        WriteLine($"Difficulty: {Difficulty}");
        WriteLine($"Reward: {Reward}");
        WriteLine($"Quest Giver: {QuestGiver}");
    }

    // Starting quest that will be overriden based on each quest
    public abstract void StartQuest();
    // Same thing for quest completion
    public abstract void CompleteQuest();

}

// Quest log class for managin quests my calling methods
class QuestLog {
    // quest list
    public List<Quest> Quests = new List<Quest>();

    // Adds a specific quest
    public void AddQuest(Quest quest) {
        Quests.Add(quest);
    }

    // Removes all (each) quests
    public void RemoveAllQuests() {
        foreach (var quest in Quests) {
            Quests.Remove(quest);
        }
    }

    // Shows each quest 

    public void ShowAll() {
        foreach (var quest in Quests) {
            WriteLine(quest.Title);
        }
    }
}
// Class for Fetch quest
public class FetchQuest : Quest {
    // Specific values for fetch quest
    public string? ItemToFetch { get; set; }
    public string? Location { get; set; }

    // Empty construction
    public FetchQuest() { }

    // Specific values (alongside base ones from the Quest class) to be set and called for every Fetch quest
    public FetchQuest(string? title, string? description,
        int difficulty, string? reward, string? questGiver,
        string? itemToFetch, string? location) : base(title, description,
            difficulty, reward, questGiver) {
        ItemToFetch = itemToFetch;
        Location = location;
    }

    // Start Quest information for Fetch quest
    public override void StartQuest() {
        // Calling this start function from the Quest to save time and typing function
        QuestStartBaseInfo();

        WriteLine($"Item to Fetch: {ItemToFetch}");
        WriteLine($"Location: {Location}");
    }

    // Starts Fetch Quest game 
    public void StartTheGame() {
        var game = new FetchingGame();
        game.Main();
    }

    // Completion information for Fetch quest
    public override void CompleteQuest() {
        WriteLine($"Quest Completed: {Title}");
        WriteLine($"You have fetched the {ItemToFetch} from {Location}.");
        WriteLine($"You received: {Reward}");
    }
}

// Class for Hunt quest
public class HuntQuest : Quest {

    // Specific values for Hunt Quest
    public string? EnemyToHunt { get; set; }
    public int? EnemyCount { get; set; }

    // Empty Constructor
    public HuntQuest() { }

    // Specific values for Hunt quest to be set and called (Alongside base values from Quest)
    public HuntQuest(string? title, string? description, int difficulty,
        string? reward, string? questGiver, string? enemyToHunt, int enemyCount)
    : base(title, description, difficulty, reward, questGiver){
        EnemyToHunt = enemyToHunt;
        EnemyCount = enemyCount;
    }
    // Hunt Quest Start text
    public override void StartQuest() {
        QuestStartBaseInfo();
        WriteLine($"Enemies to be aware of: {EnemyToHunt}");
        WriteLine($"Enemy Count: {EnemyCount}");
    }
    // Increasing the enemy count for a gameplay logic
    public void IncreaseEnemyCount() {
        EnemyCount ++;
    }
    // Decreasing the enemy count for a gameplay logic
    public void DecreaseEnemyCount() {
        EnemyCount --;
    }
    // // Hunt Quest Complete text
    public override void CompleteQuest() {
        WriteLine($"Quest Completed: {Title}");
        WriteLine($"You have hunted the {EnemyToHunt} which was/were about {EnemyCount} in numbers !");
        WriteLine($"You received: {Reward}");

        Thread.Sleep(1000);
        WriteLine("My apologies for that I was not able to make a game for HuntQuest due to the short amount of time I have. :(");
    }
}
// Escort quest class
public class EscortQuest : Quest {

    // Specific values for Escort Quest
    public string? NPCObject { get; set; }
    public int NPCCount { get; set; }
    public float Distance { get; set; }
    public int DefeatedEnemyCount { get; set; }

    // Empty Constructor
    public EscortQuest() { }

    // Specific values for escort quest to be instantiated (Alongsite Main class Quest)
    public EscortQuest(string? title, string? description, int difficulty,
        string? reward, string? questGiver, int nPCsToEscort, float distance, int defeatedEnemyCount)
    : base(title, description, difficulty, reward, questGiver){
        NPCCount = nPCsToEscort;
        Distance = distance;
        DefeatedEnemyCount = defeatedEnemyCount;
    }

    // Escort quest specific start text 
    public override void StartQuest() {
        QuestStartBaseInfo();
        WriteLine($"NPCs to escort: {NPCCount} {NPCObject}");
        WriteLine($"Distance to go: {Distance} miles");
    }

    // Escort quest specific complete text 
    public override void CompleteQuest() {
        WriteLine($"Quest Completed: {Title}");
        WriteLine($"You escorted {NPCCount} {NPCObject}'s to a safe zone !");
        WriteLine($"You have defeated {DefeatedEnemyCount} enemies and walked for {Distance} miles");
        WriteLine($"You received: {Reward}");

        Thread.Sleep(1000);
        WriteLine("My apologies for that I was not able to make a game for EscortQuest due to the short amount of time I have. :(");
    }
}
