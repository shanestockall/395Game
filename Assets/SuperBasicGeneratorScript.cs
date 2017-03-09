using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SuperBasicGeneratorScript : MonoBehaviour
{
    public Text storyText;
    public Text motivationText;
    public Text taskText;
    public Text climaxText;
    public Text endText;
    public static string pro_name;
    public static string ant_name;
    public static string conflict;
    public static string place;
    public static string setting;
    public static string time;
    public static string gendered_royalty;
    public static bool firstFlag = true;
    public int riddleNum;

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
    string[] riddles = new string[] { "follow the Songs of Earth.", "follow the Neverending Echoes.", "follow the Necromancer's Wailing." };

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
    string endOutp;
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
            riddleNum = UnityEngine.Random.Range(0, riddles.Length);

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
            pro_name + " fears he is not strong enough to defeat " + ant_name + ". to prepare for the upcoming battle, first he must train. ",
            pro_name + " was wandering through the {0} " + place + " but got caught in a trap! "
        };
        tasks = new string[] {
            "Find 5 berries.",
            "Kill 5 monsters.",
            "Find the secret to opening the door in one of the chests. Open the right one or else... \n" + riddles[riddleNum],
            "Find the key to the next room", 
        };

        rtasks = UnityEngine.Random.Range(0, tasks.Length);
        rmotivations = UnityEngine.Random.Range(0, motivations.Length);
        rmiddles = UnityEngine.Random.Range(0, middles.Length);
        rtasks = rmiddles;

        keyTask = tasks[rtasks];
        Debug.Log(keyTask);

        keyMotivation = motivations[motNum];
        Debug.Log(keyMotivation);

        outp = String.Format(setting, reshuffle(adjectives)) +
            conflicts[confNum] +
            String.Format(motivations[motNum], reshuffle(adjectives));

        middleOutp = String.Format(middles[rmiddles], reshuffle(adjectives)) +
            tasks[rtasks];

        climaxOutp = pro_name + " is finally ready to take on " + ant_name + ". Find the key to move on to the next room and challenge your greatest foe.";

        endOutp = "Hooray! " + pro_name + " has defeated " + ant_name + " and completed his journey.";

        if (motivationText != null)
            motivationText.text = outp;

        if (storyText != null)
            storyText.text = middleOutp;

        if (taskText != null)
            taskText.text = keyTask;

        if (climaxText != null)
            climaxText.text = climaxOutp;

        if (endText != null)
            endText.text = endOutp;
        
        firstFlag = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (rtasks == 1)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }
        if (rtasks == 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(2);
            }
        }
        if (rtasks == 3)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(3);
            }
        }
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

