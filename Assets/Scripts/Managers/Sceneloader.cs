using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    //Game States
    private bool canPress = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPress)
        {
            SceneManager.LoadScene(GetScene());
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(GetScene());
        }

    }

    //method for the start button
    public void StartGame()
    {
        SceneManager.LoadScene(GetScene() + 1);
    }

    //return the current scene index
    private int GetScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        return sceneIndex;
    }

    //when a trigger collider enters
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.INTER_PLAYER)
        {
            canPress = true;
        }
    }

    //when a trigger collider exits
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.INTER_PLAYER)
        {
            canPress = false;
        }
    }
}

