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
    string[] suddenly;

    int rbeg;
    int rtasks;
    int radj;
    int rmotivations;
    int rmiddles;
    int rsuddenly;

    string outp;
    string keyTask;
    string keyMotivation;
    // Use this for initialization
    void Start()
    {
        nouns = new string[] { "night", "day", "evening" };
        adjectives = new string[] { "dark", "stormy", "clear", "beautiful", "scary", "pretty", "magical" };
        motivations = new string[] {
            "John stayed out late and needs to find his house",
            "John is very bored and wants to go on a journey",
            "John got a call from Destiny and it told him to go on an adventure",
            "John hears a kitten in need and sets out on an epic quest" };
        beginnings = new string[] {
            "It was a {0} and {1} night",
            "It was a {0} day in the woods",
            "It was a {0} day" };
        middles = new string[] {
            "John kept walking along the {0} path",
            "John was feeling very {0}",
            "John sees a {0} sign and stops to read it" };
        suddenly = new string[] {
            "it started raining cats and dogs",
            "monsters fell from the sky", 
            "John heard a scream from right behind him", 
        };
        tasks = new string[] {
            "Kill 5 creatures",
            "Find the key to the next room",
            "Open the magic portal",
            "Find a berry and eat it" };


        rbeg = UnityEngine.Random.Range(0, beginnings.Length);
        rtasks = UnityEngine.Random.Range(0, tasks.Length);
        rmotivations = UnityEngine.Random.Range(0, tasks.Length);
        rmiddles = UnityEngine.Random.Range(0, middles.Length);
        rsuddenly = UnityEngine.Random.Range(0, suddenly.Length);


        keyTask = tasks[rtasks];
        Debug.Log(keyTask);

        keyMotivation = motivations[rmotivations];
        Debug.Log(keyMotivation);

        outp = String.Format(beginnings[rbeg], reshuffle(adjectives)) +
            ". " +
            String.Format(middles[rmiddles], reshuffle(adjectives)) +
            ". " +
            "Suddenly, " +
            suddenly[rsuddenly] +
            ". "
            ;

        Debug.Log(outp);
        //storyText.text = outp;
        motivationText.text = outp;





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
