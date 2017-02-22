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
    public static bool firstFlag = true;

    // Initialize Vocabulary
    string[] adjectives = new string[] { "dark", "stormy", "clear", "beautiful", "scary", "pretty", "magical" };
    string[] conflicts = new string[] { "his mother is starving", "the prince was kidnapped", "there is a zombie infestation" };
    string[] names_protagonist = new string[] { "John", "Charlie", "Sir Schmmoopy of Awesometon", "Greg" };
    string[] names_antagonist = new string[] { "Cthulu", "some jerk", "Gerg, The Dark Lord" };
    string[] places = new string[] { "woods", "penthouse loft", "randomly generated cave" };
    string[] times = new string[] { "morning", "afternoon", "night", "day", "evening" };

    string[] middles;
    string[] motivations;
    string[] tasks;

    int rbeg;
    int rtasks;
    int radj;
    int rmotivations;
    int rmiddles;

    string outp;
    string middleOutp;
    string keyTask;
    string keyMotivation;

    // Use this for initialization
    void Start()
    {

        if (firstFlag == true)
        {

            conflict = conflicts[UnityEngine.Random.Range(0, conflicts.Length)];
            place = places[UnityEngine.Random.Range(0, places.Length)];
            pro_name = names_protagonist[UnityEngine.Random.Range(0, names_protagonist.Length)];
            ant_name = names_antagonist[UnityEngine.Random.Range(0, names_antagonist.Length)];
            time = times[UnityEngine.Random.Range(0, times.Length)];

            firstFlag = false;

        }

        setting = "It is a {0} " + time;

        motivations = new string[] {
            pro_name + " is lost in the " + place + " and needs to find his house",
            pro_name + " gets bored and wants to go on a journey",
            "The prince was kidnapped by " + ant_name + " and it is up to " + pro_name + " to save him",
            pro_name + " hears a kitten in need and sets out on an epic quest",
            pro_name + " lives in his mother's basement. Amid an existential crisis, he sets out to find the meaning of life" };
		middles = new string[] {
            pro_name + " is walking along the {0} path",
            pro_name + " is feeling {0}",
            pro_name + " sees a {0} sign and stops to read it",
            pro_name + " meets a {0} man trying to sell him a magic potion, but he keeps walking",
            pro_name + " wants to stop and rest, but knows he must keep moving",
            pro_name + " stops at a {0} bar to rest and then proceeds on his way",
            pro_name + " is frightened by a {0} creature hiding in the bushes. He runs away",
            pro_name + " hears a sound in the distance. He walks the other way",
            pro_name + " sees a truly amazing sight in the distance. He continues on, reinspired" };
        tasks = new string[] {
            "Kill 5 creatures",
            "Find the key to the next room",
            "Open the magic portal",
            "Find a berry and eat it" };

        rtasks = UnityEngine.Random.Range(0, tasks.Length);
        rmotivations = UnityEngine.Random.Range(0, motivations.Length);
        rmiddles = UnityEngine.Random.Range(0, middles.Length);


        keyTask = tasks[rtasks];
        Debug.Log(keyTask);

        keyMotivation = motivations[rmotivations];
        Debug.Log(keyMotivation);

        outp = String.Format(setting, reshuffle(adjectives)) +
            ". " +
            keyMotivation +
            ". " 
            ;
        middleOutp = String.Format(middles[rmiddles], reshuffle(adjectives)) +
            ". "
            ;


        if (motivationText != null)
            motivationText.text = outp;

        if (storyText != null)
            storyText.text = middleOutp;





    }

    // Update is called once per frame
    void Update()
    {

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
