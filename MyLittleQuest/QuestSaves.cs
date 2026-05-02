using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Saving the Quest file class
public static class QuestSaves
{
    // Three files instantiated with different names to be used for saving files
    private const string ActivePath = "ActiveQuests.json";
    private const string CompletedPath = "CompletedQuests.json";
    private const string HistoryPath = "QuestHistory.json";

    // --- Save a list of quests ---
    public static void SaveQuests(List<Quest> quests, bool completed = false, bool history = false)
    {
        string path = history ? HistoryPath : (completed ? CompletedPath : ActivePath);
        string json = JsonSerializer.Serialize(quests, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }

    // --- Load a list of quests ---
    public static List<Quest> LoadQuests(bool completed = false, bool history = false)
    {
        string path = history ? HistoryPath : (completed ? CompletedPath : ActivePath);
        if (!File.Exists(path)) return new List<Quest>();

        string json = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(json)) return new List<Quest>();

        return JsonSerializer.Deserialize<List<Quest>>(json) ?? new List<Quest>();
    }

    // --- Add a quest to a list and save ---
    public static void Save(Quest quest, bool completed = false, bool history = false)
    {
        var quests = LoadQuests(completed, history);
        quests.Add(quest);
        SaveQuests(quests, completed, history);
    }
}
// DISCLAIMER: I have used ChatGPT for the saving the file because I don't have enough technical knowledge about this topic
// Can you teacher in one of your classes explain the saving or the Json deeply. Not just for the console application,
// but also for Unity. So we can understand it better.
// Sorry for not coming to your classes on the fridays. I watch you at the speed of 2X. That way I can focus better.
// Have a nice day !
