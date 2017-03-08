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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(scene);
        }

    }


}