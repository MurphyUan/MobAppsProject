using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LeaderBoardHandler : MonoBehaviour {

    [SerializeField] private TMP_Text scores;

    private void Awake() {
        scores.text = LeaderBoard.displayEntries();
    }

    public void UpdateScores(){
        scores.text = LeaderBoard.displayEntries();
    }
}

public static class LeaderBoard
{
    // https://forum.unity.com/threads/leaderboard-script-using-playerprefs.257900/ Tutorial for making Leaderboard using PlayerPrefs

    public const int numEntries = 10;

    public struct Entry {
        public string name;
        public int score;

        public Entry(string name, int score){
            this.name = name;
            this.score = score;
        }
    }

    // Saved Entry List
    private static List<Entry> s_Entries;

    // Accessor for s_Entries
    private static List<Entry> Entries {
        get{
            if(s_Entries == null){
                s_Entries = new List<Entry>();
                LoadEntries();
            }
            return s_Entries;
        }
    }

    private const string PlayerPrefsKey = "leaderboard";

    private static void SortEntries() {
        s_Entries.Sort((a, b) => b.score.CompareTo(a.score));
    }

    private static void LoadEntries(){
        // Make sure the List is Empty
        s_Entries.Clear();
        // Loop through every Entry in PlayerPrefs
        for(int i = 0; i < numEntries; i++){
            Entry entry;
            entry.name = PlayerPrefs.GetString(PlayerPrefsKey + "[" + i + "].name", "AAA");
            entry.score = PlayerPrefs.GetInt(PlayerPrefsKey + "[" + i + "].score", 0);
            s_Entries.Add(entry);
        }

        SortEntries();
    }

    private static void SaveEntries() {
        for(int i = 0; i < numEntries; i++){
            var entry = s_Entries[i];
            PlayerPrefs.SetString(PlayerPrefsKey + "[" + i + "].name", entry.name);
            PlayerPrefs.SetInt(PlayerPrefsKey + "[" + i + "].score", entry.score);
            PlayerPrefs.Save();
        }
    }

    public static Entry GetEntry(int index){
        return Entries[index];
    }

    public static void Record(string name, int score){
        Entries.Add(new Entry(name, score));
        SortEntries();
        Entries.RemoveAt(Entries.Count - 1);
        SaveEntries();
    }

    public static string displayEntries(){
        string value = "";
        for(int i = 0; i < numEntries; i++){
            var entry = Entries[i];
            value += entry.name + " - " + entry.score.ToString("000000000") + "\n";
        }
        return value;
    }
}

