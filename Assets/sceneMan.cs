using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneMan : MonoBehaviour
{
    //thanks this tutorial 
    //http://blog.teamtreehouse.com/make-loading-screen-unity

    private bool loadScene = false;
    public int scene;
   
    // Updates once per frame
    void Update()
    {

        // If the player has pressed the space bar and a new scene is not loading yet...
        if (Input.GetKeyUp(KeyCode.Space) && loadScene == false)
        {

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

           

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }


    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }

        

    }

}