using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperBasicGeneratorScript : MonoBehaviour
{
    public Text storyText;
    public Text motivationText;


    string[] beginnings;
    string[] tasks;
    string[] nouns;
    string[] adjectives;
    string[] middles;
    string[] motivations;

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
        nouns = new string[] { "night", "day", "evening" };
        adjectives = new string[] { "dark", "stormy", "clear", "beautiful", "scary", "pretty", "magical" };
        motivations = new string[] {
            "John is very lost and needs to find his house",
            "John gets very bored and wants to go on a journey",
            "John gets a call from Destiny and it told him to go on an adventure",
            "John hears a kitten in need and sets out on an epic quest" };
        beginnings = new string[] {
            "It is a {0} and {1} night",
            "It is a {0} day in the woods",
            "It is a {0} day" };
        middles = new string[] {
            "John is walking along the {0} path",
            "John is feeling very {0}",
            "John sees a {0} sign and stops to read it",
            "John meets a {0} man trying to sell him a magic potion, but he keeps walking",
            "John wants to stop and rest, but knows he must keep moving",
            "John stops at a {0} bar to rest and then proceeds on his way",};       
        tasks = new string[] {
            "Kill 5 creatures",
            "Find the key to the next room",
            "Open the magic portal",
            "Find a berry and eat it" };


        rbeg = UnityEngine.Random.Range(0, beginnings.Length);
        rtasks = UnityEngine.Random.Range(0, tasks.Length);
        rmotivations = UnityEngine.Random.Range(0, motivations.Length);
        rmiddles = UnityEngine.Random.Range(0, middles.Length);


        keyTask = tasks[rtasks];
        Debug.Log(keyTask);

        keyMotivation = motivations[rmotivations];
        Debug.Log(keyMotivation);

        outp = String.Format(beginnings[rbeg], reshuffle(adjectives)) +
            ". " +
            String.Format(middles[rmiddles], reshuffle(adjectives)) +
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
