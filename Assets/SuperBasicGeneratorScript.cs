using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperBasicGeneratorScript : MonoBehaviour
{
    public Text storyText;
    public Text motivationText;
    public static string pro_name;
    public static string ant_name;
    public static string conflict;
    public static string place;
    public static string setting;
    public static string time;
    public static string gendered_royalty;
    public static bool firstFlag = true;
    public static int scene = 0;

    // Initialize Vocabulary
    string[] adjectives = new string[] { "dark", "stormy", "clear", "beautiful", "scary", "pretty", "magical" };
    string[] conflict_types = new string[] { "Man vs Self",
                                        "Man vs Man",
                                        "Man vs Society",
                                        "Man vs Nature",
                                        "Man vs Machine",
                                        "Man vs Fate" };
    string[] names_protagonist = new string[] { "John", "Charlie", "Sir Schmmoopy of Awesometon", "Greg" };
    string[] names_antagonist = new string[] { "Cthulu", "some jerk", "Gerg, The Dark Lord", "Existential Dread", "The Boogeyman", "Santa" };
    string[] names_royalty = new string[] { "prince", "princess", "duke", "duchess", "king", "queen", "jester" };
    string[] places = new string[] { "woods", "penthouse loft", "randomly generated cave" };
    string[] times = new string[] { "morning", "afternoon", "night", "day", "evening" };

    string[] motivations;

    string[] conflicts;
    string[] middles;
    string[] tasks;

    int rbeg;
    int rtasks;
    int radj;
    int rmotivations;
    int rmiddles;

    string outp;
    string middleOutp;
    string climaxOutp;
    string keyTask;
    string keyMotivation;

    int confNum;
    int[] conf1Weights;
    int[] conf2Weights;
    int[] conf3Weights;
    int[] conf4Weights;
    int[] conf5Weights;
    int[] conf6Weights;

    int motNum;

    Dictionary<int, int[]> conflict_motivation_weights = new Dictionary<int, int[]>();

    // Use this for initialization
    void Start()
    {

        if (firstFlag == true)
        {

            conflict = conflict_types[UnityEngine.Random.Range(0, conflict_types.Length)];
            place = places[UnityEngine.Random.Range(0, places.Length)];
            pro_name = names_protagonist[UnityEngine.Random.Range(0, names_protagonist.Length)];
            ant_name = names_antagonist[UnityEngine.Random.Range(0, names_antagonist.Length)];
            time = times[UnityEngine.Random.Range(0, times.Length)];
            gendered_royalty = names_royalty[UnityEngine.Random.Range(0, names_royalty.Length)];

        }

        setting = "It is a {0} " + time + ". ";

        conflicts = new string[] {
            pro_name + " lives in his mother's basement. ", // Conflict 1
            pro_name + " has fallen into a deep depression. ", // Conflict 2
            "The " + gendered_royalty + " was kidnapped by " + ant_name + ". ", // Conflict 3
            pro_name + " heard a doomsayer predicting the end of the world. ", // Conflict 4
            "Today is the day that " + pro_name + " is supposed to die. " // Conflict 5
        };
        conf1Weights = new int[] { 45, 10, 5, 5, 1 };
        conf2Weights = new int[] { 27, 13, 5, 5, 50 };
        conf3Weights = new int[] { 5, 15, 35, 30, 5 };
        conf4Weights = new int[] { 5, 25, 5, 5, 1 };
        conf5Weights = new int[] { 27, 13, 5, 5, 50 };
        conflict_motivation_weights.Add(0, conf1Weights);
        conflict_motivation_weights.Add(1, conf2Weights);
        conflict_motivation_weights.Add(2, conf3Weights);
        conflict_motivation_weights.Add(3, conf4Weights);
        conflict_motivation_weights.Add(4, conf5Weights);

        confNum = UnityEngine.Random.Range(0, conflicts.Length);

        motNum = selectWeightedRandom(conflict_motivation_weights, confNum);

        motivations = new string[] {
            "After being screamed at by his mother " + pro_name + " decides he needs to get a job, so he becomes an adventurer.",
            "This " + time + ", " + pro_name + " is lost in the " + place + " and stumbles upon a {0} secret that belongs to " + ant_name + ".",
            pro_name + " is royal guard to the " + gendered_royalty + ", and only he can stop " + ant_name + ".",
            pro_name + " is royal guard to the " + gendered_royalty + ", and its up to him to save the kingdom from " + ant_name + ".",
            pro_name + " decides to find the meaning of life through combat." };

        middles = new string[] {
            pro_name + " meets a mysterious beggar. He offers information on " + ant_name + " in exhange for food. ",
            pro_name + " fears he is not strong enough to defeat " + ant_name + ". He has heard tale of a {0}, {1} sword that may aid him in the battle to come. ",
            pro_name + " fears he is not strong enough to defeat " + ant_name + ". to prepare for the upcoming battle, first he must train. ",
            pro_name + " was wandering through the {0} " + place + " but got caught in a trap! "
        };
        tasks = new string[] {
            "Find 5 berries and bring them to the beggar.",
            "Find the object to get stronger.",
            "Kill 5 monsters.",
            "Escape the maze before time runs out.",
            "Find the key to the next room"
        };

        rtasks = UnityEngine.Random.Range(0, tasks.Length);
        rmotivations = UnityEngine.Random.Range(0, motivations.Length);
        rmiddles = UnityEngine.Random.Range(0, middles.Length);


        keyTask = tasks[rtasks];
        Debug.Log(keyTask);

        keyMotivation = motivations[motNum];
        Debug.Log(keyMotivation);

        outp = String.Format(setting, reshuffle(adjectives)) +
            conflicts[confNum] +
            String.Format(motivations[motNum], reshuffle(adjectives));

        middleOutp = String.Format(middles[rmiddles], reshuffle(adjectives)) +
            tasks[rmiddles];

        climaxOutp = pro_name + " is finally ready to take on " + ant_name + ". Find the key to move on to the next room and challenge your greatest foe.";


        if (motivationText != null)
            motivationText.text = outp;

        if (storyText != null)
            storyText.text = middleOutp;

        scene += 1;

        firstFlag = false;


    }

    // Update is called once per frame
    void Update()
    {

    }

    int selectWeightedRandom(Dictionary<int, int[]> weightDict, int key)
    {
        int randomIndex = UnityEngine.Random.Range(0, weightDict[key].Length);
        int totalWeight = 0;
        List<int> weightCutoffs = new List<int>();
        for (int i = 0; i < weightDict[key].Length; i++)
        {
            totalWeight += weightDict[key][i];
            weightCutoffs.Add(totalWeight);
        }

        int randomInt = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < weightCutoffs.Count; i++)
        {
            if (randomInt < weightCutoffs[i])
            {
                return i;
            }
        }

        return randomIndex;

    }

    string[] reshuffle(string[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = UnityEngine.Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
        return texts;
    }

}