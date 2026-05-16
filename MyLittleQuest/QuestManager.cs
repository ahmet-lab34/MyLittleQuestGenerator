// Quest Manager Class to determine the behaviour of every quest
public static class QuestManager {

    // Static Random to remove the need to call it multiple times
    private static Random rnd = new Random();

    // Fetch quest logic and values are set here
    public static FetchQuest RandomFetchQuest() {

        // Fetch titles -> Title descriptions
        var fetchTitles = new Dictionary<string, string>() {
            {"Find The CIA Agent", "Find The Agent who is stuck in a serious operation in the Middle East"},
            {"Fetch The Royal Jewelry", "someone stole a royal Jewelry that is so valuable even Elizabeth || went to find that man,\n Your mission is to find him/her as soon as possible !"},
        };

        // Fetch Specific rewards
        string[] fetchRewards = {"Amazon prime for 30 days", "Basketball signed by Lebron James", "Fortnite OG"};

        // Fetch Specific quest givers
        string[] fetchQuestGivers = {"Donald Trump", "Joseph Stalin", "Nescafe 3 in 1"};

        // Fetch Specific items
        string[] fetchItems = { "Old Lady", "Steve", "Lost Dog", "Secret Package" };

        // Fetch Specific locations
        string[] fetchLocations = {"Fortnite Save The World", "CS2 - Dust 2", "Jesse's home" };

        // Converts the titles to numeric value to allow randomness
        var keys = fetchTitles.Keys.ToList();

        string randomTitles = keys[rnd.Next(keys.Count)];

        // Returns the final values of the Fetch Quest here
        return new FetchQuest(
            // Returns random values for everything (Nothing is set)
            // Though it can be set later by changing here
            title: randomTitles,
            // Returns the description of whatever quest was given
            description: fetchTitles[randomTitles],
            // return random number between 1 and 4
            difficulty: rnd.Next(1, 4),
            // Converts the strings to numeric values to make the randomness possible
            reward: fetchRewards[rnd.Next(fetchRewards.Length)],
            questGiver: fetchQuestGivers[rnd.Next(fetchQuestGivers.Length)],
            itemToFetch: fetchItems[rnd.Next(fetchItems.Length)],
            location: fetchLocations[rnd.Next(fetchLocations.Length)]
        );
    }
    // Does the same thing that was said in Fetch Quest but for Hunt Quest
    public static HuntQuest RandomHuntQuest() {
        var huntTitles = new Dictionary<string, string>() {
            {"Hunt The Deer", "Hunt the deer to gain experience in the wild life" },
            {"Hunt The Wolves", "Hunt the Wolves to protect Maya and the Bear from any harm !" },
        };
        string[] huntRewards = {"Maya's Bear", "Micheal Jackson's white turning potion", "JD Vance's Toothpaste"};
        string[] huntQuestGivers = {"Kratos", "Head & Shoulders 2 in 1", "Master YI"};
        string[] huntEnemies = {"KSI's forehead'(s)", "IShowSpeed'(s)", "HyperX Cloud || HeadSet'(s)"};


        var keys = huntTitles.Keys.ToList();

        string randomTitle = keys[rnd.Next(keys.Count)];

        return new HuntQuest(
            title: randomTitle,
            description: huntTitles[randomTitle],
            difficulty: rnd.Next(1, 4),
            reward: huntRewards[rnd.Next(huntRewards.Length)],
            questGiver: huntQuestGivers[rnd.Next(huntQuestGivers.Length)],
            enemyToHunt: huntEnemies[rnd.Next(huntEnemies.Length)],
            enemyCount: rnd.Next(1, 4)
        );
    }
    // Does the same thing that was said in Fetch Quest but for Escort Quest
    public static EscortQuest RandomEscortQuest() {
        var escortTitles = new Dictionary<string, string>() {
            {$"Escort Maya And The Bear", "Escort Them to a safe location before the wolves kill them !" },
            {"Escort Ahmet", "Escort Agent Ahmet to a safe location before he gets killed !" },
            {"Escort GigaChad", "Escort GigaChad to the Sigma male party before he loses himself to becoming gay" }
        };
        string[] escortRewards = {"GigaChad's Dumbell", "Ahmet's Wise Lessons", "Class Pass From Can Teacher"};
        string[] escortQuestGivers = {"Can Teacher", "Ahmet The Wise", "Angela Merkel"};
        string[] escortNPCNames = { "Ahmet's flipflops", "The White House", "Uranus(the planet)" };


        var keys = escortTitles.Keys.ToList();

        string randomTitle = keys[rnd.Next(keys.Count)];

        return new EscortQuest(
            title: randomTitle,
            description: escortTitles[randomTitle],
            difficulty: rnd.Next(1, 4),
            reward: escortRewards[rnd.Next(escortRewards.Length)],
            questGiver: escortQuestGivers[rnd.Next(escortQuestGivers.Length)],
            nPCsToEscort: rnd.Next(1, 4),
            distance: rnd.Next(5, 80),
            defeatedEnemyCount: 0,
            escortNpcNames: escortNPCNames[rnd.Next(escortNPCNames.Length)]
        );
    }
}
